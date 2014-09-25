using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using ThreadingExample;

namespace MyTester
{
    [TestClass]
    public class SimpleSharedResourceTester
    {
        private SimpleSharedResource resource = new SimpleSharedResource();

        [TestMethod]
        public void TestMethod1()
        {

            Console.WriteLine("Start");
            Thread anotherThread = new Thread(AnotherThread);
            anotherThread.Start();

            resource.SlowIncrement();
            Console.WriteLine("Current Value {0}", resource.Value);

            Thread.Sleep(3000);
            Console.WriteLine("Current Value {0}", resource.Value);
        }

        private void AnotherThread()
        {
            resource.SlowIncrement();
            Console.WriteLine("Current Value {0}", resource.Value);
        }
    }
}
