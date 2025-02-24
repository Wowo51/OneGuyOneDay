using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CristianAlgorithmSync;

namespace CristianAlgorithmSyncTest
{
    [TestClass]
    public class MultipleSynchronizationTests
    {
        [TestMethod]
        public void Test_SynchronizeTime_RandomDelays()
        {
            Random random = new Random(42);
            CristianSynchronizer synchronizer = new CristianSynchronizer();
            DateTime fixedTime = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            for (int i = 0; i < 30; i++)
            {
                int delayMs = random.Next(0, 201);
                FakeTimeServer fakeServer = new FakeTimeServer(fixedTime, delayMs);
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
        }
    }

    internal class FakeTimeServer : ITimeServer
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
                System.Threading.Thread.Sleep(delayMilliseconds);
            }

            return fixedTime;
        }
    }
}