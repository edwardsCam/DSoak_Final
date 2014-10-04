using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Net;

namespace SharedObjects
{
    [DataContract]
    public class PublicEndPoint
    {
        [DataMember]
        public string Host { get; set; }
        [DataMember]
        public Int32 Port { get; set; }

        public IPEndPoint IPEndPoint
        {
            get
            {
                IPEndPoint result = null;
                if (!string.IsNullOrWhiteSpace(Host))
                    result = new IPEndPoint(LookupAddress(Host), Port);
                return result;
            }
            set
            {
                if (value != null)
                {
                    Host = value.Address.ToString();
                    Port = value.Port;
                }
            }
        }

        public static IPAddress LookupAddress(string host)
        {
            IPAddress result = null;
            if (!string.IsNullOrWhiteSpace(host))
            {
                IPAddress[] addressList = Dns.GetHostAddresses(host);
                for (int i = 0; i < addressList.Length && result == null; i++)
                    if (addressList[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        result = addressList[i];
            }
            return result;
        }
    }
}
