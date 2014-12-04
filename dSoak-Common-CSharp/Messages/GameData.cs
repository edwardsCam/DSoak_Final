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
    public class GameData : Message
    {
        [DataMember]
        public GameInfo Info { get; set; }
        [DataMember]
        public List<ProcessData> Processes { get; set; }
    }
}
