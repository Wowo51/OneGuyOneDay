using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DynamicTimeWarping;

namespace DynamicTimeWarpingTest
{
    [TestClass]
    public class DTWTests
    {
        [TestMethod]
        public void Compute_WithNullSequences_ReturnsZero()
        {
            double result = DTW.Compute(null, null);
            Assert.AreEqual(0, result, "DTW result for null sequences should be 0");
        }

        [TestMethod]
        public void Compute_WithEmptyArrays_ReturnsZero()
        {
            double[] seq1 = new double[0];
            double[] seq2 = new double[0];
            double result = DTW.Compute(seq1, seq2);
            Assert.AreEqual(0, result, "DTW result for empty arrays should be 0");
        }

        [TestMethod]
        public void Compute_WithIdenticalSequences_ReturnsZero()
        {
            double[] seq = new double[]
            {
                1.0,
                2.0,
                3.0
            };
            double result = DTW.Compute(seq, seq);
            Assert.AreEqual(0, result, "DTW result for identical sequences should be 0");
        }

        [TestMethod]
        public void Compute_WithKnownSequences_ReturnsExpected()
        {
            double[] seq1 = new double[]
            {
                1.0,
                2.0,
                3.0
            };
            double[] seq2 = new double[]
            {
                2.0,
                3.0,
                4.0
            };
            double expected = 2.0;
            double result = DTW.Compute(seq1, seq2);
            Assert.AreEqual(expected, result, 1e-6, "DTW result for known sequences should match expected value");
        }

        [TestMethod]
        public void Compute_WithDifferentLengthSequences_ReturnsExpected()
        {
            double[] seq1 = new double[]
            {
                1.0,
                2.0,
                3.0,
                4.0
            };
            double[] seq2 = new double[]
            {
                1.0,
                2.0,
                3.0
            };
            double expected = 1.0;
            double result = DTW.Compute(seq1, seq2);
            Assert.AreEqual(expected, result, 1e-6, "DTW result for different length sequences should match expected value");
        }

        [TestMethod]
        public void Compute_IsSymmetric()
        {
            double[] seq1 = new double[]
            {
                1.0,
                3.0,
                4.0,
                9.0
            };
            double[] seq2 = new double[]
            {
                1.0,
                2.0,
                5.0,
                8.0
            };
            double result1 = DTW.Compute(seq1, seq2);
            double result2 = DTW.Compute(seq2, seq1);
            Assert.AreEqual(result1, result2, 1e-6, "DTW distance should be symmetric");
        }

        [TestMethod]
        public void Compute_WithSingleElementSequences_ReturnsAbsoluteDifference()
        {
            double[] seq1 = new double[]
            {
                5.0
            };
            double[] seq2 = new double[]
            {
                7.0
            };
            double expected = 2.0;
            double result = DTW.Compute(seq1, seq2);
            Assert.AreEqual(expected, result, 1e-6, "DTW result for single element sequences should be the absolute difference");
        }

        [TestMethod]
        public void Compute_WithLargeRandomSequences_ReturnsNonNegative()
        {
            int length = 1000;
            double[] seq1 = new double[length];
            double[] seq2 = new double[length];
            Random random = new Random(42);
            for (int i = 0; i < length; i++)
            {
                seq1[i] = random.NextDouble() * 100;
                seq2[i] = random.NextDouble() * 100;
            }

            double result = DTW.Compute(seq1, seq2);
            Assert.IsTrue(result >= 0, "DTW result for random sequences should be non-negative");
        }
    }
}