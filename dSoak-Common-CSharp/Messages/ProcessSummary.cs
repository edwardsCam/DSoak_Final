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
    public class ProcessSummary : Message
    {
        [DataMember]
        public ProcessData Data { get; set; }
    }
}
