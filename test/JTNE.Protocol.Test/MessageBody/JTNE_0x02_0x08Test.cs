﻿using JTNE.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JTNE.Protocol.Extensions;

namespace JTNE.Protocol.Test.MessageBody
{
    public class JTNE_0x02_0x08Test
    {
        [Fact]
        public void Test1()
        {
            JTNE_0x02_0x08 jTNE_0X02_0X08 = new JTNE_0x02_0x08();
            jTNE_0X02_0X08.BatteryAssemblies = new List<Metadata.BatteryAssembly>();
            Metadata.BatteryAssembly batteryAssembly1 = new Metadata.BatteryAssembly();
            batteryAssembly1.BatteryAssemblyCurrent = 123;
            batteryAssembly1.BatteryAssemblyNo = 0x01;
            batteryAssembly1.BatteryAssemblyVoltage = 0x02;
            batteryAssembly1.SingleBatteryCount = 55;
            batteryAssembly1.ThisSingleBatteryBeginCount = 0x02;
            batteryAssembly1.ThisSingleBatteryBeginNo = 111;
            batteryAssembly1.SingleBatteryVoltages = new List<ushort> {
                111,222,333
            };
            Metadata.BatteryAssembly batteryAssembly2 = new Metadata.BatteryAssembly();
            batteryAssembly2.BatteryAssemblyCurrent = 1234;
            batteryAssembly2.BatteryAssemblyNo = 0x03;
            batteryAssembly2.BatteryAssemblyVoltage = 0x05;
            batteryAssembly2.SingleBatteryCount = 66;
            batteryAssembly2.ThisSingleBatteryBeginCount = 0x02;
            batteryAssembly2.ThisSingleBatteryBeginNo = 222;
            batteryAssembly2.SingleBatteryVoltages = new List<ushort> {
                444,555,666
            };
            jTNE_0X02_0X08.BatteryAssemblies.Add(batteryAssembly1);
            jTNE_0X02_0X08.BatteryAssemblies.Add(batteryAssembly2);
            var hex = JTNESerializer.Serialize(jTNE_0X02_0X08).ToHexString();
            Assert.Equal("0802010002007B0037006F03006F00DE014D03000504D2004200DE0301BC022B029A", hex);
        }

        [Fact]
        public void Test2()
        {
            var data = "0802010002007B0037006F03006F00DE014D03000504D2004200DE0301BC022B029A".ToHexBytes();
            JTNE_0x02_0x08 jTNE_0X02_0X08 = JTNESerializer.Deserialize<JTNE_0x02_0x08>(data.AsSpan().Slice(1));
            Assert.Equal(JTNE_0x02_Body.JTNE_0x02_0x08, jTNE_0X02_0X08.TypeCode);
            Assert.Equal(2, jTNE_0X02_0X08.BatteryAssemblyCount);

            Metadata.BatteryAssembly batteryAssembly1 = jTNE_0X02_0X08.BatteryAssemblies[0];
            Assert.Equal(123, batteryAssembly1.BatteryAssemblyCurrent);
            Assert.Equal(0x01, batteryAssembly1.BatteryAssemblyNo);
            Assert.Equal(0x02, batteryAssembly1.BatteryAssemblyVoltage);
            Assert.Equal(55, batteryAssembly1.SingleBatteryCount);
            Assert.Equal(111, batteryAssembly1.ThisSingleBatteryBeginNo);
            Assert.Equal(3, batteryAssembly1.ThisSingleBatteryBeginCount);
            Assert.Equal(new List<ushort> {
                111,222,333
            }, batteryAssembly1.SingleBatteryVoltages);

            Metadata.BatteryAssembly batteryAssembly2 = jTNE_0X02_0X08.BatteryAssemblies[1];
            Assert.Equal(1234, batteryAssembly2.BatteryAssemblyCurrent);
            Assert.Equal(0x03, batteryAssembly2.BatteryAssemblyNo);
            Assert.Equal(0x05, batteryAssembly2.BatteryAssemblyVoltage);
            Assert.Equal(66, batteryAssembly2.SingleBatteryCount);
            Assert.Equal(222, batteryAssembly2.ThisSingleBatteryBeginNo);
            Assert.Equal(3, batteryAssembly2.ThisSingleBatteryBeginCount);
            Assert.Equal(new List<ushort> {
                444,555,666
            }, batteryAssembly2.SingleBatteryVoltages);
        }
    }
}
