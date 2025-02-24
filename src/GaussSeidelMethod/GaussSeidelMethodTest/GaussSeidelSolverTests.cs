using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GaussSeidelMethod;

namespace GaussSeidelMethodTest
{
    [TestClass]
    public class GaussSeidelSolverTests
    {
        [TestMethod]
        public void TestSimpleSystem()
        {
            double[, ] matrix = new double[, ]
            {
                {
                    3,
                    1
                },
                {
                    1,
                    3
                }
            };
            double[] b = new double[]
            {
                10,
                10
            };
            double[] initial = new double[]
            {
                0,
                0
            };
            double tolerance = 1e-6;
            int maxIterations = 100;
            double[] solution = GaussSeidelSolver.Solve(matrix, b, initial, tolerance, maxIterations);
            Assert.AreEqual(2, solution.Length);
            double toleranceAssert = 1e-4;
            Assert.IsTrue(Math.Abs(solution[0] - 2.5) < toleranceAssert, "x[0] not within tolerance");
            Assert.IsTrue(Math.Abs(solution[1] - 2.5) < toleranceAssert, "x[1] not within tolerance");
        }

        [TestMethod]
        public void TestInvalidDimensions()
        {
            double[, ] matrix = new double[, ]
            {
                {
                    1,
                    2
                },
                {
                    3,
                    4
                },
                {
                    5,
                    6
                }
            };
            double[] b = new double[]
            {
                1,
                1
            };
            double[] initial = new double[]
            {
                0,
                0
            };
            double tolerance = 1e-6;
            int maxIterations = 10;
            double[] solution = GaussSeidelSolver.Solve(matrix, b, initial, tolerance, maxIterations);
            Assert.AreEqual(initial.Length, solution.Length);
            Assert.AreEqual(initial[0], solution[0]);
            Assert.AreEqual(initial[1], solution[1]);
        }

        [TestMethod]
        public void TestZeroDiagonal()
        {
            double[, ] matrix = new double[, ]
            {
                {
                    0,
                    1
                },
                {
                    1,
                    3
                }
            };
            double[] b = new double[]
            {
                4,
                10
            };
            double[] initial = new double[]
            {
                1,
                2
            };
            double tolerance = 1e-6;
            int maxIterations = 10;
            double[] solution = GaussSeidelSolver.Solve(matrix, b, initial, tolerance, maxIterations);
            Assert.AreEqual(initial[0], solution[0]);
        }

        [TestMethod]
        public void TestLargeSystem()
        {
            int n = 50;
            double[, ] matrix = new double[n, n];
            double[] b = new double[n];
            double[] initial = new double[n];
            Random random = new Random(42);
            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        double value = random.NextDouble();
                        matrix[i, j] = value;
                        sum += Math.Abs(value);
                    }
                }

                matrix[i, i] = sum + random.NextDouble() + 1;
                b[i] = 1.0 + random.NextDouble();
                initial[i] = 0;
            }

            double tolerance = 1e-6;
            int maxIterations = 500;
            double[] solution = GaussSeidelSolver.Solve(matrix, b, initial, tolerance, maxIterations);
            Assert.AreEqual(n, solution.Length);
            for (int i = 0; i < n; i++)
            {
                Assert.IsFalse(Double.IsNaN(solution[i]));
                Assert.IsFalse(Double.IsInfinity(solution[i]));
            }
        }
    }
}