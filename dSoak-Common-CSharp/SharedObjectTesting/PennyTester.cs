using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SharedObjects;

namespace SharedObjectTesting
{
    [TestClass]
    public class PennyTester
    {
        [TestMethod]
        public void Penny_TestEverything()
        {
            Penny p1 = new Penny();
            Assert.IsTrue(p1.Id > 0);
            Assert.IsTrue(p1.IsValid);

            p1.Id++;
            Assert.IsFalse(p1.IsValid);
        }

        [TestMethod]
        public void Penny_TestSign()
        {
            Penny p1 = new Penny();
            p1.Sign();
            Assert.IsTrue(p1.Id > 0);
            Assert.IsTrue(p1.IsValid);

            p1.DigitalSignature = new byte[] { 1, 2, 3 };
            Assert.IsFalse(p1.IsValid);

            p1.Sign();
            Assert.IsTrue(p1.IsValid);
        }

        [TestMethod]
        public void Penny_TestCopy()
        {
            Penny p1 = new Penny();
            p1.Sign();

            Penny b2 = p1.Copy;
            Assert.AreNotSame(p1, b2);
            Assert.AreEqual(p1.Id, b2.Id);
            Assert.AreEqual(p1.DigitalSignature.Length, b2.DigitalSignature.Length);
            for (int i = 0; i < p1.DigitalSignature.Length; i++)
                Assert.AreEqual(p1.DigitalSignature[i], b2.DigitalSignature[i]);
        }

    }
}
