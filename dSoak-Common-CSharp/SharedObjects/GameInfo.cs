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
        public enum StatusCode { NotInitialized=0, Available=1, InProgress=2, Complete=3, Cancelled=4, Ending=5 } ;
        [DataMember]
        public Int16 GameId { get; set; }
        [DataMember]
        public string Label { get; set; }
        [DataMember]
        public Int16 FightManagerId { get; set; }
        [DataMember]
        public PublicEndPoint FightManagerEP { get; set; }
        [DataMember]
        public StatusCode Status { get; set; }
        [DataMember]
        public Int16 MaxPlayers { get; set; }
        [DataMember]
        public Int16 MaxThieves { get; set; }

        public DateTime AliveTimestamp { get; set; }

        public virtual GameInfo Copy
        {
            get
            {
                GameInfo result = new GameInfo();
                result.CopyFrom(this);
                return result;
            }
        }

        protected void CopyFrom(GameInfo orig)
        {
            GameId = orig.GameId;
            Label = orig.Label;
            FightManagerEP = orig.FightManagerEP;
            Status = orig.Status;
            MaxPlayers = orig.MaxPlayers;
            AliveTimestamp = orig.AliveTimestamp;
        }
    }
}
