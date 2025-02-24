using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TruncatedExponentialBackoff;

namespace TruncatedExponentialBackoffTest
{
    [TestClass]
    public class TruncatedExponentialBackoffTests
    {
        [TestMethod]
        public void GetDelay_NegativeAttempt_ShouldTreatAsZero()
        {
            TimeSpan initialDelay = TimeSpan.FromMilliseconds(100);
            TimeSpan maxDelay = TimeSpan.FromMilliseconds(500);
            TruncatedBinaryExponentialBackoff backoff = new TruncatedBinaryExponentialBackoff(initialDelay, maxDelay);
            int iterations = 100;
            for (int i = 0; i < iterations; i++)
            {
                TimeSpan delay = backoff.GetDelay(-1);
                double delayMs = delay.TotalMilliseconds;
                Assert.IsTrue(delayMs >= 0 && delayMs <= initialDelay.TotalMilliseconds, "Delay out of expected range.");
            }
        }

        [TestMethod]
        public void GetDelay_WithinExponentialBound()
        {
            TimeSpan initialDelay = TimeSpan.FromMilliseconds(100);
            TimeSpan maxDelay = TimeSpan.FromMilliseconds(1000);
            TruncatedBinaryExponentialBackoff backoff = new TruncatedBinaryExponentialBackoff(initialDelay, maxDelay);
            double expectedBound = initialDelay.TotalMilliseconds * System.Math.Pow(2, 2);
            int iterations = 100;
            for (int i = 0; i < iterations; i++)
            {
                TimeSpan delay = backoff.GetDelay(2);
                double delayMs = delay.TotalMilliseconds;
                Assert.IsTrue(delayMs >= 0 && delayMs <= expectedBound, "Delay out of expected range for attempt 2.");
            }
        }

        [TestMethod]
        public void GetDelay_ExceedingMaxDelay_ShouldBeBoundedByMaxDelay()
        {
            TimeSpan initialDelay = TimeSpan.FromMilliseconds(100);
            TimeSpan maxDelay = TimeSpan.FromMilliseconds(500);
            TruncatedBinaryExponentialBackoff backoff = new TruncatedBinaryExponentialBackoff(initialDelay, maxDelay);
            int iterations = 100;
            for (int i = 0; i < iterations; i++)
            {
                TimeSpan delay = backoff.GetDelay(4);
                double delayMs = delay.TotalMilliseconds;
                Assert.IsTrue(delayMs >= 0 && delayMs <= maxDelay.TotalMilliseconds, "Delay exceeds max delay.");
            }
        }

        [TestMethod]
        public void Constructor_NegativeInitialDelay_ShouldSetToZero()
        {
            TimeSpan initialDelay = TimeSpan.FromMilliseconds(-50);
            TimeSpan maxDelay = TimeSpan.FromMilliseconds(100);
            TruncatedBinaryExponentialBackoff backoff = new TruncatedBinaryExponentialBackoff(initialDelay, maxDelay);
            TimeSpan delay = backoff.GetDelay(0);
            Assert.AreEqual(0, delay.TotalMilliseconds, "Delay should be zero when initialDelay is negative.");
        }

        [TestMethod]
        public void Constructor_MaxDelayLessThanInitialDelay_ShouldAdjustMaxDelay()
        {
            TimeSpan initialDelay = TimeSpan.FromMilliseconds(100);
            TimeSpan maxDelay = TimeSpan.FromMilliseconds(50);
            TruncatedBinaryExponentialBackoff backoff = new TruncatedBinaryExponentialBackoff(initialDelay, maxDelay);
            int iterations = 50;
            for (int i = 0; i < iterations; i++)
            {
                TimeSpan delay = backoff.GetDelay(2);
                double delayMs = delay.TotalMilliseconds;
                Assert.IsTrue(delayMs >= 0 && delayMs <= initialDelay.TotalMilliseconds, "Delay should be bounded by adjusted maxDelay.");
            }
        }

        [TestMethod]
        public async Task DelayAsync_CompletesSuccessfully()
        {
            TimeSpan initialDelay = TimeSpan.FromMilliseconds(100);
            TimeSpan maxDelay = TimeSpan.FromMilliseconds(500);
            TruncatedBinaryExponentialBackoff backoff = new TruncatedBinaryExponentialBackoff(initialDelay, maxDelay);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            await backoff.DelayAsync(3);
            stopwatch.Stop();
            double expectedBound = System.Math.Min(initialDelay.TotalMilliseconds * System.Math.Pow(2, 3), maxDelay.TotalMilliseconds);
            Assert.IsTrue(stopwatch.ElapsedMilliseconds <= expectedBound + 50, "DelayAsync took longer than expected.");
        }
    }
}