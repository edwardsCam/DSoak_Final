using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

using log4net;

namespace GameRegistry
{
    public class EndPointReflector : IDisposable
    {
        #region Private Data Members
        private static readonly ILog log = LogManager.GetLogger(typeof(EndPointReflector));
        private static readonly int endPointReflectorPort = 51999;

        private static EndPointReflector instance;
        private static object myLock = new object();
        private UdpClient udpClient;
        private bool keepGoing = false;
        #endregion

        #region Constructors, Destructors, and Instance

        protected EndPointReflector()
        {
            log.Debug("Create an EndPointReflector");
            keepGoing = true;
            udpClient = new UdpClient(endPointReflectorPort, AddressFamily.InterNetwork);
            log.DebugFormat("Start listening for an incoming request on port {0}", ((IPEndPoint) udpClient.Client.LocalEndPoint).Port);
            udpClient.BeginReceive(ReceiveCallback, null);
        }

        public void Dispose()
        {
            udpClient.Close();
            Dispose(true);
        }
        
        protected virtual void Dispose(bool flags)
        {
            keepGoing = false;
            if (udpClient != null)
                udpClient.Close();
            this.Dispose();
            GC.SuppressFinalize(this);
        }

        public static EndPointReflector Instance
        {
            get
            {
                lock (myLock)
                {
                    if (instance == null)
                        instance = new EndPointReflector();
                    return instance;
                }
            }
        }
        #endregion

        #region Public Methods and Properties
        public IPEndPoint EndPoint { get { return udpClient.Client.LocalEndPoint as IPEndPoint; } }
        #endregion

        #region Private methods
        private void ReceiveCallback(IAsyncResult asyncResult)
        {
            log.Debug("In ReceiveCallBack");
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
            byte[] receiveBytes = null;
            lock (myLock)
            {
                try
                {
                    receiveBytes = udpClient.EndReceive(asyncResult, ref ep);
                }
                catch { }
            }
            if (receiveBytes != null)
            {
                string remoteEP = ep.ToString();
                log.DebugFormat("Send back the remote address, {0}", remoteEP);
                byte[] sendBuffer = ASCIIEncoding.ASCII.GetBytes(remoteEP);
                udpClient.Send(sendBuffer, sendBuffer.Length, ep);
            }

            // Start another receive
            if (keepGoing)
                udpClient.BeginReceive(ReceiveCallback, null); 
        }
        #endregion

    }
}