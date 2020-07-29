using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;

namespace JTNE.Protocol.Formatters.MessageBodyFormatters {
    public class JTNE_0x02_0x07_Formatter : IJTNEFormatter<JTNE_0x02_0x07> {
        public JTNE_0x02_0x07 Deserialize (ReadOnlySpan<byte> bytes, out int readSize) {
            int offset = 0;
            JTNE_0x02_0x07 jTNE_0X02_0X07 = new JTNE_0x02_0x07 ();
            jTNE_0X02_0X07.AlarmLevel = bytes.ReadByte (ref offset);
            jTNE_0X02_0X07.AlarmBatteryFlag = bytes.ReadUInt32 (ref offset);

            jTNE_0X02_0X07.AlarmBatteryOtherCount = bytes.ReadByte (ref offset);
            jTNE_0X02_0X07.AlarmBatteryOthers = new List<uint> ();
            for (int i = 0; i < jTNE_0X02_0X07.AlarmBatteryOtherCount; i++) {
                jTNE_0X02_0X07.AlarmBatteryOthers.Add (bytes.ReadUInt32 (ref offset));
            }

            jTNE_0X02_0X07.AlarmElCount = bytes.ReadByte (ref offset);
            jTNE_0X02_0X07.AlarmEls = new List<uint> ();
            for (int i = 0; i < jTNE_0X02_0X07.AlarmElCount; i++) {
                jTNE_0X02_0X07.AlarmEls.Add (bytes.ReadUInt32 (ref offset));
            }

            jTNE_0X02_0X07.AlarmEngineCount = bytes.ReadByte (ref offset);
            jTNE_0X02_0X07.AlarmEngines = new List<uint> ();
            for (int i = 0; i < jTNE_0X02_0X07.AlarmEngineCount; i++) {
                jTNE_0X02_0X07.AlarmEngines.Add (bytes.ReadUInt32 (ref offset));
            }

            jTNE_0X02_0X07.AlarmOtherCount = bytes.ReadByte (ref offset);
            jTNE_0X02_0X07.AlarmOthers = new List<uint> ();
            for (int i = 0; i < jTNE_0X02_0X07.AlarmOtherCount; i++) {
                jTNE_0X02_0X07.AlarmOthers.Add (bytes.ReadUInt32 (ref offset));
            }

            readSize = offset;
            return jTNE_0X02_0X07;
        }

        public int Serialize (ref byte[] bytes, int offset, JTNE_0x02_0x07 value) {
            offset += bytes.WriteByte (offset, value.TypeCode);
            offset += bytes.WriteByte (offset, value.AlarmLevel);
            offset += bytes.WriteUInt32 (offset, value.AlarmBatteryFlag);

            if (value.AlarmBatteryOthers != null && value.AlarmBatteryOthers.Count > 0) {
                offset += bytes.WriteByte (offset, (byte) value.AlarmBatteryOthers.Count);
                foreach (var item in value.AlarmBatteryOthers) {
                    offset += bytes.WriteUInt32 (offset, item);
                }
            }

            if (value.AlarmEls != null && value.AlarmEls.Count > 0) {
                offset += bytes.WriteByte (offset, (byte) value.AlarmEls.Count);
                foreach (var item in value.AlarmEls) {
                    offset += bytes.WriteUInt32 (offset, item);
                }
            }

            if (value.AlarmEngines != null && value.AlarmEngines.Count > 0) {
                offset += bytes.WriteByte (offset, (byte) value.AlarmEngines.Count);
                foreach (var item in value.AlarmEngines) {
                    offset += bytes.WriteUInt32 (offset, item);
                }
            }

            if (value.AlarmEngines != null && value.AlarmEngines.Count > 0) {
                offset += bytes.WriteByte (offset, (byte) value.AlarmOthers.Count);
                foreach (var item in value.AlarmOthers) {
                    offset += bytes.WriteUInt32 (offset, item);
                }
            }
            return offset;
        }
    }
}