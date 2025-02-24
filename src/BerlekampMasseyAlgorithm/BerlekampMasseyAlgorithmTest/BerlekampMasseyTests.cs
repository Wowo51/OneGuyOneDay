using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using BerlekampMasseyAlgorithm;

namespace BerlekampMasseyAlgorithmTest
{
    [TestClass]
    public class BerlekampMasseyTests
    {
        private const double Tolerance = 1e-6;
        private static void AssertArrayAreEqual(double[] expected, double[] actual)
        {
            Assert.AreEqual(expected.Length, actual.Length, "Array lengths differ.");
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(Math.Abs(expected[i] - actual[i]) < Tolerance, $"Arrays differ at index {i}. Expected: {expected[i]}, Actual: {actual[i]}");
            }
        }

        [TestMethod]
        public void TestNullInput()
        {
            double[] result = BerlekampMassey.Compute(null);
            Assert.AreEqual(0, result.Length, "Expected empty array for null input.");
        }

        [TestMethod]
        public void TestEmptyInput()
        {
            double[] sequence = new double[0];
            double[] result = BerlekampMassey.Compute(sequence);
            double[] expected = new double[]
            {
                1.0
            };
            AssertArrayAreEqual(expected, result);
        }

        [TestMethod]
        public void TestSingleElement()
        {
            double[] sequence = new double[]
            {
                7.0
            };
            double[] expected = new double[]
            {
                1.0,
                -7.0
            };
            double[] result = BerlekampMassey.Compute(sequence);
            AssertArrayAreEqual(expected, result);
        }

        [TestMethod]
        public void TestFibonacciSequence()
        {
            double[] sequence = new double[]
            {
                0,
                1,
                1,
                2,
                3,
                5,
                8,
                13,
                21
            };
            double[] result = BerlekampMassey.Compute(sequence);
            double[] expected = new double[]
            {
                1.0,
                -1.0,
                -1.0
            };
            AssertArrayAreEqual(expected, result);
        }

        [TestMethod]
        public void TestConstantSequence()
        {
            double[] sequence = new double[]
            {
                1,
                1,
                1,
                1,
                1,
                1
            };
            double[] result = BerlekampMassey.Compute(sequence);
            double[] expected = new double[]
            {
                1.0,
                -1.0
            };
            AssertArrayAreEqual(expected, result);
        }

        [TestMethod]
        public void TestSyntheticLinearRecurrence()
        {
            double[] coefficients = new double[]
            {
                1.0,
                -2.0,
                3.0,
                -1.0
            };
            double[] initial = new double[]
            {
                1.0,
                2.0,
                3.0
            };
            int sequenceLength = 30;
            double[] sequence = GenerateLinearRecurrence(initial, coefficients, sequenceLength);
            double[] result = BerlekampMassey.Compute(sequence);
            AssertArrayAreEqual(coefficients, result);
        }

        [TestMethod]
        public void TestLargeSyntheticDataSet()
        {
            double[] coefficients = new double[]
            {
                1.0,
                0.5,
                -0.25
            };
            double[] initial = new double[]
            {
                2.0,
                1.0
            };
            int sequenceLength = 1000;
            double[] sequence = GenerateLinearRecurrence(initial, coefficients, sequenceLength);
            double[] result = BerlekampMassey.Compute(sequence);
            int degree = result.Length - 1;
            for (int i = degree; i < sequence.Length; i++)
            {
                double residual = 0.0;
                for (int j = 0; j < result.Length; j++)
                {
                    residual += result[j] * sequence[i - j];
                }

                Assert.IsTrue(Math.Abs(residual) < Tolerance, $"Residual at index {i} is too high: {residual}");
            }
        }

        private static double[] GenerateLinearRecurrence(double[] initial, double[] coefficients, int length)
        {
            int degree = coefficients.Length - 1;
            double[] sequence = new double[length];
            for (int i = 0; i < initial.Length && i < length; i++)
            {
                sequence[i] = initial[i];
            }

            for (int i = initial.Length; i < length; i++)
            {
                double nextValue = 0.0;
                for (int j = 1; j <= degree; j++)
                {
                    nextValue -= coefficients[j] * sequence[i - j];
                }

                sequence[i] = nextValue;
            }

            return sequence;
        }
    }
}