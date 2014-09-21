using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Sockets;

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

        [TestMethod]
        public void LoginMessage_02_CheckSendingAndReceiving()
        {
            Login msg1 = Login.Create("Frank", "Jones");

            UdpClient comm1 = new UdpClient(0);
            UdpClient comm2 = new UdpClient(0);

            NetByteStream stream1 = new NetByteStream();
            msg1.Encode(stream1);

            byte[] bytesToSend = stream1.ToBytes();
            IPEndPoint targetEp = new IPEndPoint(IPAddress.Loopback, (comm2.Client.LocalEndPoint as IPEndPoint).Port);

            comm1.Send(bytesToSend, bytesToSend.Length, targetEp);

            IPEndPoint senderEP = new IPEndPoint(IPAddress.Any, 0);
            byte[] bytesReceived = comm2.Receive(ref senderEP);

            NetByteStream stream2 = new NetByteStream(bytesReceived);
            stream2.ResetRead();
            Message msg2 = Message.Create(stream2);
            Assert.IsTrue(msg2 is Login);
            Login msg3 = msg2 as Login;
            Assert.AreEqual("Frank", msg3.Username);
            Assert.AreEqual("Jones", msg3.Password);


        }
    }
}
