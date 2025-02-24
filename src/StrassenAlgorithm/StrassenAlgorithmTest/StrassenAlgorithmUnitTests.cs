using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StrassenAlgorithm;

namespace StrassenAlgorithmTest
{
    [TestClass]
    public class StrassenAlgorithmTests
    {
        private const double Tolerance = 1e-6;
        private static Matrix StandardMultiply(Matrix a, Matrix b)
        {
            int size = a.Size;
            Matrix result = new Matrix(size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < size; k++)
                    {
                        sum += a.Data[i, k] * b.Data[k, j];
                    }

                    result.Data[i, j] = sum;
                }
            }

            return result;
        }

        private static bool MatricesAreEqual(Matrix expected, Matrix actual)
        {
            if (expected.Size != actual.Size)
            {
                return false;
            }

            int size = expected.Size;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (Math.Abs(expected.Data[i, j] - actual.Data[i, j]) > Tolerance)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static Matrix CreateMatrixFrom2DArray(double[, ] data)
        {
            return new Matrix(data);
        }

        [TestMethod]
        public void TestMatrixAddition()
        {
            double[, ] dataA = new double[, ]
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
            double[, ] dataB = new double[, ]
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
            Matrix a = CreateMatrixFrom2DArray(dataA);
            Matrix b = CreateMatrixFrom2DArray(dataB);
            Matrix? sum = Matrix.Add(a, b);
            Assert.IsNotNull(sum, "Matrix.Add returned null");
            double[, ] expectedData = new double[, ]
            {
                {
                    6,
                    8
                },
                {
                    10,
                    12
                }
            };
            Matrix expected = CreateMatrixFrom2DArray(expectedData);
            Assert.IsTrue(MatricesAreEqual(expected, sum), "Matrix addition incorrect");
        }

        [TestMethod]
        public void TestMatrixSubtraction()
        {
            double[, ] dataA = new double[, ]
            {
                {
                    5,
                    7
                },
                {
                    9,
                    11
                }
            };
            double[, ] dataB = new double[, ]
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
            Matrix a = CreateMatrixFrom2DArray(dataA);
            Matrix b = CreateMatrixFrom2DArray(dataB);
            Matrix? diff = Matrix.Subtract(a, b);
            Assert.IsNotNull(diff, "Matrix.Subtract returned null");
            double[, ] expectedData = new double[, ]
            {
                {
                    4,
                    5
                },
                {
                    6,
                    7
                }
            };
            Matrix expected = CreateMatrixFrom2DArray(expectedData);
            Assert.IsTrue(MatricesAreEqual(expected, diff), "Matrix subtraction incorrect");
        }

        [TestMethod]
        public void TestStrassenMultiplierBaseCase()
        {
            double[, ] dataA = new double[, ]
            {
                {
                    3
                }
            };
            double[, ] dataB = new double[, ]
            {
                {
                    4
                }
            };
            Matrix a = CreateMatrixFrom2DArray(dataA);
            Matrix b = CreateMatrixFrom2DArray(dataB);
            Matrix? product = StrassenMultiplier.Multiply(a, b);
            Assert.IsNotNull(product, "StrassenMultiplier.Multiply returned null for base case");
            double[, ] expectedData = new double[, ]
            {
                {
                    12
                }
            };
            Matrix expected = CreateMatrixFrom2DArray(expectedData);
            Assert.IsTrue(MatricesAreEqual(expected, product), "Strassen base multiplication incorrect");
        }

        [TestMethod]
        public void TestStrassenMultiplier2x2()
        {
            double[, ] dataA = new double[, ]
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
            double[, ] dataB = new double[, ]
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
            Matrix a = CreateMatrixFrom2DArray(dataA);
            Matrix b = CreateMatrixFrom2DArray(dataB);
            Matrix? product = StrassenMultiplier.Multiply(a, b);
            Assert.IsNotNull(product, "StrassenMultiplier.Multiply returned null for 2x2 multiplication");
            Matrix expected = StandardMultiply(a, b);
            Assert.IsTrue(MatricesAreEqual(expected, product), "Strassen multiplication for 2x2 matrix incorrect");
        }

        [TestMethod]
        public void TestStrassenMultiplierRandom4x4()
        {
            int size = 4;
            Matrix a = GenerateRandomMatrix(size, 42);
            Matrix b = GenerateRandomMatrix(size, 84);
            Matrix? product = StrassenMultiplier.Multiply(a, b);
            Assert.IsNotNull(product, "StrassenMultiplier.Multiply returned null for random 4x4 multiplication");
            Matrix expected = StandardMultiply(a, b);
            Assert.IsTrue(MatricesAreEqual(expected, product), "Strassen multiplication for random 4x4 matrix incorrect");
        }

        private static Matrix GenerateRandomMatrix(int size, int seed)
        {
            System.Random random = new System.Random(seed);
            Matrix matrix = new Matrix(size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix.Data[i, j] = random.NextDouble() * 100;
                }
            }

            return matrix;
        }
    }
}