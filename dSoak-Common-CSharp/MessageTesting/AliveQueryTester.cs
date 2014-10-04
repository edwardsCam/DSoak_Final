using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Messages;
using SharedObjects;

namespace MessageTesting
{
    /// <summary>
    /// Summary description for AliveQueryTester
    /// </summary>
    [TestClass]
    public class AliveQueryTester
    {
        [TestInitialize]
        public void Setup()
        {
            MessageNumber.LocalProcessId = 100;
        }

        [TestMethod]
        public void AliveQuery_CheckEverything()
        {
            AliveQuery msg1 = new AliveQuery();
            Assert.IsNotNull(msg1.MessageNr);
            Assert.AreEqual(100, msg1.MessageNr.ProcessId);
            Assert.IsTrue(msg1.MessageNr.SeqNumber > 0);
            Assert.AreEqual(msg1.MessageNr, msg1.ConvId);

            AliveQuery msg2 = new AliveQuery() { ConvId = msg1.ConvId };
            Assert.IsNotNull(msg2.MessageNr);
            Assert.AreEqual(100, msg2.MessageNr.ProcessId);
            Assert.AreEqual(msg1.MessageNr.SeqNumber + 1, msg2.MessageNr.SeqNumber);
            Assert.AreEqual(msg1.ConvId, msg2.ConvId);

            byte[] bytes = msg2.Encode();
            string tmp = Encoding.ASCII.GetString(bytes);

            Message msg3 = Message.Decode(bytes);
            Assert.IsTrue(msg3 is AliveQuery);
            AliveQuery msg4 = msg3 as AliveQuery;
            Assert.AreEqual(msg4.MessageNr, msg4.MessageNr);
            Assert.AreEqual(msg2.ConvId, msg4.ConvId);

        }
    }
}
