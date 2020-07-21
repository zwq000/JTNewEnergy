using System;
using System.Collections.Generic;
using System.Text;
using JTNE.Protocol.Enums;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace JTNE.Protocol.Test {
    public class JTNEHeaderPackageTest {
        private readonly ITestOutputHelper output;

        public JTNEHeaderPackageTest (ITestOutputHelper outputHelper) {
            this.output = outputHelper;
        }

        [Fact]
        public void Test1 () {
            JTNEHeaderPackage jTNEHeaderPackage = new JTNEHeaderPackage ();
            jTNEHeaderPackage.VIN = "123456789";
            jTNEHeaderPackage.AskId = JTNEAskId.CMD.ToByteValue ();
            jTNEHeaderPackage.MsgId = JTNEMsgId.login.ToByteValue ();
            JTNE_0x01 jTNE_0X01 = new JTNE_0x01 ();
            jTNE_0X01.PDATime = DateTime.Parse ("2019-01-22 23:55:56");
            jTNE_0X01.LoginNum = 1;
            jTNE_0X01.BatteryLength = 0x04;
            jTNE_0X01.SIM = "12345678998765432100";
            jTNE_0X01.BatteryNos = new List<string> () {
                "1234",
                "4567",
                "9870"
            };
            jTNEHeaderPackage.Bodies = JTNESerializer.Serialize (jTNE_0X01);
            var hex = JTNESerializer.Serialize (jTNEHeaderPackage).ToHexString ();
            Assert.Equal ("232301FE313233343536373839000000000000000001002A130116173738000131323334353637383939383736353433323130300304313233343435363739383730FD", hex);
        }

        [Fact]
        public void Test2 () {
            var data = "232301FE313233343536373839000000000000000001002A130116173738000131323334353637383939383736353433323130300304313233343435363739383730FD".ToHexBytes ();
            JTNEHeaderPackage jTNEHeaderPackage = JTNESerializer.Deserialize<JTNEHeaderPackage> (data);
            Assert.Equal (JTNEAskId.CMD.ToByteValue (), jTNEHeaderPackage.AskId);
            Assert.Equal (JTNEMsgId.login.ToByteValue (), jTNEHeaderPackage.MsgId);
            Assert.Equal ("123456789", jTNEHeaderPackage.VIN);
            JTNE_0x01 jTNE_0X01 = JTNESerializer.Deserialize<JTNE_0x01> (jTNEHeaderPackage.Bodies);
            Assert.Equal (DateTime.Parse ("2019-01-22 23:55:56"), jTNE_0X01.PDATime);
            Assert.Equal (1, jTNE_0X01.LoginNum);
            Assert.Equal (0x04, jTNE_0X01.BatteryLength);
            Assert.Equal ("12345678998765432100", jTNE_0X01.SIM);
            Assert.Equal (3, jTNE_0X01.BatteryCount);
            Assert.Equal ("1234", jTNE_0X01.BatteryNos[0]);
            Assert.Equal ("4567", jTNE_0X01.BatteryNos[1]);
            Assert.Equal ("9870", jTNE_0X01.BatteryNos[2]);
        }

        [Fact]
        public void TestGuangtai () {
            JTNEGlobalConfigs.Instance.Encoding = Encoding.GetEncoding ("GB18030");
            var data = "23 23 05 FE 30 30 30 30 30 30 30 30 30 30 30 30 30 32 31 31 31 01 00 29 14 07 14 13 26 22 00 01 CD FE BA A3 B9 E3 CC A9 BF D5 B8 DB CD FE BA A3 B9 E3 CC A9 BF D5 B8 DB B3 B5 C1 AA CD F2 01 02 01 A1".ToHexBytes ();
            var package = JTNESerializer.Deserialize (data);
            Assert.Equal (JTNEAskId.CMD.ToByteValue (), package.AskId);
            Assert.Equal (JTNEMsgId.platformlogin.ToByteValue (), package.MsgId);
            Assert.Equal (41, package.DataUnitLength);
            Assert.NotNull (package.Bodies);

            Assert.IsType<JTNE_0x05> (package.Bodies);

            output.WriteLine (Newtonsoft.Json.JsonConvert.SerializeObject (package.Bodies, Formatting.Indented));

            var loginbody = (JTNE_0x05) package.Bodies;
            Assert.Equal ("威海广泰空港", loginbody.PlatformUserName);
            Assert.Equal (1, loginbody.LoginNum);
            Assert.Equal (JTNEEncryptMethod.None, loginbody.EncryptMethod);

        }
    }
}