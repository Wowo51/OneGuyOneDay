using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using OddsBrussAlgorithm;

namespace OddsBrussAlgorithmTest
{
    [TestClass]
    public class OddsAlgorithmTests
    {
        [TestMethod]
        public void TestRun_NullOutcomes()
        {
            double[] probabilities = new double[]
            {
                0.3,
                0.7
            };
            OddsAlgorithm algorithm = new OddsAlgorithm(probabilities);
            int result = algorithm.Run(null);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void TestRun_EmptyProbabilities()
        {
            double[] probabilities = new double[0];
            OddsAlgorithm algorithm = new OddsAlgorithm(probabilities);
            List<bool> outcomes = new List<bool>();
            outcomes.Add(false);
            int result = algorithm.Run(outcomes);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void TestRun_SingleCandidate()
        {
            double[] probabilities = new double[]
            {
                0.3
            };
            OddsAlgorithm algorithm = new OddsAlgorithm(probabilities);
            List<bool> outcomes = new List<bool>();
            outcomes.Add(false);
            int result = algorithm.Run(outcomes);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestRun_WithDistinguishedAfterThreshold()
        {
            double[] probabilities = new double[]
            {
                0.3,
                0.4,
                0.2,
                0.9,
                0.1
            };
            OddsAlgorithm algorithm = new OddsAlgorithm(probabilities);
            List<bool> outcomes = new List<bool>
            {
                false,
                false,
                false,
                true,
                false
            };
            int result = algorithm.Run(outcomes);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void TestRun_AllFalse()
        {
            double[] probabilities = new double[]
            {
                0.3,
                0.4,
                0.2,
                0.9,
                0.1
            };
            OddsAlgorithm algorithm = new OddsAlgorithm(probabilities);
            List<bool> outcomes = new List<bool>
            {
                false,
                false,
                false,
                false,
                false
            };
            int result = algorithm.Run(outcomes);
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void TestRun_ShortOutcomes()
        {
            double[] probabilities = new double[]
            {
                0.3,
                0.4,
                0.2,
                0.9,
                0.1
            };
            OddsAlgorithm algorithm = new OddsAlgorithm(probabilities);
            List<bool> outcomes = new List<bool>
            {
                false,
                false,
                false
            };
            int result = algorithm.Run(outcomes);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void TestReset()
        {
            double[] probabilities = new double[]
            {
                0.3,
                0.4,
                0.2,
                0.9,
                0.1
            };
            OddsAlgorithm algorithm = new OddsAlgorithm(probabilities);
            List<bool> outcomes1 = new List<bool>
            {
                false,
                false,
                false,
                true,
                false
            };
            int result1 = algorithm.Run(outcomes1);
            Assert.AreEqual(3, result1);
            algorithm.Reset();
            List<bool> outcomes2 = new List<bool>
            {
                false,
                false,
                false,
                false,
                false
            };
            int result2 = algorithm.Run(outcomes2);
            Assert.AreEqual(4, result2);
        }

        [TestMethod]
        public void TestLargeInput()
        {
            int size = 1000;
            double[] probabilities = new double[size];
            List<bool> outcomes = new List<bool>();
            for (int i = 0; i < size; i++)
            {
                probabilities[i] = 0.5;
                outcomes.Add(false);
            }

            OddsAlgorithm algorithm = new OddsAlgorithm(probabilities);
            int result = algorithm.Run(outcomes);
            Assert.AreEqual(size - 1, result);
        }

        [TestMethod]
        public void TestInvalidProbabilities()
        {
            double[] probabilities = new double[]
            {
                -0.1,
                1.2
            };
            OddsAlgorithm algorithm = new OddsAlgorithm(probabilities);
            List<bool> outcomes = new List<bool>
            {
                false,
                false
            };
            int result = algorithm.Run(outcomes);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void TestConstructor_WithNullProbabilities()
        {
            OddsAlgorithm algorithm = new OddsAlgorithm(null);
            List<bool> outcomes = new List<bool>
            {
                false
            };
            int result = algorithm.Run(outcomes);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void TestRun_ExtraOutcomes()
        {
            double[] probabilities = new double[]
            {
                0.3,
                0.9
            };
            OddsAlgorithm algorithm = new OddsAlgorithm(probabilities);
            List<bool> outcomes = new List<bool>
            {
                false,
                false,
                true,
                true
            };
            int result = algorithm.Run(outcomes);
            Assert.AreEqual(1, result);
        }
    }
}