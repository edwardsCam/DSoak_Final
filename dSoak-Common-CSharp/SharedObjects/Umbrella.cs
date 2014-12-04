using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SharedObjects
{
    [DataContract]
    public class Umbrella : SharedResource
    {
        public Umbrella Copy
        {
            get
            {
                Umbrella result = new Umbrella();
                result.CopyFrom(this);
                return result;
            }
        }
    }
}
