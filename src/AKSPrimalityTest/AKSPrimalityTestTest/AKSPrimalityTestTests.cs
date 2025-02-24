using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AKSPrimalityTest;

namespace AKSPrimalityTestTest
{
    [TestClass]
    public class AksPrimalityTestTests
    {
        private static bool TrialDivision(BigInteger number)
        {
            if (number < 2)
            {
                return false;
            }

            for (BigInteger i = 2; i * i <= number; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        [TestMethod]
        public void TestNegativeNumbers()
        {
            Assert.IsFalse(AksPrimalityTest.IsPrime(new BigInteger(-7)));
            Assert.IsFalse(AksPrimalityTest.IsPrime(new BigInteger(-1)));
        }

        [TestMethod]
        public void TestZeroAndOne()
        {
            Assert.IsFalse(AksPrimalityTest.IsPrime(BigInteger.Zero));
            Assert.IsFalse(AksPrimalityTest.IsPrime(BigInteger.One));
        }

        [TestMethod]
        public void TestSmallPrimes()
        {
            BigInteger[] primes = new BigInteger[]
            {
                2,
                3,
                5,
                7,
                11,
                13,
                17,
                19,
                23,
                29
            };
            foreach (BigInteger prime in primes)
            {
                Assert.IsTrue(AksPrimalityTest.IsPrime(prime), prime + " should be prime.");
            }
        }

        [TestMethod]
        public void TestSmallComposites()
        {
            BigInteger[] composites = new BigInteger[]
            {
                4,
                6,
                8,
                9,
                10,
                12,
                14,
                15,
                16,
                18,
                20
            };
            foreach (BigInteger composite in composites)
            {
                Assert.IsFalse(AksPrimalityTest.IsPrime(composite), composite + " should be composite.");
            }
        }

        [TestMethod]
        public void TestRangeUsingTrialDivision()
        {
            for (BigInteger num = 2; num <= 100; num++)
            {
                bool expected = TrialDivision(num);
                bool result = AksPrimalityTest.IsPrime(num);
                Assert.AreEqual(expected, result, "Mismatch for " + num + ".");
            }
        }

        [TestMethod]
        public void TestLargeNumbers()
        {
            BigInteger primeLarge = BigInteger.Parse("104729");
            Assert.IsTrue(AksPrimalityTest.IsPrime(primeLarge), "104729 should be prime.");
            BigInteger compositeLarge = primeLarge * 2;
            Assert.IsFalse(AksPrimalityTest.IsPrime(compositeLarge), compositeLarge + " should be composite.");
        }
    }
}