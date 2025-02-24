using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayleighQuotientIteration;

namespace RayleighQuotientIterationTest
{
    [TestClass]
    public class RayleighQuotientIterationTests
    {
        [TestMethod]
        public void TestVectorNormalization()
        {
            double[] vector = new double[]
            {
                3.0,
                4.0
            };
            double[] normalized = VectorHelper.Normalize(vector);
            double norm = VectorHelper.Norm(normalized);
            Assert.IsTrue(Math.Abs(norm - 1.0) < 1e-9, "Normalized vector should have norm 1");
        }

        [TestMethod]
        public void TestDotProduct()
        {
            double[] a = new double[]
            {
                1.0,
                2.0,
                3.0
            };
            double[] b = new double[]
            {
                4.0,
                5.0,
                6.0
            };
            double dot = VectorHelper.Dot(a, b);
            Assert.AreEqual(32.0, dot, 1e-9, "Dot product is incorrect");
        }

        [TestMethod]
        public void TestLinearSystemSolverSimple()
        {
            double[, ] A = new double[, ]
            {
                {
                    2.0,
                    1.0
                },
                {
                    1.0,
                    3.0
                }
            };
            double[] b = new double[]
            {
                5.0,
                7.0
            };
            double[] x = LinearSystemSolver.Solve(A, b);
            double[] Ax = new double[2];
            for (int i = 0; i < 2; i++)
            {
                Ax[i] = 0.0;
                for (int j = 0; j < 2; j++)
                {
                    Ax[i] += A[i, j] * x[j];
                }
            }

            for (int i = 0; i < 2; i++)
            {
                Assert.IsTrue(Math.Abs(Ax[i] - b[i]) < 1e-9, "Linear system solution incorrect at index " + i.ToString());
            }
        }

        [TestMethod]
        public void TestRayleighQuotientIterationAlgorithm_DiagonalMatrix()
        {
            double[, ] matrix = new double[, ]
            {
                {
                    3.0,
                    0.0
                },
                {
                    0.0,
                    2.0
                }
            };
            double[] initialVector = new double[]
            {
                1.0,
                0.0
            };
            int maxIterations = 10;
            double tolerance = 1e-9;
            Eigenpair eigenpair = RayleighQuotientIterationAlgorithm.Compute(matrix, initialVector, maxIterations, tolerance);
            Assert.IsTrue(Math.Abs(eigenpair.Eigenvalue - 3.0) < 1e-9, "Eigenvalue should converge to 3 for initial vector [1,0]");
        }

        [TestMethod]
        public void TestRayleighQuotientIterationAlgorithm_ResidualCheck()
        {
            int n = 5;
            double[, ] matrix = new double[n, n];
            Random rand = new Random(42);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = rand.NextDouble();
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    double avg = (matrix[i, j] + matrix[j, i]) / 2.0;
                    matrix[i, j] = avg;
                    matrix[j, i] = avg;
                }
            }

            double[] initialVector = new double[n];
            for (int i = 0; i < n; i++)
            {
                initialVector[i] = rand.NextDouble();
            }

            int maxIterations = 100;
            double tolerance = 1e-6;
            Eigenpair eigenpair = RayleighQuotientIterationAlgorithm.Compute(matrix, initialVector, maxIterations, tolerance);
            double[] residual = new double[n];
            for (int i = 0; i < n; i++)
            {
                residual[i] = 0.0;
                for (int j = 0; j < n; j++)
                {
                    residual[i] += matrix[i, j] * eigenpair.Eigenvector[j];
                }

                residual[i] -= eigenpair.Eigenvalue * eigenpair.Eigenvector[i];
            }

            double residualNorm = 0.0;
            for (int i = 0; i < n; i++)
            {
                residualNorm += residual[i] * residual[i];
            }

            residualNorm = Math.Sqrt(residualNorm);
            Assert.IsTrue(residualNorm < 1e-5, "Residual norm is too high: " + residualNorm.ToString());
        }
    }
}