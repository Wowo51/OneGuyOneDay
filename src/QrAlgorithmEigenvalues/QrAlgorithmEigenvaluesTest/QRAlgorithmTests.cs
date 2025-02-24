using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using QrAlgorithmEigenvalues;

namespace QrAlgorithmEigenvaluesTest
{
    [TestClass]
    public class QRAlgorithmTests
    {
        private const double Tolerance = 1e-6;
        [TestMethod]
        public void TestCompute_NullMatrix()
        {
            QrAlgorithmEigenvalues.QRAlgorithm.EigenResult result = QrAlgorithmEigenvalues.QRAlgorithm.Compute(null, false);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Eigenvalues.Length);
            Assert.IsNull(result.Eigenvectors);
        }

        [TestMethod]
        public void TestCompute_NonSquareMatrix()
        {
            double[, ] matrix = new double[, ]
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
            QrAlgorithmEigenvalues.QRAlgorithm.EigenResult result = QrAlgorithmEigenvalues.QRAlgorithm.Compute(matrix, false);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Eigenvalues.Length);
            Assert.IsNull(result.Eigenvectors);
        }

        [TestMethod]
        public void TestCompute_DiagonalMatrix()
        {
            double[, ] matrix = new double[, ]
            {
                {
                    5.0,
                    0.0
                },
                {
                    0.0,
                    10.0
                }
            };
            QrAlgorithmEigenvalues.QRAlgorithm.EigenResult result = QrAlgorithmEigenvalues.QRAlgorithm.Compute(matrix, false);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Eigenvalues.Length);
            Assert.AreEqual(5.0, result.Eigenvalues[0], Tolerance);
            Assert.AreEqual(10.0, result.Eigenvalues[1], Tolerance);
        }

        [TestMethod]
        public void TestCompute_IdentityMatrix()
        {
            int n = 3;
            double[, ] matrix = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = (i == j) ? 1.0 : 0.0;
                }
            }

            QrAlgorithmEigenvalues.QRAlgorithm.EigenResult result = QrAlgorithmEigenvalues.QRAlgorithm.Compute(matrix, false);
            Assert.IsNotNull(result);
            Assert.AreEqual(n, result.Eigenvalues.Length);
            for (int i = 0; i < n; i++)
            {
                Assert.AreEqual(1.0, result.Eigenvalues[i], Tolerance);
            }
        }

        [TestMethod]
        public void TestCompute_Eigenvectors()
        {
            double[, ] matrix = new double[, ]
            {
                {
                    2.0,
                    0.0
                },
                {
                    0.0,
                    3.0
                }
            };
            QrAlgorithmEigenvalues.QRAlgorithm.EigenResult result = QrAlgorithmEigenvalues.QRAlgorithm.Compute(matrix, true);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Eigenvalues.Length);
            Assert.IsNotNull(result.Eigenvectors);
            int rows = result.Eigenvectors.GetLength(0);
            int cols = result.Eigenvectors.GetLength(1);
            Assert.AreEqual(2, rows);
            Assert.AreEqual(2, cols);
        }

        [TestMethod]
        public void TestCompute_RandomSymmetricMatrix()
        {
            int n = 5;
            double[, ] matrix = new double[n, n];
            Random random = new Random(42);
            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    double value = random.NextDouble() * 20 - 10;
                    matrix[i, j] = value;
                    matrix[j, i] = value;
                }
            }

            double trace = 0.0;
            for (int i = 0; i < n; i++)
            {
                trace += matrix[i, i];
            }

            QrAlgorithmEigenvalues.QRAlgorithm.EigenResult result = QrAlgorithmEigenvalues.QRAlgorithm.Compute(matrix, false);
            Assert.IsNotNull(result);
            Assert.AreEqual(n, result.Eigenvalues.Length);
            double sumEigen = 0.0;
            for (int i = 0; i < n; i++)
            {
                sumEigen += result.Eigenvalues[i];
            }

            Assert.IsTrue(Math.Abs(trace - sumEigen) < 1e-2);
        }

        [TestMethod]
        public void TestCompute_Symmetric2x2Matrix()
        {
            double[, ] matrix = new double[, ]
            {
                {
                    2.0,
                    1.0
                },
                {
                    1.0,
                    2.0
                }
            };
            QrAlgorithmEigenvalues.QRAlgorithm.EigenResult result = QrAlgorithmEigenvalues.QRAlgorithm.Compute(matrix, false);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Eigenvalues.Length);
            bool condition = (Math.Abs(result.Eigenvalues[0] - 1.0) < Tolerance && Math.Abs(result.Eigenvalues[1] - 3.0) < Tolerance) || (Math.Abs(result.Eigenvalues[0] - 3.0) < Tolerance && Math.Abs(result.Eigenvalues[1] - 1.0) < Tolerance);
            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void TestCompute_OneByOneMatrix()
        {
            double[, ] matrix = new double[, ]
            {
                {
                    7.0
                }
            };
            QrAlgorithmEigenvalues.QRAlgorithm.EigenResult result = QrAlgorithmEigenvalues.QRAlgorithm.Compute(matrix, true);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Eigenvalues.Length);
            Assert.AreEqual(7.0, result.Eigenvalues[0], Tolerance);
            Assert.IsNotNull(result.Eigenvectors);
            int rows = result.Eigenvectors.GetLength(0);
            int cols = result.Eigenvectors.GetLength(1);
            Assert.AreEqual(1, rows);
            Assert.AreEqual(1, cols);
        }

        [TestMethod]
        public void TestCompute_LargeRandomSymmetricMatrix()
        {
            int n = 50;
            double[, ] matrix = new double[n, n];
            Random random = new Random(42);
            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    double value = random.NextDouble() * 20 - 10;
                    matrix[i, j] = value;
                    matrix[j, i] = value;
                }
            }

            double trace = 0.0;
            for (int i = 0; i < n; i++)
            {
                trace += matrix[i, i];
            }

            QrAlgorithmEigenvalues.QRAlgorithm.EigenResult result = QrAlgorithmEigenvalues.QRAlgorithm.Compute(matrix, false);
            Assert.IsNotNull(result);
            Assert.AreEqual(n, result.Eigenvalues.Length);
            double sumEigen = 0.0;
            for (int i = 0; i < n; i++)
            {
                sumEigen += result.Eigenvalues[i];
            }

            Assert.IsTrue(Math.Abs(trace - sumEigen) < 1e-2);
        }
    }
}