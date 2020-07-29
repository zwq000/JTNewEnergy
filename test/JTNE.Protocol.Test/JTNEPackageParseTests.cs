using System.Linq;
using System.Text;
using JTNE.Protocol.Enums;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace JTNE.Protocol.Test {
    public class JTNEPackageParseTests {

        private readonly ITestOutputHelper output;

        public JTNEPackageParseTests (ITestOutputHelper outputHelper) {
            this.output = outputHelper;
        }
        const string PKG_02 = "232302FE303030303030303030303032303037343101009C1407160C260901FF04010000FFFFFFFFE62827BB2EFFFE03C4FFFF020101FFFF4E204E204DFFFFFFFF03FFFFFFFFFFFF3000FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF0401FFFFFFFFFFFFFFFFFFFFFF05FFFFFFFFFFFFFFFFFF06FFFFFFFFFFFFFFFF431047200045070000000000000000003A";
        const string PKG_02_2 = "232302FE303030303030303030303032303037343101009C1407160E0E0F01FF040100E6FFFFFFFFE54C28EE2BFFFE0426FFFF020101FFFF517457F851FFFFFFFF03FFFFFFFFFFFF3000FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF0401FFFFFFFFFFFFFFFFFFFFFF05FFFFFFFFFFFFFFFFFF06FFFFFFFFFFFFFFFF43124720004507000000000000000000B9";

        [Theory]
        [InlineData (PKG_02)]
        [InlineData (PKG_02_2)]
        public void TestPackage02 (string hexStr) {
            var package = JTNESerializer.Deserialize (hexStr.ToHexBytes ());
            Assert.NotNull (package);
            Assert.Equal (JTNEMsgId.UploadIM, package.MsgId);
            Assert.NotNull (package.Bodies);
            Assert.IsType<JTNE_0x02> (package.Bodies);
            var body = (JTNE_0x02) package.Bodies;
            Assert.NotNull (body.Values);
            Assert.NotEmpty (body.Values);

            output.WriteLine (Newtonsoft.Json.JsonConvert.SerializeObject (body, Formatting.Indented));

            // foreach(var item in body.Values){
            //     output.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(item.Value,Formatting.Indented));
            // }
        }

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
        public void TestGenerateReply () {
            JTNEGlobalConfigs.Instance.Encoding = Encoding.GetEncoding ("GB18030");
            var data = "23 23 05 FE 30 30 30 30 30 30 30 30 30 30 30 30 30 32 31 31 31 01 00 29 14 07 14 13 26 22 00 01 CD FE BA A3 B9 E3 CC A9 BF D5 B8 DB CD FE BA A3 B9 E3 CC A9 BF D5 B8 DB B3 B5 C1 AA CD F2 01 02 01 A1".ToHexBytes ();
            var package = JTNESerializer.Deserialize (data);
            var reply = package.GenerateReply (JTNEAskId.Success);
            Assert.NotNull (reply);
            var bytes = JTNESerializer.Serialize (reply);
            output.WriteLine (bytes.ToHexString ());
        }

        /// <summary>
        /// 00000000: 2323 02fe 3030 3030 3030 3030 3030 3032  ##..000000000002
        /// 00000010: 3030 3734 3101 0044 1407 1c0f 2f2d 0102  00741..D..../-..
        /// 00000020: 0004 0100 0000 0003 e002 4300 005d 0200  ..........C..]..
        /// 00000030: 30cd ffff 0201 01ff 2700 0000 002a 0255  0.......'....*.U
        /// 00000040: 0000 0500 06fe becb 0255 0059 06ff 01ff  .........U.Y....
        /// 00000050: 0003 02ff 0002 4310 2020 001e 1a         ......C.  ...
        /// </summary>
        [Fact]
        public void TestMsg2 () {
            var data = "232302FE303030303030303030303032303037343101004414071C0F2F2D01020004010000000003E0024300005D020030CDFFFF020101FF27000000002A02550000050006FEBECB0255005906FF01FF000302FF000243102020001E1A";
            // 00000000: 2323 02fe 3030 3030 3030 3030 3030 3032  ##..000000000002
            // 00000010: 3030 3734 3101 0044 1407 1c0f 2f2d 0102  00741..D..../-..
            // 00000020: 0004 0100 0000 0003 e002 4300 005d 0200  ..........C..]..
            // 00000030: 30cd ffff 0201 01ff 2700 0000 002a 0255  0.......'....*.U
            // 00000040: 0000 0500 06fe becb 0255 0059 06ff 01ff  .........U.Y....
            // 00000050: 0003 02ff 0002 4310 2020 001e 1a         ......C.  ...
            var package = JTNESerializer.Deserialize (data.ToHexBytes ());
            Assert.NotNull (package);
            Assert.Equal (JTNEMsgId.UploadIM, package.MsgId);
            Assert.NotNull (package.Bodies);
            var body = (JTNE_0x02) package.Bodies;
            Assert.NotEmpty (body.Values);
            Assert.Contains<byte> (5, body.Values.Select (p => p.TypeCode));
        }
    }
}