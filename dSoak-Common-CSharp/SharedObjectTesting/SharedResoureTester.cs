using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SharedObjects;

namespace SharedObjectTesting
{
    [TestClass]
    public class SharedResoureTester
    {
        [TestMethod]
        public void SharedResource_CheckEverything()
        {
            SharedResource r1 = new SharedResource();
            Assert.IsTrue(r1.Id>0);
            Assert.IsNotNull(r1.DigitalSignature);
            Assert.IsTrue(r1.DigitalSignature.Length > 0);
            Assert.IsTrue(r1.IsValid);

            SharedResource r2 = new SharedResource();
            Assert.AreEqual(r1.Id + 1, r2.Id);
            Assert.IsNotNull(r2.DigitalSignature);
            Assert.IsTrue(r2.DigitalSignature.Length > 0);
            Assert.IsTrue(r2.IsValid);

            r1.Id = r2.Id;
            Assert.IsFalse(r1.IsValid);

            SharedResource r3 = new SharedResource() { Id = r2.Id, DigitalSignature = r2.DigitalSignature };
            Assert.AreEqual(r2.Id, r3.Id);
            Assert.IsTrue(r3.IsValid);

            Int32 originalNounce = SharedResource.Nounce;
            SharedResource.Nounce = originalNounce + 1;
            Assert.IsFalse(r3.IsValid);

            SharedResource.Nounce = originalNounce;
            Assert.IsTrue(r3.IsValid);
        }
    }
}
