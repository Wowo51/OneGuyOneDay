using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OddsAlgorithm;

namespace OddsAlgorithmTest
{
    [TestClass]
    public class BrussAlgorithmTests
    {
        [TestMethod]
        public void ComputeThresholdIndex_NullInput_ReturnsNegative()
        {
            BrussAlgorithm algorithm = new BrussAlgorithm();
            int result = algorithm.ComputeThresholdIndex(null);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void ComputeThresholdIndex_EmptyInput_ReturnsNegative()
        {
            BrussAlgorithm algorithm = new BrussAlgorithm();
            List<double> emptyList = new List<double>();
            int result = algorithm.ComputeThresholdIndex(emptyList);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void ComputeThresholdIndex_ValidProbabilities_ReturnsCorrectThreshold()
        {
            BrussAlgorithm algorithm = new BrussAlgorithm();
            List<double> probabilities = new List<double>()
            {
                0.3,
                0.3,
                0.3,
                0.3,
                0.3
            };
            int result = algorithm.ComputeThresholdIndex(probabilities);
            // Calculation: Starting from end, odds ~0.4286, then sum becomes ~0.4286, ~0.8571, and at index 2 sum >= 1.0.
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void ComputeThresholdIndex_AllOnes_ReturnsLastIndex()
        {
            BrussAlgorithm algorithm = new BrussAlgorithm();
            List<double> probabilities = new List<double>()
            {
                1.0,
                1.0,
                1.0
            };
            int result = algorithm.ComputeThresholdIndex(probabilities);
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void CreateUniformProbabilityList_CreatesListWithCorrectValues()
        {
            BrussAlgorithm algorithm = new BrussAlgorithm();
            int length = 5;
            double eventProbability = 0.4;
            IList<double> list = algorithm.CreateUniformProbabilityList(length, eventProbability);
            Assert.AreEqual(length, list.Count);
            for (int i = 0; i < length; i++)
            {
                Assert.AreEqual(eventProbability, list[i]);
            }
        }

        [TestMethod]
        public void PredictLastOccurrence_NullOutcomes_ReturnsNegative()
        {
            BrussAlgorithm algorithm = new BrussAlgorithm();
            int result = algorithm.PredictLastOccurrence(null, 0.3);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void PredictLastOccurrence_EmptyOutcomes_ReturnsNegative()
        {
            BrussAlgorithm algorithm = new BrussAlgorithm();
            List<bool> outcomes = new List<bool>();
            int result = algorithm.PredictLastOccurrence(outcomes, 0.3);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void PredictLastOccurrence_AllFalse_ReturnsNegative()
        {
            BrussAlgorithm algorithm = new BrussAlgorithm();
            List<bool> outcomes = new List<bool>()
            {
                false,
                false,
                false,
                false,
                false
            };
            int result = algorithm.PredictLastOccurrence(outcomes, 0.3);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void PredictLastOccurrence_SingleTrue_ReturnsCorrectIndex()
        {
            BrussAlgorithm algorithm = new BrussAlgorithm();
            // For 10 trials with eventProbability 0.3, the threshold is computed as 7.
            // This test places a true value after the threshold.
            List<bool> outcomes = new List<bool>()
            {
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                true,
                false
            };
            int result = algorithm.PredictLastOccurrence(outcomes, 0.3);
            Assert.AreEqual(8, result);
        }

        [TestMethod]
        public void PredictLastOccurrence_AllTrue_ReturnsThresholdIndex()
        {
            BrussAlgorithm algorithm = new BrussAlgorithm();
            // For 5 trials with eventProbability 0.5, odds equal 1 so threshold is at last index.
            List<bool> outcomes = new List<bool>()
            {
                true,
                true,
                true,
                true,
                true
            };
            int result = algorithm.PredictLastOccurrence(outcomes, 0.5);
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void PredictLastOccurrence_LargeDataset_AppliesSyntheticTest()
        {
            BrussAlgorithm algorithm = new BrussAlgorithm();
            int length = 100;
            double eventProbability = 0.2;
            List<bool> outcomes = new List<bool>();
            for (int i = 0; i < length; i++)
            {
                outcomes.Add(false);
            }

            // Compute threshold based on uniform probability list.
            IList<double> uniform = algorithm.CreateUniformProbabilityList(length, eventProbability);
            int threshold = algorithm.ComputeThresholdIndex(uniform);
            int trueIndex = threshold + 2;
            if (trueIndex < length)
            {
                outcomes[trueIndex] = true;
            }
            else
            {
                outcomes[length - 1] = true;
                trueIndex = length - 1;
            }

            int result = algorithm.PredictLastOccurrence(outcomes, eventProbability);
            Assert.AreEqual(trueIndex, result);
        }

        [TestMethod]
        public void PredictLastOccurrence_TrueBeforeThreshold_Ignored()
        {
            BrussAlgorithm algorithm = new BrussAlgorithm();
            int length = 10;
            // For eventProbability = 0.4, the threshold should be around index 8.
            List<bool> outcomes = new List<bool>(new bool[length]);
            // Set a true value before the threshold.
            outcomes[5] = true;
            // Set a true value after the threshold.
            outcomes[8] = true;
            int result = algorithm.PredictLastOccurrence(outcomes, 0.4);
            // Expect the method to return the true value encountered after the threshold.
            Assert.AreEqual(8, result);
        }

        [TestMethod]
        public void ComputeThresholdIndex_AllZeros_ReturnsCount()
        {
            BrussAlgorithm algorithm = new BrussAlgorithm();
            List<double> probabilities = new List<double>()
            {
                0.0,
                0.0,
                0.0,
                0.0
            };
            int result = algorithm.ComputeThresholdIndex(probabilities);
            Assert.AreEqual(probabilities.Count, result);
        }

        [TestMethod]
        public void CreateUniformProbabilityList_ZeroLength_ReturnsEmptyList()
        {
            BrussAlgorithm algorithm = new BrussAlgorithm();
            IList<double> list = algorithm.CreateUniformProbabilityList(0, 0.5);
            Assert.AreEqual(0, list.Count);
        }
    }
}