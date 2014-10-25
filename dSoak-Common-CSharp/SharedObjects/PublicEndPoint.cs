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
        private IPAddress myIPAddress = null;
        private IPEndPoint myIPEndPoint = null;
        private string myHost = null;
        private Int32 myPort = 0;

        [DataMember]
        public string HostAndPort
        {
            get { return (myHost == null) ? "0.0.0.0:" + myPort.ToString() : myHost.ToString() + ":" + myPort.ToString(); }
            set { SetHostAndPort(value); }
        }

        public string Host
        {
            get { return myHost; }
            set { myHost = value; myIPAddress = null; myIPEndPoint = null; }
        }

        public Int32 Port
        {
            get { return myPort; }
            set { myPort = value; myIPEndPoint = null; }
        }

        public PublicEndPoint() { }

        public PublicEndPoint(string hostnameAndPort)
        {
            if (!string.IsNullOrWhiteSpace(hostnameAndPort))
            {
                string[] tmp = hostnameAndPort.Split(':');
                if (tmp.Length == 2 && !string.IsNullOrWhiteSpace(tmp[0]))
                {
                    Host = tmp[0];
                    Int32 tmpPort = 0;
                    Int32.TryParse(tmp[1], out tmpPort);
                    Port = tmpPort;
                }
            }
        }

        public IPEndPoint IPEndPoint
        {
            get
            {
                if (myIPEndPoint == null)
                {
                    if (myIPAddress == null)
                    {
                        if (!string.IsNullOrWhiteSpace(Host))
                            myIPAddress = LookupAddress(Host);
                    }
                    if (myIPAddress != null)
                        myIPEndPoint = new IPEndPoint(myIPAddress, Port);
                }
                return myIPEndPoint;
            }
            set
            {
                if (value != null)
                {
                    Host = value.Address.ToString();
                    Port = value.Port;
                    myIPAddress = value.Address;
                    myIPEndPoint = value;
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

        private void SetHostAndPort(string hostnameAndPort)
        {
            if (!string.IsNullOrWhiteSpace(hostnameAndPort))
            {
                string[] tmp = hostnameAndPort.Split(':');
                if (tmp.Length == 2 && !string.IsNullOrWhiteSpace(tmp[0]))
                {
                    Host = tmp[0];
                    Int32 tmpPort = 0;
                    Int32.TryParse(tmp[1], out tmpPort);
                    Port = tmpPort;
                }
            }
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Host, Port);
        }
    }
}
