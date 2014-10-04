using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SharedObjects;

namespace SharedObjectTesting
{
    [TestClass]
    public class GamePlayerInfoTester
    {
        [TestMethod]
        public void GamePlayerInfo_TestEverything()
        {
            GamePlayerInfo gp_info = new GamePlayerInfo();
            Assert.AreEqual(0, gp_info.GameId);
            Assert.IsNull(gp_info.Player);
            Assert.AreEqual(GamePlayerInfo.StatusCode.Unknown, gp_info.Status);

            PublicEndPoint ep1 = new PublicEndPoint() { Host="swcwin.serv.usu.edu", Port=35420 };
            PlayerInfo p_info = new PlayerInfo() { EndPoint = ep1, PlayerId = 100, Status = PlayerInfo.StateCode.OnLine };
            gp_info = new GamePlayerInfo() { GameId = 10, Player = p_info, Status = GamePlayerInfo.StatusCode.Winner };
            Assert.AreEqual(10, gp_info.GameId);
            Assert.AreEqual(p_info, gp_info.Player);
            Assert.AreEqual(GamePlayerInfo.StatusCode.Winner, gp_info.Status);

        }

    }
}
