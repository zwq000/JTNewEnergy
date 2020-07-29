using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;

namespace JTNE.Protocol.Formatters.MessageBodyFormatters {
    public class JTNE_0x02_0x08_Formatter : IJTNEFormatter<JTNE_0x02_0x08> {
        public JTNE_0x02_0x08 Deserialize (ReadOnlySpan<byte> bytes, out int readSize) {
            int offset = 0;
            JTNE_0x02_0x08 jTNE_0X02_0X08 = new JTNE_0x02_0x08 ();
            jTNE_0X02_0X08.BatteryAssemblyCount = bytes.ReadByte (ref offset);
            jTNE_0X02_0X08.BatteryAssemblies = new List<Metadata.BatteryAssembly> ();
            for (int i = 0; i < jTNE_0X02_0X08.BatteryAssemblyCount; i++) {
                Metadata.BatteryAssembly batteryAssembly = new Metadata.BatteryAssembly ();
                batteryAssembly.BatteryAssemblyNo = bytes.ReadByte (ref offset);
                batteryAssembly.BatteryAssemblyVoltage = bytes.ReadUInt16 (ref offset);
                batteryAssembly.BatteryAssemblyCurrent = bytes.ReadUInt16 (ref offset);
                batteryAssembly.SingleBatteryCount = bytes.ReadUInt16 (ref offset);
                batteryAssembly.ThisSingleBatteryBeginNo = bytes.ReadUInt16 (ref offset);
                batteryAssembly.ThisSingleBatteryBeginCount = bytes.ReadByte (ref offset);
                batteryAssembly.SingleBatteryVoltages = new List<ushort> ();
                for (var j = 0; j < batteryAssembly.ThisSingleBatteryBeginCount; j++) {
                    batteryAssembly.SingleBatteryVoltages.Add (bytes.ReadUInt16 (ref offset));
                }
                jTNE_0X02_0X08.BatteryAssemblies.Add (batteryAssembly);
            }
            readSize = offset;
            return jTNE_0X02_0X08;
        }

        public int Serialize (ref byte[] bytes, int offset, JTNE_0x02_0x08 value) {
            offset += bytes.WriteByte (offset, value.TypeCode);
            if (value.BatteryAssemblies != null && value.BatteryAssemblies.Count > 0) {
                offset += bytes.WriteByte (offset, (byte) value.BatteryAssemblies.Count);
                foreach (var item in value.BatteryAssemblies) {
                    offset += bytes.WriteByte (offset, item.BatteryAssemblyNo);
                    offset += bytes.WriteUInt16 (offset, item.BatteryAssemblyVoltage);
                    offset += bytes.WriteUInt16 (offset, item.BatteryAssemblyCurrent);
                    offset += bytes.WriteUInt16 (offset, item.SingleBatteryCount);
                    offset += bytes.WriteUInt16 (offset, item.ThisSingleBatteryBeginNo);
                    if (item.SingleBatteryVoltages != null && item.SingleBatteryVoltages.Count > 0) {
                        offset += bytes.WriteByte (offset, (byte) item.SingleBatteryVoltages.Count);
                        foreach (var item1 in item.SingleBatteryVoltages) {
                            offset += bytes.WriteUInt16 (offset, item1);
                        }
                    }
                }
            }
            return offset;
        }
    }
}