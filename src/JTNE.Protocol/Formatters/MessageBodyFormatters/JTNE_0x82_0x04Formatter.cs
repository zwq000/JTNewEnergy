using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Enums;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;

namespace JTNE.Protocol.Formatters.MessageBodyFormatters {
    public class JTNE_0x82_0x04Formatter : IJTNEFormatter<JTNE_0x82_0x04> {
        public JTNE_0x82_0x04 Deserialize (ReadOnlySpan<byte> bytes, out int readSize) {
            int offset = 0;
            JTNE_0x82_0x04 jTNE_0x82_0x04 = new JTNE_0x82_0x04 ();
            jTNE_0x82_0x04.ParamId = bytes.ReadByteLittle (ref offset);

            readSize = offset;
            return jTNE_0x82_0x04;
        }

        public int Serialize (ref byte[] bytes, int offset, JTNE_0x82_0x04 value) {
            offset += bytes.WriteByteLittle (offset, value.ParamId);
            return offset;
        }
    }
}