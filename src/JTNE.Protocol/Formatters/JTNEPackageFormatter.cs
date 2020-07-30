using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Enums;
using JTNE.Protocol.Exceptions;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.Internal;

namespace JTNE.Protocol.Formatters {
    public class JTNEPackageFormatter : IJTNEFormatter<JTNEPackage> {
        private const byte FixedDataBodyLength = 2;

        public JTNEPackage Deserialize (ReadOnlySpan<byte> bytes, out int readSize) {
            int offset = 0;
            // 1.进行固定头校验
            if (bytes[offset] != JTNEPackage.BeginFlag && bytes[offset + 1] == JTNEPackage.BeginFlag)
                throw new JTNEException (JTNEErrorCode.BeginFlagError, $"{bytes[offset]:X2},{bytes[offset + 1]:X2}");
            // 2.进行BCC校验码
            // 校验位 = 报文长度 - 最后一位（校验位）
            if (!JTNEGlobalConfigs.Instance.SkipCRCCode) {
                byte bCCCode = bytes[bytes.Length - 1];
                byte bCCCode2 = bytes.ToXor (2, bytes.Length - 1);
                if (bCCCode != bCCCode2)
                    throw new JTNEException (JTNEErrorCode.BCCCodeError, $"数据包校验错误:0x{bCCCode:X2},计算值:0x{bCCCode2:X2}");
            }
            var package = new JTNEPackage ();
            offset += 2;
            // 3.命令标识
            package.MsgId = (JTNEMsgId) bytes.ReadByte (ref offset);
            // 4.应答标识
            package.AskId = (JTNEAskId) bytes.ReadByte (ref offset);
            // 5.VIN
            package.VIN = bytes.ReadString (ref offset, 17);
            // 6.数据加密方式
            package.EncryptMethod = (JTNEEncryptMethod) bytes.ReadByte (ref offset);
            // 7.数据单元长度是数据单元的总字节数
            package.DataUnitLength = bytes.ReadUInt16 (ref offset);
            // 8.数据体
            // 8.1.根据数据加密方式进行解码
            // 8.2.解析出对应数据体
            if (package.DataUnitLength > 0) {
                Type jTNEBodiesImplType = JTNEMsgIdFactory.GetBodyTypeByMsgId ((byte) package.MsgId);
                if (jTNEBodiesImplType != null) {
                    int bodyReadSize = 0;
                    try {
                        if (package.EncryptMethod == JTNEEncryptMethod.None) {
                            package.Body = JTNEFormatterResolverExtensions.JTNEDynamicDeserialize (
                                JTNEFormatterExtensions.GetFormatter (jTNEBodiesImplType),
                                bytes.Slice (offset, package.DataUnitLength),
                                out bodyReadSize);
                        } else {
                            if (JTNEGlobalConfigs.Instance.DataBodiesEncrypt != null) {
                                var data = JTNEGlobalConfigs.Instance.DataBodiesEncrypt (package.EncryptMethod)
                                    .Decrypt (bytes.Slice (offset, package.DataUnitLength).ToArray ());
                                package.Body = JTNEFormatterResolverExtensions.JTNEDynamicDeserialize (
                                    JTNEFormatterExtensions.GetFormatter (jTNEBodiesImplType),
                                    data,
                                    out bodyReadSize);
                            }
                        }
                    } catch (Exception ex) {
                        throw new JTNEException (JTNEErrorCode.BodiesParseError, ex);
                    }
                    offset += bodyReadSize;
                }
            }
            // 9.校验码
            //jTNEPackage.BCCCode = bytes.ReadByteLittle ( ref offset);
            //var bcc = bytes.ReadByte (ref offset);
            readSize = offset;
            return package;
        }

        public int Serialize (ref byte[] bytes, int offset, JTNEPackage value) {
            // 1.起始符1
            offset += bytes.WriteByte (offset, JTNEPackage.BeginFlag);
            // 2.起始符2
            offset += bytes.WriteByte (offset, JTNEPackage.BeginFlag);
            // 3.命令标识
            offset += bytes.WriteByte (offset, (byte) value.MsgId);
            // 4.应答标识
            offset += bytes.WriteByte (offset, (byte) value.AskId);
            // 5.VIN
            offset += bytes.WriteStringPadRight (offset, value.VIN, 17);
            // 6.数据加密方式
            offset += bytes.WriteByte (offset, (byte) value.EncryptMethod);
            // 7.记录当前偏移量
            int tmpOffset = offset;
            // 8.数据体
            Type jTNEBodiesImplType = JTNEMsgIdFactory.GetBodyTypeByMsgId ((byte) value.MsgId);
            int messageBodyOffset = 0;
            if (jTNEBodiesImplType != null) {
                if (value.Body != null) {
                    // 8.1.处理数据体
                    // 8.2.判断是否有加密
                    messageBodyOffset = JTNEFormatterResolverExtensions.JTNEDynamicSerialize (
                        JTNEFormatterExtensions.GetFormatter (jTNEBodiesImplType),
                        ref bytes,
                        offset + FixedDataBodyLength,
                        value.Body);
                    if (value.EncryptMethod == JTNEEncryptMethod.None) {
                        // 9.通过tmpOffset反写数据单元长度
                        bytes.WriteUInt16 (tmpOffset, (ushort) (messageBodyOffset - offset - FixedDataBodyLength));
                        offset = messageBodyOffset;
                    } else {
                        if (JTNEGlobalConfigs.Instance.DataBodiesEncrypt != null) {
                            // 8.1.先进行分割数据体
                            var bodiesData = bytes.AsSpan (tmpOffset + FixedDataBodyLength, messageBodyOffset - offset - FixedDataBodyLength).ToArray ();
                            // 8.2.将数据体进行加密
                            var data = JTNEGlobalConfigs.Instance.DataBodiesEncrypt (value.EncryptMethod)
                                .Encrypt (bodiesData);
                            // 9.通过tmpOffset反写加密后数据单元长度
                            bytes.WriteUInt16 (tmpOffset, (ushort) data.Length);
                            // 8.3.写入加密后的数据体
                            offset += FixedDataBodyLength;
                            offset += bytes.WriteBytes (offset, data);
                        }
                    }
                } else {
                    // 9.数据单元长度
                    offset += bytes.WriteUInt16 (offset, 0);
                }
            } else {
                // 9.数据单元长度
                offset += bytes.WriteUInt16 (offset, 0);
            }
            // 10.校验码
            var bccCode = bytes.ToXor (2, offset);
            offset += bytes.WriteByte (offset, bccCode);
            return offset;
        }
    }
}