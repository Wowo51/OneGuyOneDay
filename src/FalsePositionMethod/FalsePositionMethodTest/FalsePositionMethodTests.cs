using Microsoft.VisualStudio.TestTools.UnitTesting;
using FalsePositionMethod;
using System;

namespace FalsePositionMethodTest
{
    [TestClass]
    public class FalsePositionMethodTests
    {
        private const double Tolerance = 1e-6;
        private const int MaxIterations = 100;
        [TestMethod]
        public void TestNullFunction()
        {
            double result = FalsePositionMethodSolver.FindRoot(null, 0, 1, Tolerance, MaxIterations);
            Assert.IsTrue(double.IsNaN(result), "Result should be NaN when function is null");
        }

        [TestMethod]
        public void TestNonBracketedInterval()
        {
            // f(x) = x^2 + 1 is always positive, so there's no sign change.
            double result = FalsePositionMethodSolver.FindRoot(x => x * x + 1, 0, 1, Tolerance, MaxIterations);
            Assert.IsTrue(double.IsNaN(result), "Result should be NaN when the interval does not bracket a root");
        }

        [TestMethod]
        public void TestSimpleRoot()
        {
            // f(x) = x, which has a root at 0.
            double result = FalsePositionMethodSolver.FindRoot(x => x, -1, 1, Tolerance, MaxIterations);
            Assert.IsTrue(System.Math.Abs(result) < Tolerance, "The root of f(x)=x should be approximately 0");
        }

        [TestMethod]
        public void TestCubicRoot()
        {
            // f(x) = x^3 - x - 2 has a real root near 1.52138.
            double result = FalsePositionMethodSolver.FindRoot(x => x * x * x - x - 2, 1, 2, Tolerance, MaxIterations);
            double fResult = result * result * result - result - 2;
            Assert.IsTrue(System.Math.Abs(fResult) < Tolerance, "The found root should satisfy f(x)=0 for the cubic function");
        }

        [TestMethod]
        public void TestSinFunction()
        {
            // f(x) = sin(x) has a root at π; test with an interval that contains π (3 to 4).
            double result = FalsePositionMethodSolver.FindRoot(System.Math.Sin, 3, 4, Tolerance, MaxIterations);
            Assert.IsTrue(System.Math.Abs(System.Math.Sin(result)) < Tolerance, "The found root should satisfy sin(x)=0");
        }

        [TestMethod]
        public void TestSyntheticLinearFunctions()
        {
            // Test multiple linear functions of the form f(x)=m*x+b with known roots.
            System.Random random = new System.Random(42);
            for (int i = 0; i < 10; i++)
            {
                double m = random.NextDouble() * 18 - 9; // Range: -9 to 9
                if (System.Math.Abs(m) < 0.1)
                {
                    m = m < 0 ? -0.1 : 0.1;
                }

                double b = random.NextDouble() * 200 - 100; // Range: -100 to 100
                double actualRoot = -b / m;
                double lower = actualRoot - 10;
                double upper = actualRoot + 10;
                double result = FalsePositionMethodSolver.FindRoot(x => m * x + b, lower, upper, Tolerance, MaxIterations);
                Assert.IsTrue(System.Math.Abs(result - actualRoot) < Tolerance * 10, "The found root should be close to the actual linear function root");
            }
        }
    }
}