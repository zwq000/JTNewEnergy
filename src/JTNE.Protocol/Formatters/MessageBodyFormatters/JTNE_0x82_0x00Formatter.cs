using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Enums;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;

namespace JTNE.Protocol.Formatters.MessageBodyFormatters {
    public class JTNE_0x82_0x00Formatter : IJTNEFormatter<JTNE_0x82_0x00> {
        public JTNE_0x82_0x00 Deserialize (ReadOnlySpan<byte> bytes, out int readSize) {
            int offset = 0;
            JTNE_0x82_0x00 jTNE_0x82_0x00 = new JTNE_0x82_0x00 ();
            jTNE_0x82_0x00.ParamId = bytes.ReadByte (ref offset);
            readSize = offset;
            return jTNE_0x82_0x00;
        }

        public int Serialize (ref byte[] bytes, int offset, JTNE_0x82_0x00 value) {
            offset += bytes.WriteByte (offset, value.ParamId);
            return offset;
        }
    }
}