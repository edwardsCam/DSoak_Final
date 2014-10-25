using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using SharedObjects;

namespace SharedObjects
{
    [DataContract]
    public class RegistryEntry
    {
        public enum ProcessType { GameManager, Player };

        [DataMember]
        public PublicEndPoint Ep { get; set; }
        [DataMember]
        public string Label { get; set; }
        [DataMember]
        public Int16 ProcessId { get; set; }
        [DataMember]
        public ProcessType Type { get; set; }
        [DataMember]
        public DateTime AliveTimestamp { get; set; }
    }
}