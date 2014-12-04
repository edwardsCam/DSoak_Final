using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SharedObjects;

namespace SharedObjectTesting
{
    [TestClass]
    public class ProcessDataTester
    {
        [TestMethod]
        public void ProcessData_TestEverything()
        {
            ProcessData data = new ProcessData();
            Assert.AreEqual(0, data.GameId);
            Assert.AreEqual(0, data.ProcessId);
            Assert.AreEqual(ProcessData.PossibleProcessType.Unknown, data.ProcessType);
            Assert.AreEqual(0, data.LifePoints);
            Assert.AreEqual(0, data.HitPoints);
            Assert.AreEqual(0, data.NumberOfPennies);
            Assert.AreEqual(0, data.NumberOfUnfilledBalloon);
            Assert.AreEqual(0, data.NumberOfFilledBalloon);
            Assert.AreEqual(0, data.NumberOfUnraisedUmbrellas);
            Assert.IsFalse(data.HasUmbrellaRaised);

            data = new ProcessData()
            {
                GameId = 10,
                ProcessId = 11,
                ProcessType = ProcessData.PossibleProcessType.Thief,
                LifePoints = 12,
                HitPoints = 13,
                NumberOfPennies = 14,
                NumberOfUnfilledBalloon = 15,
                NumberOfFilledBalloon = 16,
                NumberOfUnraisedUmbrellas = 17,
                HasUmbrellaRaised = true
            };

            Assert.AreEqual(10, data.GameId);
            Assert.AreEqual(11, data.ProcessId);
            Assert.AreEqual(ProcessData.PossibleProcessType.Thief, data.ProcessType);
            Assert.AreEqual(12, data.LifePoints);
            Assert.AreEqual(13, data.HitPoints);
            Assert.AreEqual(14, data.NumberOfPennies);
            Assert.AreEqual(15, data.NumberOfUnfilledBalloon);
            Assert.AreEqual(16, data.NumberOfFilledBalloon);
            Assert.AreEqual(17, data.NumberOfUnraisedUmbrellas);
            Assert.IsTrue(data.HasUmbrellaRaised);
        }
    }
}
