using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BailliePSWTest;

namespace BailliePSWTestTest
{
    [TestClass]
    public class PrimalityTestTests
    {
        [TestMethod]
        public void TestEdgeCases()
        {
            Assert.IsFalse(PrimalityTest.IsPrime(new BigInteger(-1)));
            Assert.IsFalse(PrimalityTest.IsPrime(BigInteger.Zero));
            Assert.IsFalse(PrimalityTest.IsPrime(BigInteger.One));
            Assert.IsTrue(PrimalityTest.IsPrime(new BigInteger(2)));
            Assert.IsTrue(PrimalityTest.IsPrime(new BigInteger(3)));
        }

        [TestMethod]
        public void TestSmallPrimes()
        {
            int[] primes =
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
                29,
                31,
                37,
                41,
                43,
                47
            };
            for (int i = 0; i < primes.Length; i++)
            {
                int prime = primes[i];
                bool result = PrimalityTest.IsPrime(new BigInteger(prime));
                Assert.IsTrue(result, $"{prime} should be prime");
            }
        }

        [TestMethod]
        public void TestSmallComposites()
        {
            int[] composites =
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
                20,
                21,
                22,
                24,
                25,
                27
            };
            for (int i = 0; i < composites.Length; i++)
            {
                int composite = composites[i];
                bool result = PrimalityTest.IsPrime(new BigInteger(composite));
                Assert.IsFalse(result, $"{composite} should be composite");
            }
        }

        [TestMethod]
        public void TestLargePrimes()
        {
            BigInteger prime1 = BigInteger.Parse("6700417");
            BigInteger prime2 = BigInteger.Parse("2147483647");
            Assert.IsTrue(PrimalityTest.IsPrime(prime1), "6700417 should be prime");
            Assert.IsTrue(PrimalityTest.IsPrime(prime2), "2147483647 should be prime");
        }

        [TestMethod]
        public void TestLargeComposites()
        {
            BigInteger composite1 = BigInteger.Parse("6700418");
            BigInteger composite2 = BigInteger.Parse("2147483646");
            Assert.IsFalse(PrimalityTest.IsPrime(composite1), "6700418 should be composite");
            Assert.IsFalse(PrimalityTest.IsPrime(composite2), "2147483646 should be composite");
        }

        [TestMethod]
        public void SyntheticTestData()
        {
            for (int i = 100; i <= 200; i++)
            {
                BigInteger testValue = new BigInteger(i);
                bool expected = IsPrimeSimple(i);
                bool actual = PrimalityTest.IsPrime(testValue);
                Assert.AreEqual(expected, actual, $"Mismatch for {i}");
            }
        }

        private bool IsPrimeSimple(int n)
        {
            if (n < 2)
            {
                return false;
            }

            double sqrt = Math.Sqrt(n);
            for (int i = 2; i <= sqrt; i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}