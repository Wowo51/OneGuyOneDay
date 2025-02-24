using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApproximateCountingAlgorithm;

namespace ApproximateCountingAlgorithmTest
{
    [TestClass]
    public class MorrisCounterTests
    {
        [TestMethod]
        public void TestInitialValue()
        {
            MorrisCounter counter = new MorrisCounter();
            double initialCount = counter.GetCount();
            Assert.AreEqual(0.0, initialCount, "Initial count should be zero.");
        }

        [TestMethod]
        public void TestFirstIncrement()
        {
            MorrisCounter counter = new MorrisCounter();
            counter.Increment(); // First increment, probability is always 1.
            double countAfterFirst = counter.GetCount();
            Assert.AreEqual(1.0, countAfterFirst, "After first increment, count should be 1.");
        }

        [TestMethod]
        public void TestMonotonicity()
        {
            MorrisCounter counter = new MorrisCounter();
            double previousCount = counter.GetCount();
            for (int i = 0; i < 100; i++)
            {
                counter.Increment();
                double currentCount = counter.GetCount();
                Assert.IsTrue(currentCount >= previousCount, "Counter should be non-decreasing. Previous count: " + previousCount + ", current count: " + currentCount);
                previousCount = currentCount;
            }
        }

        [TestMethod]
        public void TestReset()
        {
            MorrisCounter counter = new MorrisCounter();
            counter.Increment();
            counter.Increment();
            Assert.IsTrue(counter.GetCount() > 0, "Count should be greater than zero after increments.");
            counter.Reset();
            double countAfterReset = counter.GetCount();
            Assert.AreEqual(0.0, countAfterReset, "After reset, count should be zero.");
        }

        [TestMethod]
        public void TestStatisticalApproximation()
        {
            int numberOfSimulations = 1000;
            int incrementsPerSimulation = 50;
            double sumCounts = 0.0;
            for (int i = 0; i < numberOfSimulations; i++)
            {
                MorrisCounter counter = new MorrisCounter();
                for (int j = 0; j < incrementsPerSimulation; j++)
                {
                    counter.Increment();
                }

                sumCounts += counter.GetCount();
            }

            double averageCount = sumCounts / numberOfSimulations;
            double tolerance = incrementsPerSimulation * 0.15; // Allow 15% margin
            Assert.IsTrue(Math.Abs(averageCount - incrementsPerSimulation) <= tolerance, "Average count " + averageCount + " is not within tolerance " + tolerance + " of expected " + incrementsPerSimulation + ".");
        }
    }
}