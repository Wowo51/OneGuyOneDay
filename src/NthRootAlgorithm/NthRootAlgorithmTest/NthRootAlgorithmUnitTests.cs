using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NthRootAlgorithm;

namespace NthRootAlgorithmTest
{
    [TestClass]
    public class NthRootAlgorithmUnitTests
    {
        [TestMethod]
        public void TestCubeRootOf27()
        {
            double value = 27.0;
            int degree = 3;
            double result = NthRootCalculator.NthRoot(value, degree);
            Assert.AreEqual(3.0, result, 1e-8, "Cube root of 27 should be approximately 3");
        }

        [TestMethod]
        public void TestSquareRootOf16()
        {
            double value = 16.0;
            int degree = 2;
            double result = NthRootCalculator.NthRoot(value, degree);
            Assert.AreEqual(4.0, result, 1e-8, "Square root of 16 should be approximately 4");
        }

        [TestMethod]
        public void TestZeroValue()
        {
            double value = 0.0;
            int degree = 5;
            double result = NthRootCalculator.NthRoot(value, degree);
            Assert.AreEqual(0.0, result, 1e-8, "Nth root of 0 should be 0 for any valid n");
        }

        [TestMethod]
        public void TestNegativeCubeRoot()
        {
            double value = -27.0;
            int degree = 3;
            double result = NthRootCalculator.NthRoot(value, degree);
            Assert.AreEqual(-3.0, result, 1e-8, "Cube root of -27 should be approximately -3 when n is odd");
        }

        [TestMethod]
        public void TestNegativeEvenRootReturnsNaN()
        {
            double value = -16.0;
            int degree = 2;
            double result = NthRootCalculator.NthRoot(value, degree);
            Assert.IsTrue(Double.IsNaN(result), "Even root of a negative number should return NaN");
        }

        [TestMethod]
        public void TestInvalidRootDegree()
        {
            double value = 16.0;
            int degree = 0;
            double result = NthRootCalculator.NthRoot(value, degree);
            Assert.IsTrue(Double.IsNaN(result), "Invalid root degree (n ≤ 0) should return NaN");
        }

        [TestMethod]
        public void TestLargeSyntheticDataset()
        {
            System.Random randomGenerator = new System.Random(42);
            for (int index = 0; index < 100; index++)
            {
                double value = 1.0 + randomGenerator.NextDouble() * 1e6;
                int degree = 2 + randomGenerator.Next(9);
                double computed = NthRootCalculator.NthRoot(value, degree);
                double expected = Math.Pow(value, 1.0 / degree);
                double tolerance = Math.Abs(expected * 1e-6);
                Assert.AreEqual(expected, computed, tolerance, "Computed nth root does not match expected value within tolerance.");
            }
        }
    }
}