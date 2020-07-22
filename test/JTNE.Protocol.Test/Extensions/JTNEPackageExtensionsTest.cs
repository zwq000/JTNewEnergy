﻿using JTNE.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JTNE.Protocol.Extensions;
using JTNE.Protocol.MessageBody;

namespace JTNE.Protocol.Test.Extensions
{
    public class JTNEPackageExtensionsTest
    {
        [Fact]
        public void Test1()
        {
            JTNEPackage jTNEPackage= JTNEMsgId.Login.Create("123456789", JTNEAskId.CMD, new JTNE_0x01
            {
                PDATime = DateTime.Parse("2019-01-22 23:55:56"),
                LoginNum = 1,
                BatteryLength = 0x04,
                SIM = "12345678998765432100",
                BatteryNos = new List<string>()
                {
                   "1234",
                   "4567",
                   "9870"
                }
            });
            var hex = JTNESerializer.Serialize(jTNEPackage).ToHexString();
            Assert.Equal("232301FE313233343536373839000000000000000001002A130116173738000131323334353637383939383736353433323130300304313233343435363739383730FD", hex);
        }
    }
}
