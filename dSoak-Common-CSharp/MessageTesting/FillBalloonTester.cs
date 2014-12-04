using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Messages;
using SharedObjects;

namespace MessageTesting
{
    [TestClass]
    public class FillBalloonTester
    {
        [TestInitialize]
        public void Setup()
        {
            MessageNumber.LocalProcessId = 100;
        }

        [TestMethod]
        public void FillBalloon_CheckEverything()
        {
            FillBalloon msg1 = new FillBalloon();
            Assert.IsNotNull(msg1.MessageNr);
            Assert.AreEqual(100, msg1.MessageNr.ProcessId);
            Assert.IsTrue(msg1.MessageNr.SeqNumber > 0);
            Assert.AreEqual(msg1.MessageNr, msg1.ConvId);

            List<Penny> pennies = new List<Penny> { new Penny(), new Penny(), new Penny() };
            FillBalloon msg2 = new FillBalloon() { Pennies = pennies };
            Assert.IsNotNull(msg2.MessageNr);
            Assert.AreEqual(100, msg2.MessageNr.ProcessId);
            Assert.AreEqual(msg1.MessageNr.SeqNumber + 1, msg2.MessageNr.SeqNumber);
            Assert.AreEqual(msg2.MessageNr, msg2.ConvId);
            Assert.IsNotNull(msg2.Pennies);
            Assert.AreSame(pennies, msg2.Pennies);

            byte[] bytes = msg2.Encode();
            string tmp = Encoding.ASCII.GetString(bytes);

            Message msg3 = Message.Decode(bytes);
            Assert.IsTrue(msg3 is FillBalloon);
            FillBalloon msg4 = msg3 as FillBalloon;
            Assert.AreEqual(msg2.MessageNr, msg4.MessageNr);
            Assert.AreEqual(msg2.ConvId, msg4.ConvId);
            Assert.AreEqual(msg2.Pennies.Count, msg4.Pennies.Count);
        }
    }
}
