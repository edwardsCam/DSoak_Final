using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SharedObjects;

namespace SharedObjectTesting
{
    [TestClass]
    public class MessageNumberTester
    {
        [TestInitialize]
        public void Setup()
        {
            MessageNumber.LocalProcessId = 100;
        }

        [TestMethod]
        public void MessageNumber_TestEverything()
        {
            MessageNumber mn1 = MessageNumber.Create();
            Assert.AreEqual(100, mn1.ProcessId);
            Assert.IsTrue(mn1.SeqNumber > 0);

            MessageNumber mn2 = MessageNumber.Create();
            Assert.AreEqual(100, mn1.ProcessId);
            Assert.AreEqual(mn1.SeqNumber + 1, mn2.SeqNumber);

            MessageNumber mn3 = new MessageNumber();
            Assert.AreEqual(0, mn3.ProcessId);
            Assert.AreEqual(0, mn3.SeqNumber);

            mn3.ProcessId = mn2.ProcessId;
            mn3.SeqNumber = mn2.SeqNumber;

            Assert.IsTrue(mn2 == mn3);
            Assert.IsTrue(mn1 <= mn3);
            Assert.IsTrue(mn1 < mn3);
            Assert.IsTrue(mn3 != mn1);
            Assert.IsTrue(mn3 >= mn1);
            Assert.IsTrue(mn3 > mn1);

        }

    }
}
