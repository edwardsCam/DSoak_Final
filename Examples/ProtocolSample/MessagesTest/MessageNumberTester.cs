using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Messages;

namespace MessagesTest
{
    [TestClass]
    public class MessageNumberTester
    {
        private const Int16 myProcessId = 10;
 
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            MessageNumber.LocalProcessId = myProcessId;
        }

        [TestMethod]
        public void MessageNumber_01_Everything()
        {
            // Case 1: Create a new message for sending out
            MessageNumber mn1 = MessageNumber.Create();
            Assert.IsNotNull(mn1);
            Assert.AreEqual(10, mn1.ProcessId);
            Assert.IsTrue(mn1.SeqNumber > 0);

            // Case 2: Create another message for sending out
            MessageNumber mn2 = MessageNumber.Create();
            Assert.IsNotNull(mn2);
            Assert.AreEqual(10, mn2.ProcessId);
            Assert.AreEqual(mn1.SeqNumber + 1, mn2.SeqNumber);

            // Case 3: Create another a bunch of message number, until the seqNumber is about to role over
            int lastNumber = mn2.SeqNumber;
            for (int i=0; i<Int16.MaxValue-lastNumber; i++) mn2 = MessageNumber.Create();
            Assert.IsNotNull(mn2);
            Assert.AreEqual(10, mn2.ProcessId);
            Assert.AreEqual(Int16.MaxValue, mn2.SeqNumber);

            // Case 4: Create one more message number and the seqNumber should rollover to 1.
            mn2 = MessageNumber.Create();
            Assert.IsNotNull(mn2);
            Assert.AreEqual(10, mn2.ProcessId);
            Assert.AreEqual(1, mn2.SeqNumber);

            // Case 5: Create a message from a stream
            NetByteStream stream = new NetByteStream();
            mn2.Encode(stream);
            stream.ResetRead();

            MessageNumber mn3 = MessageNumber.Create(stream);
            Assert.AreEqual(mn2.ProcessId, mn3.ProcessId);
            Assert.AreEqual(mn2.SeqNumber, mn3.SeqNumber);

            // Casde 6: Try to create a message from null stream
            stream = null;
            try
            {
                mn3 = MessageNumber.Create(stream);
                Assert.Fail("Exception should have been thrown");
            }
            catch (ApplicationException)
            {
            }

            // Casde 7: Try to create a message from an empty stream
            stream = new NetByteStream();
            try
            {
                mn3 = MessageNumber.Create(stream);
                Assert.Fail("Exception should have been thrown");
            }
            catch (ApplicationException)
            {
            }

            // Case 8: Try to create a message from a bad stream
            stream = new NetByteStream(new byte[] { 1, 2, 3, 4, 5, 6, 7 });
            try
            {
                mn3 = MessageNumber.Create(stream);
                Assert.Fail("Exception should have been thrown");
            }
            catch (ApplicationException)
            {
            }

            // Case 9: Try chaning the process id and sequence number
            MessageNumber mn4 = MessageNumber.Create();
            Assert.IsNotNull(mn4);
            mn4.ProcessId = 1234;
            mn4.SeqNumber = 5678;
            Assert.AreEqual(1234, mn4.ProcessId);
            Assert.AreEqual(5678, mn4.SeqNumber);

            stream.Clear();
            mn4.Encode(stream);
            stream.ResetRead();

            MessageNumber mn5 = MessageNumber.Create(stream);
            Assert.IsNotNull(mn5);
            Assert.AreEqual(1234, mn5.ProcessId);
            Assert.AreEqual(5678, mn5.SeqNumber);

        }
    }
}
