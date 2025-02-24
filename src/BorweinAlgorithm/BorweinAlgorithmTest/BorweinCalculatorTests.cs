using Microsoft.VisualStudio.TestTools.UnitTesting;
using BorweinAlgorithm;
using System;

namespace BorweinAlgorithmTest
{
    [TestClass]
    public class BorweinCalculatorTests
    {
        private const double Tolerance = 1e-6;
        [TestMethod]
        public void CalculateReciprocalPi_NegativeIterations_UsesZeroIterations()
        {
            double expected = 6.0 - 4.0 * Math.Sqrt(2.0);
            double actual = BorweinCalculator.CalculateReciprocalPi(-5);
            Assert.AreEqual(expected, actual, Tolerance, "Negative iterations should behave as 0 iterations.");
        }

        [TestMethod]
        public void CalculateReciprocalPi_ZeroIterations_ReturnsInitialApproximation()
        {
            double expected = 6.0 - 4.0 * Math.Sqrt(2.0);
            double actual = BorweinCalculator.CalculateReciprocalPi(0);
            Assert.AreEqual(expected, actual, Tolerance, "Zero iterations should return initial approximation.");
        }

        [TestMethod]
        public void CalculateReciprocalPi_OneIteration_ResultIsCloseToOneOverPi()
        {
            double actual = BorweinCalculator.CalculateReciprocalPi(1);
            double expected = 1.0 / Math.PI;
            Assert.IsTrue(System.Math.Abs(actual - expected) < 1e-3, "One iteration should give close approximation to 1/PI.");
        }

        [TestMethod]
        public void CalculateReciprocalPi_MultipleIterations_ConvergesToOneOverPi()
        {
            int testIterations = 5;
            double actual = BorweinCalculator.CalculateReciprocalPi(testIterations);
            double expected = 1.0 / Math.PI;
            Assert.IsTrue(System.Math.Abs(actual - expected) < 1e-10, "Multiple iterations should converge to 1/PI.");
        }

        [TestMethod]
        public void CalculateReciprocalPi_LargeIterationCount_ConvergenceTest()
        {
            int testIterations = 10;
            double actual = BorweinCalculator.CalculateReciprocalPi(testIterations);
            double expected = 1.0 / Math.PI;
            Assert.IsTrue(System.Math.Abs(actual - expected) < 1e-12, "Large iteration count should converge very closely to 1/PI.");
        }

        [TestMethod]
        public void CalculateReciprocalPi_SyntheticRandomIterations_Test()
        {
            System.Random random = new System.Random();
            for (int i = 0; i < 10; i++)
            {
                int iterations = random.Next(0, 10);
                double actual = BorweinCalculator.CalculateReciprocalPi(iterations);
                double expected = iterations == 0 ? 6.0 - 4.0 * Math.Sqrt(2.0) : 1.0 / Math.PI;
                double tolerance = iterations == 0 ? Tolerance : (iterations < 3 ? 1e-3 : 1e-6);
                Assert.IsTrue(System.Math.Abs(actual - expected) < tolerance, "Synthetic random iterations test failed for iteration count " + iterations.ToString());
            }
        }
    }
}