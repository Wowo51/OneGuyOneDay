using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RidderMethod;

namespace RidderMethodTest
{
    [TestClass]
    public class RidderMethodSolverTests
    {
        [TestMethod]
        public void TestNullFunction()
        {
            double result = RidderMethodSolver.Solve(null, 0.0, 1.0, 1e-6, 100);
            Assert.IsTrue(double.IsNaN(result), "Expected NaN when function is null");
        }

        [TestMethod]
        public void TestNonBracketingInterval()
        {
            Func<double, double> f = delegate (double x)
            {
                return x * x - 4;
            };
            double result = RidderMethodSolver.Solve(f, 3.0, 5.0, 1e-6, 100);
            Assert.IsTrue(double.IsNaN(result), "Expected NaN for non-bracketing interval");
        }

        [TestMethod]
        public void TestSquareFunctionPositiveRoot()
        {
            Func<double, double> f = delegate (double x)
            {
                return x * x - 4;
            };
            double result = RidderMethodSolver.Solve(f, 0.0, 3.0, 1e-6, 100);
            Assert.IsTrue(Math.Abs(result - 2.0) < 1e-5, "Expected result near 2");
        }

        [TestMethod]
        public void TestSquareFunctionNegativeRoot()
        {
            Func<double, double> f = delegate (double x)
            {
                return x * x - 4;
            };
            double result = RidderMethodSolver.Solve(f, -3.0, 0.0, 1e-6, 100);
            Assert.IsTrue(Math.Abs(result + 2.0) < 1e-5, "Expected result near -2");
        }

        [TestMethod]
        public void TestLinearFunction()
        {
            Func<double, double> f = delegate (double x)
            {
                return x - 1.0;
            };
            double result = RidderMethodSolver.Solve(f, 0.0, 2.0, 1e-6, 100);
            Assert.IsTrue(Math.Abs(result - 1.0) < 1e-5, "Expected result near 1");
        }

        [TestMethod]
        public void TestLinearFunctionRandom()
        {
            Random random = new Random(42);
            for (int i = 0; i < 10; i++)
            {
                double root = random.NextDouble() * 100.0 - 50.0;
                double a = root - 1.0;
                double b = root + 1.0;
                Func<double, double> f = delegate (double x)
                {
                    return x - root;
                };
                double result = RidderMethodSolver.Solve(f, a, b, 1e-6, 100);
                Assert.IsTrue(Math.Abs(result - root) < 1e-5, "Expected result near " + root);
            }
        }

        [TestMethod]
        public void TestSinFunction()
        {
            Func<double, double> f = delegate (double x)
            {
                return Math.Sin(x);
            };
            double result = RidderMethodSolver.Solve(f, 2.0, 4.0, 1e-6, 100);
            Assert.IsTrue(Math.Abs(result - Math.PI) < 1e-5, "Expected result near PI");
        }

        [TestMethod]
        public void TestEndpointIsRoot()
        {
            Func<double, double> f = delegate (double x)
            {
                return x - 1.0;
            };
            double result = RidderMethodSolver.Solve(f, 1.0, 2.0, 1e-6, 100);
            Assert.IsTrue(double.IsNaN(result), "Expected NaN when one endpoint is the root");
        }
    }
}