using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FiniteDifferenceMethod;

namespace FiniteDifferenceMethodTest
{
    [TestClass]
    public class FiniteDifferenceSolverTests
    {
        private FiniteDifferenceSolver solver = new FiniteDifferenceSolver();
        [TestInitialize]
        public void Setup()
        {
            solver = new FiniteDifferenceSolver();
        }

        [TestMethod]
        public void FirstDerivative_NullFunction_ReturnsNaN()
        {
            double result = solver.ApproximateFirstDerivative(null, 1.0, 0.001);
            Assert.IsTrue(Double.IsNaN(result));
        }

        [TestMethod]
        public void FirstDerivative_ZeroH_ReturnsNaN()
        {
            Func<double, double> f = new Func<double, double>(x => 2 * x + 3);
            double result = solver.ApproximateFirstDerivative(f, 1.0, 0.0);
            Assert.IsTrue(Double.IsNaN(result));
        }

        [TestMethod]
        public void FirstDerivative_LinearFunction_ReturnsCorrectSlope()
        {
            // For the linear function f(x) = 2*x + 3, the derivative is 2.
            Func<double, double> f = new Func<double, double>(x => 2 * x + 3);
            double h = 1e-5;
            double result = solver.ApproximateFirstDerivative(f, 5.0, h);
            Assert.AreEqual(2.0, result, 1e-3, "Approximation of the derivative should equal the constant slope.");
        }

        [TestMethod]
        public void FirstDerivative_SinFunction_ReturnsCosine()
        {
            // For f(x) = sin(x), the derivative is cos(x).
            Func<double, double> f = new Func<double, double>(x => Math.Sin(x));
            double h = 1e-5;
            double x = 0.5;
            double result = solver.ApproximateFirstDerivative(f, x, h);
            double expected = Math.Cos(x);
            Assert.AreEqual(expected, result, 1e-3, "Approximation of sin(x) derivative should be close to cos(x).");
        }

        [TestMethod]
        public void SecondDerivative_NullFunction_ReturnsNaN()
        {
            double result = solver.ApproximateSecondDerivative(null, 1.0, 0.001);
            Assert.IsTrue(Double.IsNaN(result));
        }

        [TestMethod]
        public void SecondDerivative_ZeroH_ReturnsNaN()
        {
            Func<double, double> f = new Func<double, double>(x => x * x);
            double result = solver.ApproximateSecondDerivative(f, 1.0, 0.0);
            Assert.IsTrue(Double.IsNaN(result));
        }

        [TestMethod]
        public void SecondDerivative_QuadraticFunction_ReturnsCorrectValue()
        {
            // For f(x) = x^2, the second derivative is 2.
            Func<double, double> f = new Func<double, double>(x => x * x);
            double h = 1e-5;
            double result = solver.ApproximateSecondDerivative(f, 3.0, h);
            Assert.AreEqual(2.0, result, 1e-3, "Approximation of the second derivative for x^2 should equal 2.");
        }

        [TestMethod]
        public void SecondDerivative_SinFunction_ReturnsNegativeSin()
        {
            // For f(x) = sin(x), the second derivative is -sin(x).
            Func<double, double> f = new Func<double, double>(x => Math.Sin(x));
            double h = 1e-5;
            double x = 1.0;
            double result = solver.ApproximateSecondDerivative(f, x, h);
            double expected = -Math.Sin(x);
            Assert.AreEqual(expected, result, 1e-3, "Approximation of the second derivative for sin(x) should be close to -sin(x).");
        }

        [TestMethod]
        public void FirstDerivative_SyntheticTest_ManyDataPoints()
        {
            // Synthetic testing with a series of linear functions f(x) = slope * x + intercept.
            for (int i = 1; i <= 10; i++)
            {
                double slope = (double)i;
                double intercept = i * 2.0;
                Func<double, double> f = new Func<double, double>(x => slope * x + intercept);
                double h = 1e-5;
                double x = 10.0;
                double result = solver.ApproximateFirstDerivative(f, x, h);
                Assert.AreEqual(slope, result, 1e-3, "Synthetic test for linear function derivative failed.");
            }
        }

        [TestMethod]
        public void SecondDerivative_SyntheticTest_ManyDataPoints()
        {
            // Synthetic testing with quadratic functions f(x) = a*x^2 + b*x + c; second derivative should be 2*a.
            for (int a = 1; a <= 5; a++)
            {
                double b = a * 2.0;
                double c = a * 3.0;
                Func<double, double> f = new Func<double, double>(x => a * x * x + b * x + c);
                double h = 1e-5;
                double x = 5.0;
                double result = solver.ApproximateSecondDerivative(f, x, h);
                Assert.AreEqual(2 * a, result, 1e-3, "Synthetic test for quadratic function second derivative failed.");
            }
        }
    }
}