using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using NewtonMethod;

namespace NewtonMethodTest
{
    [TestClass]
    public class NewtonMethodTests
    {
        private const double Tolerance = 1e-6;
        [TestMethod]
        public void TestMatrixUtil_SolveValid()
        {
            double[, ] A = new double[2, 2]
            {
                {
                    2,
                    1
                },
                {
                    1,
                    -1
                }
            };
            double[] b = new double[2]
            {
                5,
                1
            };
            double[]? solution = MatrixUtil.Solve(A, b);
            Assert.IsNotNull(solution, "Solution should not be null.");
            double[] expected = new double[2]
            {
                2,
                1
            };
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(Math.Abs(solution[i] - expected[i]) < Tolerance, "Element " + i + " differs.");
            }
        }

        [TestMethod]
        public void TestMatrixUtil_SolveInvalidDimensions()
        {
            double[, ] A = new double[2, 3];
            double[] b = new double[2];
            double[]? solution = MatrixUtil.Solve(A, b);
            Assert.IsNull(solution, "Solution should be null for invalid dimensions.");
        }

        [TestMethod]
        public void TestMatrixUtil_SolveSingular()
        {
            double[, ] A = new double[2, 2]
            {
                {
                    1,
                    2
                },
                {
                    2,
                    4
                }
            };
            double[] b = new double[2]
            {
                3,
                6
            };
            double[]? solution = MatrixUtil.Solve(A, b);
            Assert.IsNull(solution, "Solution should be null for a singular matrix.");
        }

        [TestMethod]
        public void TestVectorUtil_Subtract_Valid()
        {
            double[] a = new double[3]
            {
                5,
                7,
                9
            };
            double[] b = new double[3]
            {
                2,
                3,
                4
            };
            double[]? result = VectorUtil.Subtract(a, b);
            Assert.IsNotNull(result, "Result should not be null.");
            double[] expected = new double[3]
            {
                3,
                4,
                5
            };
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(Math.Abs(result[i] - expected[i]) < Tolerance, "Element " + i + " differs.");
            }
        }

        [TestMethod]
        public void TestVectorUtil_Subtract_Invalid()
        {
            double[] a = new double[2]
            {
                1,
                2
            };
            double[] b = new double[3]
            {
                1,
                2,
                3
            };
            double[]? result = VectorUtil.Subtract(a, b);
            Assert.IsNull(result, "Result should be null for mismatched lengths.");
        }

        [TestMethod]
        public void TestVectorUtil_Norm()
        {
            double[] a = new double[2]
            {
                3,
                4
            };
            double norm = VectorUtil.Norm(a);
            Assert.IsTrue(Math.Abs(norm - 5) < Tolerance, "Norm should be 5.");
        }

        [TestMethod]
        public void TestNewtonOptimizer_Quadratic()
        {
            NewtonOptimizer optimizer = new NewtonOptimizer();
            Func<double[], double> f = delegate (double[] x)
            {
                return (x[0] - 3) * (x[0] - 3);
            };
            Func<double[], double[]> grad = delegate (double[] x)
            {
                return new double[1]
                {
                    2 * (x[0] - 3)
                };
            };
            Func<double[], double[, ]> hess = delegate (double[] x)
            {
                double[, ] result = new double[1, 1];
                result[0, 0] = 2;
                return result;
            };
            double[] initial = new double[1]
            {
                0
            };
            double[] optimum = optimizer.Optimize(f, grad, hess, initial, Tolerance, 100);
            Assert.IsTrue(Math.Abs(optimum[0] - 3) < Tolerance, "Optimum should be close to 3.");
        }

        [TestMethod]
        public void TestNewtonOptimizer_Multidimensional()
        {
            NewtonOptimizer optimizer = new NewtonOptimizer();
            Func<double[], double> f = delegate (double[] x)
            {
                return (x[0] - 1) * (x[0] - 1) + (x[1] + 2) * (x[1] + 2);
            };
            Func<double[], double[]> grad = delegate (double[] x)
            {
                return new double[2]
                {
                    2 * (x[0] - 1),
                    2 * (x[1] + 2)
                };
            };
            Func<double[], double[, ]> hess = delegate (double[] x)
            {
                double[, ] result = new double[2, 2];
                result[0, 0] = 2;
                result[0, 1] = 0;
                result[1, 0] = 0;
                result[1, 1] = 2;
                return result;
            };
            double[] initial = new double[2]
            {
                10,
                -10
            };
            double[] optimum = optimizer.Optimize(f, grad, hess, initial, Tolerance, 10);
            Assert.IsTrue(Math.Abs(optimum[0] - 1) < Tolerance, "x-coordinate should be close to 1.");
            Assert.IsTrue(Math.Abs(optimum[1] + 2) < Tolerance, "y-coordinate should be close to -2.");
        }

        [TestMethod]
        public void TestMatrixUtil_Solve_Random()
        {
            int dimension = 5;
            double[, ] A = new double[dimension, dimension];
            double[] xKnown = new double[dimension];
            double[] b = new double[dimension];
            Random random = new Random(42);
            for (int i = 0; i < dimension; i++)
            {
                double sumOff = 0;
                for (int j = 0; j < dimension; j++)
                {
                    if (i != j)
                    {
                        A[i, j] = random.NextDouble();
                        sumOff += Math.Abs(A[i, j]);
                    }
                }

                A[i, i] = sumOff + random.NextDouble() + 1;
            }

            for (int i = 0; i < dimension; i++)
            {
                xKnown[i] = random.NextDouble() * 10;
            }

            for (int i = 0; i < dimension; i++)
            {
                double sum = 0;
                for (int j = 0; j < dimension; j++)
                {
                    sum += A[i, j] * xKnown[j];
                }

                b[i] = sum;
            }

            double[]? xComputed = MatrixUtil.Solve(A, b);
            Assert.IsNotNull(xComputed, "Solution should not be null for a well-conditioned matrix.");
            for (int i = 0; i < dimension; i++)
            {
                Assert.IsTrue(Math.Abs(xComputed[i] - xKnown[i]) < Tolerance, "Element " + i + " differs.");
            }
        }
    }
}