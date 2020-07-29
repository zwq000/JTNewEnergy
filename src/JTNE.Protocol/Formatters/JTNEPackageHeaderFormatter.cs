using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Enums;
using JTNE.Protocol.Exceptions;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.Internal;

namespace JTNE.Protocol.Formatters {
    public class JTNEPackageHeaderFormatter : IJTNEFormatter<JTNEPackageHeader> {
        public JTNEPackageHeader Deserialize (ReadOnlySpan<byte> bytes, out int readSize) {
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
                    throw new JTNEException (JTNEErrorCode.BCCCodeError, $"request:{bCCCode}!=calculate:{bCCCode2}");
            }
            JTNEPackageHeader jTNEPackage = new JTNEPackageHeader ();
            offset += 2;
            // 3.命令标识
            jTNEPackage.MsgId = (JTNEMsgId)JTNEBinaryExtensions.ReadByte (bytes, ref offset);
            // 4.应答标识
            jTNEPackage.AskId = (JTNEAskId)JTNEBinaryExtensions.ReadByte (bytes, ref offset);
            // 5.VIN
            jTNEPackage.VIN = JTNEBinaryExtensions.ReadString (bytes, ref offset, 17);
            // 6.数据加密方式
            jTNEPackage.EncryptMethod = JTNEBinaryExtensions.ReadByte (bytes, ref offset);
            // 7.数据单元长度是数据单元的总字节数
            jTNEPackage.DataUnitLength = JTNEBinaryExtensions.ReadUInt16 (bytes, ref offset);
            // 8.数据体
            // 8.1.根据数据加密方式进行解码
            // todo: 8.2.解析出对应数据体
            if (jTNEPackage.DataUnitLength > 0) {
                int bodyReadSize = 0;
                try {
                    jTNEPackage.Bodies = bytes.Slice (offset, jTNEPackage.DataUnitLength).ToArray ();
                } catch (Exception ex) {
                    throw new JTNEException (JTNEErrorCode.BodiesParseError, ex);
                }
                offset += bodyReadSize;
            }
            // 9.校验码
            //jTNEPackage.BCCCode =
            var bcc = JTNEBinaryExtensions.ReadByte (bytes, ref offset);
            readSize = offset;
            return jTNEPackage;
        }

        public int Serialize (ref byte[] bytes, int offset, JTNEPackageHeader value) {

            // 1.起始符1
            offset += JTNEBinaryExtensions.WriteByte (bytes, offset, JTNEPackage.BeginFlag);
            // 2.起始符2
            offset += JTNEBinaryExtensions.WriteByte (bytes, offset, JTNEPackage.BeginFlag);
            // 3.命令标识
            offset += JTNEBinaryExtensions.WriteByte (bytes, offset, (byte)value.MsgId);
            // 4.应答标识
            offset += JTNEBinaryExtensions.WriteByte (bytes, offset, (byte)value.AskId);
            // 5.VIN
            offset += JTNEBinaryExtensions.WriteStringPadRightLittle (bytes, offset, value.VIN, 17);
            // 6.数据加密方式
            offset += JTNEBinaryExtensions.WriteByte (bytes, offset, value.EncryptMethod);
            if (value.Bodies != null) {
                // 7.数据体
                offset += JTNEBinaryExtensions.WriteUInt16 (bytes, offset, (ushort) value.Bodies.Length);
                // 8.处理数据体
                offset += JTNEBinaryExtensions.WriteBytes (bytes, offset, value.Bodies);
            } else {
                offset += JTNEBinaryExtensions.WriteUInt16 (bytes, offset, 0);
            }
            // 9.校验码
            var bccCode = bytes.ToXor (2, offset);
            offset += JTNEBinaryExtensions.WriteByte (bytes, offset, bccCode);
            return offset;
        }
    }
}