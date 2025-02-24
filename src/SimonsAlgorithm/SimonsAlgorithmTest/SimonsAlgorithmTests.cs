using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimonsAlgorithm;

namespace SimonsAlgorithmTest
{
    [TestClass]
    public class SimonsAlgorithmTests
    {
        private static int TwoToOneF(int x, int secret)
        {
            return (x < (x ^ secret)) ? x : (x ^ secret);
        }

        [TestMethod]
        public void TestFindSecret_N1_S1()
        {
            int n = 1;
            int secret = 1;
            System.Func<int, int> f = delegate (int x)
            {
                return TwoToOneF(x, secret);
            };
            int foundSecret = SimonsAlgorithmSolver.FindSecret(n, f);
            Assert.AreEqual(secret, foundSecret, "Found secret does not match for n=1");
        }

        [TestMethod]
        public void TestFindSecret_N4_S6()
        {
            int n = 4;
            int secret = 6;
            System.Func<int, int> f = delegate (int x)
            {
                return TwoToOneF(x, secret);
            };
            int foundSecret = SimonsAlgorithmSolver.FindSecret(n, f);
            Assert.AreEqual(secret, foundSecret, "Found secret does not match for n=4");
        }

        [TestMethod]
        public void TestFindSecret_N6_S21()
        {
            int n = 6;
            int secret = 21;
            System.Func<int, int> f = delegate (int x)
            {
                return TwoToOneF(x, secret);
            };
            int foundSecret = SimonsAlgorithmSolver.FindSecret(n, f);
            Assert.AreEqual(secret, foundSecret, "Found secret does not match for n=6");
        }

        [TestMethod]
        public void TestFindSecret_MultipleTrials()
        {
            int n = 4;
            int secret = 9;
            System.Func<int, int> f = delegate (int x)
            {
                return TwoToOneF(x, secret);
            };
            int trialCount = 5;
            for (int i = 0; i < trialCount; i++)
            {
                int foundSecret = SimonsAlgorithmSolver.FindSecret(n, f);
                Assert.AreEqual(secret, foundSecret, "Found secret does not match in trial " + i);
            }
        }

        [TestMethod]
        public void TestFindSecret_RandomData()
        {
            System.Random randomDataGenerator = new System.Random();
            int[] nValues = new int[]
            {
                2,
                4,
                6,
                8
            };
            for (int i = 0; i < nValues.Length; i++)
            {
                int n = nValues[i];
                for (int trial = 0; trial < 5; trial++)
                {
                    int limit = 1 << n;
                    int secret = randomDataGenerator.Next(1, limit);
                    System.Func<int, int> f = delegate (int x)
                    {
                        return TwoToOneF(x, secret);
                    };
                    int foundSecret = SimonsAlgorithmSolver.FindSecret(n, f);
                    Assert.AreEqual(secret, foundSecret, "Found secret does not match for n=" + n + " trial=" + trial);
                }
            }
        }
    }
}