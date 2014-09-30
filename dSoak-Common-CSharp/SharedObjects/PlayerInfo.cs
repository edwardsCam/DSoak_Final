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
        public enum StateCode { OnLine, OffLine };

        [DataMember]
        public Int32 PlayerId { get; set; }
        [DataMember]
        public PublicEndPoint EndPoint { get; set; }
        [DataMember]
        public StateCode Status { get; set; }

    }
}
