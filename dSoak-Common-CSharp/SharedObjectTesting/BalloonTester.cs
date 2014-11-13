using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SharedObjects;

namespace SharedObjectTesting
{
    [TestClass]
    public class BalloonTester
    {
        [TestMethod]
        public void Balloon_TestConstructor()
        {
            Balloon b1 = new Balloon();
            Assert.IsTrue(b1.Id>0);
            Assert.IsTrue(b1.IsValid);
            Assert.AreEqual(0, b1.UnitsOfWater);

            b1.UnitsOfWater = 1;
            Assert.AreEqual(1, b1.UnitsOfWater);
            Assert.IsFalse(b1.IsValid);
        }

        [TestMethod]
        public void Balloon_TestSign()
        {
            Balloon b1 = new Balloon() { UnitsOfWater = 3 };
            b1.Sign();
            Assert.IsTrue(b1.Id > 0);
            Assert.IsTrue(b1.IsValid);
            Assert.AreEqual(3, b1.UnitsOfWater);

            b1.UnitsOfWater = 10;
            Assert.IsFalse(b1.IsValid);

            b1.Sign();
            Assert.IsTrue(b1.IsValid);
        }

        [TestMethod]
        public void Balloon_TestCopy()
        {
            Balloon b1 = new Balloon() { UnitsOfWater = 5 };
            b1.Sign();

            Balloon b2 = b1.Copy;
            Assert.AreNotSame(b1, b2);
            Assert.AreEqual(b1.Id, b2.Id);
            Assert.AreEqual(b1.DigitalSignature.Length, b2.DigitalSignature.Length);
            for (int i = 0; i < b1.DigitalSignature.Length; i++)
                Assert.AreEqual(b1.DigitalSignature[i], b2.DigitalSignature[i]);
            Assert.AreEqual(b1.UnitsOfWater, b2.UnitsOfWater);
        }
    }
}
