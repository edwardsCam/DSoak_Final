using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SharedObjects;
using GameRegistry;

namespace GameRegistryTester
{
    [TestClass]
    public class RegistryTester
    {
        [TestMethod]
        public void Registry_TestInstanceAndTakeDown()
        {
            // Test Case 1: Check instance when there is not one yet
            Registry.TakeDown();
            Registry r1 = Registry.Instance;
            Assert.IsNotNull(r1);

            // Test Case 2: Check instance when there is already one
            Registry r2 = Registry.Instance;
            Assert.IsNotNull(r2);
            Assert.AreSame(r1, r2);

            // Test Case 3: Takedown and recreate should yield a different instance
            Registry.TakeDown();
            Registry r3 = Registry.Instance;
            Assert.IsNotNull(r3);
            Assert.AreNotSame(r1, r3);
        }

        [TestMethod]
        public void Registry_TestGetProcessId()
        {
            Registry reg = Registry.Instance;

            Int16 pid1 = reg.GetProcessId(null, null, RegistryEntry.ProcessType.GameManager);
            Assert.AreEqual(-1, pid1);

            Int16 pid2 = reg.GetProcessId(null, string.Empty, RegistryEntry.ProcessType.Player);
            Assert.AreEqual(-1, pid2);

            Int16 pid3 = reg.GetProcessId(new PublicEndPoint(), string.Empty, RegistryEntry.ProcessType.Player);
            Assert.AreEqual(-1, pid3);

            int currentPlayer = reg.GetPlayers().Count;
            Int16 pid4 = reg.GetProcessId(new PublicEndPoint(),"My Player #1", RegistryEntry.ProcessType.Player);
            Assert.IsTrue(pid4>0);
            Assert.AreEqual(currentPlayer + 1, reg.GetPlayers().Count);
        }

        [TestMethod]
        private void Registry_CheckLockingInGetProcessId()
        {
            // TODO: Write test case
            // Start one hundred threads, each calling GetProcessId with valid parameters to process id
            // After each call assert the id is correct and dictionary is correct.
        }

        [TestMethod]
        public void Registry_OtherMethodsAndProperties()
        {
            Registry myRegistry = Registry.Instance;
            Assert.IsNotNull(myRegistry);

            PublicEndPoint ep1 = new PublicEndPoint("123.129.7.54:2350");
            Int16 id1 = myRegistry.GetProcessId(ep1, "Test Process 1", RegistryEntry.ProcessType.GameManager);
            Assert.IsTrue(id1 > 0);
            List<RegistryEntry> entries = myRegistry.GetGameManagers();
            Assert.IsNotNull(entries);
            Assert.AreEqual(1, entries.Count);
            Assert.AreEqual(ep1.ToString(), entries[0].Ep.ToString());
            Assert.AreEqual("Test Process 1", entries[0].Label);

            PublicEndPoint ep2 = new PublicEndPoint("123.129.7.55:2650");
            Int16 id2 = myRegistry.GetProcessId(ep2, "Test Process 2", RegistryEntry.ProcessType.Player);
            Assert.IsTrue(id2 == id1+1);
            entries = myRegistry.GetGameManagers();
            Assert.IsNotNull(entries);
            Assert.AreEqual(1, entries.Count);
            entries = myRegistry.GetPlayers();
            Assert.IsNotNull(entries);
            Assert.AreEqual(1, entries.Count);
            Assert.AreEqual(ep2.ToString(), entries[0].Ep.ToString());
            Assert.AreEqual("Test Process 2", entries[0].Label);

            PublicEndPoint ep3 = new PublicEndPoint("123.129.7.65:2850");
            Int16 id3 = myRegistry.GetProcessId(ep3, "Test Process 3", RegistryEntry.ProcessType.Player);
            Assert.IsTrue(id3 == id2 + 1);
            entries = myRegistry.GetGameManagers();
            Assert.IsNotNull(entries);
            Assert.AreEqual(1, entries.Count);
            entries = myRegistry.GetPlayers();
            Assert.IsNotNull(entries);
            Assert.AreEqual(2, entries.Count);
            Assert.AreEqual(ep2.ToString(), entries[0].Ep.ToString());
            Assert.AreEqual("Test Process 2", entries[0].Label);
            Assert.AreEqual(ep3.ToString(), entries[1].Ep.ToString());
            Assert.AreEqual("Test Process 3", entries[1].Label);

            RegistryEntry re3 = myRegistry.GetProcessInfo(id3);
            Assert.IsNotNull(re3);
            Assert.AreEqual(id3, re3.ProcessId);
            Assert.AreEqual("123.129.7.65:2850", re3.Ep.ToString());

            RegistryEntry re4 = myRegistry.GetProcessInfo(-1);
            Assert.IsNull(re4);

            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Available).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Complete).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Cancelled).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.NotInitialized).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.InProgress).Count);

            GameInfo g0 = myRegistry.RegisterGame(id1, "Game0", 10, 2);
            Assert.IsNotNull(g0);
            Assert.AreEqual("Game0", g0.Label);
            Assert.AreEqual(10, g0.MaxPlayers);
            Assert.AreEqual(2, g0.MaxThiefs);
            Assert.AreEqual(GameInfo.StatusCode.NotInitialized, g0.Status);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Available).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Complete).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Cancelled).Count);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.NotInitialized).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.InProgress).Count);
            Assert.AreSame(g0, myRegistry.GetGames(GameInfo.StatusCode.NotInitialized)[0]);

            myRegistry.ChangeGameStatus(g0.GameId, GameInfo.StatusCode.Available);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.Available).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Complete).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Cancelled).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.NotInitialized).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.InProgress).Count);
            Assert.AreSame(g0, myRegistry.GetGames(GameInfo.StatusCode.Available)[0]);

            GameInfo g1 = myRegistry.RegisterGame(id1, "Game1", 15, 3);
            Assert.IsNotNull(g1);
            Assert.AreEqual("Game1", g1.Label);
            Assert.AreEqual(15, g1.MaxPlayers);
            Assert.AreEqual(3, g1.MaxThiefs);
            Assert.AreEqual(GameInfo.StatusCode.NotInitialized, g1.Status);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.Available).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Complete).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Cancelled).Count);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.NotInitialized).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.InProgress).Count);
            Assert.AreSame(g0, myRegistry.GetGames(GameInfo.StatusCode.Available)[0]);
            Assert.AreSame(g1, myRegistry.GetGames(GameInfo.StatusCode.NotInitialized)[0]);

            myRegistry.ChangeGameStatus(g1.GameId, GameInfo.StatusCode.Available);
            Assert.AreEqual(2, myRegistry.GetGames(GameInfo.StatusCode.Available).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Complete).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Cancelled).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.NotInitialized).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.InProgress).Count);
            Assert.AreSame(g0, myRegistry.GetGames(GameInfo.StatusCode.Available)[0]);
            Assert.AreSame(g1, myRegistry.GetGames(GameInfo.StatusCode.Available)[1]);

            GameInfo g2 = myRegistry.RegisterGame(id1, "Game2", 10, 2);
            Assert.IsNotNull(g2);
            Assert.AreEqual("Game2", g2.Label);
            Assert.AreEqual(10, g2.MaxPlayers);
            Assert.AreEqual(GameInfo.StatusCode.NotInitialized, g2.Status);
            Assert.AreEqual(2, myRegistry.GetGames(GameInfo.StatusCode.Available).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Complete).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Cancelled).Count);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.NotInitialized).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.InProgress).Count);
            Assert.AreSame(g0, myRegistry.GetGames(GameInfo.StatusCode.Available)[0]);
            Assert.AreSame(g1, myRegistry.GetGames(GameInfo.StatusCode.Available)[1]);
            Assert.AreSame(g2, myRegistry.GetGames(GameInfo.StatusCode.NotInitialized)[0]);

            myRegistry.ChangeGameStatus(g2.GameId, GameInfo.StatusCode.Complete);
            Assert.AreEqual(2, myRegistry.GetGames(GameInfo.StatusCode.Available).Count);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.Complete).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Cancelled).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.NotInitialized).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.InProgress).Count);
            Assert.AreSame(g0, myRegistry.GetGames(GameInfo.StatusCode.Available)[0]);
            Assert.AreSame(g1, myRegistry.GetGames(GameInfo.StatusCode.Available)[1]);
            Assert.AreSame(g2, myRegistry.GetGames(GameInfo.StatusCode.Complete)[0]);

            GameInfo g3 = myRegistry.RegisterGame(id1, "Game3", 10,2 );
            myRegistry.ChangeGameStatus(g3.GameId, GameInfo.StatusCode.Cancelled);
            Assert.AreEqual(2, myRegistry.GetGames(GameInfo.StatusCode.Available).Count);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.Complete).Count);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.Cancelled).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.NotInitialized).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.InProgress).Count);
            Assert.AreSame(g0, myRegistry.GetGames(GameInfo.StatusCode.Available)[0]);
            Assert.AreSame(g1, myRegistry.GetGames(GameInfo.StatusCode.Available)[1]);
            Assert.AreSame(g2, myRegistry.GetGames(GameInfo.StatusCode.Complete)[0]);
            Assert.AreSame(g3, myRegistry.GetGames(GameInfo.StatusCode.Cancelled)[0]);

            GameInfo g4 = myRegistry.RegisterGame(id1, "Game4", 10, 2);
            Assert.AreEqual(2, myRegistry.GetGames(GameInfo.StatusCode.Available).Count);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.Complete).Count);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.Cancelled).Count);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.NotInitialized).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.InProgress).Count);
            Assert.AreSame(g0, myRegistry.GetGames(GameInfo.StatusCode.Available)[0]);
            Assert.AreSame(g1, myRegistry.GetGames(GameInfo.StatusCode.Available)[1]);
            Assert.AreSame(g2, myRegistry.GetGames(GameInfo.StatusCode.Complete)[0]);
            Assert.AreSame(g3, myRegistry.GetGames(GameInfo.StatusCode.Cancelled)[0]);
            Assert.AreSame(g4, myRegistry.GetGames(GameInfo.StatusCode.NotInitialized)[0]);

            GameInfo g5 = myRegistry.RegisterGame(id1, "Game5", 10, 2);
            myRegistry.ChangeGameStatus(g5.GameId, GameInfo.StatusCode.InProgress);
            Assert.AreEqual(2, myRegistry.GetGames(GameInfo.StatusCode.Available).Count);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.Complete).Count);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.Cancelled).Count);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.NotInitialized).Count);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.InProgress).Count);
            Assert.AreSame(g0, myRegistry.GetGames(GameInfo.StatusCode.Available)[0]);
            Assert.AreSame(g1, myRegistry.GetGames(GameInfo.StatusCode.Available)[1]);
            Assert.AreSame(g2, myRegistry.GetGames(GameInfo.StatusCode.Complete)[0]);
            Assert.AreSame(g3, myRegistry.GetGames(GameInfo.StatusCode.Cancelled)[0]);
            Assert.AreSame(g4, myRegistry.GetGames(GameInfo.StatusCode.NotInitialized)[0]);
            Assert.AreSame(g5, myRegistry.GetGames(GameInfo.StatusCode.InProgress)[0]);

            Registry.Instance.ChangeGameStatus(g0.GameId, GameInfo.StatusCode.Complete);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.Available).Count);
            Assert.AreEqual(2, myRegistry.GetGames(GameInfo.StatusCode.Complete).Count);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.Cancelled).Count);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.NotInitialized).Count);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.InProgress).Count);
            Assert.AreSame(g1, myRegistry.GetGames(GameInfo.StatusCode.Available)[0]);
            Assert.AreSame(g0, myRegistry.GetGames(GameInfo.StatusCode.Complete)[0]);
            Assert.AreSame(g2, myRegistry.GetGames(GameInfo.StatusCode.Complete)[1]);
            Assert.AreSame(g3, myRegistry.GetGames(GameInfo.StatusCode.Cancelled)[0]);
            Assert.AreSame(g4, myRegistry.GetGames(GameInfo.StatusCode.NotInitialized)[0]);
            Assert.AreSame(g5, myRegistry.GetGames(GameInfo.StatusCode.InProgress)[0]);

            Registry.Instance.ChangeGameStatus(g1.GameId, GameInfo.StatusCode.Cancelled);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Available).Count);
            Assert.AreEqual(2, myRegistry.GetGames(GameInfo.StatusCode.Complete).Count);
            Assert.AreEqual(2, myRegistry.GetGames(GameInfo.StatusCode.Cancelled).Count);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.NotInitialized).Count);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.InProgress).Count);
            Assert.AreSame(g0, myRegistry.GetGames(GameInfo.StatusCode.Complete)[0]);
            Assert.AreSame(g2, myRegistry.GetGames(GameInfo.StatusCode.Complete)[1]);
            Assert.AreSame(g1, myRegistry.GetGames(GameInfo.StatusCode.Cancelled)[0]);
            Assert.AreSame(g3, myRegistry.GetGames(GameInfo.StatusCode.Cancelled)[1]);
            Assert.AreSame(g4, myRegistry.GetGames(GameInfo.StatusCode.NotInitialized)[0]);
            Assert.AreSame(g5, myRegistry.GetGames(GameInfo.StatusCode.InProgress)[0]);

            GameInfo rg0 = myRegistry.GetGameInfo(g0.GameId);
            Assert.IsNotNull(rg0);
            Assert.AreEqual(g0.GameId, rg0.GameId);
            Assert.AreEqual(g0.Label, rg0.Label);
            Assert.AreEqual(g0.MaxPlayers, rg0.MaxPlayers);

            GameInfo rg5 = myRegistry.GetGameInfo(g5.GameId);
            Assert.IsNotNull(rg5);
            Assert.AreEqual(g5.GameId, rg5.GameId);
            Assert.AreEqual(g5.Label, rg5.Label);
            Assert.AreEqual(g5.MaxPlayers, rg5.MaxPlayers);

            GameInfo rg2 = myRegistry.GetGameInfo(g2.GameId);
            Assert.IsNotNull(rg2);
            Assert.AreEqual(g2.GameId, rg2.GameId);
            Assert.AreEqual(g2.Label, rg2.Label);
            Assert.AreEqual(g2.MaxPlayers, rg2.MaxPlayers);

            GameInfo rgBad = myRegistry.GetGameInfo(-1);
            Assert.IsNull(rgBad);

            for (int i = 0; i < 90; i++)
            {
                Thread.Sleep(1000);
                myRegistry.AmAlive(id1);
                myRegistry.AmAlive(id2);
                myRegistry.AmAlive(id3);
                Assert.AreEqual(1, myRegistry.GetGameManagers().Count);
                Assert.AreEqual(2, myRegistry.GetPlayers().Count);
            }

            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Available).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Complete).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Cancelled).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.NotInitialized).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.InProgress).Count);

            g0 = myRegistry.RegisterGame(id2, "Game0", 10, 2);
            Assert.IsNull(g0);

            g0 = myRegistry.RegisterGame(id1, null, 10, 2);
            Assert.IsNull(g0);

            g0 = myRegistry.RegisterGame(id1, "Game0", 10, 2);
            myRegistry.ChangeGameStatus(g0.GameId, GameInfo.StatusCode.Available);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.Available).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Complete).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Cancelled).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.NotInitialized).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.InProgress).Count);
            Assert.AreSame(g0, myRegistry.GetGames(GameInfo.StatusCode.Available)[0]);

            g1 = myRegistry.RegisterGame(id1, "Game1", 15, 5);
            myRegistry.ChangeGameStatus(g1.GameId, GameInfo.StatusCode.Available);
            Assert.AreEqual(2, myRegistry.GetGames(GameInfo.StatusCode.Available).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Complete).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Cancelled).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.NotInitialized).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.InProgress).Count);
            Assert.AreSame(g0, myRegistry.GetGames(GameInfo.StatusCode.Available)[0]);
            Assert.AreSame(g1, myRegistry.GetGames(GameInfo.StatusCode.Available)[1]);

            for (int i = 0; i < 90; i++)
            {
                Thread.Sleep(1000);
                myRegistry.AmAlive(id1);
                myRegistry.AmAlive(id2);
                myRegistry.AmAlive(id3);
                myRegistry.GameAmAlive(g0.GameId);
                myRegistry.GameAmAlive(g1.GameId);
                Assert.AreEqual(1, myRegistry.GetGameManagers().Count);
                Assert.AreEqual(2, myRegistry.GetPlayers().Count);
                Assert.AreEqual(2, myRegistry.GetGames(GameInfo.StatusCode.Available).Count);
                Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Complete).Count);
                Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Cancelled).Count);
                Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.NotInitialized).Count);
                Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.InProgress).Count);
                Assert.AreSame(g0, myRegistry.GetGames(GameInfo.StatusCode.Available)[0]);
                Assert.AreSame(g1, myRegistry.GetGames(GameInfo.StatusCode.Available)[1]);
            }

            myRegistry.SaveToFile("TestGames.txt");

            Registry.TakeDown();
            myRegistry = Registry.Instance;
            Assert.AreEqual(0, myRegistry.GetGameManagers().Count);
            Assert.AreEqual(0, myRegistry.GetPlayers().Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Available).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Complete).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Cancelled).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.NotInitialized).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.InProgress).Count);

            myRegistry.LoadFromFile("testGames.txt");
            Assert.AreEqual(1, myRegistry.GetGameManagers().Count);
            Assert.AreEqual(2, myRegistry.GetPlayers().Count);
            Assert.AreEqual(2, myRegistry.GetGames(GameInfo.StatusCode.Available).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Complete).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Cancelled).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.NotInitialized).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.InProgress).Count);

            g2 = myRegistry.RegisterGame(id1, "Game2", 10, 2);
            myRegistry.ChangeGameStatus(g2.GameId, GameInfo.StatusCode.Complete);
            g3 = myRegistry.RegisterGame(id1, "Game3", 10, 2);
            myRegistry.ChangeGameStatus(g3.GameId, GameInfo.StatusCode.Cancelled);
            g4 = myRegistry.RegisterGame(id1, "Game4", 10, 2);
            g5 = myRegistry.RegisterGame(id1, "Game5", 19, 4);
            myRegistry.ChangeGameStatus(g5.GameId, GameInfo.StatusCode.InProgress);
            Assert.AreEqual(2, myRegistry.GetGames(GameInfo.StatusCode.Available).Count);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.Complete).Count);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.Cancelled).Count);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.NotInitialized).Count);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.InProgress).Count);

            myRegistry.Save();

            Registry.TakeDown();
            myRegistry = Registry.Instance;
            myRegistry.LoadFromFile("TestGames.txt");
            Assert.AreEqual(1, myRegistry.GetGameManagers().Count);
            Assert.AreEqual(2, myRegistry.GetPlayers().Count);
            Assert.AreEqual(2, myRegistry.GetGames(GameInfo.StatusCode.Available).Count);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.Complete).Count);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.Cancelled).Count);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.NotInitialized).Count);
            Assert.AreEqual(1, myRegistry.GetGames(GameInfo.StatusCode.InProgress).Count);

            Thread.Sleep(90000);
            Assert.AreEqual(0, myRegistry.GetGameManagers().Count);
            Assert.AreEqual(0, myRegistry.GetPlayers().Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Available).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Complete).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.Cancelled).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.NotInitialized).Count);
            Assert.AreEqual(0, myRegistry.GetGames(GameInfo.StatusCode.InProgress).Count);

        }

    }
}
