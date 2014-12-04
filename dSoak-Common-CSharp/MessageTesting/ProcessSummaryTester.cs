using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Messages;
using SharedObjects;

namespace MessageTesting
{
    [TestClass]
    public class ProcessSummaryTester
    {
        [TestInitialize]
        public void Setup()
        {
            MessageNumber.LocalProcessId = 100;
        }

        [TestMethod]
        public void ProcessSummary_CheckEverything()
        {
            ProcessSummary msg1 = new ProcessSummary();
            Assert.IsNotNull(msg1.MessageNr);
            Assert.AreEqual(100, msg1.MessageNr.ProcessId);
            Assert.IsTrue(msg1.MessageNr.SeqNumber > 0);
            Assert.AreEqual(msg1.MessageNr, msg1.ConvId);

            ProcessData data = new ProcessData()
                {
                    GameId = 102,
                    ProcessId = 10,
                    ProcessType = ProcessData.PossibleProcessType.Player,
                    LifePoints = 200,
                    HitPoints = 10,
                    NumberOfPennies = 11,
                    NumberOfUnfilledBalloon = 12,
                    NumberOfFilledBalloon = 13,
                    NumberOfUnraisedUmbrellas = 14,
                    HasUmbrellaRaised = true
                };
            ProcessSummary msg2 = new ProcessSummary() { Data = data };
            Assert.IsNotNull(msg2.MessageNr);
            Assert.AreEqual(100, msg2.MessageNr.ProcessId);
            Assert.AreEqual(msg1.MessageNr.SeqNumber + 1, msg2.MessageNr.SeqNumber);
            Assert.AreEqual(msg2.MessageNr, msg2.ConvId);
            Assert.IsNotNull(msg2.Data);
            Assert.AreSame(data, msg2.Data);
 
            byte[] bytes = msg2.Encode();
            string tmp = Encoding.ASCII.GetString(bytes);

            Message msg3 = Message.Decode(bytes);
            Assert.IsTrue(msg3 is ProcessSummary);
            ProcessSummary msg4 = msg3 as ProcessSummary;
            Assert.AreEqual(msg2.MessageNr, msg4.MessageNr);
            Assert.AreEqual(msg2.ConvId, msg4.ConvId);
            Assert.IsNotNull(msg4.Data);
            Assert.AreEqual(msg2.Data.GameId, msg4.Data.GameId);
            Assert.AreEqual(msg2.Data.ProcessId, msg4.Data.ProcessId);
            Assert.AreEqual(msg2.Data.ProcessType, msg4.Data.ProcessType);
            Assert.AreEqual(msg2.Data.LifePoints, msg4.Data.LifePoints);
            Assert.AreEqual(msg2.Data.HitPoints, msg4.Data.HitPoints);
            Assert.AreEqual(msg2.Data.NumberOfPennies, msg4.Data.NumberOfPennies);
            Assert.AreEqual(msg2.Data.NumberOfUnfilledBalloon, msg4.Data.NumberOfUnfilledBalloon);
            Assert.AreEqual(msg2.Data.NumberOfFilledBalloon, msg4.Data.NumberOfFilledBalloon);
            Assert.AreEqual(msg2.Data.NumberOfUnraisedUmbrellas, msg4.Data.NumberOfUnraisedUmbrellas);
            Assert.AreEqual(msg2.Data.HasUmbrellaRaised, msg4.Data.HasUmbrellaRaised);
        }
    }
}
