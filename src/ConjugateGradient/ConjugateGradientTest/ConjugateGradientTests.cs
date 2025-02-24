using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConjugateGradient;

namespace ConjugateGradientTest
{
    [TestClass]
    public class ConjugateGradientTests
    {
        [TestMethod]
        public void Test_NullMatrixMultiply_ReturnsEmptyArray()
        {
            double[] b = new double[]
            {
                1.0,
                2.0,
                3.0
            };
            double tolerance = 1e-6;
            int maxIterations = 100;
            double[] result = ConjugateGradientSolver.Solve(null, b, tolerance, maxIterations);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Test_NullB_ReturnsEmptyArray()
        {
            double tolerance = 1e-6;
            int maxIterations = 100;
            Func<double[], double[]> identityMultiply = delegate (double[] vector)
            {
                if (vector == null)
                {
                    return new double[0];
                }

                int length = vector.Length;
                double[] result = new double[length];
                for (int i = 0; i < length; i++)
                {
                    result[i] = vector[i];
                }

                return result;
            };
            double[] result = ConjugateGradientSolver.Solve(identityMultiply, null, tolerance, maxIterations);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Test_IdentityMatrix()
        {
            double[] b = new double[]
            {
                4.0,
                5.0,
                6.0
            };
            double tolerance = 1e-6;
            int maxIterations = 100;
            Func<double[], double[]> identityMultiply = delegate (double[] vector)
            {
                if (vector == null)
                {
                    return new double[0];
                }

                int length = vector.Length;
                double[] result = new double[length];
                for (int i = 0; i < length; i++)
                {
                    result[i] = vector[i];
                }

                return result;
            };
            double[] result = ConjugateGradientSolver.Solve(identityMultiply, b, tolerance, maxIterations);
            Assert.AreEqual(b.Length, result.Length);
            for (int i = 0; i < b.Length; i++)
            {
                double diff = Math.Abs(result[i] - b[i]);
                Assert.IsTrue(diff < 1e-9, "Element " + i + " differs: expected " + b[i] + ", got " + result[i]);
            }
        }

        [TestMethod]
        public void Test_Simple2x2SPD()
        {
            double tolerance = 1e-6;
            int maxIterations = 100;
            // SPD Matrix: A = [ [4, 1], [1, 3] ]
            Func<double[], double[]> matrixMultiply = delegate (double[] vector)
            {
                if (vector == null || vector.Length != 2)
                {
                    return new double[0];
                }

                double[] result = new double[2];
                result[0] = 4 * vector[0] + 1 * vector[1];
                result[1] = 1 * vector[0] + 3 * vector[1];
                return result;
            };
            double[] b = new double[]
            {
                1.0,
                2.0
            };
            double[] expected = new double[]
            {
                1.0 / 11.0,
                7.0 / 11.0
            };
            double[] result = ConjugateGradientSolver.Solve(matrixMultiply, b, tolerance, maxIterations);
            Assert.AreEqual(expected.Length, result.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                double diff = Math.Abs(result[i] - expected[i]);
                Assert.IsTrue(diff < 1e-4, "Element " + i + " differs: expected " + expected[i] + ", got " + result[i]);
            }
        }

        [TestMethod]
        public void Test_LargeSyntheticSPD()
        {
            int n = 10;
            double tolerance = 1e-6;
            int maxIterations = 1000;
            double[, ] A = new double[n, n];
            Random random = new Random(42);
            // Generate symmetric positive definite matrix A
            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    double value = random.NextDouble();
                    if (i == j)
                    {
                        value += n;
                    }

                    A[i, j] = value;
                    A[j, i] = value;
                }
            }

            // Generate known solution xKnown and compute b = A * xKnown
            double[] xKnown = new double[n];
            for (int i = 0; i < n; i++)
            {
                xKnown[i] = random.NextDouble();
            }

            double[] b = new double[n];
            for (int i = 0; i < n; i++)
            {
                double sum = 0.0;
                for (int j = 0; j < n; j++)
                {
                    sum += A[i, j] * xKnown[j];
                }

                b[i] = sum;
            }

            Func<double[], double[]> matrixMultiply = delegate (double[] vector)
            {
                if (vector == null || vector.Length != n)
                {
                    return new double[0];
                }

                double[] result = new double[n];
                for (int i = 0; i < n; i++)
                {
                    double sum = 0.0;
                    for (int j = 0; j < n; j++)
                    {
                        sum += A[i, j] * vector[j];
                    }

                    result[i] = sum;
                }

                return result;
            };
            double[] xComputed = ConjugateGradientSolver.Solve(matrixMultiply, b, tolerance, maxIterations);
            double residualNorm = 0.0;
            double[] Ax = matrixMultiply(xComputed);
            for (int i = 0; i < n; i++)
            {
                double diff = Ax[i] - b[i];
                residualNorm += diff * diff;
            }

            residualNorm = Math.Sqrt(residualNorm);
            Assert.IsTrue(residualNorm < 1e-4, "Residual norm is too high: " + residualNorm);
        }
    }
}