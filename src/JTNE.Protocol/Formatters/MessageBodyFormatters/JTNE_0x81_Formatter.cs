using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;

namespace JTNE.Protocol.Formatters.MessageBodyFormatters {
    public class JTNE_0x81_Formatter : IJTNEFormatter<JTNE_0x81> {
        public JTNE_0x81 Deserialize (ReadOnlySpan<byte> bytes, out int readSize) {
            int offset = 0;
            JTNE_0x81 jTNE_0X81 = new JTNE_0x81 ();
            jTNE_0X81.OperateTime = bytes.ReadDateTime6Bytes (ref offset);
            jTNE_0X81.ParamNum = bytes.ReadByte (ref offset);
            for (int i = 0; i < jTNE_0X81.ParamNum; i++) {
                var paramId = bytes.ReadByte (ref offset); //参数ID         
                int readSubBodySize = 0;
                if (JTNE_0x81_Body.JTNE_0x81Method.TryGetValue (paramId, out Type type)) {
                    ReadOnlySpan<byte> readOnlySpan = new ReadOnlySpan<byte> ();
                    if (JTNE_0x81_Body.JTNE_0x81LengthOfADependOnValueOfB.TryGetValue (paramId, out byte dependOnParamId)) {
                        var length = jTNE_0X81.ParamList.FirstOrDefault (m => m.ParamId == dependOnParamId).ParamLength;
                        int tempOffset = 0;
                        int lengthVal = bytes.Slice (offset - length - 1, length).ReadByte (ref tempOffset);
                        readOnlySpan = bytes.Slice (offset, lengthVal);
                    } else {
                        readOnlySpan = bytes.Slice (offset);
                    }
                    if (jTNE_0X81.ParamList != null) {
                        jTNE_0X81.ParamList.Add (JTNEFormatterResolverExtensions.JTNEDynamicDeserialize (JTNEFormatterExtensions.GetFormatter (type), readOnlySpan, out readSubBodySize));
                    } else {
                        jTNE_0X81.ParamList = new List<JTNE_0x81_Body> { JTNEFormatterResolverExtensions.JTNEDynamicDeserialize (JTNEFormatterExtensions.GetFormatter (type), readOnlySpan, out readSubBodySize) };
                    }
                }
                offset = offset + readSubBodySize;
            }
            readSize = offset;
            return jTNE_0X81;
        }

        public int Serialize (ref byte[] bytes, int offset, JTNE_0x81 value) {
            offset += bytes.WriteDateTime6Bytes (offset, value.OperateTime);
            offset += bytes.WriteByte (offset, value.ParamNum);
            foreach (var item in value.ParamList) {
                offset += bytes.WriteByte (offset, item.ParamId);
                object obj = JTNEFormatterExtensions.GetFormatter (item.GetType ());
                offset = JTNEFormatterResolverExtensions.JTNEDynamicSerialize (obj, ref bytes, offset, item);
            }
            return offset;
        }
    }
}