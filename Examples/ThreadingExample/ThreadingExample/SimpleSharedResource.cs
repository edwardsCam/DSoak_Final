using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingExample
{
    public class SimpleSharedResource
    {
        private int value = 0;
        private object myLock = new object();

        public void SlowIncrement()
        {
            //lock (myLock)
            {
                Console.WriteLine("Entering SlowIncrement at {0}", DateTime.Now.ToLongTimeString());
                Thread.Sleep(1000);
                value++;
                Console.WriteLine("Exiting SlowIncrement at {0}", DateTime.Now.ToLongTimeString());
            }
        }

        public int Value { get { return value; } }
    }
}
