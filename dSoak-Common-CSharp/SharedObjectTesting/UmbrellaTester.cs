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
    }
}
