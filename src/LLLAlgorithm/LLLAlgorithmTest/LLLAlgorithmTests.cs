using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LLLAlgorithm;

namespace LLLAlgorithmTest
{
    [TestClass]
    public class LLLAlgorithmTests
    {
        private const double Tolerance = 1e-9;
        [TestMethod]
        public void Test_NullInput_ReturnsEmptyArray()
        {
            double[][] result = LLLReducer.Reduce(null);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Test_EmptyInput_ReturnsSameArray()
        {
            double[][] emptyBasis = new double[0][];
            double[][] result = LLLReducer.Reduce(emptyBasis);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Test_SingleVector_ReturnsSameVector()
        {
            double[] vector = new double[]
            {
                3.0,
                4.0
            };
            double[][] basis = new double[][]
            {
                vector
            };
            double[][] result = LLLReducer.Reduce(basis);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Length);
            Assert.IsTrue(ArrayEquals(vector, result[0]));
        }

        [TestMethod]
        public void Test_OrthogonalBasis_RemainsUnchanged()
        {
            double[][] basis = new double[][]
            {
                new double[]
                {
                    1.0,
                    0.0
                },
                new double[]
                {
                    0.0,
                    1.0
                }
            };
            double[][] result = LLLReducer.Reduce(basis);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Length);
            // In an orthogonal basis, the vectors should remain unchanged.
            Assert.IsTrue(ArrayEquals(basis[0], result[0]));
            Assert.IsTrue(ArrayEquals(basis[1], result[1]));
        }

        [TestMethod]
        public void Test_NonOrthogonalBasis_ReducesProperly()
        {
            // Use a non-orthogonal basis that requires reduction.
            double[][] basis = new double[][]
            {
                new double[]
                {
                    1.0,
                    1.0
                },
                new double[]
                {
                    1.0,
                    0.0
                }
            };
            double delta = 0.75;
            double[][] result = LLLReducer.Reduce(basis, delta);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Length);
            // Verify that each vector is non-zero.
            Assert.IsFalse(IsZeroVector(result[0]));
            Assert.IsFalse(IsZeroVector(result[1]));
            // Use Gram-Schmidt to compute the orthogonalized vector for the second vector.
            double[] bStar0 = result[0];
            double[] bStar1 = GramSchmidt(result, 1);
            double normBStar0 = NormSquared(bStar0);
            double normBStar1 = NormSquared(bStar1);
            double mu = Dot(result[1], bStar0) / (normBStar0 > Tolerance ? normBStar0 : 1.0);
            double condition = delta - mu * mu;
            Assert.IsTrue(normBStar1 + Tolerance >= condition * normBStar0);
        }

        [TestMethod]
        public void Test_RandomBasis_DoesNotCrash()
        {
            System.Random random = new System.Random(42);
            for (int k = 0; k < 10; k++)
            {
                int n = random.Next(2, 6);
                int dim = random.Next(2, 6);
                double[][] basis = GenerateRandomBasis(n, dim, random);
                double[][] result = LLLReducer.Reduce(basis, 0.75);
                Assert.IsNotNull(result);
                Assert.AreEqual(n, result.Length);
                for (int i = 0; i < n; i++)
                {
                    Assert.IsNotNull(result[i]);
                    for (int j = 0; j < dim; j++)
                    {
                        Assert.IsFalse(double.IsNaN(result[i][j]));
                    }
                }
            }
        }

        private static double[][] GenerateRandomBasis(int n, int dim, System.Random random)
        {
            double[][] basis = new double[n][];
            for (int i = 0; i < n; i++)
            {
                basis[i] = new double[dim];
                for (int j = 0; j < dim; j++)
                {
                    basis[i][j] = random.NextDouble() * 10 - 5;
                }
            }

            return basis;
        }

        private static bool ArrayEquals(double[] a, double[] b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }

            for (int i = 0; i < a.Length; i++)
            {
                if (System.Math.Abs(a[i] - b[i]) > Tolerance)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsZeroVector(double[] vector)
        {
            for (int i = 0; i < vector.Length; i++)
            {
                if (System.Math.Abs(vector[i]) > Tolerance)
                {
                    return false;
                }
            }

            return true;
        }

        private static double NormSquared(double[] vector)
        {
            double sum = 0.0;
            for (int i = 0; i < vector.Length; i++)
            {
                sum += vector[i] * vector[i];
            }

            return sum;
        }

        private static double Dot(double[] a, double[] b)
        {
            double sum = 0.0;
            for (int i = 0; i < a.Length; i++)
            {
                sum += a[i] * b[i];
            }

            return sum;
        }

        private static double[] GramSchmidt(double[][] basis, int index)
        {
            double[] bStar = (double[])basis[index].Clone();
            for (int j = 0; j < index; j++)
            {
                double[] prev = basis[j];
                double dot = Dot(basis[index], prev);
                double normSq = NormSquared(prev);
                if (normSq > Tolerance)
                {
                    double mu = dot / normSq;
                    double[] subtract = new double[prev.Length];
                    for (int i = 0; i < prev.Length; i++)
                    {
                        subtract[i] = mu * prev[i];
                    }

                    for (int i = 0; i < bStar.Length; i++)
                    {
                        bStar[i] -= subtract[i];
                    }
                }
            }

            return bStar;
        }
    }
}