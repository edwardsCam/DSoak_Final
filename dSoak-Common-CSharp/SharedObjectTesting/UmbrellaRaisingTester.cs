using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SharedObjects;

namespace SharedObjectTesting
{
    [TestClass]
    public class UmbrellaRaisingTester
    {
        [TestMethod]
        public void UmbrellaRaising_TestEverything()
        {
            UmbrellaRaising r1 = new UmbrellaRaising();
            Assert.AreEqual(0, r1.GameId);
            Assert.AreEqual(0, r1.PlayerId);
            Assert.AreEqual(0, r1.UmbrellaId);
            Assert.IsNull(r1.AtTime);

            DateTime d2 = DateTime.Now;
            UmbrellaRaising r2 = new UmbrellaRaising() { GameId = 1, PlayerId = 2, UmbrellaId = 3, AtTime = d2 };
            Assert.AreEqual(1, r2.GameId);
            Assert.AreEqual(2, r2.PlayerId);
            Assert.AreEqual(3, r2.UmbrellaId);
            Assert.AreEqual(d2, r2.AtTime);
        }
    }
}
