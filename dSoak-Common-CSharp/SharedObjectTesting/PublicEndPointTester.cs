using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SharedObjects;

namespace SharedObjectTesting
{
    [TestClass]
    public class PublicEndPointTester
    {
        [TestMethod]
        public void PublicEndPoint_TestEverything()
        {
            PublicEndPoint ep1 = new PublicEndPoint();
            Assert.IsNull(ep1.Host);
            Assert.AreEqual(0, ep1.Port);
            Assert.IsNull(ep1.IPEndPoint);

            PublicEndPoint ep2 = new PublicEndPoint() { Host = "swcwin.serv.usu.edu", Port = 12001 };
            Assert.AreEqual("swcwin.serv.usu.edu", ep2.Host);
            Assert.AreEqual(12001, ep2.Port);
            Assert.AreEqual("129.123.41.13:12001", ep2.IPEndPoint.ToString());
        }
    }
}
