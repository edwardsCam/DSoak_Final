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
            Assert.AreEqual(0, g1.FightManagerId);
            Assert.AreEqual(null, g1.FightManagerEP);
            Assert.AreEqual(0, g1.GameId);
            Assert.AreEqual(0, g1.MaxPlayers);
            Assert.AreEqual(GameInfo.StatusCode.NotInitialized, g1.Status);

            PublicEndPoint ep1 = new PublicEndPoint() { Host = "buzz.serv.usu.edu", Port = 20011 };
            GameInfo g2 = new GameInfo() { FightManagerId=2, FightManagerEP = ep1, GameId = 10, Label="Test Game", MaxPlayers = 5, MaxThieves=2, Status = GameInfo.StatusCode.NotInitialized };
            Assert.AreEqual(2, g2.FightManagerId);
            Assert.AreEqual(ep1, g2.FightManagerEP);
            Assert.AreEqual(10, g2.GameId);
            Assert.AreEqual("Test Game", g2.Label);
            Assert.AreEqual(5, g2.MaxPlayers);
            Assert.AreEqual(2, g2.MaxThieves);
            Assert.AreEqual(GameInfo.StatusCode.NotInitialized, g2.Status);

            GameInfo g3 = g2.Copy;
            Assert.IsNotNull(g3);
            Assert.AreNotSame(g2, g3);
            Assert.AreEqual(2, g3.FightManagerId);
            Assert.AreEqual(ep1, g3.FightManagerEP);
            Assert.AreEqual(10, g3.GameId);
            Assert.AreEqual("Test Game", g3.Label);
            Assert.AreEqual(5, g3.MaxPlayers);
            Assert.AreEqual(2, g3.MaxThieves);
            Assert.AreEqual(GameInfo.StatusCode.NotInitialized, g3.Status);
        }

    }
}
