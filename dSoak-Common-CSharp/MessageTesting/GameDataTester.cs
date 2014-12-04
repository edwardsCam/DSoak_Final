using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Messages;
using SharedObjects;

namespace MessageTesting
{
    [TestClass]
    public class GameDataTester
    {
        [TestInitialize]
        public void Setup()
        {
            MessageNumber.LocalProcessId = 100;
        }

        [TestMethod]
        public void GameData_CheckEverything()
        {
            GameData msg1 = new GameData();
            Assert.IsNotNull(msg1.MessageNr);
            Assert.AreEqual(100, msg1.MessageNr.ProcessId);
            Assert.IsTrue(msg1.MessageNr.SeqNumber > 0);
            Assert.AreEqual(msg1.MessageNr, msg1.ConvId);

            GameInfo gameInfo = new GameInfo() { GameId = 102, Status = GameInfo.StatusCode.NotInitialized };
            List<ProcessData> processes = new List<ProcessData>()
                {
                    new ProcessData() { GameId = 102, ProcessId = 10, ProcessType = ProcessData.PossibleProcessType.Player },
                    new ProcessData() { GameId = 102, ProcessId = 20, ProcessType = ProcessData.PossibleProcessType.Player },
                    new ProcessData() { GameId = 102, ProcessId = 30, ProcessType = ProcessData.PossibleProcessType.Thief },
                };
            GameData msg2 = new GameData() { Info = gameInfo, Processes = processes };
            Assert.IsNotNull(msg2.MessageNr);
            Assert.AreEqual(100, msg2.MessageNr.ProcessId);
            Assert.AreEqual(msg1.MessageNr.SeqNumber + 1, msg2.MessageNr.SeqNumber);
            Assert.AreEqual(msg2.MessageNr, msg2.ConvId);
            Assert.IsNotNull(msg2.Info);
            Assert.AreSame(gameInfo, msg2.Info);
            Assert.IsNotNull(msg2.Processes);
            Assert.AreSame(processes, msg2.Processes);

            byte[] bytes = msg2.Encode();
            string tmp = Encoding.ASCII.GetString(bytes);

            Message msg3 = Message.Decode(bytes);
            Assert.IsTrue(msg3 is GameData);
            GameData msg4 = msg3 as GameData;
            Assert.AreEqual(msg2.MessageNr, msg4.MessageNr);
            Assert.AreEqual(msg2.ConvId, msg4.ConvId);
            Assert.IsNotNull(msg4.Info);
            Assert.AreEqual(msg2.Info.GameId, msg4.Info.GameId);
            Assert.AreEqual(msg2.Info.Status, msg4.Info.Status);
            Assert.IsNotNull(msg4.Processes);
            Assert.AreEqual(msg2.Processes.Count, msg4.Processes.Count);
            for (int i = 0; i < msg4.Processes.Count; i++)
            {
                Assert.AreEqual(msg2.Processes[i].GameId, msg4.Processes[i].GameId);
                Assert.AreEqual(msg2.Processes[i].ProcessId, msg4.Processes[i].ProcessId);
                Assert.AreEqual(msg2.Processes[i].ProcessType, msg4.Processes[i].ProcessType);
                Assert.AreEqual(msg2.Processes[i].LifePoints, msg4.Processes[i].LifePoints);
                Assert.AreEqual(msg2.Processes[i].HitPoints, msg4.Processes[i].HitPoints);
                Assert.AreEqual(msg2.Processes[i].NumberOfPennies, msg4.Processes[i].NumberOfPennies);
                Assert.AreEqual(msg2.Processes[i].NumberOfUnfilledBalloon, msg4.Processes[i].NumberOfUnfilledBalloon);
                Assert.AreEqual(msg2.Processes[i].NumberOfFilledBalloon, msg4.Processes[i].NumberOfFilledBalloon);
                Assert.AreEqual(msg2.Processes[i].NumberOfUnraisedUmbrellas, msg4.Processes[i].NumberOfUnraisedUmbrellas);
                Assert.AreEqual(msg2.Processes[i].HasUmbrellaRaised, msg4.Processes[i].HasUmbrellaRaised);
            }
        }
    }
}
