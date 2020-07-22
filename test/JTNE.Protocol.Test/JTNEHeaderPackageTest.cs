using System;
using System.Collections.Generic;
using JTNE.Protocol.Enums;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;
using Xunit;
using Xunit.Abstractions;

namespace JTNE.Protocol.Test
{
    public class JTNEHeaderPackageTest {
        private readonly ITestOutputHelper output;

        public JTNEHeaderPackageTest (ITestOutputHelper outputHelper) {
            this.output = outputHelper;
        }

        [Fact]
        public void Test1 () {
            JTNEPackageHeader jTNEHeaderPackage = new JTNEPackageHeader ();
            jTNEHeaderPackage.VIN = "123456789";
            jTNEHeaderPackage.AskId = JTNEAskId.CMD.ToByteValue ();
            jTNEHeaderPackage.MsgId = JTNEMsgId.Login.ToByteValue ();
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
            JTNEPackageHeader jTNEHeaderPackage = JTNESerializer.Deserialize<JTNEPackageHeader> (data);
            Assert.Equal (JTNEAskId.CMD.ToByteValue (), jTNEHeaderPackage.AskId);
            Assert.Equal (JTNEMsgId.Login.ToByteValue (), jTNEHeaderPackage.MsgId);
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

       
    }

    public static class JTNEReplyExtensions{
        public static JTNEPackage GenerateReply(this JTNEPackage source,JTNEAskId askId){
            return new JTNEPackage(){
                MsgId = source.MsgId,
                AskId = askId,
                VIN = source.VIN,
                DataUnitLength = 0
            };
        }
    }
}