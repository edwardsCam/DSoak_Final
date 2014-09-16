using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Messages
{
    public class Login : Message
    {
        #region Private Data
        private string username = null;
        private string password = null;
        #endregion

        #region Constructors and Factories
        protected Login(bool isForSending, bool isFirstMessage) : base(isForSending, isFirstMessage) { }
        protected Login() : this(false, false) { }

        /// <summary>
        /// Factory Method that creates a message from a byte string
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Login Create(string username, string password)
        {
            Login result = new Login(true, true) { Username = username, Password = password };
            return result;
        }

        /// <summary>
        /// Factory Method that creates a message from a byte string
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public new static Login Create(NetByteStream stream)
        {
            Login result = null;

            // TODO: Decode the stream into a message number

            return result;
        }

        #endregion

        #region Public Properties
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        #endregion

        #region Encoding and Decoding methods
        /// <summary>
        /// This method encodes
        /// </summary>
        /// <param name="bytes"></param>
        public override void Encode(NetByteStream bytes)
        {
            // TODO: Implement
        }

        /// <summary>
        /// This method decodes a message from a byte list
        /// </summary>
        /// <param name="bytes"></param>
        protected override void Decode(NetByteStream bytes)
        {
            // TODO: Implement
        }
        #endregion


    }
}
