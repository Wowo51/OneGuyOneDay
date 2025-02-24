using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuccessiveOverRelaxation;

namespace SuccessiveOverRelaxationTest
{
    [TestClass]
    public class SORSolverTests
    {
        [TestMethod]
        public void TestSimpleSystem()
        {
            double[, ] A = new double[, ]
            {
                {
                    4,
                    1
                },
                {
                    1,
                    3
                }
            };
            double[] b = new double[]
            {
                15,
                10
            };
            double relaxation = 1.25;
            double tolerance = 1e-6;
            int maxIterations = 1000;
            SORSolver solver = new SORSolver();
            double[] result = solver.Solve(A, b, relaxation, tolerance, maxIterations);
            double expected0 = 35.0 / 11.0;
            double expected1 = 25.0 / 11.0;
            Assert.AreEqual(expected0, result[0], 1e-4, "x[0] not as expected");
            Assert.AreEqual(expected1, result[1], 1e-4, "x[1] not as expected");
        }

        [TestMethod]
        public void TestIdentitySystem()
        {
            int n = 3;
            double[, ] A = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    A[i, j] = (i == j) ? 1.0 : 0.0;
                }
            }

            double[] b = new double[]
            {
                1,
                2,
                3
            };
            double relaxation = 1.0;
            double tolerance = 1e-9;
            int maxIterations = 100;
            SORSolver solver = new SORSolver();
            double[] result = solver.Solve(A, b, relaxation, tolerance, maxIterations);
            for (int i = 0; i < n; i++)
            {
                Assert.AreEqual(b[i], result[i], 1e-9, $"x[{i}] is not equal to b[{i}]");
            }
        }

        [TestMethod]
        public void TestLargeSyntheticSystem()
        {
            int n = 50;
            double[, ] A = new double[n, n];
            double[] b = new double[n];
            Random random = new Random(42);
            for (int i = 0; i < n; i++)
            {
                double sum = 0.0;
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        double value = random.NextDouble() - 0.5;
                        A[i, j] = value;
                        sum += Math.Abs(value);
                    }
                }

                A[i, i] = sum + 1.0;
                b[i] = random.NextDouble();
            }

            double relaxation = 1.25;
            double tolerance = 1e-6;
            int maxIterations = 10000;
            SORSolver solver = new SORSolver();
            double[] x = solver.Solve(A, b, relaxation, tolerance, maxIterations);
            double residualNorm = 0.0;
            for (int i = 0; i < n; i++)
            {
                double Ax_minus_b = 0.0;
                for (int j = 0; j < n; j++)
                {
                    Ax_minus_b += A[i, j] * x[j];
                }

                residualNorm += (Ax_minus_b - b[i]) * (Ax_minus_b - b[i]);
            }

            residualNorm = Math.Sqrt(residualNorm);
            Assert.IsTrue(residualNorm < 1e-3, "Residual norm is too high");
        }
    }
}