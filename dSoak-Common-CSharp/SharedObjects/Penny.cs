using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SharedObjects
{
    [DataContract]
    public class Penny : SharedResource
    {
        public Penny Copy
        {
            get
            {
                Penny result = new Penny();
                result.CopyFrom(this);
                return result;
            }
        }

    }
}
