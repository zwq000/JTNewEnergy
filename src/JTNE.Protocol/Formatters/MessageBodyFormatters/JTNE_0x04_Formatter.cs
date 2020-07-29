using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;

namespace JTNE.Protocol.Formatters.MessageBodyFormatters {
    public class JTNE_0x04_Formatter : IJTNEFormatter<JTNE_0x04> {
        public JTNE_0x04 Deserialize (ReadOnlySpan<byte> bytes, out int readSize) {
            int offset = 0;
            JTNE_0x04 jTNE_0X01 = new JTNE_0x04 ();
            jTNE_0X01.LogoutTime = bytes.ReadDateTime6Bytes (ref offset);
            jTNE_0X01.LogoutNum = bytes.ReadUInt16 (ref offset);
            readSize = offset;
            return jTNE_0X01;
        }

        public int Serialize (ref byte[] bytes, int offset, JTNE_0x04 value) {
            offset += bytes.WriteDateTime6Bytes (offset, value.LogoutTime);
            offset += bytes.WriteUInt16 (offset, value.LogoutNum);
            return offset;
        }
    }
}