using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using GameRegistry;
using SampleWebserviceClient.Registry;

namespace SampleWebserviceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            RegistrarClient registry = new RegistrarClient();

            PublicEndPoint myEP = new PublicEndPoint() { HostAndPort = registry.EndPointReflector() };

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

            Thread.Sleep(90000);
            games = registry.GetGames(GameInfo.StatusCode.Available);
            // games should not contain the test game
        }
    }
}
