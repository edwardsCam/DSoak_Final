using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using SharedObjects;

namespace GameRegistry
{
    /// <summary>
    /// Registrar for registering games, looking up games, etc., implemented using WCF Webservices.
    /// </summary>
    public class Registrar : IRegistrar
    {
        public Int16 GetProcessId(PublicEndPoint ep, string label, RegistryEntry.ProcessType processType)
        {
            return Registry.Instance.GetProcessId(ep, label, processType);
        }

        public RegistryEntry[] GetGameManagers()
        {
            return Registry.Instance.GetGameManagers().ToArray();
        }

        public RegistryEntry[] GetPlayers()
        {
            return Registry.Instance.GetPlayers().ToArray();
        }

        public RegistryEntry GetProcessInfo(short processId)
        {
            return Registry.Instance.GetProcessInfo(processId);
        }

        public void AmAlive(int processId)
        {
            Registry.Instance.AmAlive(Convert.ToInt16(processId));
        }

        public GameInfo RegisterGame(int gameManagerId, string label, int maxPlayers, int maxThieves)
        {
            return Registry.Instance.RegisterGame(Convert.ToInt16(gameManagerId), label, Convert.ToInt16(maxPlayers), Convert.ToInt16(maxThieves));
        }

        public GameInfo[] GetGames(GameInfo.StatusCode status = GameInfo.StatusCode.Available)
        {
            return Registry.Instance.GetGames(status).ToArray();
        }

        public GameInfo GetGameInfo(int gameId)
        {
            return Registry.Instance.GetGameInfo(gameId);
        }

        public void GameAmAlive(int gameId)
        {
            Registry.Instance.GameAmAlive(Convert.ToInt16(gameId));
        }

        public void ChangeStatus(int gameId, GameInfo.StatusCode status)
        {
            Registry.Instance.ChangeGameStatus(gameId, status);
        }

        public string EndPointReflector()
        {
            System.Net.IPEndPoint reflectorEP = GameRegistry.EndPointReflector.Instance.EndPoint;
            string reflectorHost = OperationContext.Current.Host.BaseAddresses[0].Host;
            return string.Format("{0}:{1}", reflectorHost, reflectorEP.Port);
        }
    }
}
