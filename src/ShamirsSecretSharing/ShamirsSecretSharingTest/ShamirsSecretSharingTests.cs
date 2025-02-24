using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShamirsSecretSharing;

namespace ShamirsSecretSharingTest
{
    [TestClass]
    public class ShamirsSecretSharingTests
    {
        [TestMethod]
        public void SplitSecret_InvalidThreshold_TooHigh()
        {
            int secret = 123;
            int threshold = 5;
            int numShares = 4;
            List<Share> shares = SecretSharing.SplitSecret(secret, threshold, numShares);
            Assert.AreEqual(0, shares.Count);
        }

        [TestMethod]
        public void SplitSecret_InvalidThreshold_TooLow()
        {
            int secret = 123;
            int threshold = 1;
            int numShares = 5;
            List<Share> shares = SecretSharing.SplitSecret(secret, threshold, numShares);
            Assert.AreEqual(0, shares.Count);
        }

        [TestMethod]
        public void CombineShares_EmptyList()
        {
            List<Share> emptyList = new List<Share>();
            int result = SecretSharing.CombineShares(emptyList);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void SplitAndCombine_AllShares()
        {
            int secret = 2021;
            int threshold = 3;
            int numShares = 5;
            List<Share> shares = SecretSharing.SplitSecret(secret, threshold, numShares);
            Assert.AreEqual(numShares, shares.Count);
            int recoveredSecret = SecretSharing.CombineShares(shares);
            Assert.AreEqual(secret, recoveredSecret);
        }

        [TestMethod]
        public void SplitAndCombine_Subset()
        {
            int secret = 555;
            int threshold = 3;
            int numShares = 5;
            List<Share> shares = SecretSharing.SplitSecret(secret, threshold, numShares);
            List<Share> subset = new List<Share>();
            for (int index = 0; index < threshold; index++)
            {
                subset.Add(shares[index]);
            }

            int recoveredSecret = SecretSharing.CombineShares(subset);
            Assert.AreEqual(secret, recoveredSecret);
        }

        [TestMethod]
        public void SplitAndCombine_ZeroSecret()
        {
            int secret = 0;
            int threshold = 3;
            int numShares = 5;
            List<Share> shares = SecretSharing.SplitSecret(secret, threshold, numShares);
            int recoveredSecret = SecretSharing.CombineShares(shares);
            Assert.AreEqual(secret, recoveredSecret);
        }

        [TestMethod]
        public void SplitAndCombine_RandomDataSets()
        {
            // Generate synthetic test data to validate secret splitting and reconstruction.
            Random random = new Random();
            for (int test = 0; test < 10; test++)
            {
                int secret = random.Next(0, (int)SecretSharing.Prime);
                int threshold = random.Next(2, 6); // threshold between 2 and 5
                int numShares = threshold + random.Next(0, 6); // ensure numShares >= threshold
                List<Share> shares = SecretSharing.SplitSecret(secret, threshold, numShares);
                if (shares.Count == 0)
                {
                    Assert.Fail("SplitSecret returned empty shares for valid parameters.");
                }

                // Test combining all generated shares.
                int recoveredAll = SecretSharing.CombineShares(shares);
                Assert.AreEqual(secret, recoveredAll, "Recovery from all shares failed.");
                // Test combining only a valid subset of shares (of size threshold).
                List<Share> subset = new List<Share>();
                for (int index = 0; index < threshold; index++)
                {
                    subset.Add(shares[index]);
                }

                int recoveredSubset = SecretSharing.CombineShares(subset);
                Assert.AreEqual(secret, recoveredSubset, "Recovery from subset of shares failed.");
            }
        }

        [TestMethod]
        public void SplitAndCombine_MaxSecret()
        {
            // Test with secret equal to Prime - 1 (maximum valid secret).
            int secret = (int)(SecretSharing.Prime - 1);
            int threshold = 3;
            int numShares = 5;
            List<Share> shares = SecretSharing.SplitSecret(secret, threshold, numShares);
            int recovered = SecretSharing.CombineShares(shares);
            Assert.AreEqual(secret, recovered, "Recovery with maximum secret value failed.");
        }

        [TestMethod]
        public void SplitSecret_SharesHaveUniqueX()
        {
            int secret = 999;
            int threshold = 3;
            int numShares = 5;
            List<Share> shares = SecretSharing.SplitSecret(secret, threshold, numShares);
            HashSet<int> xValues = new HashSet<int>();
            foreach (Share share in shares)
            {
                bool added = xValues.Add(share.X);
                Assert.IsTrue(added, $"Duplicate share x value {share.X} found.");
            }

            for (int i = 1; i <= numShares; i++)
            {
                Assert.IsTrue(xValues.Contains(i), $"Missing share with x value {i}.");
            }
        }
    }
}