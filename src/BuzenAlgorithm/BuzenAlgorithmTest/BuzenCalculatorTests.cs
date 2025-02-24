using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuzenAlgorithm;

namespace BuzenAlgorithmTest
{
    [TestClass]
    public class BuzenCalculatorTests
    {
        [TestMethod]
        public void Test_NullWeights_ReturnsExpected()
        {
            double result = BuzenCalculator.ComputeNormalizationConstant(0, null);
            Assert.AreEqual(1.0, result, 1e-9);
            result = BuzenCalculator.ComputeNormalizationConstant(5, null);
            Assert.AreEqual(0.0, result, 1e-9);
        }

        [TestMethod]
        public void Test_NegativeK_ReturnsZero()
        {
            List<double> weights = new List<double>
            {
                1.0,
                2.0,
                3.0
            };
            double result = BuzenCalculator.ComputeNormalizationConstant(-1, weights);
            Assert.AreEqual(0.0, result, 1e-9);
        }

        [TestMethod]
        public void Test_EmptyWeights_ReturnsExpected()
        {
            List<double> weights = new List<double>();
            double result = BuzenCalculator.ComputeNormalizationConstant(0, weights);
            Assert.AreEqual(1.0, result, 1e-9);
            result = BuzenCalculator.ComputeNormalizationConstant(3, weights);
            Assert.AreEqual(0.0, result, 1e-9);
        }

        [TestMethod]
        public void Test_SingleWeight()
        {
            List<double> weights = new List<double>
            {
                5.0
            };
            double result = BuzenCalculator.ComputeNormalizationConstant(0, weights);
            Assert.AreEqual(1.0, result, 1e-9);
            result = BuzenCalculator.ComputeNormalizationConstant(1, weights);
            Assert.AreEqual(5.0, result, 1e-9);
            result = BuzenCalculator.ComputeNormalizationConstant(2, weights);
            Assert.AreEqual(25.0, result, 1e-9);
        }

        [TestMethod]
        public void Test_MultipleWeights()
        {
            List<double> weights = new List<double>
            {
                2.0,
                3.0
            };
            double result = BuzenCalculator.ComputeNormalizationConstant(0, weights);
            Assert.AreEqual(1.0, result, 1e-9);
            result = BuzenCalculator.ComputeNormalizationConstant(1, weights);
            Assert.AreEqual(5.0, result, 1e-9);
            result = BuzenCalculator.ComputeNormalizationConstant(2, weights);
            Assert.AreEqual(19.0, result, 1e-9);
        }

        [TestMethod]
        public void Test_LargeDataSet()
        {
            int count = 1000;
            int K = 10;
            List<double> weights = new List<double>();
            Random random = new Random(12345);
            for (int i = 0; i < count; i++)
            {
                double value = random.NextDouble() * 0.9 + 0.1;
                weights.Add(value);
            }

            double result = BuzenCalculator.ComputeNormalizationConstant(K, weights);
            double expected = ComputeExpectedNormalizationConstant(K, weights);
            Assert.AreEqual(expected, result, 1e-6);
        }

        [TestMethod]
        public void Test_WeightsWithZeros()
        {
            List<double> weights = new List<double>
            {
                0.0,
                2.0,
                0.0,
                3.0
            };
            double result = BuzenCalculator.ComputeNormalizationConstant(0, weights);
            Assert.AreEqual(1.0, result, 1e-9);
            result = BuzenCalculator.ComputeNormalizationConstant(1, weights);
            Assert.AreEqual(5.0, result, 1e-9);
            result = BuzenCalculator.ComputeNormalizationConstant(2, weights);
            Assert.AreEqual(19.0, result, 1e-9);
        }

        [TestMethod]
        public void Test_NegativeWeights()
        {
            List<double> weights = new List<double>
            {
                -1.0,
                2.0
            };
            double result = BuzenCalculator.ComputeNormalizationConstant(0, weights);
            Assert.AreEqual(1.0, result, 1e-9);
            result = BuzenCalculator.ComputeNormalizationConstant(1, weights);
            Assert.AreEqual(1.0, result, 1e-9);
            result = BuzenCalculator.ComputeNormalizationConstant(2, weights);
            Assert.AreEqual(3.0, result, 1e-9);
        }

        [TestMethod]
        public void Test_ExcessiveK()
        {
            List<double> weights = new List<double>
            {
                1.5
            };
            double result = BuzenCalculator.ComputeNormalizationConstant(5, weights);
            Assert.AreEqual(7.59375, result, 1e-9);
        }

        private static double ComputeExpectedNormalizationConstant(int K, IList<double> weights)
        {
            double[] G = new double[K + 1];
            G[0] = 1.0;
            for (int i = 0; i < weights.Count; i++)
            {
                for (int k = 1; k <= K; k++)
                {
                    G[k] += weights[i] * G[k - 1];
                }
            }

            return G[K];
        }
    }
}