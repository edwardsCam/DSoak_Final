using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Messages
{
    [DataContract]
    public class Continue : Message
    {
        [DataMember]
        public Int16 MissingReplyDeqNr { get; set; }
    }
}
