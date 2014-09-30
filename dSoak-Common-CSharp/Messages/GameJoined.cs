using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using SharedObjects;

namespace Messages
{
    [DataContract]
    public class GameJoined : Message
    {
        [DataMember]
        public Int16 GameId { get; set; }
        [DataMember]
        public List<Penny> Pennies { get; set; }
    }
}
