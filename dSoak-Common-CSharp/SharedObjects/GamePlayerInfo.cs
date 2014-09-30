using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SharedObjects
{
    [DataContract]
    class GamePlayerInfo
    {
        public enum StatusCode { InGame, LeftGame, Winner };

        [DataMember]
        public Int16 GameId { get; set; }
        [DataMember]
        public PlayerInfo Player { get; set; }
        [DataMember]
        public StatusCode Status { get; set; }
    }
}
