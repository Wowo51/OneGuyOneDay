using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MiserAlgorithm;

namespace MiserAlgorithmTest
{
    [TestClass]
    public class MiserIntegratorTests
    {
        [TestMethod]
        public void TestIntegrate_Square()
        {
            MiserIntegrator integrator = new MiserIntegrator();
            double[] lower = new double[]
            {
                0.0
            };
            double[] upper = new double[]
            {
                1.0
            };
            Func<double[], double> square = delegate (double[] x)
            {
                return x[0] * x[0];
            };
            long samples = 100000;
            double result = integrator.Integrate(square, lower, upper, samples);
            double expected = 1.0 / 3.0;
            Assert.AreEqual(expected, result, 0.01, "Integration of x^2 over [0,1] should be approximately 1/3");
        }

        [TestMethod]
        public void TestIntegrate_NullArguments()
        {
            MiserIntegrator integrator = new MiserIntegrator();
            double[]? lower = null;
            double[]? upper = null;
            Func<double[], double> constant = delegate (double[] x)
            {
                return 1.0;
            };
            long samples = 1000;
            double result = integrator.Integrate(constant, lower, upper, samples);
            double expected = 0.0;
            Assert.AreEqual(expected, result, 0.0, "Integration with null bounds should return 0.0");
        }

        [TestMethod]
        public void TestIntegrate_InvalidDimensions()
        {
            MiserIntegrator integrator = new MiserIntegrator();
            double[] lower = new double[]
            {
                0.0
            };
            double[] upper = new double[]
            {
                1.0,
                2.0
            };
            Func<double[], double> constant = delegate (double[] x)
            {
                return 1.0;
            };
            long samples = 1000;
            double result = integrator.Integrate(constant, lower, upper, samples);
            double expected = 0.0;
            Assert.AreEqual(expected, result, 0.0, "Integration with invalid dimensions should return 0.0");
        }

        [TestMethod]
        public void TestIntegrate_MultiDimension_Sum()
        {
            MiserIntegrator integrator = new MiserIntegrator();
            double[] lower = new double[]
            {
                0.0,
                0.0
            };
            double[] upper = new double[]
            {
                1.0,
                1.0
            };
            Func<double[], double> sumFunction = delegate (double[] x)
            {
                return x[0] + x[1];
            };
            long samples = 100000;
            double result = integrator.Integrate(sumFunction, lower, upper, samples);
            double expected = 1.0;
            Assert.AreEqual(expected, result, 0.01, "Integration of (x+y) over [0,1]x[0,1] should be approximately 1.0");
        }

        [TestMethod]
        public void TestIntegrate_ConstantFunction()
        {
            MiserIntegrator integrator = new MiserIntegrator();
            double[] lower = new double[]
            {
                1.0,
                2.0
            };
            double[] upper = new double[]
            {
                3.0,
                5.0
            };
            Func<double[], double> constant = delegate (double[] x)
            {
                return 2.0;
            };
            long samples = 50000;
            double volume = (3.0 - 1.0) * (5.0 - 2.0);
            double expected = 2.0 * volume;
            double result = integrator.Integrate(constant, lower, upper, samples);
            Assert.AreEqual(expected, result, 0.1, "Integration of constant function should be 2 * volume");
        }

        [TestMethod]
        public void TestIntegrate_MultiDimension_Quadratic()
        {
            MiserIntegrator integrator = new MiserIntegrator();
            double[] lower = new double[]
            {
                0.0,
                0.0
            };
            double[] upper = new double[]
            {
                1.0,
                1.0
            };
            Func<double[], double> quadratic = delegate (double[] x)
            {
                return x[0] * x[0] + x[1] * x[1];
            };
            long samples = 100000;
            double expected = (1.0 / 3.0) + (1.0 / 3.0);
            double result = integrator.Integrate(quadratic, lower, upper, samples);
            Assert.AreEqual(expected, result, 0.01, "Integration of x^2+y^2 over [0,1]^2 should be approximately 2/3");
        }

        [TestMethod]
        public void TestIntegrate_LowSamples_Constant()
        {
            MiserIntegrator integrator = new MiserIntegrator();
            double[] lower = new double[]
            {
                0.0
            };
            double[] upper = new double[]
            {
                1.0
            };
            Func<double[], double> constant = delegate (double[] x)
            {
                return 5.0;
            };
            long samples = 50;
            double expected = 5.0;
            double result = integrator.Integrate(constant, lower, upper, samples);
            Assert.AreEqual(expected, result, 1e-6, "Integration of constant function with low sample count should be exact");
        }

        [TestMethod]
        public void TestIntegrate_HighDimension_Sum()
        {
            MiserIntegrator integrator = new MiserIntegrator();
            int dimensions = 5;
            double[] lower = new double[dimensions];
            double[] upper = new double[dimensions];
            for (int i = 0; i < dimensions; i++)
            {
                lower[i] = 0.0;
                upper[i] = 1.0;
            }

            Func<double[], double> sumFunction = delegate (double[] x)
            {
                double sum = 0.0;
                for (int i = 0; i < x.Length; i++)
                {
                    sum += x[i];
                }

                return sum;
            };
            long samples = 100000;
            double expected = 2.5;
            double result = integrator.Integrate(sumFunction, lower, upper, samples);
            Assert.AreEqual(expected, result, 0.01, "Integration of sum(x) over 5D unit hypercube should be approximately 2.5");
        }
    }
}