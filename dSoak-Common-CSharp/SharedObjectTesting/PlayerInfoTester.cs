using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SharedObjects;

namespace SharedObjectTesting
{
    [TestClass]
    public class PlayerInfoTester
    {
        [TestMethod]
        public void PlayerInfo_TestEverything()
        {
            PlayerInfo p1 = new PlayerInfo();
            Assert.AreEqual(0, p1.PlayerId);
            Assert.AreEqual(null, p1.EndPoint);
            Assert.AreEqual(PlayerInfo.StateCode.Unknown, p1.Status);

            PublicEndPoint ep1 = new PublicEndPoint() { Host = "swcwin.serv.usu.edu", Port = 32541 };
            PlayerInfo p2 = new PlayerInfo() { EndPoint = ep1, PlayerId = 101, Status = PlayerInfo.StateCode.OffLine };
            Assert.AreEqual(101, p2.PlayerId);
            Assert.AreEqual(ep1, p2.EndPoint);
            Assert.AreEqual(PlayerInfo.StateCode.OffLine, p2.Status);
            
        }

    }
}
