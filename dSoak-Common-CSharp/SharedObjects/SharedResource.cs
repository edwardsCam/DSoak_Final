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
        private static Int32 nounceInt = 0;
        private static byte[] nounce;
        private static Random randomizer = null;
        private static HashAlgorithm hasher = null;
        private static bool hasBeenInitialized = false;
        private static object myLock = new object();
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
        [DataMember]
        public Int16 Id { get; set; }
        [DataMember]
        public byte[] DigitalSignature { get; set; }

        public static Int32 Nounce
        {
            get { return nounceInt; }
            set
            {
                nounceInt = value;
                nounce = BitConverter.GetBytes(nounceInt);
            }
        }

        public static void Initialize()
        {
            hasher = MD5.Create();
            randomizer = new Random();
            randomizer.Next();
            nounceInt = randomizer.Next();
            nounce = BitConverter.GetBytes(nounceInt);
            hasBeenInitialized = true;
        }

        public void Sign()
        {
            DigitalSignature = ComputeDigitalSignature(new MemoryStream());
        }

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

        public string DigitalSignatureString
        {
            get
            {
                string result = string.Empty;
                foreach (byte b in DigitalSignature)
                    result += b.ToString().PadLeft(4);
                return result;
            }
        }

        public static bool AreValidToUse<T>(List<T> resources) where T : SharedResource
        {
            bool result = (resources != null && resources.Count > 0);
            for (int i = 0; i < resources.Count && result; i++)
                result = ValidateUse(resources[i]);
            return result;
        }

        public static bool ValidateUse(SharedResource resource)
        {
            return (resource.IsValid && CheckUsedId(resource.Id));
        }

        #endregion

        #region Private Methods
        private static Int16 GetNextId()
        {
            if (nextId == Int16.MaxValue)
                nextId = 0;
            return ++nextId;
        }

        protected void CopyFrom(SharedResource orig)
        {
            this.Id = orig.Id;
            this.DigitalSignature = new byte[orig.DigitalSignature.Length];
            for (int i = 0; i < orig.DigitalSignature.Length; i++)
                this.DigitalSignature[i] = orig.DigitalSignature[i];
        }

        protected byte[] ComputeDigitalSignature(MemoryStream mStream)
        {
            AddOwnDataToStream(mStream);
            mStream.Position = 0;

            byte[] result = hasher.ComputeHash(mStream);
            return result;
        }

        protected virtual void AddOwnDataToStream(MemoryStream mStream)
        {
            byte[] idBytes = BitConverter.GetBytes(Id);
            mStream.Write(idBytes, 0, idBytes.Length);
            mStream.Write(nounce, 0, nounce.Length);
        }

        #endregion

        #region Id Management
        private static List<Int16> usedIds = new List<short>();
        public static bool CheckUsedId(Int16 id)
        {
            bool result = false;
            lock (myLock)
            {
                if (!usedIds.Contains(id))
                {
                    usedIds.Add(id);
                    result = true;
                }
            }
            return result;
        }
        #endregion
    }
}
