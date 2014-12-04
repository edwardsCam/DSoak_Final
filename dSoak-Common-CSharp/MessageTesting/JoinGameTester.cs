using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Messages;
using SharedObjects;

namespace MessageTesting
{
    [TestClass]
    public class JoinGameTester
    {
        [TestInitialize]
        public void Setup()
        {
            MessageNumber.LocalProcessId = 100;
        }

        [TestMethod]
        public void JoinGame_CheckEverything()
        {
            JoinGame msg1 = new JoinGame();
            Assert.IsNotNull(msg1.MessageNr);
            Assert.AreEqual(100, msg1.MessageNr.ProcessId);
            Assert.IsTrue(msg1.MessageNr.SeqNumber > 0);
            Assert.AreEqual(msg1.MessageNr, msg1.ConvId);

            PlayerInfo player = new PlayerInfo() { PlayerId = 10, Status = PlayerInfo.StateCode.OnLine };
            JoinGame msg2 = new JoinGame() { GameId = 123, Player = player };
            Assert.IsNotNull(msg2.MessageNr);
            Assert.AreEqual(100, msg2.MessageNr.ProcessId);
            Assert.AreEqual(msg1.MessageNr.SeqNumber + 1, msg2.MessageNr.SeqNumber);
            Assert.AreEqual(msg2.MessageNr, msg2.ConvId);
            Assert.AreEqual(123, msg2.GameId);
            Assert.IsNotNull(msg2.Player);
            Assert.AreSame(player, msg2.Player);

            byte[] bytes = msg2.Encode();
            string tmp = Encoding.ASCII.GetString(bytes);

            Message msg3 = Message.Decode(bytes);
            Assert.IsTrue(msg3 is JoinGame);
            JoinGame msg4 = msg3 as JoinGame;
            Assert.AreEqual(msg2.MessageNr, msg4.MessageNr);
            Assert.AreEqual(msg2.ConvId, msg4.ConvId);
            Assert.AreEqual(msg2.GameId, msg4.GameId);
            Assert.IsNotNull(msg4.Player);
            Assert.AreEqual(msg2.Player.PlayerId, msg4.Player.PlayerId);
            Assert.AreEqual(msg2.Player.Status, msg4.Player.Status);
        }
    }
}
