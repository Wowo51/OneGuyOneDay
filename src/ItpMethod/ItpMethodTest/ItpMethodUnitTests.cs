using Microsoft.VisualStudio.TestTools.UnitTesting;
using ItpMethod;
using System;

namespace ItpMethodTest
{
    [TestClass]
    public class ItpMethodUnitTests
    {
        [TestMethod]
        public void TestNullFunction()
        {
            double result = ItpMethodSolver.Solve(null, 0.0, 1.0, 1e-6, 100);
            Assert.IsTrue(double.IsNaN(result), "Should return NaN for null function.");
        }

        [TestMethod]
        public void TestNoBracketRoot()
        {
            Func<double, double> constantFunc = delegate (double x)
            {
                return 1.0;
            };
            double result = ItpMethodSolver.Solve(constantFunc, 0.0, 1.0, 1e-6, 100);
            Assert.IsTrue(double.IsNaN(result), "Should return NaN when the interval does not bracket a root.");
        }

        [TestMethod]
        public void TestRootAtLeft()
        {
            Func<double, double> linearFunc = delegate (double x)
            {
                return x;
            };
            double result = ItpMethodSolver.Solve(linearFunc, 0.0, 1.0, 1e-6, 100);
            Assert.AreEqual(0.0, result, 1e-6, "Should return left endpoint when it is a root.");
        }

        [TestMethod]
        public void TestRootAtRight()
        {
            Func<double, double> linearFunc = delegate (double x)
            {
                return x - 1.0;
            };
            double result = ItpMethodSolver.Solve(linearFunc, 0.0, 1.0, 1e-6, 100);
            Assert.AreEqual(1.0, result, 1e-6, "Should return right endpoint when it is a root.");
        }

        [TestMethod]
        public void TestConvergenceBasic()
        {
            Func<double, double> linearFunc = delegate (double x)
            {
                return x - 2.0;
            };
            double result = ItpMethodSolver.Solve(linearFunc, 1.0, 3.0, 1e-6, 100);
            Assert.AreEqual(2.0, result, 1e-6, "The root should be 2 for f(x)=x-2.");
        }

        [TestMethod]
        public void TestConvergenceCubic()
        {
            Func<double, double> cubicFunc = delegate (double x)
            {
                return x * x * x - x - 2.0;
            };
            double result = ItpMethodSolver.Solve(cubicFunc, 1.0, 2.0, 1e-6, 100);
            double expected = 1.52138;
            Assert.AreEqual(expected, result, 1e-4, "The root should be approximately 1.52138 for f(x)=x^3 - x -2.");
        }

        [TestMethod]
        public void TestMaxIterationsExceeded()
        {
            Func<double, double> expFunc = delegate (double x)
            {
                return Math.Exp(x) - 4.0;
            };
            double left = 1.0;
            double right = 2.0;
            double tol = 1e-12;
            int maxIter = 1;
            double fLeft = Math.Exp(left) - 4.0;
            double fRight = Math.Exp(right) - 4.0;
            double mid = (left + right) / 2.0;
            double candidate = left - fLeft * (right - left) / (fRight - fLeft);
            double delta = (right - left) / 4.0;
            double diff = candidate - mid;
            candidate = mid + (diff >= 0 ? 1.0 : -1.0) * Math.Min(Math.Abs(diff), delta);
            double expected = (candidate + right) / 2.0;
            double result = ItpMethodSolver.Solve(expFunc, left, right, tol, maxIter);
            Assert.AreEqual(expected, result, 1e-6, "Should return the mid point after maximum iterations are reached.");
        }

        [TestMethod]
        public void TestSyntheticDataGeneration()
        {
            Func<double, double> sineFunc = delegate (double x)
            {
                return Math.Sin(x);
            };
            double tol = 1e-6;
            int maxIter = 100;
            for (int i = 0; i < 10; i++)
            {
                double left = i * Math.PI;
                double right = (i + 1) * Math.PI;
                double result = ItpMethodSolver.Solve(sineFunc, left, right, tol, maxIter);
                bool atBoundary = Math.Abs(result - left) < tol || Math.Abs(result - right) < tol;
                Assert.IsTrue(atBoundary || (result > left && result < right), "Result should lie within the interval or at its boundaries.");
                Assert.IsTrue(Math.Abs(Math.Sin(result)) < tol, "Returned value should be a root of sin(x) within tolerance.");
            }
        }

        [TestMethod]
        public void TestMaxIterationsZero()
        {
            double left = 0.0;
            double right = 3.0;
            double tol = 1e-6;
            int maxIter = 0;
            Func<double, double> linearFunc = delegate (double x)
            {
                return x - 2.0;
            };
            double result = ItpMethodSolver.Solve(linearFunc, left, right, tol, maxIter);
            double expected = (left + right) / 2.0;
            Assert.AreEqual(expected, result, tol, "With maxIter 0, should return the midpoint.");
        }
    }
}