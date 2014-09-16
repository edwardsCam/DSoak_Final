using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Messages;

namespace MessagesTest
{
    [TestClass]
    public class LoginMessageTester
    {
        private const Int16 myProcessId = 10;
 
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            MessageNumber.LocalProcessId = myProcessId;
        }

        [TestMethod]
        public void LoginMessageTester_01_Everything()
        {
            // Case 1: Create a login message for a sender
            Login msg1 = Login.Create("Joe", "Francisco");
            Assert.IsNotNull(msg1.MessageNr);
            Assert.AreEqual(10, msg1.MessageNr.ProcessId);
            Assert.IsTrue(msg1.MessageNr.SeqNumber > 0);
            Assert.AreEqual(msg1.MessageNr, msg1.ConversationId);
            Assert.AreEqual("Joe", msg1.Username);
            Assert.AreEqual("Francisco", msg1.Password);

            // Case 2: Create a login message from a NetByteStream
            NetByteStream stream = new NetByteStream(new byte[] { 101, 1, 0, 11, 0, 4, 1, 0, 12, 0, 5, 0, 6, 0, (byte) 'J', 0, (byte) 'o', 0, (byte) 'e',
                                            0, 18, 0, (byte) 'F', 0, (byte) 'r', 0, (byte) 'a', 0, (byte) 'n', 0, (byte) 'c',  0, (byte) 'i',
                                            0, (byte) 's', 0, (byte) 'c', 0, (byte) 'o'});
            stream.ResetRead();

            Login msg2 = Login.Create(stream);
            Assert.IsNotNull(msg2.MessageNr);
            Assert.AreEqual(11, msg2.MessageNr.ProcessId);
            Assert.AreEqual(4, msg2.MessageNr.SeqNumber);
            Assert.AreEqual(12, msg2.ConversationId.ProcessId);
            Assert.AreEqual(5, msg2.ConversationId.SeqNumber);
            Assert.AreEqual("Joe", msg2.Username);
            Assert.AreEqual("Francisco", msg2.Password);

            stream.Clear();
            msg2.Encode(stream);
            stream.ResetRead();

            Login msg3 = Login.Create(stream);
            Assert.IsNotNull(msg3.MessageNr);
            Assert.AreEqual(11, msg3.MessageNr.ProcessId);
            Assert.AreEqual(4, msg3.MessageNr.SeqNumber);
            Assert.AreEqual(12, msg3.ConversationId.ProcessId);
            Assert.AreEqual(5, msg3.ConversationId.SeqNumber);
            Assert.AreEqual("Joe", msg3.Username);
            Assert.AreEqual("Francisco", msg3.Password);

        }
    }
}
