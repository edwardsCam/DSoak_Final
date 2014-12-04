using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;

namespace SharedObjects
{
    [DataContract]
    public class Balloon : SharedResource
    {
        [DataMember]
        public Int16 UnitsOfWater { get; set; }

        public Balloon Copy
        {
            get
            {
                Balloon result = new Balloon();
                result.CopyFrom(this);
                result.UnitsOfWater = UnitsOfWater;
                return result;
            }
        }

        protected override void AddOwnDataToStream(MemoryStream mStream)
        {
            byte[] tmp = BitConverter.GetBytes(UnitsOfWater);
            mStream.Write(tmp, 0, tmp.Length);

            base.AddOwnDataToStream(mStream);
        }
    }

}
