﻿using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;

namespace JTNE.Protocol.Formatters.MessageBodyFormatters {
    public class JTNE_0x81_0x04Formatter : IJTNEFormatter<JTNE_0x81_0x04> {
        public JTNE_0x81_0x04 Deserialize (ReadOnlySpan<byte> bytes, out int readSize) {
            int offset = 0;
            JTNE_0x81_0x04 jTNE_0x81_0x04 = new JTNE_0x81_0x04 ();
            jTNE_0x81_0x04.ParamValue = bytes.ReadByte (ref offset);
            readSize = offset;
            return jTNE_0x81_0x04;
        }

        public int Serialize (ref byte[] bytes, int offset, JTNE_0x81_0x04 value) {
            offset += bytes.WriteByte (offset, value.ParamValue);
            return offset;
        }
    }
}