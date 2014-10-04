using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SharedObjects
{
    [DataContract]
    public class GamePlayerInfo
    {
        public enum StatusCode { Unknown=0, InGame=1, LeftGame=2, Winner=3 };

        [DataMember]
        public Int16 GameId { get; set; }
        [DataMember]
        public PlayerInfo Player { get; set; }
        [DataMember]
        public StatusCode Status { get; set; }
    }
}
