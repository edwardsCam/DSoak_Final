using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Sockets;

using GameRegistry;
using GameRegistryTester.GameRegistry;

namespace GameRegistryTester
{
    /// <summary>
    /// Summary description for EndPointReflectorTester
    /// </summary>
    [TestClass]
    public class EndPointReflectorTester
    {
        [TestMethod]
        public void EndPointReflector_StandAlone()
        {
            EndPointReflector reflector = EndPointReflector.Instance;
            Assert.IsNotNull(reflector);
            Assert.IsNotNull(reflector.EndPoint);

            UdpClient testClient = new UdpClient(0, AddressFamily.InterNetwork);
            byte[] sendBuffer = ASCIIEncoding.ASCII.GetBytes("hello");
            IPEndPoint reflectorEP = new IPEndPoint(IPAddress.Loopback, reflector.EndPoint.Port);
            testClient.Send(sendBuffer, sendBuffer.Length, reflectorEP);

            testClient.Client.ReceiveTimeout = 1000;
            IPEndPoint sendingEP = new IPEndPoint(IPAddress.Any, 0);
            byte[] receiveBuffer = testClient.Receive(ref sendingEP);

            Assert.IsNotNull(receiveBuffer);
            string reflectedEP = ASCIIEncoding.ASCII.GetString(receiveBuffer);
            Assert.IsNotNull(reflectedEP);
            Assert.IsTrue(reflectedEP.Length > 8);
            string[] tmp = reflectedEP.Split(':');
            Assert.IsTrue(tmp.Length == 2);
        }

        [TestMethod]
        public void EndPointReflector_Webservice()
        {
            RegistrarClient registrar = new RegistrarClient("LocalHttpBinding_IRegistrar");

            string publicEndPoint = registrar.EndPointReflector();
            Assert.IsNotNull(publicEndPoint);
        }
    }
}
