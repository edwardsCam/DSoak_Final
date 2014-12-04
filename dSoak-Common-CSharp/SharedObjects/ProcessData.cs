using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SharedObjects
{
    [DataContract]
    public class ProcessData
    {
        public enum PossibleProcessType { Unknown = 0, Player = 1, BalloonStore = 2, WaterServer = 3, UmbrellaSupplier = 4, Thief = 5 };
        [DataMember]
        public short GameId { get; set; }
        [DataMember]
        public short ProcessId { get; set; }
        [DataMember]
        public PossibleProcessType ProcessType { get; set; }
        [DataMember]
        public short LifePoints { get; set; }
        [DataMember]
        public short HitPoints { get; set; }
        [DataMember]
        public short NumberOfPennies { get; set; }
        [DataMember]
        public short NumberOfUnfilledBalloon { get; set; }
        [DataMember]
        public short NumberOfFilledBalloon { get; set; }
        [DataMember]
        public short NumberOfUnraisedUmbrellas { get; set; }
        [DataMember]
        public bool HasUmbrellaRaised { get; set; }
    }
}
