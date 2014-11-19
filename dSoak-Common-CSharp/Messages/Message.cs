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

            Message result = (Message)serializer.ReadObject(mstream);

            return result;
        }

        private static void Initialize()
        {
            serializers = new Dictionary<string, DataContractJsonSerializer>
            {
                {"Ack", new DataContractJsonSerializer(typeof (Ack))},
                {"AliveQuery", new DataContractJsonSerializer(typeof (AliveQuery))},
                {"BalloonFilled", new DataContractJsonSerializer(typeof (BalloonFilled))},
                {"BalloonPurchased", new DataContractJsonSerializer(typeof (BalloonPurchased))},
                {"BuyBalloon", new DataContractJsonSerializer(typeof (BuyBalloon))},
                {"BuyUmbrella", new DataContractJsonSerializer(typeof (BuyUmbrella))},
                {"Continue", new DataContractJsonSerializer(typeof (Continue))},
                {"FillBalloon", new DataContractJsonSerializer(typeof (FillBalloon))},
                {"GameData", new DataContractJsonSerializer(typeof(GameData))},
                {"GameJoined", new DataContractJsonSerializer(typeof (GameJoined))},
                {"GameOver", new DataContractJsonSerializer(typeof (GameOver))},
                {"Hit", new DataContractJsonSerializer(typeof (Hit))},
                {"JoinGame", new DataContractJsonSerializer(typeof (JoinGame))},
                {"LeaveGame", new DataContractJsonSerializer(typeof (LeaveGame))},
                {"Nak", new DataContractJsonSerializer(typeof (Nak))},
                {"RaiseUmbrella", new DataContractJsonSerializer(typeof (RaiseUmbrella))},
                {"SetupStream", new DataContractJsonSerializer(typeof (SetupStream))},
                {"Shutdown", new DataContractJsonSerializer(typeof (Shutdown))},
                {"StopStream", new DataContractJsonSerializer(typeof (StopStream))},
                {"ThrowBalloon", new DataContractJsonSerializer(typeof (ThrowBalloon))},
                {"UmbrellaPurchased", new DataContractJsonSerializer(typeof (UmbrellaPurchased))},
                {"ProcessSummary", new DataContractJsonSerializer(typeof (ProcessSummary))},
                {"Stealing", new DataContractJsonSerializer(typeof (Stealing))},
                {"BlockStealing", new DataContractJsonSerializer(typeof (BlockStealing))},
                {"StealingBlocked", new DataContractJsonSerializer(typeof (StealingBlocked))},
                {"GiveUpABalloon", new DataContractJsonSerializer(typeof (GiveUpABalloon))},
                {"BalloonStolen", new DataContractJsonSerializer(typeof (BalloonStolen))}
            };
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
                bytes[index] = (byte)mstream.ReadByte();
                if (bytes[index] == (int)':')
                    break;
            }

            if (index > 0)
                result = Encoding.ASCII.GetString(bytes, 0, index);
            return result;
        }
    }
}
