using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

using SampleWebserviceClient.Registry;
using SharedObjects;

using log4net;
using log4net.Config;

namespace SampleWebserviceClient
{
    public class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));
        private static RegistrarClient registry;

        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            // NOTE: That the following code represents a sample scenario of communications and a few coding techniques.
            // It does not represent a good design for your player.  Also, it does some things (like register a game)
            // that only the Fight Manager has to do.

            log.Debug("Get a Web Service proxy object");
            registry = new RegistrarClient("BasicHttpBinding_IRegistrar");

            log.Debug("Use this web-service proxy to get the end point of the end-point reflector from the Webservice");
            PublicEndPoint reflectorEP = new PublicEndPoint() { HostAndPort = registry.EndPointReflector() };

            log.Debug("Send a dummy message off to the end-point reflector so we can discover our own public end point");
            UdpClient myUdpClient = new UdpClient();
            myUdpClient.Send(new byte[] { 1, 2, 3 }, 3, reflectorEP.IPEndPoint);

            log.Debug("Wait for a response, up to 20 seconds and convert to a public end point -- this will be our own public end point");
            log.Debug("for our own UDP Client.  You will want to do this for the UdpClient in your Communicator.");
            myUdpClient.Client.ReceiveTimeout = 20000;
            IPEndPoint senderEP = new IPEndPoint(IPAddress.Any, 0);
            byte[] response = myUdpClient.Receive(ref senderEP);
            string tmp = ASCIIEncoding.ASCII.GetString(response);
            PublicEndPoint myEP = new PublicEndPoint() { HostAndPort = tmp };

            log.Debug("Use the web Service to register this process as a process in the game.  You will register as a player");
            // not a game manager
            Int16 processId = registry.GetProcessId(myEP, "Test Process 123", RegistryEntry.ProcessType.GameManager);

            log.Debug("This is another of using the web service to register a game.  You don't have to do this in your player.");
            GameInfo myGame = registry.RegisterGame(processId, "Test Game",  10);

            log.Debug("This is an example of how to get a list of games that ARE NOT yet ready to join");
            GameInfo[] games = registry.GetGames(GameInfo.StatusCode.NotInitialized);
            LogGames(games);

            log.Debug("This is an example of how to get a list of games that ARE yet ready to join");
            registry.ChangeStatus(myGame.GameId, GameInfo.StatusCode.Available);
            LogGames(games);

            // You need to periodically to this to keep it's alive
            log.Debug("Stay alive");
            registry.AmAlive(processId);

            // The Fight Manager will do this to keep a game alive
            log.Debug("Keep a game alive");
            registry.GameAmAlive(myGame.GameId);
            games = registry.GetGames(GameInfo.StatusCode.Available);
            LogGames(games);

        }

        private static void LogGames(GameInfo[] games)
        {
            if (games != null)
            {
                foreach (GameInfo game in games)
                    log.DebugFormat("Game #{0}, {1}, {2}", game.GameId, game.Label, game.Status);
            }
        }
    }
}
