using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;

namespace JTNE.Protocol.Formatters.MessageBodyFormatters {
    public class JTNE_0x02_0x02_Formatter : IJTNEFormatter<JTNE_0x02_0x02> {
        public JTNE_0x02_0x02 Deserialize (ReadOnlySpan<byte> bytes, out int readSize) {
            int offset = 0;
            JTNE_0x02_0x02 jTNE_0X02_0X02 = new JTNE_0x02_0x02 ();
            jTNE_0X02_0X02.ElectricalCount = bytes.ReadByte (ref offset);
            jTNE_0X02_0X02.Electricals = new List<Metadata.Electrical> ();
            for (var i = 0; i < jTNE_0X02_0X02.ElectricalCount; i++) {
                Metadata.Electrical electrical = new Metadata.Electrical ();
                electrical.ElNo = bytes.ReadByte (ref offset);
                electrical.ElStatus = bytes.ReadByte (ref offset);
                electrical.ElControlTemp = bytes.ReadByte (ref offset);
                electrical.ElSpeed = bytes.ReadUInt16 (ref offset);
                electrical.ElTorque = bytes.ReadUInt16 (ref offset);
                electrical.ElTemp = bytes.ReadByte (ref offset);
                electrical.ElVoltage = bytes.ReadUInt16 (ref offset);
                electrical.ElCurrent = bytes.ReadUInt16 (ref offset);
                jTNE_0X02_0X02.Electricals.Add (electrical);
            }
            readSize = offset;
            return jTNE_0X02_0X02;
        }

        public int Serialize (ref byte[] bytes, int offset, JTNE_0x02_0x02 value) {
            offset += bytes.WriteByte (offset, value.TypeCode);
            if (value.Electricals != null && value.Electricals.Count > 0) {
                offset += bytes.WriteByte (offset, (byte) value.Electricals.Count);
                foreach (var item in value.Electricals) {
                    offset += bytes.WriteByte (offset, item.ElNo);
                    offset += bytes.WriteByte (offset, item.ElStatus);
                    offset += bytes.WriteByte (offset, item.ElControlTemp);
                    offset += bytes.WriteUInt16 (offset, item.ElSpeed);
                    offset += bytes.WriteUInt16 (offset, item.ElTorque);
                    offset += bytes.WriteByte (offset, item.ElTemp);
                    offset += bytes.WriteUInt16 (offset, item.ElVoltage);
                    offset += bytes.WriteUInt16 (offset, item.ElCurrent);
                }
            }
            return offset;
        }
    }
}