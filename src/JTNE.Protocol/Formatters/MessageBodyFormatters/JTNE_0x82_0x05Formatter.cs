﻿using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Enums;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;

namespace JTNE.Protocol.Formatters.MessageBodyFormatters {
    public class JTNE_0x82_0x05Formatter : IJTNEFormatter<JTNE_0x82_0x05> {
        public JTNE_0x82_0x05 Deserialize (ReadOnlySpan<byte> bytes, out int readSize) {
            int offset = 0;
            JTNE_0x82_0x05 jTNE_0x82_0x05 = new JTNE_0x82_0x05 ();
            jTNE_0x82_0x05.ParamId = bytes.ReadByteLittle (ref offset);

            readSize = offset;
            return jTNE_0x82_0x05;
        }

        public int Serialize (ref byte[] bytes, int offset, JTNE_0x82_0x05 value) {
            offset += bytes.WriteByteLittle (offset, value.ParamId);
            return offset;
        }
    }
}