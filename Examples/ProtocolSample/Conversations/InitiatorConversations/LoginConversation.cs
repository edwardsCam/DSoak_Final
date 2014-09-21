using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messages;

namespace Conversations.InitiatorConversations
{
    public delegate void ConversationCompleteMethod(bool success);

    public class LoginConverstion
    {
        public event ConversationCompleteMethod Complete;

        public string Username { get; set; }
        public string Password { get; set; }
        public LoginConverstion() { }

        public bool Start()
        {
            bool result = false;

            return result;
        }

        protected void ReceiveDataHandler()
        {
            Message reply = null;
            // Get message object

            bool success = reply is Ack;

            if (Complete != null)
                Complete(success);

        }

    }
}
