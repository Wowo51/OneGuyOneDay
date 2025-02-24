using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContextTreeWeighting;
using System;
using System.Text;

namespace ContextTreeWeightingTest
{
    [TestClass]
    public class ContextTreeWeightingTests
    {
        [TestMethod]
        public void TestInitialProbability()
        {
            ContextTreeWeightingAlgorithm algorithm = new ContextTreeWeightingAlgorithm(5);
            double probability = algorithm.GetWeightedProbability("");
            Assert.AreEqual(1.0, probability, 1e-6, "Initial probability should be 1.0 for empty context.");
        }

        [TestMethod]
        public void TestSingleUpdateProbability()
        {
            ContextTreeWeightingAlgorithm algorithm = new ContextTreeWeightingAlgorithm(5);
            algorithm.Update("0", 0);
            double probability = algorithm.GetWeightedProbability("0");
            Assert.IsTrue(probability > 0.0 && probability < 1.0, "Probability should be between 0 and 1 after a single update.");
        }

        [TestMethod]
        public void TestMultipleUpdatesProbability()
        {
            ContextTreeWeightingAlgorithm algorithm = new ContextTreeWeightingAlgorithm(5);
            algorithm.Update("01", 0);
            algorithm.Update("01", 1);
            algorithm.Update("01", 1);
            double probability = algorithm.GetWeightedProbability("01");
            Assert.IsTrue(probability > 0.0 && probability <= 1.0, "Probability should be valid after multiple updates.");
        }

        [TestMethod]
        public void TestLongContext()
        {
            ContextTreeWeightingAlgorithm algorithm = new ContextTreeWeightingAlgorithm(3);
            string longContext = "010101";
            for (int i = 0; i < longContext.Length; i++)
            {
                int bit = (longContext[i] == '0') ? 0 : 1;
                algorithm.Update(longContext.Substring(i), bit);
            }

            double probability = algorithm.GetWeightedProbability(longContext);
            Assert.IsTrue(probability > 0.0 && probability <= 1.0, "Probability should be valid for long context when exceeding max depth.");
        }

        [TestMethod]
        public void TestSyntheticData()
        {
            ContextTreeWeightingAlgorithm algorithm = new ContextTreeWeightingAlgorithm(5);
            Random random = new Random(42);
            for (int i = 0; i < 1000; i++)
            {
                int length = random.Next(1, 10);
                StringBuilder contextBuilder = new StringBuilder();
                for (int j = 0; j < length; j++)
                {
                    char bit = (random.Next(0, 2) == 0) ? '0' : '1';
                    contextBuilder.Append(bit);
                }

                string context = contextBuilder.ToString();
                int updateBit = random.Next(0, 2);
                algorithm.Update(context, updateBit);
            }

            double probability = algorithm.GetWeightedProbability("101");
            Assert.IsTrue(probability > 0.0 && probability <= 1.0, "Probability should be valid for synthetic data.");
        }

        [TestMethod]
        public void TestNonBinaryContext()
        {
            ContextTreeWeightingAlgorithm algorithm = new ContextTreeWeightingAlgorithm(5);
            // 'x' is not a valid binary symbol so only the root count is affected.
            algorithm.Update("x", 0);
            double probabilityEmpty = algorithm.GetWeightedProbability("");
            double probabilityX = algorithm.GetWeightedProbability("x");
            Assert.AreEqual(probabilityEmpty, probabilityX, 1e-6, "Non binary context should not affect probability calculation compared to empty context.");
        }
    }
}