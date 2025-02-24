using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BisectionMethod;

namespace BisectionMethodTest
{
    [TestClass]
    public class BisectionSolverTests
    {
        [TestMethod]
        public void TestFindRoot_SquareRoot()
        {
            double tolerance = 1e-6;
            double expected = Math.Sqrt(2);
            double root = BisectionSolver.FindRoot(x => x * x - 2, 0, 2, tolerance);
            Assert.IsFalse(double.IsNaN(root), "Root should be computed.");
            Assert.IsTrue(Math.Abs(root - expected) < tolerance, $"Expected root approx {expected}, got {root}");
        }

        [TestMethod]
        public void TestFindRoot_LinearFunction()
        {
            double tolerance = 1e-6;
            double expected = 1.5;
            double root = BisectionSolver.FindRoot(x => x - 1.5, 0, 2, tolerance);
            Assert.IsFalse(double.IsNaN(root), "Root should be computed.");
            Assert.IsTrue(Math.Abs(root - expected) < tolerance, $"Expected root {expected}, got {root}");
        }

        [TestMethod]
        public void TestFindRoot_NoRoot()
        {
            double tolerance = 1e-6;
            double root = BisectionSolver.FindRoot(x => x * x + 1, 0, 1, tolerance);
            Assert.IsTrue(double.IsNaN(root), "Method should return NaN when no root exists in the interval.");
        }

        [TestMethod]
        public void TestFindRoot_SinFunction()
        {
            double tolerance = 1e-6;
            double root = BisectionSolver.FindRoot(Math.Sin, 3, 4, tolerance);
            Assert.IsFalse(double.IsNaN(root), "Root should be computed.");
            Assert.IsTrue(Math.Abs(Math.Sin(root)) < tolerance, "Root should satisfy sin(root) near 0.");
            double piApprox = 3.14159;
            Assert.IsTrue(Math.Abs(root - piApprox) < 1e-3, $"Expected root near pi, got {root}");
        }

        [TestMethod]
        public void TestFindRoot_MultipleLinearFunctions()
        {
            Random random = new Random();
            double tolerance = 1e-6;
            for (int i = 0; i < 10; i++)
            {
                double r = random.NextDouble() * 200.0 - 100.0;
                double a = r - 10;
                double b = r + 10;
                double root = BisectionSolver.FindRoot(x => x - r, a, b, tolerance);
                Assert.IsFalse(double.IsNaN(root), "Root should be computed.");
                Assert.IsTrue(Math.Abs(root - r) < tolerance, $"Expected root {r}, got {root}");
            }
        }
    }
}