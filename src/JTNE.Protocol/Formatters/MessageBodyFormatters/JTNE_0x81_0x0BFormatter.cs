using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;

namespace JTNE.Protocol.Formatters.MessageBodyFormatters {
    public class JTNE_0x81_0x0BFormatter : IJTNEFormatter<JTNE_0x81_0x0B> {
        public JTNE_0x81_0x0B Deserialize (ReadOnlySpan<byte> bytes, out int readSize) {
            int offset = 0;
            JTNE_0x81_0x0B jTNE_0x81_0x0B = new JTNE_0x81_0x0B ();
            jTNE_0x81_0x0B.ParamValue = bytes.ReadUInt16 (ref offset);
            readSize = offset;
            return jTNE_0x81_0x0B;
        }

        public int Serialize (ref byte[] bytes, int offset, JTNE_0x81_0x0B value) {
            offset += bytes.WriteUInt16 (offset, value.ParamValue);
            return offset;
        }
    }
}