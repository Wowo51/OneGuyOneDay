using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlakeysSecretSharing;

namespace BlakeysSecretSharingTest
{
    [TestClass]
    public class SecretSharingTests
    {
        [TestMethod]
        public void TestGenerateShares_NullSecret_ReturnsEmptyList()
        {
            List<Hyperplane> shares = SecretSharing.GenerateShares(null, 5);
            Assert.AreEqual(0, shares.Count);
        }

        [TestMethod]
        public void TestGenerateShares_CorrectConstant()
        {
            double[] secret = new double[]
            {
                1.0,
                2.0,
                3.0
            };
            List<Hyperplane> shares = SecretSharing.GenerateShares(secret, 5);
            for (int i = 0; i < shares.Count; i++)
            {
                double computed = 0.0;
                for (int j = 0; j < secret.Length; j++)
                {
                    computed += shares[i].Coefficients[j] * secret[j];
                }

                Assert.IsTrue(Math.Abs(computed - shares[i].Constant) < 1e-6);
            }
        }

        [TestMethod]
        public void TestReconstructSecret_InsufficientShares_ReturnsNaNs()
        {
            double[] secret = new double[]
            {
                1.0,
                2.0,
                3.0
            };
            List<Hyperplane> shares = SecretSharing.GenerateShares(secret, 2);
            double[] result = SecretSharing.ReconstructSecret(shares);
            Assert.AreEqual(secret.Length, result.Length);
            for (int i = 0; i < result.Length; i++)
            {
                Assert.IsTrue(double.IsNaN(result[i]));
            }
        }

        [TestMethod]
        public void TestReconstructSecret_EmptyShares_ReturnsEmptyArray()
        {
            double[] result = SecretSharing.ReconstructSecret(new List<Hyperplane>());
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void TestReconstructSecret_ValidShares_ReconstructsSecret()
        {
            double[] secret = new double[]
            {
                2.0,
                -3.0,
                5.0
            };
            List<Hyperplane> shares = SecretSharing.GenerateShares(secret, secret.Length);
            double[] reconstructed = SecretSharing.ReconstructSecret(shares);
            Assert.AreEqual(secret.Length, reconstructed.Length);
            for (int i = 0; i < secret.Length; i++)
            {
                Assert.IsTrue(Math.Abs(secret[i] - reconstructed[i]) < 1e-6, "Mismatch at index " + i);
            }
        }

        [TestMethod]
        public void TestReconstructSecret_RandomData()
        {
            System.Random random = new System.Random();
            int dimension = 10;
            double[] secret = new double[dimension];
            for (int i = 0; i < dimension; i++)
            {
                secret[i] = random.NextDouble() * 100.0 - 50.0;
            }

            List<Hyperplane> shares = SecretSharing.GenerateShares(secret, dimension);
            double[] reconstructed = SecretSharing.ReconstructSecret(shares);
            for (int i = 0; i < dimension; i++)
            {
                Assert.IsTrue(Math.Abs(secret[i] - reconstructed[i]) < 1e-6, "Mismatch at index " + i);
            }
        }

        [TestMethod]
        public void TestGenerateShares_ZeroShareCount_ReturnsEmptyList()
        {
            double[] secret = new double[]
            {
                1.0,
                2.0,
                3.0
            };
            List<Hyperplane> shares = SecretSharing.GenerateShares(secret, 0);
            Assert.AreEqual(0, shares.Count);
        }

        [TestMethod]
        public void TestReconstructSecret_ExtraShares_ReconstructsSecret()
        {
            double[] secret = new double[]
            {
                4.0,
                5.0,
                6.0
            };
            List<Hyperplane> shares = SecretSharing.GenerateShares(secret, secret.Length + 2);
            double[] reconstructed = SecretSharing.ReconstructSecret(shares);
            Assert.AreEqual(secret.Length, reconstructed.Length);
            for (int i = 0; i < secret.Length; i++)
            {
                Assert.IsTrue(Math.Abs(secret[i] - reconstructed[i]) < 1e-6, "Mismatch at index " + i);
            }
        }

        [TestMethod]
        public void TestReconstructSecret_LargeRandomData()
        {
            System.Random random = new System.Random();
            int dimension = 100;
            double[] secret = new double[dimension];
            for (int i = 0; i < dimension; i++)
            {
                secret[i] = random.NextDouble() * 200.0 - 100.0;
            }

            List<Hyperplane> shares = SecretSharing.GenerateShares(secret, dimension);
            double[] reconstructed = SecretSharing.ReconstructSecret(shares);
            Assert.AreEqual(secret.Length, reconstructed.Length);
            for (int i = 0; i < secret.Length; i++)
            {
                Assert.IsTrue(Math.Abs(secret[i] - reconstructed[i]) < 1e-6, "Mismatch at index " + i);
            }
        }
    }
}