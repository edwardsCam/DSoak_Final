using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

using GameRegistry;
using SampleWebserviceClient.Registry;
using SharedObjects;

namespace SampleWebserviceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            RegistrarClient registry = new RegistrarClient("BasicHttpBinding_IRegistrar");

            // Get the end point of the end-point reflector
            PublicEndPoint reflectorEP = new PublicEndPoint() { HostAndPort = registry.EndPointReflector() };

            // Send a dummy message off to the reflectorEP
            UdpClient myUdpClient = new UdpClient();
            myUdpClient.Send(new byte[] { 1, 2, 3 }, 3, reflectorEP.IPEndPoint);

            // Wait for a response for 2 seconds
            myUdpClient.Client.ReceiveTimeout = 20000;
            IPEndPoint senderEP = new IPEndPoint(IPAddress.Any, 0);
            byte[] response = myUdpClient.Receive(ref senderEP);
            string tmp = ASCIIEncoding.ASCII.GetString(response);
            PublicEndPoint myEP = new PublicEndPoint() { HostAndPort = tmp };

            Int16 processId = registry.GetProcessId(myEP, "Test Process 123", RegistryEntry.ProcessType.GameManager);

            GameInfo myGame = registry.RegisterGame(processId, "Test Game",  10);

            GameInfo[] games = registry.GetGames(GameInfo.StatusCode.NotInitialized);
            // games should contain the test game

            registry.ChangeStatus(myGame.GameId, GameInfo.StatusCode.Available);
            games = registry.GetGames(GameInfo.StatusCode.Available);
            // games should contain the test game

            registry.AmAlive(myGame.GameId);
            games = registry.GetGames(GameInfo.StatusCode.Available);
            // games should contain the test game

        }
    }
}
