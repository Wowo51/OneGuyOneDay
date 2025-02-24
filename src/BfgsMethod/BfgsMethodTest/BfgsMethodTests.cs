using Microsoft.VisualStudio.TestTools.UnitTesting;
using BfgsMethod;
using System;

namespace BfgsMethodTest
{
    [TestClass]
    public class BfgsMethodTests
    {
        private class QuadraticFunction : IObjectiveFunction
        {
            private double[] _optimum;
            public QuadraticFunction(double[] optimum)
            {
                _optimum = (double[])optimum.Clone();
            }

            public double Value(double[] point)
            {
                double sum = 0.0;
                for (int i = 0; i < point.Length; i++)
                {
                    double diff = point[i] - _optimum[i];
                    sum += diff * diff;
                }

                return sum;
            }

            public double[] Gradient(double[] point)
            {
                int n = point.Length;
                double[] grad = new double[n];
                for (int i = 0; i < n; i++)
                {
                    grad[i] = 2.0 * (point[i] - _optimum[i]);
                }

                return grad;
            }
        }

        [TestMethod]
        public void TestQuadratic2D()
        {
            double[] optimum = new double[]
            {
                2.0,
                3.0
            };
            QuadraticFunction func = new QuadraticFunction(optimum);
            BfgsOptimizer optimizer = new BfgsOptimizer();
            double[] initialPoint = new double[]
            {
                0.0,
                0.0
            };
            double[] result = optimizer.Optimize(func, initialPoint, 1e-8, 1000);
            Assert.AreEqual(optimum[0], result[0], 1e-4, "X coordinate differs from expected optimum.");
            Assert.AreEqual(optimum[1], result[1], 1e-4, "Y coordinate differs from expected optimum.");
        }

        [TestMethod]
        public void TestQuadraticND()
        {
            double[] optimum = new double[]
            {
                1,
                2,
                3,
                4,
                5
            };
            QuadraticFunction func = new QuadraticFunction(optimum);
            BfgsOptimizer optimizer = new BfgsOptimizer();
            double[] initial = new double[]
            {
                0,
                0,
                0,
                0,
                0
            };
            double[] result = optimizer.Optimize(func, initial, 1e-8, 1000);
            for (int i = 0; i < optimum.Length; i++)
            {
                Assert.AreEqual(optimum[i], result[i], 1e-4, "Coordinate " + i + " differs.");
            }
        }

        [TestMethod]
        public void TestQuadraticRandom()
        {
            Random random = new Random(42);
            for (int test = 0; test < 10; test++)
            {
                int n = random.Next(2, 10);
                double[] optimum = new double[n];
                double[] initial = new double[n];
                for (int i = 0; i < n; i++)
                {
                    optimum[i] = random.NextDouble() * 10 - 5;
                    initial[i] = random.NextDouble() * 10 - 5;
                }

                QuadraticFunction func = new QuadraticFunction(optimum);
                BfgsOptimizer optimizer = new BfgsOptimizer();
                double[] result = optimizer.Optimize(func, initial, 1e-8, 2000);
                for (int i = 0; i < n; i++)
                {
                    Assert.AreEqual(optimum[i], result[i], 1e-3, "Test " + test + " coordinate " + i + " differs.");
                }
            }
        }
    }
}