using Microsoft.VisualStudio.TestTools.UnitTesting;
using BiconjugateGradientMethod;
using System;

namespace BiconjugateGradientMethodTest
{
    [TestClass]
    public class BiconjugateGradientMethodTests
    {
        private const double Tolerance = 1e-6;
        private static void AssertArraysAreEqual(double[] expected, double[] actual, double tol)
        {
            if (expected.Length != actual.Length)
            {
                Assert.Fail("Array lengths differ.");
            }

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(Math.Abs(expected[i] - actual[i]) < tol, $"Difference at index {i} exceeds tolerance.");
            }
        }

        [TestMethod]
        public void TestIdentityMatrix()
        {
            double[, ] A = new double[2, 2]
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
            double[] b = new double[2]
            {
                3,
                5
            };
            double[] result = BiconjugateGradientSolver.Solve(A, b, null, 1e-10, 1000);
            AssertArraysAreEqual(b, result, 1e-8);
        }

        [TestMethod]
        public void TestSimple2x2System()
        {
            double[, ] A = new double[2, 2]
            {
                {
                    4,
                    1
                },
                {
                    1,
                    3
                }
            };
            double[] b = new double[2]
            {
                1,
                2
            };
            double[] expected = new double[2]
            {
                1.0 / 11.0,
                7.0 / 11.0
            };
            double[] result = BiconjugateGradientSolver.Solve(A, b, null, 1e-10, 1000);
            AssertArraysAreEqual(expected, result, 1e-6);
        }

        [TestMethod]
        public void TestWithInitialGuess()
        {
            double[, ] A = new double[3, 3]
            {
                {
                    1,
                    0,
                    0
                },
                {
                    0,
                    1,
                    0
                },
                {
                    0,
                    0,
                    1
                }
            };
            double[] b = new double[3]
            {
                7,
                11,
                13
            };
            double[] initialGuess = new double[3]
            {
                1,
                1,
                1
            };
            double[] result = BiconjugateGradientSolver.Solve(A, b, initialGuess, 1e-10, 1000);
            AssertArraysAreEqual(b, result, 1e-8);
        }

        [TestMethod]
        public void TestZeroRHS()
        {
            double[, ] A = new double[2, 2]
            {
                {
                    2,
                    0
                },
                {
                    0,
                    2
                }
            };
            double[] b = new double[2]
            {
                0,
                0
            };
            double[] result = BiconjugateGradientSolver.Solve(A, b, null, 1e-10, 1000);
            double[] expected = new double[2]
            {
                0,
                0
            };
            AssertArraysAreEqual(expected, result, 1e-8);
        }

        [TestMethod]
        public void TestLargeSystem()
        {
            int size = 20;
            double[, ] A = new double[size, size];
            double[] xExpected = new double[size];
            double[] b = new double[size];
            Random random = new Random(42);
            double[, ] M = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    M[i, j] = random.NextDouble();
                }
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < size; k++)
                    {
                        sum += M[k, i] * M[k, j];
                    }

                    A[i, j] = sum;
                }
            }

            for (int i = 0; i < size; i++)
            {
                xExpected[i] = 1.0;
            }

            for (int i = 0; i < size; i++)
            {
                double sum = 0;
                for (int j = 0; j < size; j++)
                {
                    sum += A[i, j] * xExpected[j];
                }

                b[i] = sum;
            }

            double[] result = BiconjugateGradientSolver.Solve(A, b, null, 1e-6, 10000);
            double error = 0;
            for (int i = 0; i < size; i++)
            {
                error += Math.Abs(result[i] - xExpected[i]);
            }

            Assert.IsTrue(error < 1e-4, "The computed solution is not sufficiently accurate.");
        }
    }
}