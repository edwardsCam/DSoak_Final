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
            Assert.AreEqual(null, g1.FightManagerEP);
            Assert.AreEqual(0, g1.GameId);
            Assert.AreEqual(0, g1.MaxPlayers);
            Assert.AreEqual(GameInfo.StatusCode.NotInitialized, g1.Status);

            PublicEndPoint ep1 = new PublicEndPoint() { Host = "buzz.serv.usu.edu", Port = 20011 };
            GameInfo g2 = new GameInfo() { FightManagerEP = ep1, GameId = 10, MaxPlayers = 5, Status = GameInfo.StatusCode.NotInitialized };
            Assert.AreEqual(ep1, g2.FightManagerEP);
            Assert.AreEqual(10, g2.GameId);
            Assert.AreEqual(5, g2.MaxPlayers);
            Assert.AreEqual(GameInfo.StatusCode.NotInitialized, g2.Status);

        }

    }
}
