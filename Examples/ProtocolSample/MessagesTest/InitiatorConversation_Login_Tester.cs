using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Conversations.InitiatorConversations;

namespace MessagesTest
{
    [TestClass]
    class InitiatorConversation_Login_Tester
    {
        [TestMethod]
        public void InitiatorConversation_Login_01_CheckEverything()
        {
            LoginConverstion conv = new LoginConverstion()
                        {
                            Password = "Test",
                            Username = "Sue"
                        };

            conv.Complete += conv_Complete;

            conv.Start();
            
            // TODO: Finish
        }

        void conv_Complete(bool success)
        {
            
        }
            
    }
}
