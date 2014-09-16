using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Messages;

namespace MessagesTest
{
    /// <summary>
    /// Summary description for AckTester
    /// </summary>
    [TestClass]
    public class AckTester
    {
        private const Int16 myProcessId = 10;

        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            MessageNumber.LocalProcessId = myProcessId;
        }

        [TestMethod]
        public void AckMessage_01_Everything()
        {
            //
            // TODO: Add test logic here
            //
        }
    }
}
