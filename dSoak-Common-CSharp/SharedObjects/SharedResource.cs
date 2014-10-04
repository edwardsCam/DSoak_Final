using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Security.Cryptography;

namespace SharedObjects
{
    [DataContract]
    public class SharedResource
    {
        #region Private Static Attributes
        private static Int16 nextId = 0;
        private static byte[] nounce;
        private static Random randomizer = null;
        private static HashAlgorithm hasher = null;
        private static bool hasBeenInitialized = false;
        #endregion

        #region Public Properties
        [DataMember]
        public Int16 Id { get; set; }
        [DataMember]
        public byte[] DigitalSignature { get; set; }
        #endregion

        #region Constructor(s)
        public SharedResource()
        {
            if (!hasBeenInitialized)
                Initialize();
            
            Id = GetNextId();
            Sign();
        }
        #endregion

        #region Public Methods
        public bool IsValid
        {
            get
            {
                bool result = false;
                if (DigitalSignature != null)
                {
                    byte[] tmpSignature = ComputeDigitalSignature(new MemoryStream());
                    result = (DigitalSignature.Length==tmpSignature.Length);
                    for (int i = 0; i < DigitalSignature.Length && result; i++)
                        if (DigitalSignature[i] != tmpSignature[i])
                            result = false;
                }
                return result;
            }
        }
        #endregion

        #region Private Methods
        private static void Initialize()
        {
            hasher = MD5.Create();
            randomizer = new Random();
            randomizer.Next();
            nounce = BitConverter.GetBytes(randomizer.Next());
            hasBeenInitialized = true;
        }

        private static Int16 GetNextId()
        {
            if (nextId == Int16.MaxValue)
                nextId = 0;
            return ++nextId;
        }

        protected void Sign()
        {
            DigitalSignature = ComputeDigitalSignature(new MemoryStream());
        }

        protected byte[] ComputeDigitalSignature(MemoryStream mStream)
        {
            AddOwnDataToStream(mStream);
            mStream.Position = 0;

            byte[] result = hasher.ComputeHash(mStream);
            return result;
        }

        virtual protected void AddOwnDataToStream(MemoryStream mStream)
        {
            byte[] idBytes = BitConverter.GetBytes(Id);
            mStream.Write(idBytes, 0, idBytes.Length);
            mStream.Write(nounce, 0, nounce.Length);
        }

        #endregion

    }
}
