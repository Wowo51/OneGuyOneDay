using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KarnsAlgorithm;

namespace KarnsAlgorithmTest
{
    [TestClass]
    public class RttEstimatorTests
    {
        [TestMethod]
        public void TestInitialSample()
        {
            RttEstimator estimator = new RttEstimator();
            TimeSpan sample = TimeSpan.FromMilliseconds(100);
            estimator.AddRttSample(sample, false);
            Assert.AreEqual(100.0, estimator.EstimatedRTT, 0.001);
            Assert.AreEqual(50.0, estimator.Deviation, 0.001);
            Assert.AreEqual(300.0, estimator.RetransmissionTimeout, 0.001);
        }

        [TestMethod]
        public void TestSubsequentSamples()
        {
            RttEstimator estimator = new RttEstimator();
            estimator.AddRttSample(TimeSpan.FromMilliseconds(100), false);
            estimator.AddRttSample(TimeSpan.FromMilliseconds(120), false);
            // Expected: EstimatedRTT = 0.875*100 + 0.125*120 = 102.5
            // Deviation = 0.75*50 + 0.25*|120-100| = 42.5
            Assert.AreEqual(102.5, estimator.EstimatedRTT, 0.001);
            Assert.AreEqual(42.5, estimator.Deviation, 0.001);
            Assert.AreEqual(102.5 + 4.0 * 42.5, estimator.RetransmissionTimeout, 0.001);
        }

        [TestMethod]
        public void TestRetransmissionIgnored()
        {
            RttEstimator estimator = new RttEstimator();
            estimator.AddRttSample(TimeSpan.FromMilliseconds(100), false);
            double initialEstimatedRTT = estimator.EstimatedRTT;
            double initialDeviation = estimator.Deviation;
            estimator.AddRttSample(TimeSpan.FromMilliseconds(300), true);
            Assert.AreEqual(initialEstimatedRTT, estimator.EstimatedRTT, 0.001);
            Assert.AreEqual(initialDeviation, estimator.Deviation, 0.001);
        }

        [TestMethod]
        public void TestSyntheticSamples()
        {
            RttEstimator estimator = new RttEstimator();
            System.Random random = new System.Random(42);
            for (int i = 0; i < 100; i++)
            {
                double sampleMs = random.NextDouble() * 100 + 50; // sample between 50 and 150 ms
                estimator.AddRttSample(System.TimeSpan.FromMilliseconds(sampleMs), false);
            }

            Assert.IsTrue(estimator.EstimatedRTT >= 50.0 && estimator.EstimatedRTT <= 150.0);
            Assert.IsTrue(estimator.Deviation >= 0);
        }
    }
}