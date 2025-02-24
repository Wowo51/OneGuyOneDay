using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LenstraEllipticFactorization;

namespace LenstraEllipticFactorizationTest
{
    [TestClass]
    public class LenstraFactorizationTests
    {
        [TestMethod]
        public void TestFactorEven()
        {
            BigInteger n = new BigInteger(8);
            BigInteger factor = LenstraFactorization.Factor(n);
            Assert.AreEqual(new BigInteger(2), factor, "Even number factorization should return 2.");
        }

        [TestMethod]
        public void TestFactorOddComposite()
        {
            BigInteger n = new BigInteger(91); // 7 * 13
            int maxTrials = 10;
            bool foundNonTrivial = false;
            BigInteger factor = n;
            for (int i = 0; i < maxTrials; i++)
            {
                factor = LenstraFactorization.Factor(n);
                if (factor != n && factor != new BigInteger(1) && (n % factor) == 0)
                {
                    foundNonTrivial = true;
                    break;
                }
            }

            Assert.IsTrue(foundNonTrivial, "Failed to find a nontrivial factor for composite odd number after multiple trials.");
        }

        [TestMethod]
        public void TestFactorPrime()
        {
            BigInteger n = new BigInteger(97); // prime
            BigInteger factor = LenstraFactorization.Factor(n);
            Assert.AreEqual(n, factor, "Prime number factorization should return the number itself.");
        }

        [TestMethod]
        public void TestLargeComposite()
        {
            // 8051 = 97 * 83
            BigInteger n = new BigInteger(8051);
            int maxTrials = 10;
            bool foundNonTrivial = false;
            BigInteger factor = n;
            for (int i = 0; i < maxTrials; i++)
            {
                factor = LenstraFactorization.Factor(n);
                if (factor != n && factor != new BigInteger(1) && (n % factor) == 0)
                {
                    foundNonTrivial = true;
                    break;
                }
            }

            Assert.IsTrue(foundNonTrivial, "Failed to find a nontrivial factor for large composite number after multiple trials.");
        }

        [TestMethod]
        public void TestRandomComposite()
        {
            System.Random randomRand = new System.Random();
            int[] smallPrimes = new int[]
            {
                3,
                5,
                7,
                11,
                13,
                17,
                19
            };
            for (int i = 0; i < 5; i++)
            {
                int a = smallPrimes[randomRand.Next(smallPrimes.Length)];
                int b = smallPrimes[randomRand.Next(smallPrimes.Length)];
                BigInteger n = new BigInteger(a * b);
                BigInteger factor = LenstraFactorization.Factor(n);
                if (factor != n && factor != new BigInteger(1))
                {
                    Assert.IsTrue((n % factor) == 0, $"Factor {factor} should divide {n}.");
                }
                else
                {
                    Assert.AreEqual(n, factor, $"Factorization returned trivial factor for composite {n}.");
                }
            }
        }
    }
}