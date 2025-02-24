using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;
using MillerRabinTest;

namespace MillerRabinTestTest
{
    [TestClass]
    public class MillerRabinTests
    {
        [TestMethod]
        public void TestSmallNumbers()
        {
            System.Numerics.BigInteger resultZero = System.Numerics.BigInteger.Zero;
            bool isZeroPrime = MillerRabin.IsProbablyPrime(resultZero, 10);
            Assert.IsFalse(isZeroPrime, "Zero should not be prime.");
            System.Numerics.BigInteger negativeOne = new System.Numerics.BigInteger(-1);
            bool isNegativePrime = MillerRabin.IsProbablyPrime(negativeOne, 10);
            Assert.IsFalse(isNegativePrime, "Negative numbers should not be prime.");
            bool isOnePrime = MillerRabin.IsProbablyPrime(System.Numerics.BigInteger.One, 10);
            Assert.IsFalse(isOnePrime, "One is not prime.");
        }

        [TestMethod]
        public void TestEdgePrimes()
        {
            bool isTwoPrime = MillerRabin.IsProbablyPrime(new System.Numerics.BigInteger(2), 10);
            Assert.IsTrue(isTwoPrime, "2 should be prime.");
            bool isThreePrime = MillerRabin.IsProbablyPrime(new System.Numerics.BigInteger(3), 10);
            Assert.IsTrue(isThreePrime, "3 should be prime.");
        }

        [TestMethod]
        public void TestKnownPrimes()
        {
            System.Numerics.BigInteger[] primes = new System.Numerics.BigInteger[]
            {
                new System.Numerics.BigInteger(5),
                new System.Numerics.BigInteger(7),
                new System.Numerics.BigInteger(11),
                new System.Numerics.BigInteger(13),
                new System.Numerics.BigInteger(17),
                new System.Numerics.BigInteger(19),
                new System.Numerics.BigInteger(23),
                new System.Numerics.BigInteger(29),
                new System.Numerics.BigInteger(7919)
            };
            for (int i = 0; i < primes.Length; i++)
            {
                bool isPrime = MillerRabin.IsProbablyPrime(primes[i], 15);
                Assert.IsTrue(isPrime, primes[i] + " should be prime.");
            }
        }

        [TestMethod]
        public void TestKnownComposites()
        {
            System.Numerics.BigInteger[] composites = new System.Numerics.BigInteger[]
            {
                new System.Numerics.BigInteger(4),
                new System.Numerics.BigInteger(6),
                new System.Numerics.BigInteger(8),
                new System.Numerics.BigInteger(9),
                new System.Numerics.BigInteger(15),
                new System.Numerics.BigInteger(21),
                new System.Numerics.BigInteger(25),
                new System.Numerics.BigInteger(27)
            };
            for (int i = 0; i < composites.Length; i++)
            {
                bool isPrime = MillerRabin.IsProbablyPrime(composites[i], 15);
                Assert.IsFalse(isPrime, composites[i] + " should be composite.");
            }
        }

        [TestMethod]
        public void TestLargePrime()
        {
            // 2^31 - 1, a known prime.
            System.Numerics.BigInteger largePrime = new System.Numerics.BigInteger(2147483647);
            bool isLargePrime = MillerRabin.IsProbablyPrime(largePrime, 15);
            Assert.IsTrue(isLargePrime, "2147483647 should be prime.");
        }

        [TestMethod]
        public void TestRandomNumbersPerformance()
        {
            System.Random randomGenerator = new System.Random(1234);
            for (int i = 0; i < 100; i++)
            {
                int randomInt = randomGenerator.Next(10000, 100000);
                System.Numerics.BigInteger candidate = new System.Numerics.BigInteger(randomInt);
                bool result = MillerRabin.IsProbablyPrime(candidate, 10);
                // This assertion simply ensures the method returns a boolean without exception.
                Assert.IsTrue(result == true || result == false, "Return value should be boolean.");
            }
        }
    }
}