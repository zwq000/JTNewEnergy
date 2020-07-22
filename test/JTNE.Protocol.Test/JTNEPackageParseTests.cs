using System.Text;
using JTNE.Protocol.Enums;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace JTNE.Protocol.Test
{
    public class JTNEPackageParseTests{

         private readonly ITestOutputHelper output;

        public JTNEPackageParseTests (ITestOutputHelper outputHelper) {
            this.output = outputHelper;
        }
        const string data="232302FE303030303030303030303032303037343101009C1407160B210801FF04010000FFFFFFFFE5F6280732FFFE03D1FFFF020101FFFF4E204E204BFFFFFFFF03FFFFFFFFFFFF3000FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF0401FFFFFFFFFFFFFFFFFFFFFF05FFFFFFFFFFFFFFFFFF06FFFFFFFFFFFFFFFF4800482502450700000000000000000049";

         [Fact]
        public void TestGuangtai () {
            JTNEGlobalConfigs.Instance.Encoding = Encoding.GetEncoding ("GB18030");
            var data = "23 23 05 FE 30 30 30 30 30 30 30 30 30 30 30 30 30 32 31 31 31 01 00 29 14 07 14 13 26 22 00 01 CD FE BA A3 B9 E3 CC A9 BF D5 B8 DB CD FE BA A3 B9 E3 CC A9 BF D5 B8 DB B3 B5 C1 AA CD F2 01 02 01 A1".ToHexBytes ();
            var package = JTNESerializer.Deserialize (data);
            Assert.Equal (JTNEAskId.CMD, package.AskId);
            Assert.Equal (JTNEMsgId.PlatformLogin, package.MsgId);
            Assert.Equal (41, package.DataUnitLength);
            Assert.NotNull (package.Bodies);

            Assert.IsType<JTNE_0x05> (package.Bodies);

            output.WriteLine (Newtonsoft.Json.JsonConvert.SerializeObject (package.Bodies, Formatting.Indented));

            var loginbody = (JTNE_0x05) package.Bodies;
            Assert.Equal ("威海广泰空港", loginbody.PlatformUserName);
            Assert.Equal (1, loginbody.LoginNum);
            Assert.Equal (JTNEEncryptMethod.None, loginbody.EncryptMethod);

        }

        [Fact]
        public void TestGenerateReply(){
            JTNEGlobalConfigs.Instance.Encoding = Encoding.GetEncoding ("GB18030");
            var data = "23 23 05 FE 30 30 30 30 30 30 30 30 30 30 30 30 30 32 31 31 31 01 00 29 14 07 14 13 26 22 00 01 CD FE BA A3 B9 E3 CC A9 BF D5 B8 DB CD FE BA A3 B9 E3 CC A9 BF D5 B8 DB B3 B5 C1 AA CD F2 01 02 01 A1".ToHexBytes ();
            var package = JTNESerializer.Deserialize (data);
            var reply = package.GenerateReply(JTNEAskId.Success);
            Assert.NotNull(reply);
            var bytes = JTNESerializer.Serialize(reply);
            output.WriteLine(bytes.ToHexString());

        }
    }
}