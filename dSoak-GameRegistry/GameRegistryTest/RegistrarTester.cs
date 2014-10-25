using System;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Sockets;

using SharedObjects;
using GameRegistry;
using GameRegistryTester.GameRegistry;

namespace GameRegistryTester
{
    [TestClass]
    public class RegistrarTester
    {
        [TestMethod]
        public void Registrar_TestEverything()
        {
            RegistrarClient registrar = new RegistrarClient("LocalHttpBinding_IRegistrar");
            // RegistrarClient registrar = new RegistrarClient("ProdHttpBinding_IRegistrar");

            PublicEndPoint myPublicEP = new PublicEndPoint() { HostAndPort = registrar.EndPointReflector() };

            Int16 localProcessId = registrar.GetProcessId(myPublicEP, "Test Process 34", GameRegistry.RegistryEntry.ProcessType.GameManager);

            GameInfo g0 = registrar.RegisterGame(localProcessId, "Test Game 0", 10);
            Assert.IsNotNull(g0);
            Assert.AreEqual("Test Game 0", g0.Label);
            Assert.AreEqual(myPublicEP.ToString(), g0.FightManagerEP.ToString());
            Assert.AreEqual(GameInfo.StatusCode.NotInitialized, g0.Status);

            GameInfo g1 = registrar.RegisterGame(localProcessId, "Test Game 1", 15);
            Assert.IsNotNull(g1);
            Assert.AreEqual("Test Game 1", g1.Label);
            Assert.AreEqual(myPublicEP.ToString(), g1.FightManagerEP.ToString());
            Assert.AreEqual(GameInfo.StatusCode.NotInitialized, g0.Status);

            GameInfo[] games = registrar.GetGames(GameInfo.StatusCode.NotInitialized);
            Assert.IsTrue(GamesContain(games, g0.GameId, g0.Label));
            Assert.IsTrue(GamesContain(games, g1.GameId, g1.Label));

            registrar.ChangeStatus(g1.GameId, GameInfo.StatusCode.Available);
            games = registrar.GetGames(GameInfo.StatusCode.NotInitialized);
            Assert.IsTrue(GamesContain(games, g0.GameId, g0.Label));
            games = registrar.GetGames(GameInfo.StatusCode.Available);
            Assert.IsTrue(GamesContain(games, g1.GameId, g1.Label));
        }

        [TestMethod]
        public void Registrar_TestEndPointReflection()
        {
            RegistrarClient registrar = new RegistrarClient("LocalHttpBinding_IRegistrar");
            // RegistrarClient registrar = new RegistrarClient("ProdHttpBinding_IRegistrar");

            string reflectorEndPointString = registrar.EndPointReflector();
            Assert.IsNotNull(reflectorEndPointString);
            PublicEndPoint reflectorEndPoint = new PublicEndPoint(reflectorEndPointString);

            UdpClient testClient = new UdpClient(0, AddressFamily.InterNetwork);
            byte[] sendBuffer = ASCIIEncoding.ASCII.GetBytes("hello");
            
            testClient.Send(sendBuffer, sendBuffer.Length, reflectorEndPoint.IPEndPoint );

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

        private bool GamesContain(GameInfo[] games, Int16 id, string label)
        {
            bool result = false;
            foreach (GameInfo game in games)
                if (game.GameId == id && game.Label == label)
                {
                    result = true;
                    break;
                }
            return result;
        }
    }

}
