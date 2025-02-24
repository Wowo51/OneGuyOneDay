using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EulerIntegration;

namespace EulerIntegrationTest
{
    [TestClass]
    public class EulerIntegrationTests
    {
        [TestMethod]
        public void Test_ZeroDerivative()
        {
            double t0 = 0;
            double y0 = 5;
            double stepSize = 0.1;
            int steps = 10;
            Func<double, double, double> derivative = (double t, double y) => 0;
            double result = EulerIntegrator.Integrate(derivative, t0, y0, stepSize, steps);
            Assert.AreEqual(5.0, result, 1e-9, "Zero derivative test failed.");
        }

        [TestMethod]
        public void Test_ConstantDerivative()
        {
            double t0 = 0;
            double y0 = 3;
            double stepSize = 0.2;
            int steps = 10;
            Func<double, double, double> derivative = (double t, double y) => 1;
            double result = EulerIntegrator.Integrate(derivative, t0, y0, stepSize, steps);
            double expected = y0 + 1 * stepSize * steps;
            Assert.AreEqual(expected, result, 1e-9, "Constant derivative test failed.");
        }

        [TestMethod]
        public void Test_ExponentialGrowth()
        {
            double t0 = 0;
            double y0 = 1;
            double stepSize = 0.001;
            int steps = 1000;
            Func<double, double, double> derivative = (double t, double y) => y;
            double result = EulerIntegrator.Integrate(derivative, t0, y0, stepSize, steps);
            double expected = y0 * Math.Pow(1 + stepSize, steps);
            Assert.AreEqual(expected, result, 1e-6, "Exponential growth test failed.");
        }

        [TestMethod]
        public void Test_RandomConstantDerivative()
        {
            double t0 = 0;
            double y0 = 10;
            double stepSize = 0.05;
            int steps = 100;
            System.Random random = new System.Random(42);
            for (int i = 0; i < 5; i++)
            {
                double constant = random.NextDouble() * 10 - 5;
                Func<double, double, double> derivative = (double t, double y) => constant;
                double result = EulerIntegrator.Integrate(derivative, t0, y0, stepSize, steps);
                double expected = y0 + constant * stepSize * steps;
                Assert.AreEqual(expected, result, 1e-9, $"Random constant derivative test failed for constant = {constant}.");
            }
        }
    }
}