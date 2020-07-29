using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;

namespace JTNE.Protocol.Formatters.MessageBodyFormatters {
    public class JTNE_0x02_0x03_Formatter : IJTNEFormatter<JTNE_0x02_0x03> {
        public JTNE_0x02_0x03 Deserialize (ReadOnlySpan<byte> bytes, out int readSize) {
            int offset = 0;
            JTNE_0x02_0x03 jTNE_0X02_0X03 = new JTNE_0x02_0x03 ();
            //jTNE_0X02_0X03.TypeCode = JTNEBinaryExtensions.ReadByteLittle (bytes, ref offset);
            jTNE_0X02_0X03.FuelBatteryVoltage = bytes.ReadUInt16 (ref offset);
            jTNE_0X02_0X03.FuelBatteryCurrent = bytes.ReadUInt16 (ref offset);
            jTNE_0X02_0X03.FuelConsumptionRate = bytes.ReadUInt16 (ref offset);
            jTNE_0X02_0X03.TemperatureProbeCount = bytes.ReadUInt16 (ref offset);
            if (jTNE_0X02_0X03.TemperatureProbeCount != 0xFEFE && jTNE_0X02_0X03.TemperatureProbeCount < bytes.Length - offset) {
                jTNE_0X02_0X03.Temperatures = bytes.ReadBytes (ref offset, jTNE_0X02_0X03.TemperatureProbeCount);
            } else {
                jTNE_0X02_0X03.Temperatures = new byte[0];
            }

            jTNE_0X02_0X03.HydrogenSystemMaxTemp = bytes.ReadUInt16 (ref offset);
            jTNE_0X02_0X03.HydrogenSystemMaxTempNo = bytes.ReadByte (ref offset);
            jTNE_0X02_0X03.HydrogenSystemMaxConcentrations = bytes.ReadUInt16 (ref offset);
            jTNE_0X02_0X03.HydrogenSystemMaxConcentrationsNo = bytes.ReadByte (ref offset);
            jTNE_0X02_0X03.HydrogenSystemMaxPressure = bytes.ReadUInt16 (ref offset);
            jTNE_0X02_0X03.HydrogenSystemMaxPressureNo = bytes.ReadByte (ref offset);
            jTNE_0X02_0X03.DCStatus = bytes.ReadByte (ref offset);
            readSize = offset;
            return jTNE_0X02_0X03;
        }

        public int Serialize (ref byte[] bytes, int offset, JTNE_0x02_0x03 value) {
            offset += bytes.WriteByte (offset, value.TypeCode);
            offset += bytes.WriteUInt16 (offset, value.FuelBatteryVoltage);
            offset += bytes.WriteUInt16 (offset, value.FuelBatteryCurrent);
            offset += bytes.WriteUInt16 (offset, value.FuelConsumptionRate);
            offset += bytes.WriteUInt16 (offset, (ushort) value.Temperatures.Length);
            offset += bytes.WriteBytes (offset, value.Temperatures);
            offset += bytes.WriteUInt16 (offset, value.HydrogenSystemMaxTemp);
            offset += bytes.WriteByte (offset, value.HydrogenSystemMaxTempNo);
            offset += bytes.WriteUInt16 (offset, value.HydrogenSystemMaxConcentrations);
            offset += bytes.WriteByte (offset, value.HydrogenSystemMaxConcentrationsNo);
            offset += bytes.WriteUInt16 (offset, value.HydrogenSystemMaxPressure);
            offset += bytes.WriteByte (offset, value.HydrogenSystemMaxPressureNo);
            offset += bytes.WriteByte (offset, value.DCStatus);
            return offset;
        }
    }
}