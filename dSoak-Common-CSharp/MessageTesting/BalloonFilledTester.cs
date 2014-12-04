using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Messages;
using SharedObjects;

namespace MessageTesting
{
    [TestClass]
    public class BalloonFilledTester
    {
        [TestInitialize]
        public void Setup()
        {
            MessageNumber.LocalProcessId = 100;
        }

        [TestMethod]
        public void BalloonFilled_CheckEverything()
        {
            BalloonFilled msg1 = new BalloonFilled();
            Assert.IsNotNull(msg1.MessageNr);
            Assert.AreEqual(100, msg1.MessageNr.ProcessId);
            Assert.IsTrue(msg1.MessageNr.SeqNumber > 0);
            Assert.AreEqual(msg1.MessageNr, msg1.ConvId);

            Balloon b = new Balloon();
            BalloonFilled msg2 = new BalloonFilled() { ConvId = msg1.ConvId, Balloon=b };
            Assert.IsNotNull(msg2.MessageNr);
            Assert.AreEqual(100, msg2.MessageNr.ProcessId);
            Assert.AreEqual(msg1.MessageNr.SeqNumber + 1, msg2.MessageNr.SeqNumber);
            Assert.AreEqual(msg1.ConvId, msg2.ConvId);
            Assert.IsNotNull(msg2.Balloon);
            Assert.AreSame(b, msg2.Balloon);

            byte[] bytes = msg2.Encode();
            string tmp = Encoding.ASCII.GetString(bytes);

            Message msg3 = Message.Decode(bytes);
            Assert.IsTrue(msg3 is BalloonFilled);
            BalloonFilled msg4 = msg3 as BalloonFilled;
            Assert.AreEqual(msg2.MessageNr, msg4.MessageNr);
            Assert.AreEqual(msg2.ConvId, msg4.ConvId);
            Assert.AreEqual(msg2.Balloon.Id, msg4.Balloon.Id);

        }
    }
}
