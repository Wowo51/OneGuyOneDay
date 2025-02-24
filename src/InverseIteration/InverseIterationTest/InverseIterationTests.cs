using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InverseIteration;

namespace InverseIterationTest
{
    [TestClass]
    public class InverseIterationTests
    {
        [TestMethod]
        public void TestComputeEigenvector_ValidInput()
        {
            double[, ] A = new double[2, 2]
            {
                {
                    2,
                    1
                },
                {
                    1,
                    2
                }
            };
            double eigenvalue = 3.0;
            double[] initialVector = new double[2]
            {
                1,
                0
            };
            int maxIterations = 50;
            double tolerance = 1e-6;
            double[] result = InverseIterationSolver.ComputeEigenvector(A, eigenvalue, initialVector, maxIterations, tolerance);
            double sqrt2 = Math.Sqrt(2.0);
            double[] expected = new double[2]
            {
                1.0 / sqrt2,
                1.0 / sqrt2
            };
            double diff1 = ComputeDistance(result, expected);
            double[] negExpected = new double[2]
            {
                -expected[0],
                -expected[1]
            };
            double diff2 = ComputeDistance(result, negExpected);
            double diff = diff1 < diff2 ? diff1 : diff2;
            Assert.IsTrue(diff < 1e-4, "Computed eigenvector does not match expected value.");
        }

        [TestMethod]
        public void TestComputeEigenvector_InvalidDimensions()
        {
            double[, ] A = new double[2, 2]
            {
                {
                    2,
                    1
                },
                {
                    1,
                    2
                }
            };
            double eigenvalue = 3.0;
            double[] initialVector = new double[3]
            {
                1,
                0,
                0
            };
            int maxIterations = 50;
            double tolerance = 1e-6;
            double[] result = InverseIterationSolver.ComputeEigenvector(A, eigenvalue, initialVector, maxIterations, tolerance);
            Assert.AreEqual(0, result.Length, "Expected result to be empty array for invalid dimensions.");
        }

        [TestMethod]
        public void TestComputeEigenvector_NullMatrix()
        {
            double eigenvalue = 3.0;
            double[] initialVector = new double[2]
            {
                1,
                0
            };
            int maxIterations = 50;
            double tolerance = 1e-6;
            double[, ]? A = null;
            double[] result = InverseIterationSolver.ComputeEigenvector(A, eigenvalue, initialVector, maxIterations, tolerance);
            Assert.AreEqual(0, result.Length, "Expected empty array when matrix is null.");
        }

        [TestMethod]
        public void TestComputeEigenvector_NullVector()
        {
            double[, ] A = new double[2, 2]
            {
                {
                    2,
                    1
                },
                {
                    1,
                    2
                }
            };
            double eigenvalue = 3.0;
            double[]? initialVector = null;
            int maxIterations = 50;
            double tolerance = 1e-6;
            double[] result = InverseIterationSolver.ComputeEigenvector(A, eigenvalue, initialVector, maxIterations, tolerance);
            Assert.AreEqual(0, result.Length, "Expected empty array when initial vector is null.");
        }

        [TestMethod]
        public void TestMatrixHelper_CopyMatrix()
        {
            double[, ] original = new double[2, 2]
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
            double[, ] copy = MatrixHelper.CopyMatrix(original);
            original[0, 0] = 100;
            Assert.AreEqual(1, copy[0, 0], "Copy matrix should not reflect changes in the original.");
            Assert.AreEqual(2, copy[0, 1]);
            Assert.AreEqual(3, copy[1, 0]);
            Assert.AreEqual(4, copy[1, 1]);
        }

        [TestMethod]
        public void TestMatrixHelper_SubtractFromDiagonal()
        {
            double[, ] matrix = new double[2, 2]
            {
                {
                    5,
                    1
                },
                {
                    2,
                    6
                }
            };
            MatrixHelper.SubtractFromDiagonal(matrix, 1);
            Assert.AreEqual(4, matrix[0, 0], "Diagonal element not subtracted correctly.");
            Assert.AreEqual(5, matrix[1, 1], "Diagonal element not subtracted correctly.");
            Assert.AreEqual(1, matrix[0, 1]);
            Assert.AreEqual(2, matrix[1, 0]);
        }

        [TestMethod]
        public void TestMatrixHelper_Solve()
        {
            double[, ] A = new double[2, 2]
            {
                {
                    2,
                    3
                },
                {
                    1,
                    4
                }
            };
            double[] b = new double[2]
            {
                8,
                7
            };
            double[] solution = MatrixHelper.Solve(A, b);
            Assert.IsTrue(Math.Abs(solution[0] - 2.2) < 1e-4, "Solution x is incorrect.");
            Assert.IsTrue(Math.Abs(solution[1] - 1.2) < 1e-4, "Solution y is incorrect.");
        }

        [TestMethod]
        public void TestMatrixHelper_NormalizeAndNorm()
        {
            double[] vector = new double[3]
            {
                3,
                4,
                0
            };
            double[] normalized = MatrixHelper.Normalize(vector);
            double norm = MatrixHelper.Norm(normalized);
            Assert.IsTrue(Math.Abs(norm - 1.0) < 1e-6, "Normalized vector does not have norm 1.");
        }

        [TestMethod]
        public void TestMatrixHelper_Distance()
        {
            double[] v1 = new double[3]
            {
                1,
                2,
                3
            };
            double[] v2 = new double[3]
            {
                4,
                6,
                3
            };
            double distance = MatrixHelper.Distance(v1, v2);
            Assert.IsTrue(Math.Abs(distance - 5) < 1e-6, "Distance computed is incorrect.");
        }

        [TestMethod]
        public void TestMatrixHelper_Normalize_LargeVector()
        {
            int size = 1000;
            double[] vector = new double[size];
            Random random = new Random(42);
            for (int i = 0; i < size; i++)
            {
                vector[i] = random.NextDouble();
            }

            double[] normalized = MatrixHelper.Normalize(vector);
            double norm = MatrixHelper.Norm(normalized);
            Assert.IsTrue(Math.Abs(norm - 1.0) < 1e-6, "Large normalized vector does not have norm 1.");
        }

        private double ComputeDistance(double[] v1, double[] v2)
        {
            if (v1 == null || v2 == null || v1.Length != v2.Length)
            {
                return double.MaxValue;
            }

            double sum = 0;
            for (int i = 0; i < v1.Length; i++)
            {
                double diff = v1[i] - v2[i];
                sum += diff * diff;
            }

            return Math.Sqrt(sum);
        }
    }
}