using System;
using System.Collections.Generic;
using System.Text;

namespace Messages
{
    abstract public class Message : IComparable
    {
        #region Private Properties
        protected enum MessageType { LOGIN, ACK, NAK }
        #endregion

        #region Public Properties

        public MessageNumber MessageNr { get; set; }
        public MessageNumber ConversationId { get; set; }
        public MessageType MyMessageType { get; set; }

        #endregion

        #region Constructors and Factory Methods
        /// <summary>
        /// Default Constructed called by the Request and Reply constructors used by the Senders.
        /// Note how this construct creates a new message number and set the conversation Id to
        /// the message number.  This is the expected behavior for an initial messsage in a conversation.
        /// </summary>
        protected Message(bool isForSending, bool isFirstMessage)
        {
            if (isForSending)
            {
                MessageNr = MessageNumber.Create();
                if (isFirstMessage)
                    ConversationId = MessageNr;
            }
        }
        protected Message(bool isForSending) : this(isForSending, false) {}
        protected Message() : this(false, false) { }

        /// <summary>
        /// Factor method to create a message from a byte list
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns>A new message of the right specialization</returns>
        public static Message Create(NetByteStream bytes)
        {
            Message result = null;

            byte tmpMessageType = bytes.ReadByte();
            MessageType messageType = (MessageType) tmpMessageType;

            switch (messageType)
            {
                case MessageType.LOGIN:
                    // result = Login.Create();
                    break;
                case MessageType.ACK:
                    // result = Ack.Create();
                    break;
                case MessageType.NAK:
                    // result = Nak.Create();
                    break;
            }
            // TODO: Decode byte stream into new message

            return result;
        }
        #endregion

        #region Encoding and Decoding methods

        /// <summary>
        /// This method encodes
        /// </summary>
        /// <param name="bytes"></param>
        virtual public void Encode(NetByteStream bytes)
        {
            // TODO: Implement
        }

        /// <summary>
        /// This method decodes a message from a byte list
        /// </summary>
        /// <param name="bytes"></param>
        virtual protected void Decode(NetByteStream bytes)
        {
            // TODO: Implement
        }

        #endregion

        #region Comparison Methods and Operators
        public static int Compare(Message a, Message b)
        {
            int result = 0;

            if (!System.Object.ReferenceEquals(a, b))
            {
                if (((object)a == null) && ((object)b != null))
                    result = -1;
                else if (((object)a != null) && ((object)b == null))
                    result = 1;
                else if (a.MessageNr < b.MessageNr)
                    result = -1;
                else if (a.MessageNr > b.MessageNr)
                    result = 1;
            }
            return result;
        }

        public static bool operator ==(Message a, Message b)
        {
            return (Compare(a, b) == 0);
        }

        public static bool operator !=(Message a, Message b)
        {
            return (Compare(a, b) != 0);
        }

        public static bool operator <(Message a, Message b)
        {
            return (Compare(a, b) < 0);
        }

        public static bool operator >(Message a, Message b)
        {
            return (Compare(a, b) > 0);
        }

        public static bool operator <=(Message a, Message b)
        {
            return (Compare(a, b) <= 0);
        }

        public static bool operator >=(Message a, Message b)
        {
            return (Compare(a, b) >= 0);
        }
        #endregion

        #region IComparable Members

        public int CompareTo(object obj)
        {
            return Compare(this, obj as Message);
        }

        public override bool Equals(object obj)
        {
            return (Compare(this, obj as Message)==0);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
