using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewtonsMethod;

namespace NewtonsMethodTest
{
    [TestClass]
    public class NewtonSolverTests
    {
        [TestMethod]
        public void Test_NullFunction_ReturnsNaN()
        {
            double initialGuess = 1.0;
            double tolerance = 1e-6;
            int maxIterations = 10;
            double result = NewtonSolver.Solve(null, (double x) => 2 * x, initialGuess, tolerance, maxIterations);
            Assert.IsTrue(Double.IsNaN(result));
        }

        [TestMethod]
        public void Test_NullDerivative_ReturnsNaN()
        {
            double initialGuess = 2.0;
            double tolerance = 1e-6;
            int maxIterations = 10;
            double result = NewtonSolver.Solve((double x) => x * x - 4, null, initialGuess, tolerance, maxIterations);
            Assert.IsTrue(Double.IsNaN(result));
        }

        [TestMethod]
        public void Test_DerivativeNearZero_ReturnsNaN()
        {
            double initialGuess = 0.0;
            double tolerance = 1e-6;
            int maxIterations = 10;
            double result = NewtonSolver.Solve((double x) => x * x, (double x) => 2 * x, initialGuess, tolerance, maxIterations);
            Assert.IsTrue(Double.IsNaN(result));
        }

        [TestMethod]
        public void Test_Convergence_Positive()
        {
            double initialGuess = 3.0;
            double tolerance = 1e-6;
            int maxIterations = 100;
            double result = NewtonSolver.Solve((double x) => x * x - 4, (double x) => 2 * x, initialGuess, tolerance, maxIterations);
            Assert.IsTrue(Math.Abs(result - 2.0) < tolerance, "Convergence did not meet expected value for positive initial guess.");
        }

        [TestMethod]
        public void Test_Convergence_Negative()
        {
            double initialGuess = -3.0;
            double tolerance = 1e-6;
            int maxIterations = 100;
            double result = NewtonSolver.Solve((double x) => x * x - 4, (double x) => 2 * x, initialGuess, tolerance, maxIterations);
            Assert.IsTrue(Math.Abs(result + 2.0) < tolerance, "Convergence did not meet expected value for negative initial guess.");
        }

        [TestMethod]
        public void Test_MaxIterationsZero()
        {
            double initialGuess = 3.0;
            double tolerance = 1e-6;
            int maxIterations = 0;
            double result = NewtonSolver.Solve((double x) => x * x - 4, (double x) => 2 * x, initialGuess, tolerance, maxIterations);
            Assert.AreEqual(initialGuess, result, 1e-9, "With zero max iterations, the result should be the initial guess.");
        }

        [TestMethod]
        public void Test_NonConvergence()
        {
            double initialGuess = 3.0;
            double tolerance = 1e-12;
            int maxIterations = 2;
            double result = NewtonSolver.Solve((double x) => x * x - 4, (double x) => 2 * x, initialGuess, tolerance, maxIterations);
            double residual = Math.Abs(result * result - 4);
            Assert.IsTrue(residual > tolerance, "Result unexpectedly converged with low max iterations.");
        }

        [TestMethod]
        public void Test_SyntheticRandomQuadratic()
        {
            int tests = 10;
            double tolerance = 1e-6;
            Random random = new Random(42);
            for (int i = 0; i < tests; i++)
            {
                double guess = random.NextDouble() * 20 - 10;
                if (Math.Abs(guess) < 1e-3)
                {
                    guess = 1.0;
                }

                double expected = guess > 0 ? Math.Sqrt(2) : -Math.Sqrt(2);
                double result = NewtonSolver.Solve((double x) => x * x - 2, (double x) => 2 * x, guess, tolerance, 100);
                if (Double.IsNaN(result))
                {
                    Assert.IsTrue(Math.Abs(guess) < 1e-3, "Result should not be NaN for a non-zero initial guess.");
                }
                else
                {
                    double fValue = result * result - 2;
                    Assert.IsTrue(Math.Abs(fValue) < tolerance, $"Residual is too high for initial guess {guess}: {fValue}");
                    Assert.IsTrue(Math.Abs(result - expected) < tolerance, $"For initial guess {guess}, expected {expected} but got {result}");
                }
            }
        }

        [TestMethod]
        public void Test_InitialGuessIsRoot()
        {
            double root = 2.0;
            double tolerance = 1e-6;
            int maxIterations = 100;
            double result = NewtonSolver.Solve((double x) => x * x - 4, (double x) => 2 * x, root, tolerance, maxIterations);
            Assert.AreEqual(root, result, tolerance, "When initial guess is a root, the solver should return it immediately.");
        }
    }
}