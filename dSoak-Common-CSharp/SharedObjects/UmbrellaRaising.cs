using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SharedObjects
{
    [DataContract]
    public class UmbrellaRaising
    {
        [DataMember]
        public Int16 GameId { get; set;  }
        [DataMember]
        public Int16 PlayerId { get; set; }
        [DataMember]
        public Int16 UmbrellaId { get; set; }
        [DataMember]
        public DateTime? AtTime { get; set; }
    }
}
