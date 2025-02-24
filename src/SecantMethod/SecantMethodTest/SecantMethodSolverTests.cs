using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecantMethod;

namespace SecantMethodTest
{
    [TestClass]
    public class SecantMethodSolverTests
    {
        private const double Tolerance = 1E-6;
        private const int MaxIterations = 100;
        [TestMethod]
        public void TestNullFunction()
        {
            double result = SecantMethodSolver.Solve(null, 0.0, 1.0, Tolerance, MaxIterations);
            Assert.IsTrue(Double.IsNaN(result), "Expected NaN when function is null");
        }

        [TestMethod]
        public void TestSquareEquationPositive()
        {
            Func<double, double> function = delegate (double x)
            {
                return x * x - 4.0;
            };
            double root = SecantMethodSolver.Solve(function, 1.0, 3.0, Tolerance, MaxIterations);
            Assert.AreEqual(2.0, root, Tolerance, "Expected root approximately 2 for positive starting points");
        }

        [TestMethod]
        public void TestSquareEquationNegative()
        {
            Func<double, double> function = delegate (double x)
            {
                return x * x - 4.0;
            };
            double root = SecantMethodSolver.Solve(function, -3.0, -1.0, Tolerance, MaxIterations);
            Assert.AreEqual(-2.0, root, Tolerance, "Expected root approximately -2 for negative starting points");
        }

        [TestMethod]
        public void TestConstantFunction()
        {
            Func<double, double> function = delegate (double x)
            {
                return 1.0;
            };
            double root = SecantMethodSolver.Solve(function, 0.0, 10.0, Tolerance, MaxIterations);
            Assert.AreEqual(10.0, root, Tolerance, "Expected the method to return x1 when function is constant");
        }

        [TestMethod]
        public void TestCosMinusX()
        {
            Func<double, double> function = delegate (double x)
            {
                return Math.Cos(x) - x;
            };
            double root = SecantMethodSolver.Solve(function, 0.0, 1.0, Tolerance, MaxIterations);
            double expected = 0.7390851332151607;
            Assert.AreEqual(expected, root, Tolerance, "Expected root for cos(x)-x equation");
        }

        [TestMethod]
        public void TestSquareEquationRandom()
        {
            Func<double, double> function = delegate (double x)
            {
                return x * x - 4.0;
            };
            System.Random random = new System.Random(42);
            for (int i = 0; i < 10; i++)
            {
                double x0 = 1.0 + random.NextDouble() * 2.0;
                double x1 = 2.0 + random.NextDouble() * 2.0;
                double root = SecantMethodSolver.Solve(function, x0, x1, Tolerance, MaxIterations);
                double fValue = function(root);
                Assert.IsTrue(Math.Abs(fValue) < Tolerance, "Function value at root should be near zero");
            }
        }

        [TestMethod]
        public void TestSquareEquationSyntheticData()
        {
            Func<double, double> function = delegate (double x)
            {
                return x * x - 4.0;
            };
            System.Random random = new System.Random(42);
            for (int i = 0; i < 100; i++)
            {
                double x0;
                double x1;
                if (i % 2 == 0)
                {
                    x0 = 1.0 + random.NextDouble() * 2.0;
                    x1 = 2.0 + random.NextDouble() * 2.0;
                }
                else
                {
                    x0 = -2.0 - random.NextDouble() * 2.0;
                    x1 = -1.0 - random.NextDouble() * 2.0;
                }

                double root = SecantMethodSolver.Solve(function, x0, x1, Tolerance, MaxIterations);
                double fValue = function(root);
                Assert.IsTrue(Math.Abs(fValue) < Tolerance, "Function value at root should be near zero");
            }
        }
    }
}