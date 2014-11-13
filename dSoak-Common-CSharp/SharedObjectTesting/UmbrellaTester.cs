using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SharedObjects;

namespace SharedObjectTesting
{
    [TestClass]
    public class UmbrellaTester
    {
        [TestMethod]
        public void Umbrella_TestEverything()
        {
            Umbrella u1 = new Umbrella();
            Assert.IsTrue(u1.Id > 0);
            Assert.IsTrue(u1.IsValid);

            u1.Id++;
            Assert.IsFalse(u1.IsValid);
        }

        [TestMethod]
        public void Umbrella_TestSign()
        {
            Umbrella u1 = new Umbrella();
            u1.Sign();
            Assert.IsTrue(u1.Id > 0);
            Assert.IsTrue(u1.IsValid);

            u1.DigitalSignature = new byte[] { 1, 2, 3 };
            Assert.IsFalse(u1.IsValid);

            u1.Sign();
            Assert.IsTrue(u1.IsValid);
        }

        [TestMethod]
        public void Umbrella_TestCopy()
        {
            Umbrella u1 = new Umbrella();
            u1.Sign();

            Umbrella b2 = u1.Copy;
            Assert.AreNotSame(u1, b2);
            Assert.AreEqual(u1.Id, b2.Id);
            Assert.AreEqual(u1.DigitalSignature.Length, b2.DigitalSignature.Length);
            for (int i = 0; i < u1.DigitalSignature.Length; i++)
                Assert.AreEqual(u1.DigitalSignature[i], b2.DigitalSignature[i]);
        }
    }
}
