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

    }
}
