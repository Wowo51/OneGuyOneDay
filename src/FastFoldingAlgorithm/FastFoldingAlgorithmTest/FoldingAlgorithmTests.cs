using Microsoft.VisualStudio.TestTools.UnitTesting;
using FastFoldingAlgorithm;
using System;
using System.Collections.Generic;

namespace FastFoldingAlgorithmTest
{
    [TestClass]
    public class FoldingAlgorithmTests
    {
        private FoldingAlgorithm foldingAlgorithm = new FoldingAlgorithm();
        [TestInitialize]
        public void Setup()
        {
            foldingAlgorithm = new FoldingAlgorithm();
        }

        [TestMethod]
        public void DetectApproximatePeriod_NullInput_ReturnsEmptyList()
        {
            double[]? timeSeries = null;
            List<int> result = foldingAlgorithm.DetectApproximatePeriod(timeSeries, 1, 5, 0.5);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void DetectApproximatePeriod_EmptyInput_ReturnsEmptyList()
        {
            double[] timeSeries = new double[0];
            List<int> result = foldingAlgorithm.DetectApproximatePeriod(timeSeries, 1, 5, 0.5);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void DetectApproximatePeriod_AllPeriodsAboveThreshold_ReturnsAllPeriods()
        {
            double[] timeSeries = new double[]
            {
                1,
                1,
                1,
                1,
                1,
                1
            };
            int minPeriod = 1;
            int maxPeriod = 3;
            double threshold = 0.5;
            List<int> result = foldingAlgorithm.DetectApproximatePeriod(timeSeries, minPeriod, maxPeriod, threshold);
            CollectionAssert.AreEqual(new List<int> { 1, 2, 3 }, result);
        }

        [TestMethod]
        public void DetectApproximatePeriod_NoPeriodsAboveThreshold_ReturnsEmptyList()
        {
            double[] timeSeries = new double[]
            {
                1,
                1,
                1,
                1,
                1,
                1
            };
            int minPeriod = 1;
            int maxPeriod = 4;
            double threshold = 10.0;
            List<int> result = foldingAlgorithm.DetectApproximatePeriod(timeSeries, minPeriod, maxPeriod, threshold);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void FoldTimeSeries_CorrectFolding()
        {
            double[] timeSeries = new double[]
            {
                1,
                2,
                3,
                4,
                5
            };
            int period = 3;
            double[] expected = new double[]
            {
                5,
                7,
                3
            };
            double[] result = foldingAlgorithm.FoldTimeSeries(timeSeries, period);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DetectApproximatePeriod_LargeSyntheticData()
        {
            int dataSize = 10000;
            double[] timeSeries = new double[dataSize];
            int simulatedPeriod = 50;
            Random random = new Random(42);
            for (int i = 0; i < dataSize; i++)
            {
                double value = (i % simulatedPeriod == 0) ? 10 : 1;
                value += random.NextDouble();
                timeSeries[i] = value;
            }

            int minPeriod = 40;
            int maxPeriod = 60;
            double threshold = 150;
            List<int> result = foldingAlgorithm.DetectApproximatePeriod(timeSeries, minPeriod, maxPeriod, threshold);
            Assert.IsTrue(result.Contains(simulatedPeriod));
        }
    }
}