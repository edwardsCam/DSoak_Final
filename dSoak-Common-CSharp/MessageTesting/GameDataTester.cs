using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Messages;
using SharedObjects;

namespace MessageTesting
{
    [TestClass]
    public class GameDataTester
    {
        [TestInitialize]
        public void Setup()
        {
            MessageNumber.LocalProcessId = 100;
        }

        [TestMethod]
        public void GameData_CheckEverything()
        {
            GameData msg1 = new GameData();
            Assert.IsNotNull(msg1.MessageNr);
            Assert.AreEqual(100, msg1.MessageNr.ProcessId);
            Assert.IsTrue(msg1.MessageNr.SeqNumber > 0);
            Assert.AreEqual(msg1.MessageNr, msg1.ConvId);

            GameInfo gameInfo = new GameInfo() { GameId = 102, Status = GameInfo.StatusCode.NotInitialized };
            List<PlayerInfo> players = new List<PlayerInfo>() { new PlayerInfo() { PlayerId = 10 }, new PlayerInfo() { PlayerId = 20 }, new PlayerInfo() { PlayerId = 30 } };
            GameData msg2 = new GameData() { Info = gameInfo, Players = players };
            Assert.IsNotNull(msg2.MessageNr);
            Assert.AreEqual(100, msg2.MessageNr.ProcessId);
            Assert.AreEqual(msg1.MessageNr.SeqNumber + 1, msg2.MessageNr.SeqNumber);
            Assert.AreEqual(msg2.MessageNr, msg2.ConvId);
            Assert.IsNotNull(msg2.Info);
            Assert.AreSame(gameInfo, msg2.Info);
            Assert.IsNotNull(msg2.Players);
            Assert.AreSame(players, msg2.Players);

            byte[] bytes = msg2.Encode();
            string tmp = Encoding.ASCII.GetString(bytes);

            Message msg3 = Message.Decode(bytes);
            Assert.IsTrue(msg3 is GameData);
            GameData msg4 = msg3 as GameData;
            Assert.AreEqual(msg2.MessageNr, msg4.MessageNr);
            Assert.AreEqual(msg2.ConvId, msg4.ConvId);
            Assert.IsNotNull(msg4.Info);
            Assert.AreEqual(msg2.Info.GameId, msg4.Info.GameId);
            Assert.AreEqual(msg2.Info.Status, msg4.Info.Status);
            Assert.IsNotNull(msg4.Players);
            Assert.AreEqual(msg2.Players.Count, msg4.Players.Count);
        }
    }
}
