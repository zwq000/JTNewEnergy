using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;

namespace JTNE.Protocol.Formatters.MessageBodyFormatters {
    public class JTNE_0x81_0x0DFormatter : IJTNEFormatter<JTNE_0x81_0x0D> {
        public JTNE_0x81_0x0D Deserialize (ReadOnlySpan<byte> bytes, out int readSize) {
            int offset = 0;
            JTNE_0x81_0x0D jTNE_0x81_0x0D = new JTNE_0x81_0x0D ();
            jTNE_0x81_0x0D.ParamValue = bytes.ReadByte (ref offset);
            readSize = offset;
            return jTNE_0x81_0x0D;
        }

        public int Serialize (ref byte[] bytes, int offset, JTNE_0x81_0x0D value) {
            offset += bytes.WriteByte (offset, value.ParamValue);
            return offset;
        }
    }
}