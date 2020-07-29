using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;

namespace JTNE.Protocol.Formatters.MessageBodyFormatters {
    public class JTNE_0x02_0x04_Formatter : IJTNEFormatter<JTNE_0x02_0x04> {
        public JTNE_0x02_0x04 Deserialize (ReadOnlySpan<byte> bytes, out int readSize) {
            int offset = 0;
            JTNE_0x02_0x04 jTNE_0X02_0X04 = new JTNE_0x02_0x04 ();
            jTNE_0X02_0X04.EngineStatus = bytes.ReadByte (ref offset);
            jTNE_0X02_0X04.Revs = bytes.ReadUInt16 (ref offset);
            jTNE_0X02_0X04.FuelRate = bytes.ReadUInt16 (ref offset);
            readSize = offset;
            return jTNE_0X02_0X04;
        }

        public int Serialize (ref byte[] bytes, int offset, JTNE_0x02_0x04 value) {
            offset += bytes.WriteByte (offset, value.TypeCode);
            offset += bytes.WriteByte (offset, value.EngineStatus);
            offset += bytes.WriteUInt16 (offset, value.Revs);
            offset += bytes.WriteUInt16 (offset, value.FuelRate);
            return offset;
        }
    }
}