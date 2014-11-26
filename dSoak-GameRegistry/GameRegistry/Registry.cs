using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Threading;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

using SharedObjects;

using log4net;

namespace GameRegistry
{
    // TODO: Switch over to Concurrent Dictionaries
    public class Registry : IDisposable
    {
        #region Private and Protected Data Members
        private static readonly ILog log = LogManager.GetLogger(typeof(Registry));
        private static Registry instance = null;
        private static object myLock = new object();

        private const int cleanupFrequency = 12000;
        private const int deadTimeout = 60000;

        protected Dictionary<string, RegistryEntry> gameManagers;
        protected Dictionary<string, RegistryEntry> players;
        protected Dictionary<Int16, RegistryEntry> processes;
        protected Dictionary<int, GameInfo> games;
        private Int16 nextId = 1;
        protected Timer cleanupTimer;
        protected string fileName;
        protected int inCleanup = 0;
        #endregion

        #region Constructors and Instance Accessor
        protected Registry()
        {
            log.Debug("Creating a registry object");
            gameManagers = new Dictionary<string, RegistryEntry>();
            players = new Dictionary<string, RegistryEntry>();
            processes = new Dictionary<Int16, RegistryEntry>();
            games = new Dictionary<int, GameInfo>();
            cleanupTimer = new Timer(Cleanup, null, cleanupFrequency, cleanupFrequency);
        }

        public void Dispose() { Dispose(true); }

        protected virtual void Dispose(bool flag)
        {
            if (gameManagers != null)
                gameManagers.Clear();
            if (players != null)
                players.Clear();
            if (processes != null)
                processes.Clear();
            if (games != null)
                games.Clear();
            if (cleanupTimer != null)
                cleanupTimer.Dispose();

            gameManagers = null;
            players = null;
            processes = null;
            cleanupTimer = null;

            GC.SuppressFinalize(this);
        }

        public static Registry Instance
        {
            get
            {
                lock (myLock)
                {
                    if (instance == null)
                        instance = new Registry();
                    return instance;
                }
            }
        }
        #endregion

        #region Public Methods
        public static void TakeDown()
        {
            lock (myLock)
            {
                if (instance != null)
                {
                    instance.Save();
                    instance.Dispose();
                    instance = null;
                }
            }
        }

        public Int16 GetProcessId(PublicEndPoint ep, string label, RegistryEntry.ProcessType processType)
        {
            Int16 result = -1;
            RegistryEntry entry = null;
            if (ep!=null && !string.IsNullOrWhiteSpace(label))
            {
                string epString = ep.ToString();
                lock (myLock)
                {
                    if (gameManagers.ContainsKey(epString))
                        result = (processType == RegistryEntry.ProcessType.GameManager) ? gameManagers[epString].ProcessId : (Int16)(-2);
                    else if (players.ContainsKey(epString))
                    {
                        result = players[epString].ProcessId;
                        players[epString].Type = processType;
                    }
                    else
                    {
                        result = GetNextIdNumber();
                        entry = new RegistryEntry() { Ep = ep, Label = label, ProcessId = result, Type = processType, AliveTimestamp = DateTime.Now };
                        Dictionary<string, RegistryEntry> dictionary = (processType == RegistryEntry.ProcessType.GameManager) ? gameManagers : players;
                        dictionary.Add(ep.ToString(), entry);
                        processes.Add(result, entry);
                    }
                }
            }
            return result;
        }

        public List<RegistryEntry> GetGameManagers()
        {
            List<RegistryEntry> result = null;
            lock (myLock)
            {
                result = gameManagers.Values.ToList();
            }
            return result;
        }

        public List<RegistryEntry> GetPlayers()
        {
            List<RegistryEntry> result = null;
            lock (myLock)
            {
                result = players.Values.ToList();
            }
            return result;
        }

        public RegistryEntry GetProcessInfo(short processId)
        {
            RegistryEntry entry = null;

            lock (myLock)
            {
                IEnumerable<KeyValuePair<string, RegistryEntry>> set = gameManagers.Where(e => e.Value.ProcessId == processId);
                if (set.Count() > 0)
                    entry = set.First().Value;
                else
                {
                    set = players.Where(e => e.Value.ProcessId == processId);
                    if (set.Count() > 0)
                        entry = set.First().Value;
                }
            }
            return entry;
        }

        public void AmAlive(Int16 processId)
        {
            lock (myLock)
            {
                log.DebugFormat("Enter AmLive, with processId={0}", processId);
                if (processes.ContainsKey(processId))
                {
                    processes[processId].AliveTimestamp = DateTime.Now;
                    log.DebugFormat("Updated Timestamp for Process {0} to {1}", processId, processes[processId].AliveTimestamp);
                }
            }
        }

        public GameInfo RegisterGame(Int16 gameManagerId, string label, Int16 maxPlayers, Int16 maxThieves)
        {
            log.DebugFormat("In RegisterGame, with gameManagerId={0}, label={1}, maxPlayers={2}, maxThieves={3}", gameManagerId, label, maxPlayers, maxThieves);
            GameInfo game = null;
            if (!string.IsNullOrWhiteSpace(label) && processes.ContainsKey(gameManagerId) && processes[gameManagerId].Type == RegistryEntry.ProcessType.GameManager)
            {
                log.DebugFormat("Register {0} to process {1}", label, gameManagerId);

                game = new GameInfo()
                            {
                                Label = label,
                                FightManagerId = gameManagerId,
                                FightManagerEP = processes[gameManagerId].Ep,
                                MaxPlayers = maxPlayers,
                                MaxThieves = maxThieves,
                                AliveTimestamp = DateTime.Now
                            };
                game.GameId = GetNextIdNumber();
                log.DebugFormat("New game's id={0}", game.GameId);
                lock (myLock)
                {
                    games.Add(game.GameId, game);
                }
                Save();
                LogContents();
            }
            else
                log.WarnFormat("Invalid RegisterGame request from gameManagerId={0}", gameManagerId);
            return game;
        }

        public List<GameInfo> GetAllGames()
        {
            log.Debug("In GetGames");

            List<GameInfo> gameList;

            lock (myLock)
            {
                gameList = games.Values.ToList();
            }
            
            return gameList;
        }

        public List<GameInfo> GetGames(GameInfo.StatusCode status)
        {
            log.Debug("In GetGames");

            List<GameInfo> filteredGameList = new List<GameInfo>();

            lock (myLock)
            {
                filteredGameList = games.Where(item => item.Value.Status == status).Select(item => item.Value) .ToList();            
            }

            return filteredGameList;
        }

        public GameInfo GetGameInfo(int gameId)
        {
            GameInfo gameInfo = null;

            log.DebugFormat("In ChangeChangeStatus for gameId={0}", gameId);
            lock (myLock)
            {
                if (games.ContainsKey(gameId))
                    gameInfo = games[gameId];
            }

            return gameInfo;
        }

        public void GameAmAlive(Int16 gameId)
        {
            lock (myLock)
            {
                log.DebugFormat("Enter GameAmAlive, with gameId={0}", gameId);
                if (games.ContainsKey(gameId))
                {
                    if (processes.ContainsKey(games[gameId].FightManagerId))
                    {
                        games[gameId].AliveTimestamp = DateTime.Now;
                        log.DebugFormat("Update Timestamp for game {0} to {1}", gameId, games[gameId].AliveTimestamp);
                    }
                    else
                        log.Warn("Can no long keep game alive, because it game manager is dead");
                }
            }
        }

        public void ChangeGameStatus(int gameId, GameInfo.StatusCode status)
        {
            log.DebugFormat("In ChangeChangeStatus for gameId={0}", gameId);
            lock (myLock)
            {
                if (games.ContainsKey(gameId))
                {
                    log.DebugFormat("Change status to {0}", status);
                    games[gameId].Status = status;
                    games[gameId].AliveTimestamp = DateTime.Now;
                    Save();
                }
            }
        }

        public void LoadFromFile(string filename)
        {
            log.Debug("In LoadFromFile");

            if (!string.IsNullOrWhiteSpace(filename) &&
                File.Exists(filename))
            {
                log.DebugFormat("Load from {0}", filename);
                this.fileName = filename;
                StreamReader reader = new StreamReader(fileName);
                LoadProcessesFromFile(reader);
                LoadGamesFromFile(reader);
                reader.Close();
            }
        }

        private void LoadProcessesFromFile(StreamReader reader)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(RegistryEntry));
            bool foundBegin = false;
            bool foundEnd = false;
            while (!reader.EndOfStream && !foundBegin)
            {
                string tmp = reader.ReadLine();
                if (tmp.Trim().ToUpper() == "PROCESSES BEGIN")
                    foundBegin = true;
            }

            while (!reader.EndOfStream && foundBegin && !foundEnd)
            {
                string tmp = reader.ReadLine();
                if (tmp.Trim().ToUpper() == "PROCESSES END")
                    foundEnd = true;
                else
                {
                    MemoryStream mstream = new MemoryStream(ASCIIEncoding.ASCII.GetBytes(tmp));
                    RegistryEntry entry = (RegistryEntry)serializer.ReadObject(mstream);
                    entry.AliveTimestamp = DateTime.Now;
                    processes.Add(entry.ProcessId, entry);
                    Dictionary<string, RegistryEntry> dictionary = (entry.Type == RegistryEntry.ProcessType.GameManager) ? gameManagers : players;
                    dictionary.Add(entry.Ep.ToString(), entry);
                    nextId = Math.Max(nextId, Convert.ToInt16(entry.ProcessId+1));
                }
            }
        }

        private void LoadGamesFromFile(StreamReader reader)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(GameInfo));
            bool foundBegin = false;
            bool foundEnd = false;
            while (!reader.EndOfStream && !foundBegin)
            {
                string tmp = reader.ReadLine();
                if (tmp.Trim().ToUpper() == "GAMES BEGIN")
                    foundBegin = true;
            }

            while (!reader.EndOfStream && foundBegin && !foundEnd)
            {
                string tmp = reader.ReadLine();
                if (tmp.Trim().ToUpper() == "GAMES END")
                    foundEnd = true;
                else
                {
                    MemoryStream mstream = new MemoryStream(ASCIIEncoding.ASCII.GetBytes(tmp));
                    GameInfo game = (GameInfo) serializer.ReadObject(mstream);
                    game.AliveTimestamp = DateTime.Now;
                    games.Add(game.GameId, game);
                    nextId = Math.Max(nextId, Convert.ToInt16(game.GameId+1));
                }
            }
        }

        public void Save()
        {
            SaveToFile(fileName);
        }

        public void SaveToFile(string filename)
        {
            log.Debug("In SaveToFile");
            if (!string.IsNullOrWhiteSpace(filename))
            {
                log.DebugFormat("Save to {0}", filename);
                this.fileName = filename;
                StreamWriter writer = new StreamWriter(filename);

                lock (myLock)
                {
                    SaveProcessesToFile(writer);
                    SaveGamesToFile(writer);
                }
                writer.Close();
            }
        }

        private void SaveProcessesToFile(StreamWriter writer)
        {
            writer.WriteLine("Processes Begin");
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(RegistryEntry));
            Dictionary<Int16, RegistryEntry>.Enumerator iterator = processes.GetEnumerator();
            while (iterator.MoveNext())
            {
                RegistryEntry entry = iterator.Current.Value;
                if (iterator.Current.Value.AliveTimestamp.AddMilliseconds(cleanupFrequency) >= DateTime.Now)
                {
                    MemoryStream mstream = new MemoryStream();
                    serializer.WriteObject(mstream, entry);
                    mstream.Position = 0;
                    StreamReader sr = new StreamReader(mstream);
                    writer.WriteLine(sr.ReadToEnd());
                }
            }
            writer.WriteLine("Processes End");
        }

        private void SaveGamesToFile(StreamWriter writer)
        {
            writer.WriteLine("Games Begin");
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(GameInfo));
            Dictionary<int, GameInfo>.Enumerator iterator = games.GetEnumerator();
            while (iterator.MoveNext())
            {
                GameInfo game = iterator.Current.Value;
                if ((game.Status != GameInfo.StatusCode.Cancelled &&
                    game.Status != GameInfo.StatusCode.Complete) ||
                    iterator.Current.Value.AliveTimestamp.AddMilliseconds(cleanupFrequency) >= DateTime.Now)
                {
                    MemoryStream mstream = new MemoryStream();
                    serializer.WriteObject(mstream, game);
                    mstream.Position = 0;
                    StreamReader sr = new StreamReader(mstream);
                    writer.WriteLine(sr.ReadToEnd());
                }
            }
            writer.WriteLine("Games End");
        }
        #endregion

        #region Private Methods
        private Int16 GetNextIdNumber()
        {
            if (nextId == Int16.MaxValue)
                nextId = 1;
            return nextId++;
        }

        private void Cleanup(object state)
        {
            // Perform a test-and-set operation to check if another thread is already in this method.  Skip,
            // if there is one.
            if (Interlocked.CompareExchange(ref inCleanup, 1, 0) == 0)
            {
                log.Debug("Do a Cleanup");
                CleanupProcesses();
                CleanupGames();
                inCleanup = 0;
                LogContents();
            }
        }

        private void CleanupProcesses()
        {
            Dictionary<Int16, RegistryEntry> livingProcesses = new Dictionary<Int16, RegistryEntry>();
            lock (myLock)
            {
                Dictionary<Int16, RegistryEntry>.Enumerator iterator = processes.GetEnumerator();
                while (iterator.MoveNext())
                {
                    RegistryEntry entry = iterator.Current.Value;

                    if (entry.AliveTimestamp.AddMilliseconds(deadTimeout) >= DateTime.Now)
                        livingProcesses.Add(entry.ProcessId, entry);
                    else
                    {
                        log.DebugFormat("Don't keep process {0}", entry.ProcessId);
                        CancelGamesForProcess(entry.ProcessId);
                    }
                }
                processes = livingProcesses;

                gameManagers.Clear();
                players.Clear();

                iterator = processes.GetEnumerator();
                while (iterator.MoveNext())
                {
                    log.DebugFormat("Keep {0} process {1}", iterator.Current.Value.Type, iterator.Current.Value.ProcessId);
                    Dictionary<string, RegistryEntry> dictionary = (iterator.Current.Value.Type == RegistryEntry.ProcessType.GameManager) ? gameManagers : players;
                    dictionary.Add(iterator.Current.Value.Ep.ToString(), iterator.Current.Value);
                }
            }
        }

        private void CancelGamesForProcess(Int16 gameManagerId)
        {
            log.DebugFormat("Cleanup games for {0}", gameManagerId);
            Dictionary<int, GameInfo>.Enumerator gameIterator = games.GetEnumerator();
            while (gameIterator.MoveNext())
            {
                log.DebugFormat("Consider game {0}, FightManagerId={1}, Status={2}", gameIterator.Current.Value.GameId, gameIterator.Current.Value.FightManagerId, gameIterator.Current.Value.Status);
                if (gameIterator.Current.Value.FightManagerId == gameManagerId &&
                    gameIterator.Current.Value.Status != GameInfo.StatusCode.Complete)
                {
                    log.DebugFormat("Cancelling game {0}", gameIterator.Current.Value.GameId);
                    gameIterator.Current.Value.Status = GameInfo.StatusCode.Cancelled;
                }
            }
        }

        private void CleanupGames()
        {
            Dictionary<int, GameInfo> livingGames = new Dictionary<int, GameInfo>();
            lock (myLock)
            {
                Dictionary<int, GameInfo>.Enumerator iterator = games.GetEnumerator();
                while (iterator.MoveNext())
                {
                    GameInfo game = iterator.Current.Value;
                    bool keep = false;
                    switch (iterator.Current.Value.Status)
                    {
                        case GameInfo.StatusCode.NotInitialized:
                        case GameInfo.StatusCode.Available:
                        case GameInfo.StatusCode.InProgress:
                            keep = true;
                            if (game.AliveTimestamp.AddMilliseconds(deadTimeout) < DateTime.Now)
                            {
                                log.DebugFormat("Game {0} is not alive, mark as cancelled", game.GameId);
                                game.Status = GameInfo.StatusCode.Cancelled;
                            }
                            break;
                        case GameInfo.StatusCode.Cancelled:
                            keep = false;
                            break;
                        case GameInfo.StatusCode.Complete:
                            keep = (game.AliveTimestamp.AddMilliseconds(cleanupFrequency) >= DateTime.Now);
                            break;
                    }
                    if (keep)
                    {
                        livingGames.Add(game.GameId, game);
                        log.DebugFormat("Keep game {0} around", game.GameId);
                    }
                    else
                        log.DebugFormat("Don't keep {0} around", game.GameId);
                }
                games = livingGames;
            }
        }

        private void LogContents()
        {
            #if (DEBUG)
            lock (myLock)
            {
                Dictionary<int, GameInfo>.Enumerator iterator = games.GetEnumerator();
                while (iterator.MoveNext())
                {
                    log.DebugFormat("Id={0,-10} EP={1,-20} Label={2,-50} Status={3}", iterator.Current.Value.GameId, iterator.Current.Value.FightManagerEP.ToString(),
                                    iterator.Current.Value.Label, iterator.Current.Value.Status.ToString());
                }
            }
            #endif
        }
        #endregion

    }
}