using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;

namespace JTNE.Protocol.Formatters.MessageBodyFormatters {
    public class JTNE_0x01_Formatter : IJTNEFormatter<JTNE_0x01> {
        public JTNE_0x01 Deserialize (ReadOnlySpan<byte> bytes, out int readSize) {
            int offset = 0;
            JTNE_0x01 jTNE_0X01 = new JTNE_0x01 ();
            jTNE_0X01.PDATime = bytes.ReadDateTime6Bytes (ref offset);
            jTNE_0X01.LoginNum = bytes.ReadUInt16 (ref offset);
            jTNE_0X01.SIM = bytes.ReadString (ref offset, 20);
            jTNE_0X01.BatteryCount = bytes.ReadByte (ref offset);
            jTNE_0X01.BatteryLength = bytes.ReadByte (ref offset);
            jTNE_0X01.BatteryNos = new List<string> ();
            if ((jTNE_0X01.BatteryCount * jTNE_0X01.BatteryLength) > 0) {
                for (int i = 0; i < jTNE_0X01.BatteryCount; i++) {
                    jTNE_0X01.BatteryNos.Add (bytes.ReadString (ref offset, jTNE_0X01.BatteryLength));
                }
            }
            readSize = offset;
            return jTNE_0X01;
        }

        public int Serialize (ref byte[] bytes, int offset, JTNE_0x01 value) {
            offset += bytes.WriteDateTime6Bytes (offset, value.PDATime);
            offset += bytes.WriteUInt16 (offset, value.LoginNum);
            offset += bytes.WriteString (offset, value.SIM, 20);
            offset += bytes.WriteByte (offset, (byte) value.BatteryNos.Count);
            offset += bytes.WriteByte (offset, value.BatteryLength);
            foreach (var item in value.BatteryNos) {
                offset += bytes.WriteString (offset, item, value.BatteryLength);
            }
            return offset;
        }
    }
}