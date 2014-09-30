using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Messages
{
    [DataContract]
    public class MessageNumber :  IComparable
    {
        #region Private Properties
        private static Int16 nextSeqNumber = 0;                     // Start with message #1
        #endregion

        #region Public Properties
        public static Int16 LocalProcessId { get; set; }            // Local process Id -- set once when the
                                                                    // process joins the distributed application
        [DataMember]
        public Int16 ProcessId { get; set; }
        [DataMember]
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
