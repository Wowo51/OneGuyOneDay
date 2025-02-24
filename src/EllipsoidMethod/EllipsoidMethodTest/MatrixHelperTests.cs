using Microsoft.VisualStudio.TestTools.UnitTesting;
using EllipsoidMethod;
using System;

namespace EllipsoidMethodTest
{
    [TestClass]
    public class MatrixHelperTests
    {
        private static bool AreArraysEqual(double[] expected, double[] actual, double tolerance)
        {
            if (expected.Length != actual.Length)
            {
                return false;
            }

            for (int i = 0; i < expected.Length; i++)
            {
                if (System.Math.Abs(expected[i] - actual[i]) > tolerance)
                {
                    return false;
                }
            }

            return true;
        }

        [TestMethod]
        public void TestMultiplyMatrixVector()
        {
            double[, ] matrix = new double[, ]
            {
                {
                    1.0,
                    2.0
                },
                {
                    3.0,
                    4.0
                }
            };
            double[] vector = new double[2]
            {
                5.0,
                6.0
            };
            double[] expected = new double[2]
            {
                17.0,
                39.0
            };
            double[] result = MatrixHelper.MultiplyMatrixVector(matrix, vector);
            Assert.IsTrue(AreArraysEqual(expected, result, 1e-10));
        }

        [TestMethod]
        public void TestInnerProduct()
        {
            double[] vector1 = new double[3]
            {
                1.0,
                2.0,
                3.0
            };
            double[] vector2 = new double[3]
            {
                4.0,
                5.0,
                6.0
            };
            double expected = 32.0;
            double result = MatrixHelper.InnerProduct(vector1, vector2);
            Assert.AreEqual(expected, result, 1e-10);
        }

        [TestMethod]
        public void TestSubtractMatrices()
        {
            double[, ] a = new double[, ]
            {
                {
                    5.0,
                    7.0
                },
                {
                    9.0,
                    11.0
                }
            };
            double[, ] b = new double[, ]
            {
                {
                    1.0,
                    2.0
                },
                {
                    3.0,
                    4.0
                }
            };
            double[, ] expected = new double[, ]
            {
                {
                    4.0,
                    5.0
                },
                {
                    6.0,
                    7.0
                }
            };
            double[, ] result = MatrixHelper.SubtractMatrices(a, b);
            int rows = result.GetLength(0);
            int cols = result.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Assert.AreEqual(expected[i, j], result[i, j], 1e-10);
                }
            }
        }

        [TestMethod]
        public void TestScaleMatrix()
        {
            double[, ] matrix = new double[, ]
            {
                {
                    2.0,
                    4.0
                },
                {
                    6.0,
                    8.0
                }
            };
            double factor = 0.5;
            double[, ] expected = new double[, ]
            {
                {
                    1.0,
                    2.0
                },
                {
                    3.0,
                    4.0
                }
            };
            double[, ] result = MatrixHelper.ScaleMatrix(matrix, factor);
            int rows = result.GetLength(0);
            int cols = result.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Assert.AreEqual(expected[i, j], result[i, j], 1e-10);
                }
            }
        }

        [TestMethod]
        public void TestOuterProduct()
        {
            double[] vector1 = new double[2]
            {
                1.0,
                2.0
            };
            double[] vector2 = new double[2]
            {
                3.0,
                4.0
            };
            double[, ] expected = new double[, ]
            {
                {
                    3.0,
                    4.0
                },
                {
                    6.0,
                    8.0
                }
            };
            double[, ] result = MatrixHelper.OuterProduct(vector1, vector2);
            int rows = result.GetLength(0);
            int cols = result.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Assert.AreEqual(expected[i, j], result[i, j], 1e-10);
                }
            }
        }

        [TestMethod]
        public void TestLargeMatrixVectorMultiplication()
        {
            System.Random random = new System.Random(42);
            int size = 100;
            double[, ] matrix = new double[size, size];
            double[] vector = new double[size];
            for (int i = 0; i < size; i++)
            {
                vector[i] = random.NextDouble();
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = random.NextDouble();
                }
            }

            double[] result = MatrixHelper.MultiplyMatrixVector(matrix, vector);
            double[] expected = new double[size];
            for (int i = 0; i < size; i++)
            {
                double sum = 0.0;
                for (int j = 0; j < size; j++)
                {
                    sum += matrix[i, j] * vector[j];
                }

                expected[i] = sum;
            }

            Assert.IsTrue(AreArraysEqual(expected, result, 1e-8));
        }
    }
}