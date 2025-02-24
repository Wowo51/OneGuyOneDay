using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GradientDescent;

namespace GradientDescentTest
{
    [TestClass]
    public class GradientDescentSolverTests
    {
        [TestMethod]
        public void TestNullInitialGuess()
        {
            GradientDescentSolver solver = new GradientDescentSolver(0.1, 100, 1e-6);
            double[] result = solver.Solve(delegate (double[] dummy)
            {
                return 0.0;
            }, delegate (double[] dummy)
            {
                return new double[dummy.Length];
            }, null);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void TestNullCostAndGradient()
        {
            double[] initialGuess = new double[]
            {
                1.0,
                2.0
            };
            GradientDescentSolver solver = new GradientDescentSolver(0.1, 100, 1e-6);
            double[] result = solver.Solve(null, null, initialGuess);
            CollectionAssert.AreEqual(initialGuess, result);
        }

        [TestMethod]
        public void TestQuadratic1D()
        {
            double[] initialGuess = new double[]
            {
                0.0
            };
            GradientDescentSolver solver = new GradientDescentSolver(0.1, 1000, 1e-6);
            Func<double[], double> cost = delegate (double[] x)
            {
                return (x[0] - 5.0) * (x[0] - 5.0);
            };
            Func<double[], double[]> gradient = delegate (double[] x)
            {
                return new double[]
                {
                    2.0 * (x[0] - 5.0)
                };
            };
            double[] result = solver.Solve(cost, gradient, initialGuess);
            Assert.IsTrue(Math.Abs(result[0] - 5.0) < 1e-3);
        }

        [TestMethod]
        public void TestQuadratic2D()
        {
            double[] initialGuess = new double[]
            {
                0.0,
                0.0
            };
            GradientDescentSolver solver = new GradientDescentSolver(0.1, 1000, 1e-6);
            Func<double[], double> cost = delegate (double[] x)
            {
                return (x[0] - 2.0) * (x[0] - 2.0) + (x[1] - 3.0) * (x[1] - 3.0);
            };
            Func<double[], double[]> gradient = delegate (double[] x)
            {
                return new double[]
                {
                    2.0 * (x[0] - 2.0),
                    2.0 * (x[1] - 3.0)
                };
            };
            double[] result = solver.Solve(cost, gradient, initialGuess);
            Assert.IsTrue(Math.Abs(result[0] - 2.0) < 1e-3);
            Assert.IsTrue(Math.Abs(result[1] - 3.0) < 1e-3);
        }

        [TestMethod]
        public void TestQuadraticHighDimension()
        {
            int dimension = 100;
            double[] initialGuess = new double[dimension];
            for (int i = 0; i < dimension; i++)
            {
                initialGuess[i] = 0.0;
            }

            GradientDescentSolver solver = new GradientDescentSolver(0.1, 1000, 1e-6);
            Func<double[], double> cost = delegate (double[] x)
            {
                double sum = 0;
                for (int i = 0; i < x.Length; i++)
                {
                    double diff = x[i] - 1.0;
                    sum += diff * diff;
                }

                return sum;
            };
            Func<double[], double[]> gradient = delegate (double[] x)
            {
                double[] grad = new double[x.Length];
                for (int i = 0; i < x.Length; i++)
                {
                    grad[i] = 2.0 * (x[i] - 1.0);
                }

                return grad;
            };
            double[] result = solver.Solve(cost, gradient, initialGuess);
            for (int i = 0; i < dimension; i++)
            {
                Assert.IsTrue(Math.Abs(result[i] - 1.0) < 1e-2);
            }
        }
    }
}