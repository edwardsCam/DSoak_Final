using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Messages;

namespace MessagesTest
{
    [TestClass]
    public class NetByteStreamTester
    {
        [TestMethod]
        public void NetByteStream_01_Constructors()
        {
            // Check out the default constructor
            NetByteStream myBytes = new NetByteStream();
            Assert.IsNotNull(myBytes);
            Assert.AreEqual(0, myBytes.Length);

            // Case 1: An empty stream
            myBytes = new NetByteStream();
            Assert.IsNotNull(myBytes);
            Assert.AreEqual(0, myBytes.Length);

            // Case 1: A single boolean object
            myBytes = new NetByteStream(true);
            Assert.IsNotNull(myBytes);
            Assert.AreEqual(1, myBytes.Length);
            Assert.AreEqual(1, myBytes[0]);

            // Case 2: Create a stream with 3 different objects
            myBytes = new NetByteStream(true, 123, "Hello");
            Assert.IsNotNull(myBytes);
            Assert.AreEqual(1 + 4 + (2 + 2 * 5), myBytes.Length);
            Assert.AreEqual(1, myBytes[0]);
            Assert.AreEqual(0, myBytes[1]);
            Assert.AreEqual(0, myBytes[2]);
            Assert.AreEqual(0, myBytes[3]);
            Assert.AreEqual(123, myBytes[4]);
            Assert.AreEqual(0, myBytes[5]);
            Assert.AreEqual(10, myBytes[6]);
            Assert.AreEqual(0, myBytes[7]);
            Assert.AreEqual(72, myBytes[8]);
            Assert.AreEqual(0, myBytes[9]);
            Assert.AreEqual(101, myBytes[10]);
            Assert.AreEqual(0, myBytes[11]);
            Assert.AreEqual(108, myBytes[12]);
            Assert.AreEqual(0, myBytes[13]);
            Assert.AreEqual(108, myBytes[14]);
            Assert.AreEqual(0, myBytes[15]);
            Assert.AreEqual(111, myBytes[16]);

            // Case 3: 3 strings of lengths 5, 5, and 52
            myBytes = new NetByteStream("Hello", "There", "You amazing software developer and brilliant student");
            Assert.IsNotNull(myBytes);
            Assert.AreEqual((2 + 2 * 5) + (2 + 2 * 5) + (2 + 2 * 52), myBytes.Length);

            // Case 4: with a bunch of other parameters types
            NetByteStream moreBytes = new NetByteStream(myBytes,
                                                (Int16) 10,
                                                (Int64) 20,
                                                (Single) 30.0,
                                                (Double) 40.0,
                                                new byte[] { 1, 2, 3 });
            Assert.IsNotNull(moreBytes);
            Assert.AreEqual(myBytes.Length + 2 + 8 + 4 + 8 + 3, moreBytes.Length);

            byte[] bigArray = new byte[10000];
            for (int i = 0; i < 10000; i++)
                bigArray[i] = Convert.ToByte(i & 255);
            NetByteStream bigList = new NetByteStream(bigArray);

            bigList.ResetRead();
            byte[] bigArray2 = bigList.ReadBytes(8192);
            for (int i = 0; i < 8192; i++)
                Assert.AreEqual(bigArray[i], bigArray2[i]);

        }

        [TestMethod]
        public void NetByteStream_02_WriteAndReadMethods()
        {
            NetByteStream myBytes = new NetByteStream();

            // Case: Write out a boolean of True
            myBytes.Clear();
            myBytes.Write(true);
            Assert.IsNotNull(myBytes);
            Assert.AreEqual(1, myBytes.Length);
            Assert.AreEqual(1, myBytes[0]);
            myBytes.ResetRead();
            Assert.AreEqual(true, myBytes.ReadBool());

            // Case: Write out a boolean of False
            myBytes.Clear();
            myBytes.Write(false);
            Assert.IsNotNull(myBytes);
            Assert.AreEqual(1, myBytes.Length);
            Assert.AreEqual(0, myBytes[0]);
            myBytes.ResetRead();
            Assert.AreEqual(false, myBytes.ReadBool());

            // Case: Write out a Byte
            myBytes.Clear();
            myBytes.Write((byte) 4);
            Assert.IsNotNull(myBytes);
            Assert.AreEqual(1, myBytes.Length);
            Assert.AreEqual((byte) 4, myBytes[0]);
            myBytes.ResetRead();
            Assert.AreEqual(4, myBytes.ReadByte());

            // Case: Write out a Char
            myBytes.Clear();
            myBytes.Write('A');
            Assert.IsNotNull(myBytes);
            Assert.AreEqual(2, myBytes.Length);
            Assert.AreEqual(65, myBytes[0]);
            Assert.AreEqual(0, myBytes[1]);
            myBytes.ResetRead();
            Assert.AreEqual('A', myBytes.ReadChar());

            // Case: Write out a Int16
            myBytes.Clear();
            myBytes.Write((Int16)7);
            Assert.IsNotNull(myBytes);
            Assert.AreEqual(2, myBytes.Length);
            Assert.AreEqual(0, myBytes[0]);
            Assert.AreEqual(7, myBytes[1]);
            myBytes.ResetRead();
            Assert.AreEqual(7, myBytes.ReadInt16());

            // Case: Write out Max Int16
            myBytes.Clear();
            myBytes.Write(Int16.MaxValue);
            Assert.IsNotNull(myBytes);
            Assert.AreEqual(2, myBytes.Length);
            Assert.AreEqual(127, myBytes[0]);
            Assert.AreEqual(255, myBytes[1]);
            myBytes.ResetRead();
            Assert.AreEqual(Int16.MaxValue, myBytes.ReadInt16());

            // Case: Write out a Int32
            myBytes.Clear();
            myBytes.Write((Int32)7);
            Assert.IsNotNull(myBytes);
            Assert.AreEqual(4, myBytes.Length);
            for (int i = 0; i < 3; i++) Assert.AreEqual(0, myBytes[i]);
            Assert.AreEqual(7, myBytes[3]);
            myBytes.ResetRead();
            Assert.AreEqual(7, myBytes.ReadInt32());

            // Case: Write out Max Int32
            myBytes.Clear();
            myBytes.Write(Int32.MaxValue);
            Assert.IsNotNull(myBytes);
            Assert.AreEqual(4, myBytes.Length);
            Assert.AreEqual(127, myBytes[0]);
            for (int i = 1; i < 4; i++) Assert.AreEqual(255, myBytes[i]);
            myBytes.ResetRead();
            Assert.AreEqual(Int32.MaxValue, myBytes.ReadInt32());

            // Case: Write out a Int64
            myBytes.Clear();
            myBytes.Write((Int64)7);
            Assert.IsNotNull(myBytes);
            Assert.AreEqual(8, myBytes.Length);
            for (int i = 0; i < 7; i++) Assert.AreEqual(0, myBytes[i]);
            Assert.AreEqual(7, myBytes[7]);
            myBytes.ResetRead();
            Assert.AreEqual(7, myBytes.ReadInt64());

            // Case 7: Write out Max Int64
            myBytes.Clear();
            myBytes.Write(Int64.MaxValue);
            Assert.IsNotNull(myBytes);
            Assert.AreEqual(8, myBytes.Length);
            Assert.AreEqual(127, myBytes[0]);
            for (int i = 1; i < 8; i++) Assert.AreEqual(255, myBytes[i]);
            myBytes.ResetRead();
            Assert.AreEqual( Int64.MaxValue, myBytes.ReadInt64());

            // Case: Write out a Single Precision Real
            myBytes.Clear();
            myBytes.Write((float)7.5);
            Assert.IsNotNull(myBytes);
            Assert.AreEqual(4, myBytes.Length);
            myBytes.ResetRead();
            Assert.AreEqual(7.5, myBytes.ReadFloat());

            // Case: Write out a Double Precision Real
            myBytes.Clear();
            myBytes.Write((Double)7.5);
            Assert.IsNotNull(myBytes);
            Assert.AreEqual(8, myBytes.Length);
            myBytes.ResetRead();
            Assert.AreEqual(7.5, myBytes.ReadDouble());

            // Case: Write out a Byte Array
            myBytes.Clear();
            myBytes.Write(new byte[] { 1, 2, 3, 4, 5, 6 });
            Assert.AreEqual(6, myBytes.Length);
            for (int i = 0; i < 6; i++) Assert.AreEqual(i + 1, myBytes[i]);
            myBytes.ResetRead();
            byte[] tmpBytes = myBytes.ReadBytes(6);
            for (int i = 0; i < 6; i++) Assert.AreEqual(i + 1, tmpBytes[i]);

            // Case: Write out a string
            myBytes.Clear();
            myBytes.Write((string)null);
            Assert.AreEqual(2, myBytes.Length);
            Assert.AreEqual(0, myBytes[0]);
            Assert.AreEqual(0, myBytes[1]);
            myBytes.ResetRead();
            Assert.AreEqual(string.Empty, myBytes.ReadString());

            // Case: Write out a string
            myBytes.Clear();
            myBytes.Write(string.Empty);
            Assert.AreEqual(2, myBytes.Length);
            Assert.AreEqual(0, myBytes[0]);
            Assert.AreEqual(0, myBytes[1]);
            myBytes.ResetRead();
            Assert.AreEqual(string.Empty, myBytes.ReadString());

            // Case 11: Write out a string
            myBytes = new NetByteStream("abc");
            Assert.AreEqual(2 + 2 * 3, myBytes.Length);
            Assert.AreEqual(0, myBytes[0]);
            Assert.AreEqual(2 * 3, myBytes[1]);
            Assert.AreEqual(0, myBytes[2]);
            Assert.AreEqual(97, myBytes[3]);
            Assert.AreEqual(0, myBytes[4]);
            Assert.AreEqual(98, myBytes[5]);
            Assert.AreEqual(0, myBytes[6]);
            Assert.AreEqual(99, myBytes[7]);
            myBytes.ResetRead();
            Assert.AreEqual("abc", myBytes.ReadString());

            // Case 12: Write out a list of objects
            NetByteStream moreMyBytes = new NetByteStream();
            moreMyBytes.WriteObjects(new object[] { myBytes, 123, "good bye" });
            Assert.AreEqual(myBytes.Length + 4 + (2+ 2 * 8), moreMyBytes.Length);
            Assert.AreEqual(0, moreMyBytes[0]);
            Assert.AreEqual(2 * 3, moreMyBytes[1]);
            Assert.AreEqual(0, moreMyBytes[2]);
            Assert.AreEqual(97, moreMyBytes[3]);
            Assert.AreEqual(0, moreMyBytes[4]);
            Assert.AreEqual(98, moreMyBytes[5]);
            Assert.AreEqual(0, moreMyBytes[6]);
            Assert.AreEqual(99, moreMyBytes[7]);
            Assert.AreEqual(0, moreMyBytes[8]);
            Assert.AreEqual(0, moreMyBytes[9]);
            Assert.AreEqual(0, moreMyBytes[10]);
            Assert.AreEqual(123, moreMyBytes[11]);
            Assert.AreEqual(0, moreMyBytes[12]);
            Assert.AreEqual(2 * 8, moreMyBytes[13]);
            Assert.AreEqual(0, moreMyBytes[14]);
            Assert.AreEqual(103, moreMyBytes[15]);
            Assert.AreEqual(0, moreMyBytes[16]);
            Assert.AreEqual(111, moreMyBytes[17]);
            Assert.AreEqual(0, moreMyBytes[18]);
            Assert.AreEqual(111, moreMyBytes[19]);
            Assert.AreEqual(0, moreMyBytes[20]);
            Assert.AreEqual(100, moreMyBytes[21]);
            Assert.AreEqual(0, moreMyBytes[22]);
            Assert.AreEqual(32, moreMyBytes[23]);
            Assert.AreEqual(0, moreMyBytes[24]);
            Assert.AreEqual(98, moreMyBytes[25]);
            Assert.AreEqual(0, moreMyBytes[26]);
            Assert.AreEqual(121, moreMyBytes[27]);
            Assert.AreEqual(0, moreMyBytes[28]);
            Assert.AreEqual(101, moreMyBytes[29]);
            moreMyBytes.ResetRead();
            Assert.AreEqual("abc", moreMyBytes.ReadString());
            Assert.AreEqual(123, moreMyBytes.ReadInt32());
            Assert.AreEqual("good bye", moreMyBytes.ReadString());

        }

        [TestMethod]
        public void NetByteStream_03_OtherPublicMethods()
        {
            byte[] bigArray = new byte[300];
            for (int i = 0; i < 300; i++)
                bigArray[i] = Convert.ToByte(i * 11 & 255);
            NetByteStream bigList = new NetByteStream(bigArray);

            string logString = bigList.CreateLogString();
            Assert.IsNotNull(logString);

            // TODO: Finish
        }

    }
}
