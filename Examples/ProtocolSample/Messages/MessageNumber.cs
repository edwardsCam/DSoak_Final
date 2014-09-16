using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Messages
{
    public class MessageNumber :  IComparable
    {
        #region Private Properties
        private static Int16 nextSeqNumber = 1;                     // Start with message #1
        #endregion

        #region Public Properties
        public static MessageNumber Empty { get { return new MessageNumber(); } }
        public static Int16 LocalProcessId { get; set; }            // Local process Id -- set once when the
                                                                    // process joins the distributed application
        public Int16 ProcessId { get; set; }
        public Int16 SeqNumber { get; set; }

        #endregion

        #region Constructors and Factories
        /// <summary>
        /// Factory method creates and new, unique message number.
        /// </summary>
        /// <returns>A new message number</returns>
        public static MessageNumber Create()
        {
            MessageNumber result = new MessageNumber();
            result.ProcessId = LocalProcessId;
            result.SeqNumber = GetNextSeqNumber();
            return result;
        }

        /// <summary>
        /// Factory Method that creates a message from a byte string
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static MessageNumber Create(NetByteStream stream)
        {
            MessageNumber result = null;

            // TODO: Decode the stream into a message number

            return result;
        }

        /// <summary>
        /// Factory method that creates and message number from an existing
        /// processId and seqNumber.  This will be used for testing.
        /// </summary>
        /// <param name="processId">process Id to use in the message number</param>
        /// <param name="seqNumber">sequece number to use in the message number</param>
        /// <returns>A new message number</returns>
        public static MessageNumber Create(Int16 processId, Int16 seqNumber)
        {
            MessageNumber result = new MessageNumber();
            result.ProcessId = processId;
            result.SeqNumber = seqNumber;
            return result;
        }

        /// <summary>
        /// Default constructor, used by factory methods (the Create) methods.  It should not be public,
        /// because external object should all use one of the two factor methods. 
        /// </summary>
        private MessageNumber() { }

        #endregion

        #region Overridden public methods of Object
        public override string ToString()
        {
            return ProcessId.ToString() + "." + SeqNumber.ToString();
        }

        public override bool Equals(object obj)
        {
            return (Compare(this,obj as MessageNumber)==0);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion

        #region Private Methods
        private static Int16 GetNextSeqNumber()
        {
            if (nextSeqNumber == Int16.MaxValue)
                nextSeqNumber = 1;
            return nextSeqNumber++;
        }
        #endregion

        #region Encoding and Decoding methods
        /// <summary>
        /// This method encodes
        /// </summary>
        /// <param name="bytes"></param>
        public void Encode(NetByteStream bytes)
        {
            // TODO: Implement
        }

        /// <summary>
        /// This method decodes a message from a byte list
        /// </summary>
        /// <param name="bytes"></param>
        protected void Decode(NetByteStream bytes)
        {
            // TODO: Implement
        }
        #endregion

        #region Comparison Methods and Operators

        public static int Compare(MessageNumber a, MessageNumber b)
        {
            int result = 0;

            if (!System.Object.ReferenceEquals(a, b))
            {
                if (((object)a == null) && ((object)b != null))
                    result = -1;                             
                else if (((object)a != null) && ((object)b == null))
                    result = 1;                             
                else
                {
                    if (a.ProcessId < b.ProcessId)
                        result = -1;
                    else if (a.ProcessId > b.ProcessId)
                        result = 1;
                    else if (a.SeqNumber < b.SeqNumber)
                        result = -1;
                    else if (a.SeqNumber > b.SeqNumber)
                        result = 1;
                }
            }
            return result;
        }

        public static bool operator ==(MessageNumber a, MessageNumber b)
        {
            return (Compare(a,b) == 0);
        }

        public static bool operator !=(MessageNumber a, MessageNumber b)
        {
            return (Compare(a,b) !=0 );
        }

        public static bool operator <(MessageNumber a, MessageNumber b)
        {
            return (Compare(a, b) < 0);
        }

        public static bool operator >(MessageNumber a, MessageNumber b)
        {
            return (Compare(a, b) > 0);
        }

        public static bool operator <=(MessageNumber a, MessageNumber b)
        {
            return (Compare(a, b) <= 0);
        }

        public static bool operator >= (MessageNumber a, MessageNumber b)
        {
            return (Compare(a, b) >= 0);
        }

        public int CompareTo(object obj)
        {
            return Compare(this, obj as MessageNumber);
        }

        #endregion

    }
}
