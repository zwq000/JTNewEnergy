using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;

namespace JTNE.Protocol.Formatters.MessageBodyFormatters {
    public class JTNE_0x02_0x09_Formatter : IJTNEFormatter<JTNE_0x02_0x09> {
        public JTNE_0x02_0x09 Deserialize (ReadOnlySpan<byte> bytes, out int readSize) {
            int offset = 0;
            JTNE_0x02_0x09 jTNE_0X02_0X09 = new JTNE_0x02_0x09 ();
            jTNE_0X02_0X09.BatteryTemperatureCount = bytes.ReadByte (ref offset);
            jTNE_0X02_0X09.BatteryTemperatures = new List<Metadata.BatteryTemperature> ();
            for (int i = 0; i < jTNE_0X02_0X09.BatteryTemperatureCount; i++) {
                Metadata.BatteryTemperature batteryTemperature = new Metadata.BatteryTemperature ();
                batteryTemperature.BatteryAssemblyNo = bytes.ReadByte (ref offset);
                batteryTemperature.TemperatureProbeCount = bytes.ReadUInt16 (ref offset);
                batteryTemperature.Temperatures = bytes.ReadBytes (ref offset, batteryTemperature.TemperatureProbeCount);
                jTNE_0X02_0X09.BatteryTemperatures.Add (batteryTemperature);
            }
            readSize = offset;
            return jTNE_0X02_0X09;
        }

        public int Serialize (ref byte[] bytes, int offset, JTNE_0x02_0x09 value) {
            offset += bytes.WriteByte (offset, value.TypeCode);
            if (value.BatteryTemperatures != null && value.BatteryTemperatures.Count > 0) {
                offset += bytes.WriteByte (offset, (byte) value.BatteryTemperatures.Count);
                foreach (var item in value.BatteryTemperatures) {
                    offset += bytes.WriteByte (offset, item.BatteryAssemblyNo);
                    if (item.Temperatures != null && item.Temperatures.Length > 0) {
                        offset += bytes.WriteUInt16 (offset, (byte) item.Temperatures.Length);
                        offset += bytes.WriteBytes (offset, item.Temperatures);
                    }
                }
            }
            return offset;
        }
    }
}