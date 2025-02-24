using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PartialLeastSquares;

namespace PartialLeastSquaresTest
{
    [TestClass]
    public class MatrixHelperTests
    {
        private void AssertDoubleArraysAreEqual(double[] expected, double[] actual, double tolerance)
        {
            Assert.AreEqual(expected.Length, actual.Length, "Arrays length differ.");
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(Math.Abs(expected[i] - actual[i]) <= tolerance, $"Arrays differ at index {i}: expected {expected[i]}, got {actual[i]}.");
            }
        }

        private void AssertMatrixAreEqual(double[][] expected, double[][] actual, double tolerance)
        {
            Assert.AreEqual(expected.Length, actual.Length, "Matrix row counts differ.");
            for (int i = 0; i < expected.Length; i++)
            {
                AssertDoubleArraysAreEqual(expected[i], actual[i], tolerance);
            }
        }

        [TestMethod]
        public void TestClone()
        {
            double[][] original = new double[][]
            {
                new double[]
                {
                    1.0,
                    2.0
                },
                new double[]
                {
                    3.0,
                    4.0
                }
            };
            double[][] clone = MatrixHelper.Clone(original);
            AssertMatrixAreEqual(original, clone, 1e-9);
            clone[0][0] = 10.0;
            Assert.AreEqual(1.0, original[0][0], 1e-9, "Original matrix should not change after clone modification.");
        }

        [TestMethod]
        public void TestTranspose()
        {
            double[][] matrix = new double[][]
            {
                new double[]
                {
                    1.0,
                    2.0,
                    3.0
                },
                new double[]
                {
                    4.0,
                    5.0,
                    6.0
                }
            };
            double[][] expected = new double[][]
            {
                new double[]
                {
                    1.0,
                    4.0
                },
                new double[]
                {
                    2.0,
                    5.0
                },
                new double[]
                {
                    3.0,
                    6.0
                }
            };
            double[][] result = MatrixHelper.Transpose(matrix);
            AssertMatrixAreEqual(expected, result, 1e-9);
        }

        [TestMethod]
        public void TestMultiply()
        {
            double[][] A = new double[][]
            {
                new double[]
                {
                    1,
                    2
                },
                new double[]
                {
                    3,
                    4
                }
            };
            double[][] B = new double[][]
            {
                new double[]
                {
                    5,
                    6
                },
                new double[]
                {
                    7,
                    8
                }
            };
            double[][] expected = new double[][]
            {
                new double[]
                {
                    19,
                    22
                },
                new double[]
                {
                    43,
                    50
                }
            };
            double[][] result = MatrixHelper.Multiply(A, B);
            AssertMatrixAreEqual(expected, result, 1e-9);
        }

        [TestMethod]
        public void TestMultiplyVector()
        {
            double[][] A = new double[][]
            {
                new double[]
                {
                    1,
                    2
                },
                new double[]
                {
                    3,
                    4
                }
            };
            double[] vector = new double[]
            {
                5,
                6
            };
            double[] expected = new double[]
            {
                17,
                39
            };
            double[] result = MatrixHelper.MultiplyVector(A, vector);
            AssertDoubleArraysAreEqual(expected, result, 1e-9);
        }

        [TestMethod]
        public void TestDot()
        {
            double[] a = new double[]
            {
                1.0,
                2.0,
                3.0
            };
            double[] b = new double[]
            {
                4.0,
                5.0,
                6.0
            };
            double expected = 32.0;
            double result = MatrixHelper.Dot(a, b);
            Assert.AreEqual(expected, result, 1e-9);
        }

        [TestMethod]
        public void TestNorm()
        {
            double[] vector = new double[]
            {
                3.0,
                4.0
            };
            double expected = 5.0;
            double result = MatrixHelper.Norm(vector);
            Assert.AreEqual(expected, result, 1e-9);
        }

        [TestMethod]
        public void TestOuterProduct()
        {
            double[] a = new double[]
            {
                1.0,
                2.0
            };
            double[] b = new double[]
            {
                3.0,
                4.0,
                5.0
            };
            double[][] expected = new double[][]
            {
                new double[]
                {
                    3,
                    4,
                    5
                },
                new double[]
                {
                    6,
                    8,
                    10
                }
            };
            double[][] result = MatrixHelper.OuterProduct(a, b);
            AssertMatrixAreEqual(expected, result, 1e-9);
        }

        [TestMethod]
        public void TestSubtract()
        {
            double[][] A = new double[][]
            {
                new double[]
                {
                    5.0,
                    7.0
                },
                new double[]
                {
                    9.0,
                    11.0
                }
            };
            double[][] B = new double[][]
            {
                new double[]
                {
                    1.0,
                    2.0
                },
                new double[]
                {
                    3.0,
                    4.0
                }
            };
            double[][] expected = new double[][]
            {
                new double[]
                {
                    4.0,
                    5.0
                },
                new double[]
                {
                    6.0,
                    7.0
                }
            };
            double[][] result = MatrixHelper.Subtract(A, B);
            AssertMatrixAreEqual(expected, result, 1e-9);
        }

        [TestMethod]
        public void TestInverse()
        {
            double[][] matrix = new double[][]
            {
                new double[]
                {
                    4,
                    7
                },
                new double[]
                {
                    2,
                    6
                }
            };
            double[][] expected = new double[][]
            {
                new double[]
                {
                    0.6,
                    -0.7
                },
                new double[]
                {
                    -0.2,
                    0.4
                }
            };
            double[][] result = MatrixHelper.Inverse(matrix);
            AssertMatrixAreEqual(expected, result, 1e-6);
        }
    }
}