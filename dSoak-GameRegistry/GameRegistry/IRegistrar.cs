﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using SharedObjects;

namespace GameRegistry
{
    [ServiceContract]
    public interface IRegistrar
    {
        [OperationContract]
        Int16 GetProcessId(PublicEndPoint ep, string label, RegistryEntry.ProcessType processType);

        [OperationContract]
        void AmAlive(int processId);

        [OperationContract]
        RegistryEntry[] GetGameManagers();

        [OperationContract]
        RegistryEntry[] GetPlayers();

        [OperationContract]
        RegistryEntry GetProcessInfo(short processId);

        [OperationContract]
        GameInfo RegisterGame(int gameManagerId, string label, int maxPlayers, int maxThieves);

        [OperationContract]
        GameInfo[] GetAllGames();

        [OperationContract]
        GameInfo[] GetGames(GameInfo.StatusCode status = GameInfo.StatusCode.Available);

        [OperationContract]
        GameInfo GetGameInfo(int gameId);

        [OperationContract]
        void GameAmAlive(int gameId);

        [OperationContract]
        void ChangeStatus(int gameId, GameInfo.StatusCode status);

        [OperationContract]
        string EndPointReflector();

    }
}
