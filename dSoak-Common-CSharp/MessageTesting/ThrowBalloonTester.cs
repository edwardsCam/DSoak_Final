using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Messages;
using SharedObjects;

namespace MessageTesting
{
    [TestClass]
    public class ThrowBalloonTester
    {
        [TestInitialize]
        public void Setup()
        {
            MessageNumber.LocalProcessId = 100;
        }

        [TestMethod]
        public void ThrowBalloon_CheckEveything()
        {
            ThrowBalloon msg1 = new ThrowBalloon();
            Assert.IsNotNull(msg1.MessageNr);
            Assert.AreEqual(100, msg1.MessageNr.ProcessId);
            Assert.IsTrue(msg1.MessageNr.SeqNumber > 0);
            Assert.AreEqual(msg1.MessageNr, msg1.ConvId);

            Balloon b = new Balloon() { Id = 245 };
            ThrowBalloon msg2 = new ThrowBalloon() { GameId =123, Balloon = b, TargetPlayerId = 352 };
            Assert.IsNotNull(msg2.MessageNr);
            Assert.AreEqual(100, msg2.MessageNr.ProcessId);
            Assert.AreEqual(msg1.MessageNr.SeqNumber + 1, msg2.MessageNr.SeqNumber);
            Assert.AreEqual(msg2.MessageNr, msg2.ConvId);
            Assert.AreEqual(123, msg2.GameId);
            Assert.IsNotNull(msg2.Balloon);
            Assert.AreEqual(352, msg2.TargetPlayerId);

            byte[] bytes = msg2.Encode();
            string tmp = Encoding.ASCII.GetString(bytes);

            Message msg3 = Message.Decode(bytes);
            Assert.IsTrue(msg3 is ThrowBalloon);
            ThrowBalloon msg4 = msg3 as ThrowBalloon;
            Assert.AreEqual(msg2.MessageNr, msg4.MessageNr);
            Assert.AreEqual(msg2.ConvId, msg4.ConvId);
            Assert.AreEqual(msg2.GameId, msg4.GameId);
            Assert.IsNotNull(msg4.Balloon);
            Assert.AreEqual(msg2.Balloon.Id, msg4.Balloon.Id);
            Assert.AreEqual(msg2.TargetPlayerId, msg4.TargetPlayerId);
        }
    }
}
