using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SharedObjects
{
    [DataContract]
    public class GameInfo
    {
        public enum StatusCode { Avaliable, InProgress, Complete, Cancelled } ;
        [DataMember]
        public Int16 GameId { get; set; }
        [DataMember]
        public PublicEndPoint FlightManagerEP { get; set; }
        [DataMember]
        public StatusCode Status { get; set; }
        [DataMember]
        public Int16 MaxPlayers { get; set; }
    }
}
