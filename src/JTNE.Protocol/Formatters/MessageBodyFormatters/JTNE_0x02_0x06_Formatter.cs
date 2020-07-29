using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;

namespace JTNE.Protocol.Formatters.MessageBodyFormatters {
    public class JTNE_0x02_0x06_Formatter : IJTNEFormatter<JTNE_0x02_0x06> {
        public JTNE_0x02_0x06 Deserialize (ReadOnlySpan<byte> bytes, out int readSize) {
            int offset = 0;
            JTNE_0x02_0x06 jTNE_0X02_0X06 = new JTNE_0x02_0x06 ();
            jTNE_0X02_0X06.MaxVoltageBatteryAssemblyNo = bytes.ReadByte (ref offset);
            jTNE_0X02_0X06.MaxVoltageSingleBatteryNo = bytes.ReadByte (ref offset);
            jTNE_0X02_0X06.MaxVoltageSingleBatteryValue = bytes.ReadUInt16 (ref offset);
            jTNE_0X02_0X06.MinVoltageBatteryAssemblyNo = bytes.ReadByte (ref offset);
            jTNE_0X02_0X06.MinVoltageSingleBatteryNo = bytes.ReadByte (ref offset);
            jTNE_0X02_0X06.MinVoltageSingleBatteryValue = bytes.ReadUInt16 (ref offset);
            jTNE_0X02_0X06.MaxTempProbeBatteryNo = bytes.ReadByte (ref offset);
            jTNE_0X02_0X06.MaxTempBatteryAssemblyNo = bytes.ReadByte (ref offset);
            jTNE_0X02_0X06.MaxTempProbeBatteryValue = bytes.ReadByte (ref offset);
            jTNE_0X02_0X06.MinTempProbeBatteryNo = bytes.ReadByte (ref offset);
            jTNE_0X02_0X06.MinTempBatteryAssemblyNo = bytes.ReadByte (ref offset);
            jTNE_0X02_0X06.MinTempProbeBatteryValue = bytes.ReadByte (ref offset);
            readSize = offset;
            return jTNE_0X02_0X06;
        }

        public int Serialize (ref byte[] bytes, int offset, JTNE_0x02_0x06 value) {
            offset += bytes.WriteByte (offset, value.TypeCode);
            offset += bytes.WriteByte (offset, value.MaxVoltageBatteryAssemblyNo);
            offset += bytes.WriteByte (offset, value.MaxVoltageSingleBatteryNo);
            offset += bytes.WriteUInt16 (offset, value.MaxVoltageSingleBatteryValue);
            offset += bytes.WriteByte (offset, value.MinVoltageBatteryAssemblyNo);
            offset += bytes.WriteByte (offset, value.MinVoltageSingleBatteryNo);
            offset += bytes.WriteUInt16 (offset, value.MinVoltageSingleBatteryValue);
            offset += bytes.WriteByte (offset, value.MaxTempProbeBatteryNo);
            offset += bytes.WriteByte (offset, value.MaxTempBatteryAssemblyNo);
            offset += bytes.WriteByte (offset, value.MaxTempProbeBatteryValue);
            offset += bytes.WriteByte (offset, value.MinTempProbeBatteryNo);
            offset += bytes.WriteByte (offset, value.MinTempBatteryAssemblyNo);
            offset += bytes.WriteByte (offset, value.MinTempProbeBatteryValue);
            return offset;
        }
    }
}