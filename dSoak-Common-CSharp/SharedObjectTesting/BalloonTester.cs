using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SharedObjects;

namespace SharedObjectTesting
{
    [TestClass]
    public class BalloonTester
    {
        [TestMethod]
        public void Balloon_TestEverything()
        {
            Balloon b1 = new Balloon();
            Assert.IsTrue(b1.Id>0);
            Assert.IsTrue(b1.IsValid);
            Assert.AreEqual(0, b1.UnitsOfWater);

            b1.UnitsOfWater = 1;
            Assert.AreEqual(1, b1.UnitsOfWater);
            Assert.IsFalse(b1.IsValid);

        }

    }
}
