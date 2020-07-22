using System;
using JTNE.Protocol.Enums;
using JTNE.Protocol.Internal;
using Xunit;

namespace JTNE.Protocol.Test.Internal {
    public class JTNEMsgIdFactoryTests {
        [Theory]
        [InlineData (JTNEMsgId.Login)]
        [InlineData (JTNEMsgId.UploadIM)]
        [InlineData (JTNEMsgId.Logout)]
        [InlineData (JTNEMsgId.PlatformLogin)]
        [InlineData (JTNEMsgId.PlatformLogout)]
        [InlineData (JTNEMsgId.HeartBeat)]
        [InlineData (JTNEMsgId.CheckTime)]
        public void TestGetBodiesImplTypeByMsgId (JTNEMsgId msgId) {
            var body = JTNEMsgIdFactory.GetBodyTypeByMsgId ((byte) msgId);
            Assert.NotNull (body);
        }
    }
}