﻿using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;

namespace JTNE.Protocol.Formatters.MessageBodyFormatters {
    public class JTNE_0x02_0x05_Formatter : IJTNEFormatter<JTNE_0x02_0x05> {
        public JTNE_0x02_0x05 Deserialize (ReadOnlySpan<byte> bytes, out int readSize) {
            int offset = 0;
            JTNE_0x02_0x05 jTNE_0X02_0X05 = new JTNE_0x02_0x05 ();
            jTNE_0X02_0X05.PositioStatus = (PositioStatus) bytes.ReadByte (ref offset);
            jTNE_0X02_0X05.Lng = bytes.ReadUInt32 (ref offset);
            jTNE_0X02_0X05.Lat = bytes.ReadUInt32 (ref offset);
            readSize = offset;
            return jTNE_0X02_0X05;
        }

        public int Serialize (ref byte[] bytes, int offset, JTNE_0x02_0x05 value) {
            offset += bytes.WriteByte (offset, value.TypeCode);
            offset += bytes.WriteByte (offset, (byte) value.PositioStatus);
            offset += bytes.WriteUInt32 (offset, value.Lng);
            offset += bytes.WriteUInt32 (offset, value.Lat);
            return offset;
        }
    }
}