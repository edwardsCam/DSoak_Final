using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SharedObjects
{
    [DataContract]
    public class PlayerInfo
    {
        public enum StateCode { Unknown=0, OnLine=1, OffLine=2 };

        [DataMember]
        public Int32 PlayerId { get; set; }
        [DataMember]
        public PublicEndPoint EndPoint { get; set; }
        [DataMember]
        public StateCode Status { get; set; }

    }
}
