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
    public class FillBalloon : ResourceRequest
    {
        [DataMember]
        public Balloon Balloon { get; set; }
    }
}
