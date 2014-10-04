using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

using SharedObjects;

namespace Messages
{
    [DataContract]
    public class Message
    {
        private static Dictionary<string, DataContractJsonSerializer> serializers = null;
        private static bool hasBeenInitialized = false;

        [DataMember]
        public MessageNumber MessageNr;
        [DataMember]
        public MessageNumber ConvId;

        public Message()
        {
            if (!hasBeenInitialized)
                Initialize();

            MessageNr = MessageNumber.Create();
            ConvId = MessageNr;
        }

        public byte[] Encode()
        {
            DataContractJsonSerializer serializer = LookupSerializer(this);

            MemoryStream mstream = new MemoryStream();
            string type = this.GetType().Name + ":";
            byte[] typeBytes = Encoding.ASCII.GetBytes(type);
            mstream.Write(typeBytes, 0, typeBytes.Length);

            serializer.WriteObject(mstream, this);
            return mstream.ToArray();
        }

        public static Message Decode(byte[] bytes)
        {
            MemoryStream mstream = new MemoryStream(bytes);

            string typeName = ParseTypeName(mstream);
            DataContractJsonSerializer serializer = LookupSerializer(typeName);

            Message result = (Message) serializer.ReadObject(mstream);

            return result;
        }

        private static void Initialize()
        {
            serializers = new Dictionary<string, DataContractJsonSerializer>();

            serializers.Add("Ack", new DataContractJsonSerializer(typeof(Ack)));
            serializers.Add("AliveQuery", new DataContractJsonSerializer(typeof(AliveQuery)));
            serializers.Add("BalloonFilled", new DataContractJsonSerializer(typeof(BalloonFilled)));
            serializers.Add("BalloonPurchased", new DataContractJsonSerializer(typeof(BalloonPurchased)));
            serializers.Add("BuyBalloon", new DataContractJsonSerializer(typeof(BuyBalloon)));
            serializers.Add("Continue", new DataContractJsonSerializer(typeof(Continue)));
            serializers.Add("FillBalloon", new DataContractJsonSerializer(typeof(FillBalloon)));
            serializers.Add("GameJoined", new DataContractJsonSerializer(typeof(GameJoined)));
            serializers.Add("GameOver", new DataContractJsonSerializer(typeof(GameOver)));
            serializers.Add("Hit", new DataContractJsonSerializer(typeof(Hit)));
            serializers.Add("JoinGame", new DataContractJsonSerializer(typeof(JoinGame)));
            serializers.Add("LeaveGame", new DataContractJsonSerializer(typeof(LeaveGame)));
            serializers.Add("Nak", new DataContractJsonSerializer(typeof(Nak)));
            serializers.Add("RaiseUmbrella", new DataContractJsonSerializer(typeof(RaiseUmbrella)));
            serializers.Add("SetupStream", new DataContractJsonSerializer(typeof(SetupStream)));
            serializers.Add("Shutdown", new DataContractJsonSerializer(typeof(Shutdown)));
            serializers.Add("StopStream", new DataContractJsonSerializer(typeof(StopStream)));
            serializers.Add("ThrowBalloon", new DataContractJsonSerializer(typeof(ThrowBalloon)));
            serializers.Add("UmbrellaPurchased", new DataContractJsonSerializer(typeof(UmbrellaPurchased)));
        }

        private static DataContractJsonSerializer LookupSerializer(Message message)
        {
            return LookupSerializer(message.GetType().Name);
        }

        private static DataContractJsonSerializer LookupSerializer(string typeName)
        {
            return serializers[typeName];
        }

        private static string ParseTypeName(MemoryStream mstream)
        {
            string result = string.Empty;
            byte[] bytes = new byte[mstream.Length - mstream.Position];
            int index;
            for (index = 0; index < mstream.Length - mstream.Position; index++)
            {
                bytes[index] = (byte) mstream.ReadByte();
                if (bytes[index] == (int)':')
                    break;
            }

            if (index>0)
                result = Encoding.ASCII.GetString(bytes, 0, index);
            return result;
        }
    }
}
