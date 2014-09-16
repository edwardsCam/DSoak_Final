using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Messages
{
    public class Login : Message
    {
        #region Public Properties
        public string Username { get; set; }
        public string Password { get; set; }
        #endregion

        #region Constructors and Factories
        protected Login(bool isForSending, bool isFirstMessage) : base(MessageType.LOGIN, isForSending, isFirstMessage) { }
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
            if (stream==null || stream.RemainingToRead==0)
                throw new ApplicationException("Cannot create a LoginMessage from a null stream or stream with no more bytes to be read");

            if ((MessageType)stream.PeekByte() != MessageType.LOGIN)
                throw new ApplicationException("The current read position in the specified stream doesn't contain a Login Message");

            result = new Login();
            result.Decode(stream);

            return result;
        }

        #endregion

        #region Encoding and Decoding methods
        /// <summary>
        /// This method encodes
        /// </summary>
        /// <param name="bytes"></param>
        public override void Encode(NetByteStream stream)
        {
            stream.Write((byte)MyMessageType);
            base.Encode(stream);
            stream.Write(Username);
            stream.Write(Password);
        }

        /// <summary>
        /// This method decodes a message from a byte list
        /// </summary>
        /// <param name="bytes"></param>
        protected override void Decode(NetByteStream stream)
        {
            stream.ReadByte();
            base.Decode(stream);
            Username = stream.ReadString();
            Password = stream.ReadString();
        }
        #endregion

    }
}
