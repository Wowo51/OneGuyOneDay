using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AlphaBetaAlgorithm;

namespace AlphaBetaAlgorithmTest
{
    [TestClass]
    public class AlphaBetaCalculatorTests
    {
        [TestMethod]
        public void TestWithKnownValues()
        {
            double result = AlphaBetaCalculator.ApproximateHypotenuse(3.0, 4.0);
            double expected = 4.0 + 0.41421356 * 3.0;
            Assert.AreEqual(expected, result, 0.0001, "Approximate hypotenuse for (3,4) is incorrect.");
        }

        [TestMethod]
        public void TestWithZeroValues()
        {
            double result = AlphaBetaCalculator.ApproximateHypotenuse(0.0, 0.0);
            double expected = 0.0;
            Assert.AreEqual(expected, result, 0.0001);
            result = AlphaBetaCalculator.ApproximateHypotenuse(0.0, 5.0);
            expected = 5.0;
            Assert.AreEqual(expected, result, 0.0001);
            result = AlphaBetaCalculator.ApproximateHypotenuse(7.0, 0.0);
            expected = 7.0;
            Assert.AreEqual(expected, result, 0.0001);
        }

        [TestMethod]
        public void TestWithNegativeValues()
        {
            double result = AlphaBetaCalculator.ApproximateHypotenuse(-3.0, 4.0);
            double expected = 4.0 + 0.41421356 * 3.0;
            Assert.AreEqual(expected, result, 0.0001, "Approximate hypotenuse for (-3,4) is incorrect.");
            result = AlphaBetaCalculator.ApproximateHypotenuse(-3.0, -4.0);
            expected = 4.0 + 0.41421356 * 3.0;
            Assert.AreEqual(expected, result, 0.0001, "Approximate hypotenuse for (-3,-4) is incorrect.");
        }

        [TestMethod]
        public void TestRandomInputs()
        {
            Random random = new Random(42);
            for (int i = 0; i < 100; i++)
            {
                double a = (random.NextDouble() * 200.0) - 100.0;
                double b = (random.NextDouble() * 200.0) - 100.0;
                double result = AlphaBetaCalculator.ApproximateHypotenuse(a, b);
                double absA = Math.Abs(a);
                double absB = Math.Abs(b);
                double maximum = (absA >= absB) ? absA : absB;
                double minimum = (absA < absB) ? absA : absB;
                double expected = maximum + 0.41421356 * minimum;
                Assert.AreEqual(expected, result, 0.0001, "Approximate hypotenuse for random input is incorrect.");
            }
        }
    }
}