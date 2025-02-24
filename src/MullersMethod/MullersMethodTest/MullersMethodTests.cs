using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MullersMethod;

namespace MullersMethodTest
{
    [TestClass]
    public class MullersMethodTests
    {
        [TestMethod]
        public void TestRealRoot()
        {
            Func<Complex, Complex> f = delegate (Complex x)
            {
                return x * x - 2;
            };
            Complex x0 = new Complex(1, 0);
            Complex x1 = new Complex(1.5, 0);
            Complex x2 = new Complex(2, 0);
            Complex result = MullersMethodSolver.FindRoot(f, x0, x1, x2);
            double expected = Math.Sqrt(2);
            Assert.IsTrue(Math.Abs(result.Real - expected) < 1e-4, "Real root not accurate");
        }

        [TestMethod]
        public void TestComplexRoot()
        {
            Func<Complex, Complex> f = delegate (Complex x)
            {
                return x * x + 1;
            };
            Complex x0 = new Complex(0, 0);
            Complex x1 = new Complex(0, 1);
            Complex x2 = new Complex(1, 0);
            Complex result = MullersMethodSolver.FindRoot(f, x0, x1, x2);
            // Verify that the computed root is indeed a root: f(result) must be near zero.
            Assert.IsTrue(Complex.Abs(f(result)) < 1e-3, "Complex root did not converge sufficiently");
        }

        [TestMethod]
        public void TestConstantFunction()
        {
            Func<Complex, Complex> f = delegate (Complex x)
            {
                return new Complex(1, 0);
            };
            Complex initial = new Complex(2, 2);
            // If initial values are all the same, the step sizes will be zero and the method returns the input.
            Complex result = MullersMethodSolver.FindRoot(f, initial, initial, initial);
            Assert.AreEqual(initial, result, "For a constant function, the result should equal the initial value");
        }

        [TestMethod]
        public void TestSyntheticMultipleTrials()
        {
            Func<Complex, Complex> f = delegate (Complex x)
            {
                return x * x - 2;
            };
            int testCount = 1000;
            Random rnd = new Random(12345);
            for (int i = 0; i < testCount; i++)
            {
                double baseVal = 0.5 + rnd.NextDouble() * 2.0;
                Complex x0 = new Complex(baseVal, 0);
                Complex x1 = new Complex(baseVal + 0.1, 0);
                Complex x2 = new Complex(baseVal + 0.2, 0);
                Complex result = MullersMethodSolver.FindRoot(f, x0, x1, x2);
                Assert.IsTrue(Math.Abs(result.Real - Math.Sqrt(2)) < 1e-3, "Synthetic trial " + i.ToString() + " failed");
            }
        }
    }
}