using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;

namespace JTNE.Protocol.Formatters.MessageBodyFormatters {
    public class JTNE_0x82_Formatter : IJTNEFormatter<JTNE_0x82> {
        public JTNE_0x82 Deserialize (ReadOnlySpan<byte> bytes, out int readSize) {
            int offset = 0;
            JTNE_0x82 jTNE_0x82 = new JTNE_0x82 ();
            jTNE_0x82.ControlTime = bytes.ReadDateTime6Bytes (ref offset);
            jTNE_0x82.ParamID = bytes.ReadByte (ref offset); //参数ID         

            if (JTNE_0x82_Body.JTNE_0x82Method.TryGetValue (jTNE_0x82.ParamID, out Type type)) {
                int readSubBodySize = 0;
                jTNE_0x82.Parameter = JTNEFormatterResolverExtensions.JTNEDynamicDeserialize (JTNEFormatterExtensions.GetFormatter (type), bytes.Slice (offset), out readSubBodySize);
                offset = offset + readSubBodySize;
            }
            readSize = offset;
            return jTNE_0x82;
        }

        public int Serialize (ref byte[] bytes, int offset, JTNE_0x82 value) {
            offset += bytes.WriteDateTime6Bytes (offset, value.ControlTime);
            offset += bytes.WriteByte (offset, value.ParamID);
            if (JTNE_0x82_Body.JTNE_0x82Method.TryGetValue (value.ParamID, out Type type)) {
                offset = JTNEFormatterResolverExtensions.JTNEDynamicSerialize (JTNEFormatterExtensions.GetFormatter (type), ref bytes, offset, value.Parameter);
            }
            return offset;
        }
    }
}