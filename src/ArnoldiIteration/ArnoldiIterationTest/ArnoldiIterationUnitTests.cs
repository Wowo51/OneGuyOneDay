using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArnoldiIteration;

namespace ArnoldiIterationTest
{
    [TestClass]
    public class ArnoldiIterationUnitTests
    {
        [TestMethod]
        public void TestNonSquareMatrix()
        {
            double[, ] A = new double[, ]
            {
                {
                    1,
                    2,
                    3
                },
                {
                    4,
                    5,
                    6
                }
            };
            double[] b = new double[]
            {
                1,
                0
            };
            int m = 2;
            ArnoldiSolver solver = new ArnoldiSolver();
            ArnoldiResult result = solver.Compute(A, b, m);
            Assert.IsNull(result.Basis, "Basis should be null for non-square matrix");
            Assert.IsNull(result.Hessenberg, "Hessenberg should be null for non-square matrix");
        }

        [TestMethod]
        public void TestZeroVector()
        {
            double[, ] A = new double[, ]
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
            double[] b = new double[]
            {
                0,
                0
            };
            int m = 1;
            ArnoldiSolver solver = new ArnoldiSolver();
            ArnoldiResult result = solver.Compute(A, b, m);
            Assert.IsNull(result.Basis, "Basis should be null for zero vector b");
            Assert.IsNull(result.Hessenberg, "Hessenberg should be null for zero vector b");
        }

        [TestMethod]
        public void TestIdentityMatrixSingleIteration()
        {
            double[, ] A = new double[, ]
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
            double[] b = new double[]
            {
                1,
                0
            };
            int m = 1;
            ArnoldiSolver solver = new ArnoldiSolver();
            ArnoldiResult result = solver.Compute(A, b, m);
            Assert.IsNotNull(result.Basis, "Basis should not be null for identity matrix with nonzero b");
            Assert.IsNotNull(result.Hessenberg, "Hessenberg should not be null for identity matrix with nonzero b");
            double tolerance = 1e-6;
            double v0_0 = result.Basis[0, 0];
            double v0_1 = result.Basis[1, 0];
            Assert.IsTrue(System.Math.Abs(v0_0 - 1.0) < tolerance, "First element of v0 should be 1");
            Assert.IsTrue(System.Math.Abs(v0_1 - 0.0) < tolerance, "Second element of v0 should be 0");
            double h00 = result.Hessenberg[0, 0];
            double h10 = result.Hessenberg[1, 0];
            Assert.IsTrue(System.Math.Abs(h00 - 1.0) < tolerance, "H[0,0] should be 1");
            Assert.IsTrue(System.Math.Abs(h10 - 0.0) < tolerance, "H[1,0] should be 0");
        }

        [TestMethod]
        public void TestArnoldiWithRandomMatrix()
        {
            int n = 4;
            int m = 3;
            double[, ] A = new double[n, n];
            double[] b = new double[n];
            System.Random random = new System.Random(12345);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    A[i, j] = 0.1 + random.NextDouble();
                }

                b[i] = 0.1 + random.NextDouble();
            }

            ArnoldiSolver solver = new ArnoldiSolver();
            ArnoldiResult result = solver.Compute(A, b, m);
            Assert.IsNotNull(result.Basis, "Basis should not be null for random nonzero data");
            Assert.IsNotNull(result.Hessenberg, "Hessenberg should not be null for random nonzero data");
            int computedColumns = 0;
            double tol = 1e-6;
            int rows = result.Basis.GetLength(0);
            int cols = result.Basis.GetLength(1);
            for (int j = 0; j < cols; j++)
            {
                double normCol = 0;
                for (int i = 0; i < rows; i++)
                {
                    normCol += result.Basis[i, j] * result.Basis[i, j];
                }

                normCol = System.Math.Sqrt(normCol);
                if (normCol > tol)
                {
                    computedColumns++;
                }
            }

            Assert.IsTrue(computedColumns >= 2, "At least 2 columns should be computed");
            for (int j = 0; j < computedColumns; j++)
            {
                double[] vj = GetColumn(result.Basis, j);
                double normvj = Norm(vj);
                Assert.IsTrue(System.Math.Abs(normvj - 1.0) < tol, "Basis column " + j + " is not normalized");
                for (int k = j + 1; k < computedColumns; k++)
                {
                    double[] vk = GetColumn(result.Basis, k);
                    double dot = DotProduct(vj, vk);
                    Assert.IsTrue(System.Math.Abs(dot) < tol, "Basis columns " + j + " and " + k + " are not orthogonal");
                }
            }

            for (int j = 0; j < computedColumns - 1; j++)
            {
                double[] vj = GetColumn(result.Basis, j);
                double[] Avj = MultiplyMatrixVector(A, vj);
                double[] reconstruction = new double[n];
                for (int i = 0; i <= j; i++)
                {
                    double h = result.Hessenberg[i, j];
                    double[] vi = GetColumn(result.Basis, i);
                    for (int k = 0; k < n; k++)
                    {
                        reconstruction[k] += h * vi[k];
                    }
                }

                double hNext = result.Hessenberg[j + 1, j];
                double[] vNext = GetColumn(result.Basis, j + 1);
                for (int k = 0; k < n; k++)
                {
                    reconstruction[k] += hNext * vNext[k];
                }

                for (int k = 0; k < n; k++)
                {
                    Assert.IsTrue(System.Math.Abs(Avj[k] - reconstruction[k]) < tol, "Arnoldi relation failed at index " + k + " for iteration " + j);
                }
            }
        }

        private static double[] GetColumn(double[, ] matrix, int colIndex)
        {
            int rows = matrix.GetLength(0);
            double[] column = new double[rows];
            for (int i = 0; i < rows; i++)
            {
                column[i] = matrix[i, colIndex];
            }

            return column;
        }

        private static double DotProduct(double[] a, double[] b)
        {
            int length = a.Length;
            double sum = 0;
            for (int i = 0; i < length; i++)
            {
                sum += a[i] * b[i];
            }

            return sum;
        }

        private static double Norm(double[] a)
        {
            return System.Math.Sqrt(DotProduct(a, a));
        }

        private static double[] MultiplyMatrixVector(double[, ] matrix, double[] vector)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            double[] result = new double[rows];
            for (int i = 0; i < rows; i++)
            {
                double sum = 0;
                for (int j = 0; j < cols; j++)
                {
                    sum += matrix[i, j] * vector[j];
                }

                result[i] = sum;
            }

            return result;
        }
    }
}