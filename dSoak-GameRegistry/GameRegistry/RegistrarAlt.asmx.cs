using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using SharedObjects;

namespace GameRegistry
{
    /// <summary>
    /// Summary description for RegistrarAlt
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class RegistrarAlt : System.Web.Services.WebService
    {
        [WebMethod]
        public Int16 GetProcessId(PublicEndPoint ep, string label, RegistryEntry.ProcessType processType)
        {
            return Registry.Instance.GetProcessId(ep, label, processType);
        }

        [WebMethod]
        public void AmAlive(int processId)
        {
            Registry.Instance.AmAlive(Convert.ToInt16(processId));
        }

        [WebMethod]
        public RegistryEntry[] GetGameManagers()
        {
            return Registry.Instance.GetGameManagers().ToArray();
        }

        [WebMethod]
        public RegistryEntry[] GetPlayers()
        {
            return Registry.Instance.GetPlayers().ToArray();
        }

        [WebMethod]
        public RegistryEntry GetProcessInfo(short processId)
        {
            return Registry.Instance.GetProcessInfo(processId);
        }

        [WebMethod]
        public GameInfo RegisterGame(int gameManagerId, string label, int maxPlayers)
        {
            return Registry.Instance.RegisterGame(Convert.ToInt16(gameManagerId), label, Convert.ToInt16(maxPlayers));
        }

        [WebMethod]
        public GameInfo[] GetGames(GameInfo.StatusCode status = GameInfo.StatusCode.Available)
        {
            return Registry.Instance.GetGames(status).ToArray();
        }

        [WebMethod]
        public GameInfo GetGameInfo(int gameId)
        {
            return Registry.Instance.GetGameInfo(gameId);
        }

        [WebMethod]
        public void GameAmAlive(int gameId)
        {
            Registry.Instance.GameAmAlive(Convert.ToInt16(gameId));
        }

        [WebMethod]
        public void ChangeStatus(int gameId, GameInfo.StatusCode status)
        {
            Registry.Instance.ChangeGameStatus(gameId, status);
        }

        [WebMethod]
        public string EndPointReflector()
        {
            System.Net.IPEndPoint reflectorEP = GameRegistry.EndPointReflector.Instance.EndPoint;
            string reflectorHost = this.Context.Request.Url.Host;
            return string.Format("{0}:{1}", reflectorHost, reflectorEP.Port);
        }

    }
}
