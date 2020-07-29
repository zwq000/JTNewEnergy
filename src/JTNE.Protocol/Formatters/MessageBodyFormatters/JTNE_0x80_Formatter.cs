using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;

namespace JTNE.Protocol.Formatters.MessageBodyFormatters {
    public class JTNE_0x80_Formatter : IJTNEFormatter<JTNE_0x80> {
        public JTNE_0x80 Deserialize (ReadOnlySpan<byte> bytes, out int readSize) {
            int offset = 0;
            JTNE_0x80 jTNE_0X80 = new JTNE_0x80 ();
            jTNE_0X80.QueryTime = bytes.ReadDateTime6Bytes (ref offset);
            jTNE_0X80.ParamNum = bytes.ReadByte (ref offset);
            jTNE_0X80.ParamList = bytes.ReadBytes (ref offset, jTNE_0X80.ParamNum);
            readSize = offset;
            return jTNE_0X80;
        }

        public int Serialize (ref byte[] bytes, int offset, JTNE_0x80 value) {
            offset += bytes.WriteDateTime6Bytes (offset, value.QueryTime);
            offset += bytes.WriteByte (offset, value.ParamNum);
            offset += bytes.WriteBytes (offset, value.ParamList);
            return offset;
        }
    }
}