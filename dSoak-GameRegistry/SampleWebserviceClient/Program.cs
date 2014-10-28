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
            // NOTE: That the following code represents a sample scenario of communications and a few coding techniques.
            // It does not represent a good design for your player.  Also, it does some things (like register a game)
            // that only the Fight Manager has to do.

            // Get a Web Service proxy object
            RegistrarClient registry = new RegistrarClient("BasicHttpBinding_IRegistrar");

            // Use this web-service proxy to get the end point of the end-point reflector from the Webservice
            PublicEndPoint reflectorEP = new PublicEndPoint() { HostAndPort = registry.EndPointReflector() };

            // Send a dummy message off to the end-point reflector so we can discover our own public end point
            UdpClient myUdpClient = new UdpClient();
            myUdpClient.Send(new byte[] { 1, 2, 3 }, 3, reflectorEP.IPEndPoint);

            // Wait for a response, up to 20 seconds and convert to a public end point -- this will be our own public end point
            // for our own UDP Client.  You will want to do this for the UdpClient in your Communicator.
            myUdpClient.Client.ReceiveTimeout = 20000;
            IPEndPoint senderEP = new IPEndPoint(IPAddress.Any, 0);
            byte[] response = myUdpClient.Receive(ref senderEP);
            string tmp = ASCIIEncoding.ASCII.GetString(response);
            PublicEndPoint myEP = new PublicEndPoint() { HostAndPort = tmp };

            // Use the web Service to register this process as a process in the game.  You will register as a player
            // not a game manager
            Int16 processId = registry.GetProcessId(myEP, "Test Process 123", RegistryEntry.ProcessType.GameManager);

            // This is another of using the web service to register a game.  You don't have to do this in your player.
            GameInfo myGame = registry.RegisterGame(processId, "Test Game",  10);

            // This is an example of how to get a list of games that ARE NOT yet ready to join
            GameInfo[] games = registry.GetGames(GameInfo.StatusCode.NotInitialized);
            // games should contain the test game

            // This is an example of how to get a list of games that ARE yet ready to join
            registry.ChangeStatus(myGame.GameId, GameInfo.StatusCode.Available);
            games = registry.GetGames(GameInfo.StatusCode.Available);
            // games should contain the test game

            // You need to periodically to this to keep it's alive
            registry.AmAlive(processId);

            // The Fight Manager will do this to keep a game alive
            registry.GameAmAlive(myGame.GameId);
            games = registry.GetGames(GameInfo.StatusCode.Available);
            // games should still contain the test game

        }
    }
}
