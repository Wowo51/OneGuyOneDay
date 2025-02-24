using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerIteration;

namespace PowerIterationTest
{
    [TestClass]
    public class PowerIterationAlgorithmTests
    {
        [TestMethod]
        public void TestValidMatrix()
        {
            double[, ] matrix = new double[, ]
            {
                {
                    2,
                    0
                },
                {
                    0,
                    1
                }
            };
            double[] initial = new double[]
            {
                1,
                1
            };
            double tolerance = 1E-6;
            int maxIterations = 1000;
            (double eigenvalue, double[] eigenvector) result = PowerIterationAlgorithm.Compute(matrix, initial, tolerance, maxIterations);
            Assert.IsTrue(Math.Abs(result.eigenvalue - 2) < tolerance, "Eigenvalue is not as expected.");
            Assert.IsTrue(Math.Abs(result.eigenvector[0]) > 0.9, "Eigenvector first component is too small.");
        }

        [TestMethod]
        public void TestDimensionMismatch()
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
                }
            };
            double[] initial = new double[]
            {
                1
            };
            double tolerance = 1E-6;
            int maxIterations = 1000;
            (double eigenvalue, double[] eigenvector) result = PowerIterationAlgorithm.Compute(matrix, initial, tolerance, maxIterations);
            Assert.AreEqual(0.0, result.eigenvalue, "Eigenvalue should be 0 for dimension mismatch.");
            Assert.AreEqual(0, result.eigenvector.Length, "Eigenvector should be empty for dimension mismatch.");
        }

        [TestMethod]
        public void TestNullMatrix()
        {
            double[, ]? matrix = null;
            double[] initial = new double[]
            {
                1,
                1
            };
            double tolerance = 1E-6;
            int maxIterations = 1000;
            (double eigenvalue, double[] eigenvector) result = PowerIterationAlgorithm.Compute(matrix, initial, tolerance, maxIterations);
            Assert.AreEqual(0.0, result.eigenvalue, "Eigenvalue should be 0 for null matrix.");
            Assert.AreEqual(0, result.eigenvector.Length, "Eigenvector should be empty for null matrix.");
        }

        [TestMethod]
        public void TestNullVector()
        {
            double[, ] matrix = new double[, ]
            {
                {
                    4,
                    1
                },
                {
                    2,
                    3
                }
            };
            double[]? initial = null;
            double tolerance = 1E-6;
            int maxIterations = 1000;
            (double eigenvalue, double[] eigenvector) result = PowerIterationAlgorithm.Compute(matrix, initial, tolerance, maxIterations);
            Assert.AreEqual(0.0, result.eigenvalue, "Eigenvalue should be 0 for null vector.");
            Assert.AreEqual(0, result.eigenvector.Length, "Eigenvector should be empty for null vector.");
        }

        [TestMethod]
        public void TestIdentityMatrix()
        {
            double[, ] matrix = new double[, ]
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
            double[] initial = new double[]
            {
                2,
                3
            };
            double tolerance = 1E-6;
            int maxIterations = 1000;
            (double eigenvalue, double[] eigenvector) result = PowerIterationAlgorithm.Compute(matrix, initial, tolerance, maxIterations);
            Assert.IsTrue(Math.Abs(result.eigenvalue - 1.0) < tolerance, "Eigenvalue should be 1 for identity matrix.");
            double norm = Math.Sqrt(result.eigenvector[0] * result.eigenvector[0] + result.eigenvector[1] * result.eigenvector[1]);
            Assert.IsTrue(Math.Abs(norm - 1.0) < tolerance, "Eigenvector should be normalized.");
        }

        [TestMethod]
        public void TestLargeSymmetricMatrix()
        {
            int size = 5;
            double[, ] matrix = GenerateRandomSymmetricMatrix(size);
            double[] initial = GenerateRandomVector(size);
            double tolerance = 1E-6;
            int maxIterations = 10000;
            (double eigenvalue, double[] eigenvector) result = PowerIterationAlgorithm.Compute(matrix, initial, tolerance, maxIterations);
            double norm = 0.0;
            for (int i = 0; i < result.eigenvector.Length; i++)
            {
                norm += result.eigenvector[i] * result.eigenvector[i];
            }

            norm = Math.Sqrt(norm);
            Assert.IsTrue(Math.Abs(norm - 1.0) < tolerance, "Eigenvector is not normalized.");
        }

        private static double[, ] GenerateRandomSymmetricMatrix(int size)
        {
            double[, ] matrix = new double[size, size];
            Random random = new Random(0);
            for (int i = 0; i < size; i++)
            {
                for (int j = i; j < size; j++)
                {
                    double value = random.NextDouble() * 10;
                    matrix[i, j] = value;
                    matrix[j, i] = value;
                }
            }

            return matrix;
        }

        private static double[] GenerateRandomVector(int size)
        {
            double[] vector = new double[size];
            Random random = new Random(1);
            for (int i = 0; i < size; i++)
            {
                vector[i] = random.NextDouble() * 10;
            }

            return vector;
        }
    }
}