using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;

namespace JTNE.Protocol.Formatters.MessageBodyFormatters {
    public class JTNE_0x81_0x0CFormatter : IJTNEFormatter<JTNE_0x81_0x0C> {
        public JTNE_0x81_0x0C Deserialize (ReadOnlySpan<byte> bytes, out int readSize) {
            int offset = 0;
            JTNE_0x81_0x0C jTNE_0x81_0x0C = new JTNE_0x81_0x0C ();
            jTNE_0x81_0x0C.ParamValue = bytes.ReadByte (ref offset);
            readSize = offset;
            return jTNE_0x81_0x0C;
        }

        public int Serialize (ref byte[] bytes, int offset, JTNE_0x81_0x0C value) {
            offset += bytes.WriteByte (offset, value.ParamValue);
            return offset;
        }
    }
}