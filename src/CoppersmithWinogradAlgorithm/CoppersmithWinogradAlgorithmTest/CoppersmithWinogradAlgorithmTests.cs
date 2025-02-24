using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoppersmithWinogradAlgorithm;

namespace CoppersmithWinogradAlgorithmTest
{
    [TestClass]
    public class CoppersmithWinogradAlgorithmTests
    {
        private const double Tolerance = 1e-6;
        // Helper method to perform a naive matrix multiplication.
        private Matrix MultiplyNaive(Matrix A, Matrix B)
        {
            int n = A.Size;
            Matrix result = new Matrix(n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    double sum = 0.0;
                    for (int k = 0; k < n; k++)
                    {
                        sum += A.Data[i, k] * B.Data[k, j];
                    }

                    result.Data[i, j] = sum;
                }
            }

            return result;
        }

        // Helper method to compare two matrices elementwise with a given tolerance.
        private bool AreMatricesEqual(Matrix a, Matrix b, double tolerance)
        {
            if (a.Size != b.Size)
            {
                return false;
            }

            for (int i = 0; i < a.Size; i++)
            {
                for (int j = 0; j < a.Size; j++)
                {
                    double diff = Math.Abs(a.Data[i, j] - b.Data[i, j]);
                    if (diff > tolerance)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        // Test: Multiply with null arguments should return a matrix with size 0.
        [TestMethod]
        public void TestMultiply_NullArguments()
        {
            Matrix matrix = new Matrix(2);
            Matrix result1 = CoppersmithWinograd.Multiply(null, matrix);
            Matrix result2 = CoppersmithWinograd.Multiply(matrix, null);
            Matrix result3 = CoppersmithWinograd.Multiply(null, null);
            Assert.AreEqual(0, result1.Size);
            Assert.AreEqual(0, result2.Size);
            Assert.AreEqual(0, result3.Size);
        }

        // Test: Multiply with mismatched matrix sizes should return a matrix with size 0.
        [TestMethod]
        public void TestMultiply_MismatchedSizes()
        {
            Matrix A = new Matrix(2);
            Matrix B = new Matrix(3);
            Matrix result = CoppersmithWinograd.Multiply(A, B);
            Assert.AreEqual(0, result.Size);
        }

        // Test: Multiply even-sized matrices.
        [TestMethod]
        public void TestMultiply_EvenSize()
        {
            Matrix A = new Matrix(2);
            Matrix B = new Matrix(2);
            A.Data[0, 0] = 1;
            A.Data[0, 1] = 2;
            A.Data[1, 0] = 3;
            A.Data[1, 1] = 4;
            B.Data[0, 0] = 5;
            B.Data[0, 1] = 6;
            B.Data[1, 0] = 7;
            B.Data[1, 1] = 8;
            Matrix expected = MultiplyNaive(A, B);
            Matrix result = CoppersmithWinograd.Multiply(A, B);
            Assert.IsTrue(AreMatricesEqual(expected, result, Tolerance));
        }

        // Test: Multiply odd-sized matrices (which triggers internal padding).
        [TestMethod]
        public void TestMultiply_OddSize()
        {
            Matrix A = new Matrix(3);
            Matrix B = new Matrix(3);
            A.Data[0, 0] = 1;
            A.Data[0, 1] = 2;
            A.Data[0, 2] = 3;
            A.Data[1, 0] = 4;
            A.Data[1, 1] = 5;
            A.Data[1, 2] = 6;
            A.Data[2, 0] = 7;
            A.Data[2, 1] = 8;
            A.Data[2, 2] = 9;
            B.Data[0, 0] = 9;
            B.Data[0, 1] = 8;
            B.Data[0, 2] = 7;
            B.Data[1, 0] = 6;
            B.Data[1, 1] = 5;
            B.Data[1, 2] = 4;
            B.Data[2, 0] = 3;
            B.Data[2, 1] = 2;
            B.Data[2, 2] = 1;
            Matrix expected = MultiplyNaive(A, B);
            Matrix result = CoppersmithWinograd.Multiply(A, B);
            Assert.IsTrue(AreMatricesEqual(expected, result, Tolerance));
        }

        // Test: Multiply single element matrices.
        [TestMethod]
        public void TestMultiply_SingleElement()
        {
            Matrix A = new Matrix(1);
            Matrix B = new Matrix(1);
            A.Data[0, 0] = 3;
            B.Data[0, 0] = 7;
            Matrix expected = MultiplyNaive(A, B);
            Matrix result = CoppersmithWinograd.Multiply(A, B);
            Assert.IsTrue(AreMatricesEqual(expected, result, Tolerance));
        }

        // Test: Multiply random matrices to simulate synthetic test data.
        [TestMethod]
        public void TestMultiply_RandomMatrices()
        {
            int size = 10;
            Matrix A = CreateRandomMatrix(size, 100.0);
            Matrix B = CreateRandomMatrix(size, 100.0);
            Matrix expected = MultiplyNaive(A, B);
            Matrix result = CoppersmithWinograd.Multiply(A, B);
            Assert.IsTrue(AreMatricesEqual(expected, result, Tolerance));
        }

        // Helper method to generate a random matrix with values in the range [-maxValue, maxValue].
        private Matrix CreateRandomMatrix(int size, double maxValue)
        {
            Matrix matrix = new Matrix(size);
            Random random = new Random(42);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    double value = (random.NextDouble() * 2 * maxValue) - maxValue;
                    matrix.Data[i, j] = value;
                }
            }

            return matrix;
        }
    }
}