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
    public abstract class StealingBase : Message
    {
        [DataMember]
        public short GameId { get; set; }
        [DataMember]
        public short TargetProcessId { get; set; }
        [DataMember]
        public short ThiefId { get; set; }
    }
}
