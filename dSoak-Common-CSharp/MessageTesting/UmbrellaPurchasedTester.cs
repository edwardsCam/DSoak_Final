using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Messages;
using SharedObjects;

namespace MessageTesting
{
    [TestClass]
    public class UmbrellaPurchasedTester
    {
        [TestInitialize]
        public void Setup()
        {
            MessageNumber.LocalProcessId = 100;
        }

        [TestMethod]
        public void UmbrellaPurchased_CheckEverything()
        {
            UmbrellaPurchased msg1 = new UmbrellaPurchased();
            Assert.IsNotNull(msg1.MessageNr);
            Assert.AreEqual(100, msg1.MessageNr.ProcessId);
            Assert.IsTrue(msg1.MessageNr.SeqNumber > 0);
            Assert.AreEqual(msg1.MessageNr, msg1.ConvId);

            Umbrella u = new Umbrella();
            UmbrellaPurchased msg2 = new UmbrellaPurchased() { ConvId = msg1.ConvId, Umbrella = u };
            Assert.IsNotNull(msg2.MessageNr);
            Assert.AreEqual(100, msg2.MessageNr.ProcessId);
            Assert.AreEqual(msg1.MessageNr.SeqNumber + 1, msg2.MessageNr.SeqNumber);
            Assert.AreEqual(msg1.ConvId, msg2.ConvId);
            Assert.IsNotNull(msg2.Umbrella);
            Assert.AreSame(u, msg2.Umbrella);

            byte[] bytes = msg2.Encode();
            string tmp = Encoding.ASCII.GetString(bytes);

            Message msg3 = Message.Decode(bytes);
            Assert.IsTrue(msg3 is UmbrellaPurchased);
            UmbrellaPurchased msg4 = msg3 as UmbrellaPurchased;
            Assert.AreEqual(msg2.MessageNr, msg4.MessageNr);
            Assert.AreEqual(msg2.ConvId, msg4.ConvId);
            Assert.IsNotNull(msg4.Umbrella);
            Assert.AreEqual(msg2.Umbrella.Id, msg4.Umbrella.Id);

        }
    }
}
