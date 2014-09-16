using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Messages
{
    public class MessageNumber :  IComparable
    {
        #region Private Properties
        private static Int16 nextSeqNumber = 0;                     // Start with message #1
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
        /// Default constructor, used by factory methods (the Create) methods.  It should not be public,
        /// because external object should all use one of the two factor methods. 
        /// </summary>
        protected MessageNumber() { }

        /// <summary>
        /// Factory method creates and new, unique message number.
        /// </summary>
        /// <returns>A new message number</returns>
        public static MessageNumber Create()
        {
            MessageNumber result = new MessageNumber()
                {
                    ProcessId = LocalProcessId,
                    SeqNumber = GetNextSeqNumber()
                };
            return result;
        }

        /// <summary>
        /// Factory Method that creates a message from a byte string
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static MessageNumber Create(NetByteStream stream)
        {
            MessageNumber result = new MessageNumber();
            result.Decode(stream);
            return result;
        }

        #endregion

        #region Overridden public methods of Object
        public override string ToString()
        {
            return ProcessId.ToString() + "." + SeqNumber.ToString();
        }
        #endregion

        #region Private Methods
        private static Int16 GetNextSeqNumber()
        {
            if (nextSeqNumber == Int16.MaxValue)
                nextSeqNumber = 0;
            return ++nextSeqNumber;
        }
        #endregion

        #region Encoding and Decoding methods
        /// <summary>
        /// This method encodes
        /// </summary>
        /// <param name="bytes"></param>
        public void Encode(NetByteStream bytes)
        {
            if (bytes == null)
                throw new ApplicationException("Cannot encode into a null NetByteStream object");

            bytes.Write(ProcessId);
            bytes.Write(SeqNumber);
        }

        /// <summary>
        /// This method decodes a message from a byte list
        /// </summary>
        /// <param name="bytes"></param>
        protected void Decode(NetByteStream bytes)
        {
            if (bytes == null)
                throw new ApplicationException("Cannot decode from a null NetByteStream object");

            if (bytes.RemainingToRead < 4)
                throw new ApplicationException("Not enough data in NetByteStream for decode MessageNumber");

            ProcessId = bytes.ReadInt16();
            SeqNumber = bytes.ReadInt16(); 
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

        public override bool Equals(object obj)
        {
            return (Compare(this, obj as MessageNumber) == 0);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
