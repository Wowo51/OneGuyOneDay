using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConjugateGradientMethods;

namespace ConjugateGradientMethodsTest
{
    [TestClass]
    public class ConjugateGradientSolverTests
    {
        private static double ComputeResidualNorm(double[, ] A, double[] x, double[] b)
        {
            int n = b.Length;
            double[] Ax = new double[n];
            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                for (int j = 0; j < n; j++)
                {
                    sum += A[i, j] * x[j];
                }

                Ax[i] = sum;
            }

            double norm = 0;
            for (int i = 0; i < n; i++)
            {
                double diff = Ax[i] - b[i];
                norm += diff * diff;
            }

            return Math.Sqrt(norm);
        }

        private static void AssertArrayApproxEqual(double[] expected, double[] actual, double tolerance)
        {
            Assert.AreEqual(expected.Length, actual.Length, "Arrays have different lengths");
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(Math.Abs(expected[i] - actual[i]) <= tolerance, $"Element {i} differs: expected {expected[i]}, got {actual[i]}");
            }
        }

        [TestMethod]
        public void TestSmallSystem()
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
                1,
                2
            };
            double[] x0 = new double[]
            {
                0,
                0
            };
            double tolerance = 1e-4;
            int maxIterations = 100;
            double[] solution = ConjugateGradientSolver.Solve(A, b, x0, tolerance, maxIterations);
            double residual = ComputeResidualNorm(A, solution, b);
            Assert.IsTrue(residual < tolerance * 10, $"Residual {residual} is not below tolerance threshold");
        }

        [TestMethod]
        public void TestIdentityMatrix()
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
                5.0,
                -3.2,
                7.8
            };
            double[] x0 = new double[n];
            double tolerance = 1e-6;
            int maxIterations = 50;
            double[] solution = ConjugateGradientSolver.Solve(A, b, x0, tolerance, maxIterations);
            AssertArrayApproxEqual(b, solution, tolerance);
        }

        [TestMethod]
        public void TestDimensionMismatch()
        {
            double[, ] A = new double[, ]
            {
                {
                    1,
                    2
                },
                {
                    3,
                    4
                }
            };
            double[] b = new double[]
            {
                5,
                6
            };
            double[] x0 = new double[]
            {
                0,
                0,
                0
            }; // Mismatched length
            double tolerance = 1e-6;
            int maxIterations = 10;
            double[] solution = ConjugateGradientSolver.Solve(A, b, x0, tolerance, maxIterations);
            Assert.AreEqual(0, solution.Length, "Expected empty solution for dimension mismatch");
        }

        [TestMethod]
        public void TestNullArguments()
        {
            double[, ]? A = null;
            double[]? b = new double[]
            {
                1,
                2
            };
            double[]? x0 = new double[]
            {
                0,
                0
            };
            double tolerance = 1e-6;
            int maxIterations = 10;
            double[] solution = ConjugateGradientSolver.Solve(A, b, x0, tolerance, maxIterations);
            Assert.AreEqual(0, solution.Length, "Expected empty solution when A is null");
            A = new double[, ]
            {
                {
                    1,
                    0
                },
                {
                    0,
                    1
                }
            };
            b = null;
            solution = ConjugateGradientSolver.Solve(A, b, x0, tolerance, maxIterations);
            Assert.AreEqual(0, solution.Length, "Expected empty solution when b is null");
            b = new double[]
            {
                1,
                2
            };
            x0 = null;
            solution = ConjugateGradientSolver.Solve(A, b, x0, tolerance, maxIterations);
            Assert.AreEqual(0, solution.Length, "Expected empty solution when x0 is null");
        }

        [TestMethod]
        public void TestLargeSyntheticData()
        {
            int n = 50;
            double[, ] R = new double[n, n];
            Random random = new Random(42);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    R[i, j] = random.NextDouble();
                }
            }

            double[, ] A = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    double sum = 0.0;
                    for (int k = 0; k < n; k++)
                    {
                        sum += R[k, i] * R[k, j];
                    }

                    A[i, j] = sum;
                }

                A[i, i] += n;
            }

            double[] b = new double[n];
            for (int i = 0; i < n; i++)
            {
                b[i] = random.NextDouble();
            }

            double[] x0 = new double[n];
            double tolerance = 1e-6;
            int maxIterations = 1000;
            double[] solution = ConjugateGradientSolver.Solve(A, b, x0, tolerance, maxIterations);
            double residual = ComputeResidualNorm(A, solution, b);
            Assert.IsTrue(residual < tolerance * 100, $"Residual {residual} is not sufficiently small for large synthetic data");
        }
    }
}