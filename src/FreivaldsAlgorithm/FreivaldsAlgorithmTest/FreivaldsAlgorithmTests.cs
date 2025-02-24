using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FreivaldsAlgorithm;

namespace FreivaldsAlgorithmTest
{
    [TestClass]
    public class FreivaldsAlgorithmTests
    {
        private static int[, ] MultiplyMatrix(int[, ] A, int[, ] B)
        {
            int mA = A.GetLength(0);
            int nA = A.GetLength(1);
            int mB = B.GetLength(0);
            int nB = B.GetLength(1);
            int[, ] product = new int[mA, nB];
            for (int i = 0; i < mA; i++)
            {
                for (int j = 0; j < nB; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < nA; k++)
                    {
                        sum += A[i, k] * B[k, j];
                    }

                    product[i, j] = sum;
                }
            }

            return product;
        }

        private static int[, ] GenerateRandomMatrix(int rows, int cols, int minValue, int maxValue, Random rnd)
        {
            int[, ] matrix = new int[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = rnd.Next(minValue, maxValue);
                }
            }

            return matrix;
        }

        [TestMethod]
        public void Test_CorrectMultiplication()
        {
            int[, ] A = new int[, ]
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
            int[, ] B = new int[, ]
            {
                {
                    5,
                    6
                },
                {
                    7,
                    8
                }
            };
            int[, ] C = MultiplyMatrix(A, B);
            bool result = Freivalds.VerifyMultiplication(A, B, C, 20);
            Assert.IsTrue(result, "Verification should succeed for correct multiplication.");
        }

        [TestMethod]
        public void Test_IncorrectMultiplication()
        {
            int[, ] A = new int[, ]
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
            int[, ] B = new int[, ]
            {
                {
                    5,
                    6
                },
                {
                    7,
                    8
                }
            };
            int[, ] C = MultiplyMatrix(A, B);
            C[0, 0] = C[0, 0] + 1;
            bool result = Freivalds.VerifyMultiplication(A, B, C, 20);
            Assert.IsFalse(result, "Verification should fail for incorrect multiplication.");
        }

        [TestMethod]
        public void Test_InvalidDimensions()
        {
            int[, ] A = new int[, ]
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
            int[, ] B = new int[, ]
            {
                {
                    5,
                    6
                },
                {
                    7,
                    8
                }
            };
            int[, ] C = new int[1, 2];
            bool result = Freivalds.VerifyMultiplication(A, B, C, 10);
            Assert.IsFalse(result, "Verification should fail for mismatched dimensions.");
        }

        [TestMethod]
        public void Test_RandomLargeMatrix()
        {
            Random rnd = new Random();
            int rows = 10;
            int common = 10;
            int cols = 10;
            int[, ] A = GenerateRandomMatrix(rows, common, -10, 11, rnd);
            int[, ] B = GenerateRandomMatrix(common, cols, -10, 11, rnd);
            int[, ] C = MultiplyMatrix(A, B);
            bool validResult = Freivalds.VerifyMultiplication(A, B, C, 30);
            Assert.IsTrue(validResult, "Verification should succeed for correct random multiplication.");
            int originalValue = C[rnd.Next(0, rows), rnd.Next(0, cols)];
            C[0, 0] = originalValue + 1;
            bool invalidResult = Freivalds.VerifyMultiplication(A, B, C, 30);
            Assert.IsFalse(invalidResult, "Verification should fail for incorrect random multiplication.");
        }
    }
}