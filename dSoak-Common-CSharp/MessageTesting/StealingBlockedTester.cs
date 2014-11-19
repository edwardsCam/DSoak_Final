using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Messages;
using SharedObjects;

namespace MessageTesting
{
    [TestClass]
    public class StealingBlockedTester
    {
        [TestInitialize]
        public void Setup()
        {
            MessageNumber.LocalProcessId = 100;
        }

        [TestMethod]
        public void StealingBlocked_TestEverything()
        {
            StealingBlocked msg1 = new StealingBlocked();
            Assert.IsNotNull(msg1.MessageNr);
            Assert.AreEqual(100, msg1.MessageNr.ProcessId);
            Assert.IsTrue(msg1.MessageNr.SeqNumber > 0);
            Assert.AreEqual(msg1.MessageNr, msg1.ConvId);
            Assert.AreEqual(0, msg1.GameId);
            Assert.AreEqual(0, msg1.TargetProcessId);
            Assert.AreEqual(0, msg1.ThiefId);

            StealingBlocked msg2 = new StealingBlocked() { GameId = 11, TargetProcessId = 12, ThiefId = 13 };
            Assert.IsNotNull(msg2.MessageNr);
            Assert.AreEqual(100, msg2.MessageNr.ProcessId);
            Assert.AreEqual(msg1.MessageNr.SeqNumber + 1, msg2.MessageNr.SeqNumber);
            Assert.AreEqual(msg2.MessageNr, msg2.ConvId);
            Assert.AreEqual(11, msg2.GameId);
            Assert.AreEqual(12, msg2.TargetProcessId);
            Assert.AreEqual(13, msg2.ThiefId);

            byte[] bytes = msg2.Encode();
            string tmp = Encoding.ASCII.GetString(bytes);

            Message msg3 = Message.Decode(bytes);
            Assert.IsTrue(msg3 is StealingBlocked);
            StealingBlocked msg4 = msg3 as StealingBlocked;
            Assert.AreEqual(msg2.MessageNr, msg4.MessageNr);
            Assert.AreEqual(msg2.ConvId, msg4.ConvId);
            Assert.AreEqual(msg2.GameId, msg4.GameId);
            Assert.AreEqual(msg2.TargetProcessId, msg4.TargetProcessId);
            Assert.AreEqual(msg2.ThiefId, msg4.ThiefId);
        }
    }
}
