using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SharedObjects;


namespace SharedObjectTesting
{
    [TestClass]
    public class GameInfoTester
    {
        [TestMethod]
        public void GameInfo_TestEverything()
        {
            GameInfo g1 = new GameInfo();
            Assert.AreEqual(null, g1.FlightManagerEP);
            Assert.AreEqual(0, g1.GameId);
            Assert.AreEqual(0, g1.MaxPlayers);
            Assert.AreEqual(GameInfo.StatusCode.Unknown, g1.Status);

            PublicEndPoint ep1 = new PublicEndPoint() { Host = "buzz.serv.usu.edu", Port = 20011 };
            GameInfo g2 = new GameInfo() { FlightManagerEP = ep1, GameId = 10, MaxPlayers = 5, Status = GameInfo.StatusCode.Avaliable };
            Assert.AreEqual(ep1, g2.FlightManagerEP);
            Assert.AreEqual(10, g2.GameId);
            Assert.AreEqual(5, g2.MaxPlayers);
            Assert.AreEqual(GameInfo.StatusCode.Avaliable, g2.Status);

        }

    }
}
