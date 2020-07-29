using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;

namespace JTNE.Protocol.Formatters.MessageBodyFormatters {
    public class JTNE_0x06_Formatter : IJTNEFormatter<JTNE_0x06> {
        public JTNE_0x06 Deserialize (ReadOnlySpan<byte> bytes, out int readSize) {
            int offset = 0;
            JTNE_0x06 jTNE_0X06 = new JTNE_0x06 ();
            jTNE_0X06.LogoutTime = bytes.ReadDateTime6Bytes (ref offset);
            jTNE_0X06.LogoutNum = bytes.ReadUInt16 (ref offset);
            readSize = offset;
            return jTNE_0X06;
        }

        public int Serialize (ref byte[] bytes, int offset, JTNE_0x06 value) {
            offset += bytes.WriteDateTime6Bytes (offset, value.LogoutTime);
            offset += bytes.WriteUInt16 (offset, value.LogoutNum);
            return offset;
        }
    }
}