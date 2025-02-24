using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CannonsAlgorithm;

namespace CannonsAlgorithmTest
{
    [TestClass]
    public class CannonAlgorithmTests
    {
        public static Matrix NaiveMultiply(Matrix m1, Matrix m2)
        {
            if (m1 == null || m2 == null)
            {
                return new Matrix(0, 0);
            }

            if (m1.Columns != m2.Rows)
            {
                return new Matrix(0, 0);
            }

            Matrix result = new Matrix(m1.Rows, m2.Columns);
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m2.Columns; j++)
                {
                    double sum = 0.0;
                    for (int k = 0; k < m1.Columns; k++)
                    {
                        sum += m1.Data[i, k] * m2.Data[k, j];
                    }

                    result.Data[i, j] = sum;
                }
            }

            return result;
        }

        public static bool AreMatricesEqual(Matrix m1, Matrix m2, double tolerance)
        {
            if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
            {
                return false;
            }

            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Columns; j++)
                {
                    if (Math.Abs(m1.Data[i, j] - m2.Data[i, j]) > tolerance)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        [TestMethod]
        public void Test_NullMatrices_ReturnZeroMatrix()
        {
            Matrix result = CannonAlgorithm.Multiply(null, null, 1);
            Assert.AreEqual(0, result.Rows, "Expected 0 rows for null input.");
            Assert.AreEqual(0, result.Columns, "Expected 0 columns for null input.");
        }

        [TestMethod]
        public void Test_IncompatibleDimensions_ReturnZeroMatrix()
        {
            Matrix a = new Matrix(2, 2);
            Matrix b = new Matrix(2, 3); // b is not square, triggering dimension check
            Matrix result = CannonAlgorithm.Multiply(a, b, 1);
            Assert.AreEqual(0, result.Rows, "Expected 0 rows for incompatible dimensions.");
            Assert.AreEqual(0, result.Columns, "Expected 0 columns for incompatible dimensions.");
        }

        [TestMethod]
        public void Test_InvalidGridSize_ReturnZeroMatrix()
        {
            Matrix a = new Matrix(3, 3);
            Matrix b = new Matrix(3, 3);
            Matrix result = CannonAlgorithm.Multiply(a, b, 2); // 3 mod 2 ≠ 0
            Assert.AreEqual(0, result.Rows, "Expected 0 rows for invalid grid size.");
            Assert.AreEqual(0, result.Columns, "Expected 0 columns for invalid grid size.");
        }

        [TestMethod]
        public void Test_ValidMultiplication_Small()
        {
            Matrix a = new Matrix(2, 2);
            Matrix b = new Matrix(2, 2);
            // Populate matrix a
            a.Data[0, 0] = 1;
            a.Data[0, 1] = 2;
            a.Data[1, 0] = 3;
            a.Data[1, 1] = 4;
            // Populate matrix b
            b.Data[0, 0] = 5;
            b.Data[0, 1] = 6;
            b.Data[1, 0] = 7;
            b.Data[1, 1] = 8;
            Matrix expected = new Matrix(2, 2);
            expected.Data[0, 0] = 19;
            expected.Data[0, 1] = 22;
            expected.Data[1, 0] = 43;
            expected.Data[1, 1] = 50;
            Matrix result = CannonAlgorithm.Multiply(a, b, 1);
            bool equal = AreMatricesEqual(expected, result, 1e-6);
            Assert.IsTrue(equal, "The small matrix multiplication result does not match the expected value.");
        }

        [TestMethod]
        public void Test_ValidMultiplication_MultipleGrid()
        {
            int size = 4;
            Matrix a = new Matrix(size, size);
            Matrix b = new Matrix(size, size);
            // Fill matrices with sequential numbers for determinism.
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    a.Data[i, j] = (double)(i * size + j + 1);
                    b.Data[i, j] = (double)(i * size + j + size * size + 1);
                }
            }

            Matrix expected = NaiveMultiply(a, b);
            Matrix result = CannonAlgorithm.Multiply(a, b, 2);
            bool equal = AreMatricesEqual(expected, result, 1e-6);
            Assert.IsTrue(equal, "The multiplication result with grid size 2 did not match the expected value.");
        }

        [TestMethod]
        public void Test_LargeMatrixMultiplication()
        {
            int size = 8;
            int gridSize = 4;
            Matrix a = new Matrix(size, size);
            Matrix b = new Matrix(size, size);
            // Fill matrices with synthetic data.
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    a.Data[i, j] = (double)(i * size + j + 1);
                    b.Data[i, j] = (double)(i * size + j + 1);
                }
            }

            Matrix expected = NaiveMultiply(a, b);
            Matrix result = CannonAlgorithm.Multiply(a, b, gridSize);
            bool equal = AreMatricesEqual(expected, result, 1e-6);
            Assert.IsTrue(equal, "The large matrix multiplication result did not match the expected value.");
        }

        private static System.Random randomGenerator = new System.Random(42);
        private static Matrix GenerateRandomMatrix(int rows, int columns)
        {
            Matrix m = new Matrix(rows, columns);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    m.Data[i, j] = randomGenerator.NextDouble() * 10.0;
                }
            }

            return m;
        }

        private static Matrix CreateIdentityMatrix(int size)
        {
            Matrix m = new Matrix(size, size);
            for (int i = 0; i < size; i++)
            {
                m.Data[i, i] = 1.0;
            }

            return m;
        }

        [TestMethod]
        public void Test_RandomMatrixMultiplication()
        {
            int size = 16;
            int gridSize = 4;
            Matrix a = GenerateRandomMatrix(size, size);
            Matrix b = GenerateRandomMatrix(size, size);
            Matrix expected = NaiveMultiply(a, b);
            Matrix result = CannonAlgorithm.Multiply(a, b, gridSize);
            bool equal = AreMatricesEqual(expected, result, 1e-6);
            Assert.IsTrue(equal, "Random matrix multiplication did not produce expected result.");
        }

        [TestMethod]
        public void Test_IdentityMultiplication()
        {
            int size = 8;
            int gridSize = 2;
            Matrix identity = CreateIdentityMatrix(size);
            Matrix randomMatrix = GenerateRandomMatrix(size, size);
            Matrix expected = NaiveMultiply(identity, randomMatrix);
            Matrix result = CannonAlgorithm.Multiply(identity, randomMatrix, gridSize);
            bool equal = AreMatricesEqual(expected, result, 1e-6);
            Assert.IsTrue(equal, "Multiplication with identity matrix did not produce expected result.");
        }
    }
}