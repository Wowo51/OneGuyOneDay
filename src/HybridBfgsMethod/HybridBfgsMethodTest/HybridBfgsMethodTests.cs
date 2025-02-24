using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HybridBfgsMethod;

namespace HybridBfgsMethodTest
{
    [TestClass]
    public class HybridBfgsOptimizerTests
    {
        [TestMethod]
        public void Optimize_NullInputs_ReturnsEmptyArray()
        {
            HybridBfgsOptimizer optimizer = new HybridBfgsOptimizer();
            double[] initial = new double[]
            {
                1.0,
                1.0
            };
            // Passing null as objective should return an empty array.
            double[] result = optimizer.Optimize(null, (double[] x) => new double[x.Length], initial, 1e-6, 10);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Optimize_QuadraticFunction_ConvergesToMinimum()
        {
            HybridBfgsOptimizer optimizer = new HybridBfgsOptimizer();
            // Define a simple quadratic function: f(x) = (x0 - 3)^2 + (x1 + 2)^2,
            // whose minimum is at (3, -2)
            Func<double[], double> objective = delegate (double[] x)
            {
                return (x[0] - 3.0) * (x[0] - 3.0) + (x[1] + 2.0) * (x[1] + 2.0);
            };
            Func<double[], double[]> gradient = delegate (double[] x)
            {
                double[] grad = new double[2];
                grad[0] = 2.0 * (x[0] - 3.0);
                grad[1] = 2.0 * (x[1] + 2.0);
                return grad;
            };
            double[] initial = new double[]
            {
                0.0,
                0.0
            };
            double tolerance = 1e-6;
            int maxIterations = 100;
            double[] optimum = optimizer.Optimize(objective, gradient, initial, tolerance, maxIterations);
            Assert.IsTrue(Math.Abs(optimum[0] - 3.0) < 1e-4, "x0 did not converge to 3.0");
            Assert.IsTrue(Math.Abs(optimum[1] + 2.0) < 1e-4, "x1 did not converge to -2.0");
        }

        // New test: Passing null for gradient should return an empty array.
        [TestMethod]
        public void Optimize_NullGradient_ReturnsEmptyArray()
        {
            HybridBfgsOptimizer optimizer = new HybridBfgsOptimizer();
            double[] initial = new double[]
            {
                1.0,
                1.0
            };
            Func<double[], double> objective = (double[] x) => x[0] * x[0] + x[1] * x[1];
            double[] result = optimizer.Optimize(objective, null, initial, 1e-6, 10);
            Assert.AreEqual(0, result.Length);
        }

        // New test: Passing null for the initial vector should return an empty array.
        [TestMethod]
        public void Optimize_NullInitial_ReturnsEmptyArray()
        {
            HybridBfgsOptimizer optimizer = new HybridBfgsOptimizer();
            Func<double[], double> objective = (double[] x) => x[0] * x[0] + x[1] * x[1];
            Func<double[], double[]> gradient = (double[] x) => new double[]
            {
                2 * x[0],
                2 * x[1]
            };
            double[] result = optimizer.Optimize(objective, gradient, null, 1e-6, 10);
            Assert.AreEqual(0, result.Length);
        }
    }

    [TestClass]
    public class VectorMathTests
    {
        [TestMethod]
        public void Dot_Product_ReturnsCorrectResult()
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
            double dot = VectorMath.Dot(a, b);
            // 1*4 + 2*5 + 3*6 = 32
            Assert.AreEqual(32.0, dot, 1e-6);
        }

        [TestMethod]
        public void Norm_ReturnsCorrectResult()
        {
            double[] a = new double[]
            {
                3.0,
                4.0
            };
            double norm = VectorMath.Norm(a);
            Assert.AreEqual(5.0, norm, 1e-6);
        }

        [TestMethod]
        public void Add_Subtract_Scale_WorksCorrectly()
        {
            double[] a = new double[]
            {
                1.0,
                2.0
            };
            double[] b = new double[]
            {
                3.0,
                4.0
            };
            double[] added = VectorMath.Add(a, b);
            Assert.AreEqual(4.0, added[0], 1e-6);
            Assert.AreEqual(6.0, added[1], 1e-6);
            double[] subtracted = VectorMath.Subtract(b, a);
            Assert.AreEqual(2.0, subtracted[0], 1e-6);
            Assert.AreEqual(2.0, subtracted[1], 1e-6);
            double[] scaled = VectorMath.Scale(a, 2.0);
            Assert.AreEqual(2.0, scaled[0], 1e-6);
            Assert.AreEqual(4.0, scaled[1], 1e-6);
        }

        [TestMethod]
        public void LargeSyntheticVectorNormTest()
        {
            int size = 1000;
            double[] vector = new double[size];
            double expected = 0.0;
            System.Random random = new System.Random(12345);
            for (int i = 0; i < size; i++)
            {
                vector[i] = random.NextDouble();
                expected += vector[i] * vector[i];
            }

            expected = Math.Sqrt(expected);
            double actual = VectorMath.Norm(vector);
            Assert.AreEqual(expected, actual, 1e-6);
        }
    }

    [TestClass]
    public class MatrixMathTests
    {
        [TestMethod]
        public void IdentityMatrix_ReturnsCorrectMatrix()
        {
            int n = 3;
            double[, ] identity = MatrixMath.IdentityMatrix(n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        Assert.AreEqual(1.0, identity[i, j], 1e-6);
                    }
                    else
                    {
                        Assert.AreEqual(0.0, identity[i, j], 1e-6);
                    }
                }
            }
        }

        [TestMethod]
        public void MatrixOperations_WorkCorrectly()
        {
            double[, ] A = new double[, ]
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
            double[, ] B = new double[, ]
            {
                {
                    4.0,
                    3.0
                },
                {
                    2.0,
                    1.0
                }
            };
            double[, ] sum = MatrixMath.Add(A, B);
            Assert.AreEqual(5.0, sum[0, 0], 1e-6);
            Assert.AreEqual(5.0, sum[0, 1], 1e-6);
            Assert.AreEqual(5.0, sum[1, 0], 1e-6);
            Assert.AreEqual(5.0, sum[1, 1], 1e-6);
            double[, ] diff = MatrixMath.Subtract(A, B);
            Assert.AreEqual(-3.0, diff[0, 0], 1e-6);
            Assert.AreEqual(-1.0, diff[0, 1], 1e-6);
            Assert.AreEqual(1.0, diff[1, 0], 1e-6);
            Assert.AreEqual(3.0, diff[1, 1], 1e-6);
            double[, ] scaled = MatrixMath.Scale(A, 2.0);
            Assert.AreEqual(2.0, scaled[0, 0], 1e-6);
            Assert.AreEqual(4.0, scaled[0, 1], 1e-6);
            Assert.AreEqual(6.0, scaled[1, 0], 1e-6);
            Assert.AreEqual(8.0, scaled[1, 1], 1e-6);
            double[, ] mult = MatrixMath.Multiply(A, B);
            // Calculation: [ [1*4+2*2, 1*3+2*1], [3*4+4*2, 3*3+4*1] ] = [ [8,5], [20,13] ]
            Assert.AreEqual(8.0, mult[0, 0], 1e-6);
            Assert.AreEqual(5.0, mult[0, 1], 1e-6);
            Assert.AreEqual(20.0, mult[1, 0], 1e-6);
            Assert.AreEqual(13.0, mult[1, 1], 1e-6);
            double[, ] outer = MatrixMath.OuterProduct(new double[] { 1.0, 2.0 }, new double[] { 3.0, 4.0 });
            Assert.AreEqual(3.0, outer[0, 0], 1e-6);
            Assert.AreEqual(4.0, outer[0, 1], 1e-6);
            Assert.AreEqual(6.0, outer[1, 0], 1e-6);
            Assert.AreEqual(8.0, outer[1, 1], 1e-6);
        }

        [TestMethod]
        public void LargeSyntheticMatrixMultiplyTest()
        {
            int rows = 10;
            int common = 10;
            int cols = 10;
            double[, ] A = new double[rows, common];
            double[, ] B = new double[common, cols];
            System.Random random = new System.Random(54321);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < common; j++)
                {
                    A[i, j] = random.NextDouble();
                }
            }

            for (int i = 0; i < common; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    B[i, j] = random.NextDouble();
                }
            }

            double[, ] C = MatrixMath.Multiply(A, B);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    double expected = 0.0;
                    for (int k = 0; k < common; k++)
                    {
                        expected += A[i, k] * B[k, j];
                    }

                    Assert.AreEqual(expected, C[i, j], 1e-6);
                }
            }
        }
    }
}