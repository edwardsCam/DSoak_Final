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
    public class SharedResource
    {
        private static Int16 nextId = 0;
        private static byte[] nounce;
        private static Random randomizer = null;
        private static HashAlgorithm hasher = null;

        [DataMember]
        public Int16 Id { get; set; }
        [DataMember]
        public byte[] DigitalSignature { get; set; }

        public SharedResource()
        {
            Id = GetNextId();
            Sign();
        }

        #region Private Methods
        private static void Initialize()
        {
            hasher = MD5.Create();
            randomizer.Next();
            nounce = BitConverter.GetBytes(randomizer.Next());
        }

        private static Int16 GetNextId()
        {
            if (nextId == Int16.MaxValue)
                nextId = 0;
            return ++nextId;
        }

        protected void Sign()
        {
            if (hasher != null)
                Initialize();
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
