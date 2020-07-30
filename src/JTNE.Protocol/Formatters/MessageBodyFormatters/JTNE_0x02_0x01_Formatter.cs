using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;

namespace JTNE.Protocol.Formatters.MessageBodyFormatters {
    public class JTNE_0x02_0x01_Formatter : IJTNEFormatter<JTNE_0x02_0x01> {
        public JTNE_0x02_0x01 Deserialize (ReadOnlySpan<byte> bytes, out int readSize) {
            int offset = 0;
            var body = new JTNE_0x02_0x01 ();
            body.CarStatus = bytes.ReadByte (ref offset);
            body.ChargeStatus = bytes.ReadByte (ref offset);
            body.OperationMode = bytes.ReadByte (ref offset);
            body.Speed = bytes.ReadUInt16 (ref offset);
            body.TotalDis = bytes.ReadUInt32 (ref offset);
            body.TotalVoltage = bytes.ReadUInt16 (ref offset);
            body.TotalTemp = bytes.ReadUInt16 (ref offset);
            body.SOC = bytes.ReadByte (ref offset);
            body.DCStatus = bytes.ReadByte (ref offset);
            body.Stall = bytes.ReadByte (ref offset);
            body.Resistance = bytes.ReadUInt16 (ref offset);
            body.Accelerator = bytes.ReadByte (ref offset);
            body.Brakes = bytes.ReadByte (ref offset);
            readSize = offset;
            return body;
        }

        public int Serialize (ref byte[] bytes, int offset, JTNE_0x02_0x01 value) {
            offset += bytes.WriteByte (offset, value.TypeCode);
            offset += bytes.WriteByte (offset, value.CarStatus);
            offset += bytes.WriteByte (offset, value.ChargeStatus);
            offset += bytes.WriteByte (offset, value.OperationMode);
            offset += bytes.WriteUInt16 (offset, value.Speed.RawValue);
            offset += bytes.WriteUInt32 (offset, value.TotalDis);
            offset += bytes.WriteUInt16 (offset, value.TotalVoltage);
            offset += bytes.WriteUInt16 (offset, value.TotalTemp);
            offset += bytes.WriteByte (offset, value.SOC);
            offset += bytes.WriteByte (offset, value.DCStatus);
            offset += bytes.WriteByte (offset, value.Stall);
            offset += bytes.WriteUInt16 (offset, value.Resistance);
            offset += bytes.WriteByte (offset, value.Accelerator);
            offset += bytes.WriteByte (offset, value.Brakes);
            return offset;
        }
    }
}