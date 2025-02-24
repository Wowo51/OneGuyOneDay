using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScoringAlgorithm;

namespace ScoringAlgorithmTest
{
    [TestClass]
    public class NewtonScoringAlgorithmTests
    {
        [TestMethod]
        public void TestConvergencePositiveRoot()
        {
            double tolerance = 1e-6;
            int maxIterations = 100;
            Func<double, double> scoreFunc = delegate (double x)
            {
                return x * x - 2;
            };
            Func<double, double> derivativeFunc = delegate (double x)
            {
                return 2 * x;
            };
            double initialGuess = 1.0;
            double result = NewtonScoringAlgorithm.Solve(scoreFunc, derivativeFunc, initialGuess, tolerance, maxIterations);
            double expected = Math.Sqrt(2);
            Assert.AreEqual(expected, result, tolerance, "Expected to converge to sqrt(2)");
        }

        [TestMethod]
        public void TestConvergenceNegativeRoot()
        {
            double tolerance = 1e-6;
            int maxIterations = 100;
            Func<double, double> scoreFunc = delegate (double x)
            {
                return x * x - 2;
            };
            Func<double, double> derivativeFunc = delegate (double x)
            {
                return 2 * x;
            };
            double initialGuess = -1.0;
            double result = NewtonScoringAlgorithm.Solve(scoreFunc, derivativeFunc, initialGuess, tolerance, maxIterations);
            double expected = -Math.Sqrt(2);
            Assert.AreEqual(expected, result, tolerance, "Expected to converge to -sqrt(2)");
        }

        [TestMethod]
        public void TestNullScoreFunction()
        {
            double tolerance = 1e-6;
            int maxIterations = 100;
            Func<double, double> derivativeFunc = delegate (double x)
            {
                return 2 * x;
            };
            double result = NewtonScoringAlgorithm.Solve(null, derivativeFunc, 1.0, tolerance, maxIterations);
            Assert.IsTrue(Double.IsNaN(result), "Expected NaN when scoreFunc is null");
        }

        [TestMethod]
        public void TestNullDerivativeFunction()
        {
            double tolerance = 1e-6;
            int maxIterations = 100;
            Func<double, double> scoreFunc = delegate (double x)
            {
                return x * x - 2;
            };
            double result = NewtonScoringAlgorithm.Solve(scoreFunc, null, 1.0, tolerance, maxIterations);
            Assert.IsTrue(Double.IsNaN(result), "Expected NaN when derivativeFunc is null");
        }

        [TestMethod]
        public void TestZeroMaxIterations()
        {
            double tolerance = 1e-6;
            int maxIterations = 0;
            Func<double, double> scoreFunc = delegate (double x)
            {
                return x * x - 2;
            };
            Func<double, double> derivativeFunc = delegate (double x)
            {
                return 2 * x;
            };
            double result = NewtonScoringAlgorithm.Solve(scoreFunc, derivativeFunc, 1.0, tolerance, maxIterations);
            Assert.IsTrue(Double.IsNaN(result), "Expected NaN when maxIterations is zero");
        }

        [TestMethod]
        public void TestRandomInitialGuesses()
        {
            double tolerance = 1e-6;
            int maxIterations = 100;
            Func<double, double> scoreFunc = delegate (double x)
            {
                return x * x - 2;
            };
            Func<double, double> derivativeFunc = delegate (double x)
            {
                return 2 * x;
            };
            Random rnd = new Random(42);
            for (int i = 0; i < 10; i++)
            {
                double initialGuess = rnd.NextDouble() * 10.0 + 0.1;
                double result = NewtonScoringAlgorithm.Solve(scoreFunc, derivativeFunc, initialGuess, tolerance, maxIterations);
                Assert.AreEqual(Math.Sqrt(2), result, tolerance, "Expected convergence to sqrt(2) for initial guess " + initialGuess.ToString());
            }
        }

        [TestMethod]
        public void TestZeroDerivativeValue()
        {
            double tolerance = 1e-6;
            int maxIterations = 100;
            Func<double, double> scoreFunc = delegate (double x)
            {
                return x * x - 2;
            };
            Func<double, double> derivativeFunc = delegate (double x)
            {
                return 0.0;
            };
            double result = NewtonScoringAlgorithm.Solve(scoreFunc, derivativeFunc, 1.0, tolerance, maxIterations);
            Assert.IsTrue(Double.IsNaN(result), "Expected NaN when derivative returns zero");
        }

        [TestMethod]
        public void TestConvergenceQuadraticFunction_Root1()
        {
            double tolerance = 1e-6;
            int maxIterations = 100;
            Func<double, double> scoreFunc = delegate (double x)
            {
                return x * x - 3 * x + 2;
            };
            Func<double, double> derivativeFunc = delegate (double x)
            {
                return 2 * x - 3;
            };
            double result = NewtonScoringAlgorithm.Solve(scoreFunc, derivativeFunc, 0.0, tolerance, maxIterations);
            Assert.AreEqual(1.0, result, tolerance, "Expected convergence to 1");
        }

        [TestMethod]
        public void TestConvergenceQuadraticFunction_Root2()
        {
            double tolerance = 1e-6;
            int maxIterations = 100;
            Func<double, double> scoreFunc = delegate (double x)
            {
                return x * x - 3 * x + 2;
            };
            Func<double, double> derivativeFunc = delegate (double x)
            {
                return 2 * x - 3;
            };
            double result = NewtonScoringAlgorithm.Solve(scoreFunc, derivativeFunc, 3.0, tolerance, maxIterations);
            Assert.AreEqual(2.0, result, tolerance, "Expected convergence to 2");
        }
    }
}