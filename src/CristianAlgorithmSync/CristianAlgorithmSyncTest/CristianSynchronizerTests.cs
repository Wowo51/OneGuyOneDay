using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CristianAlgorithmSync;

namespace CristianAlgorithmSyncTest
{
    [TestClass]
    public class CristianSynchronizerTests
    {
        [TestMethod]
        public void Test_NullTimeServer_ReturnsCurrentTime()
        {
            DateTime before = DateTime.UtcNow;
            CristianSynchronizer synchronizer = new CristianSynchronizer();
            DateTime result = synchronizer.SynchronizeTime(null);
            DateTime after = DateTime.UtcNow;
            Assert.IsTrue(result >= before && result <= after, "Result should be between before and after times.");
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(20)]
        [DataRow(50)]
        [DataRow(100)]
        public void Test_SynchronizeTime_WithDelay(int delayMs)
        {
            DateTime fixedTime = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            FakeTimeServer fakeServer = new FakeTimeServer(fixedTime, delayMs);
            CristianSynchronizer synchronizer = new CristianSynchronizer();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            DateTime result = synchronizer.SynchronizeTime(fakeServer);
            stopwatch.Stop();
            TimeSpan measuredRoundTrip = stopwatch.Elapsed;
            TimeSpan expectedOffset = new TimeSpan(measuredRoundTrip.Ticks / 2);
            TimeSpan actualOffset = result - fixedTime;
            double toleranceMilliseconds = Math.Max(5.0, expectedOffset.TotalMilliseconds * 0.2);
            double difference = Math.Abs(actualOffset.TotalMilliseconds - expectedOffset.TotalMilliseconds);
            Assert.IsTrue(difference <= toleranceMilliseconds, $"For delay {delayMs}ms, expected offset approximately {expectedOffset.TotalMilliseconds}ms, but got {actualOffset.TotalMilliseconds}ms.");
        }

        private class FakeTimeServer : ITimeServer
        {
            private DateTime fixedTime;
            private int delayMilliseconds;
            public FakeTimeServer(DateTime fixedTime, int delayMilliseconds)
            {
                this.fixedTime = fixedTime;
                this.delayMilliseconds = delayMilliseconds;
            }

            public DateTime GetServerTime()
            {
                if (delayMilliseconds > 0)
                {
                    Thread.Sleep(delayMilliseconds);
                }

                return fixedTime;
            }
        }
    }
}