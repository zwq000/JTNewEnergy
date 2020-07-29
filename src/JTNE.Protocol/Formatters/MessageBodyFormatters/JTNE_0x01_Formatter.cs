﻿using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;

namespace JTNE.Protocol.Formatters.MessageBodyFormatters
{
    public class JTNE_0x01_Formatter : IJTNEFormatter<JTNE_0x01>
    {
        public JTNE_0x01 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JTNE_0x01 jTNE_0X01 = new JTNE_0x01();
            jTNE_0X01.PDATime = JTNEBinaryExtensions.ReadDateTime6Bytes(bytes, ref offset);
            jTNE_0X01.LoginNum = JTNEBinaryExtensions.ReadUInt16(bytes, ref offset);
            jTNE_0X01.SIM = JTNEBinaryExtensions.ReadString(bytes, ref offset, 20);
            jTNE_0X01.BatteryCount = JTNEBinaryExtensions.ReadByte(bytes, ref offset);
            jTNE_0X01.BatteryLength = JTNEBinaryExtensions.ReadByte(bytes, ref offset);
            jTNE_0X01.BatteryNos = new List<string>();
            if ((jTNE_0X01.BatteryCount * jTNE_0X01.BatteryLength) > 0)
            {
                for (int i = 0; i < jTNE_0X01.BatteryCount; i++)
                {
                    jTNE_0X01.BatteryNos.Add(JTNEBinaryExtensions.ReadString(bytes, ref offset, jTNE_0X01.BatteryLength));
                }
            }
            readSize = offset;
            return jTNE_0X01;
        }

        public int Serialize(ref byte[] bytes, int offset, JTNE_0x01 value)
        {
            offset += JTNEBinaryExtensions.WriteDateTime6Bytes(bytes, offset, value.PDATime);
            offset += JTNEBinaryExtensions.WriteUInt16(bytes, offset, value.LoginNum);
            offset += JTNEBinaryExtensions.WriteStringLittle(bytes, offset, value.SIM, 20);
            offset += JTNEBinaryExtensions.WriteByte(bytes, offset, (byte)value.BatteryNos.Count);
            offset += JTNEBinaryExtensions.WriteByte(bytes, offset, value.BatteryLength);
            foreach(var item in value.BatteryNos)
            {
                offset += JTNEBinaryExtensions.WriteStringLittle(bytes, offset, item, value.BatteryLength);
            }
            return offset;
        }
    }
}
