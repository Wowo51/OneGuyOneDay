using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LanczosIteration;

namespace LanczosIterationTest
{
    [TestClass]
    public class LanczosAlgorithmTests
    {
        [TestMethod]
        public void TestNullMatrix_ReturnsEmptyResult()
        {
            LanczosResult result = LanczosAlgorithm.ReduceToTridiagonal(null);
            Assert.AreEqual(0, result.Alphas.Length, "Expected empty Alphas for null matrix.");
            Assert.AreEqual(0, result.Betas.Length, "Expected empty Betas for null matrix.");
        }

        [TestMethod]
        public void TestNonSquareMatrix_ReturnsEmptyResult()
        {
            double[, ] nonSquare = new double[, ]
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
            LanczosResult result = LanczosAlgorithm.ReduceToTridiagonal(nonSquare);
            Assert.AreEqual(0, result.Alphas.Length, "Expected empty Alphas for non-square matrix.");
            Assert.AreEqual(0, result.Betas.Length, "Expected empty Betas for non-square matrix.");
        }

        [TestMethod]
        public void TestIdentityMatrix_Tridiagonal()
        {
            int n = 3;
            double[, ] identity = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    identity[i, j] = (i == j) ? 1.0 : 0.0;
                }
            }

            LanczosResult result = LanczosAlgorithm.ReduceToTridiagonal(identity);
            Assert.AreEqual(n, result.Alphas.Length, "Alphas length should equal matrix dimension.");
            Assert.AreEqual(1.0, result.Alphas[0], 1e-9, "First alpha should be 1.");
            double[, ] triMatrix = result.GetTridiagonalMatrix();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (Math.Abs(i - j) > 1)
                    {
                        Assert.AreEqual(0.0, triMatrix[i, j], 1e-9, "Element outside tridiagonal should be zero.");
                    }
                }
            }
        }

        [TestMethod]
        public void TestSingleElementMatrix()
        {
            double[, ] matrix = new double[1, 1];
            matrix[0, 0] = 42.0;
            LanczosResult result = LanczosAlgorithm.ReduceToTridiagonal(matrix);
            Assert.AreEqual(1, result.Alphas.Length, "Alpha length for 1x1 matrix should be 1.");
            Assert.AreEqual(0, result.Betas.Length, "Beta length for 1x1 matrix should be 0.");
            double[, ] triMatrix = result.GetTridiagonalMatrix();
            Assert.AreEqual(42.0, triMatrix[0, 0], 1e-9, "Diagonal element should match.");
        }

        [TestMethod]
        public void TestGeneratedSymmetricMatrix()
        {
            int n = 5;
            double[, ] matrix = new double[n, n];
            Random randomGenerator = new Random(1234);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    double value = randomGenerator.NextDouble() * 100;
                    matrix[i, j] = value;
                    matrix[j, i] = value;
                }
            }

            LanczosResult result = LanczosAlgorithm.ReduceToTridiagonal(matrix);
            double[, ] triMatrix = result.GetTridiagonalMatrix();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Assert.AreEqual(triMatrix[i, j], triMatrix[j, i], 1e-9, "Tridiagonal matrix must be symmetric.");
                    if (Math.Abs(i - j) > 1)
                    {
                        Assert.AreEqual(0.0, triMatrix[i, j], 1e-9, "Element (" + i + ", " + j + ") should be zero.");
                    }
                }
            }
        }

        [TestMethod]
        public void TestLargeRandomSymmetricMatrix()
        {
            int n = 50;
            double[, ] matrix = new double[n, n];
            Random randomGenerator = new Random(5678);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    double value = randomGenerator.NextDouble() * 100;
                    matrix[i, j] = value;
                    matrix[j, i] = value;
                }
            }

            LanczosResult result = LanczosAlgorithm.ReduceToTridiagonal(matrix);
            double[, ] triMatrix = result.GetTridiagonalMatrix();
            Assert.AreEqual(n, result.Alphas.Length, "Alphas length should equal matrix dimension.");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Assert.AreEqual(triMatrix[i, j], triMatrix[j, i], 1e-9, "Tridiagonal matrix must be symmetric.");
                    if (Math.Abs(i - j) > 1)
                    {
                        Assert.AreEqual(0.0, triMatrix[i, j], 1e-9, "Element outside tridiagonal should be zero.");
                    }
                }
            }
        }
    }
}