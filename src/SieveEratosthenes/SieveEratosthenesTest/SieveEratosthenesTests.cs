using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using SieveEratosthenes;

namespace SieveEratosthenesTest
{
    [TestClass]
    public class SieveEratosthenesTests
    {
        private bool IsPrime(int number)
        {
            if (number < 2)
            {
                return false;
            }

            for (int i = 2; i * i <= number; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        [TestMethod]
        public void TestBoundLessThanTwo()
        {
            List<int> primesForZero = Sieve.GetPrimes(0);
            Assert.AreEqual(0, primesForZero.Count);
            List<int> primesForOne = Sieve.GetPrimes(1);
            Assert.AreEqual(0, primesForOne.Count);
            List<int> primesForNegative = Sieve.GetPrimes(-10);
            Assert.AreEqual(0, primesForNegative.Count);
        }

        [TestMethod]
        public void TestBoundEqualTwo()
        {
            List<int> primes = Sieve.GetPrimes(2);
            Assert.AreEqual(1, primes.Count);
            Assert.AreEqual(2, primes[0]);
        }

        [TestMethod]
        public void TestBoundTen()
        {
            List<int> expected = new List<int>
            {
                2,
                3,
                5,
                7
            };
            List<int> actual = Sieve.GetPrimes(10);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestBoundHundred()
        {
            List<int> actual = Sieve.GetPrimes(100);
            Assert.AreEqual(25, actual.Count);
            foreach (int value in actual)
            {
                Assert.IsTrue(IsPrime(value));
            }
        }

        [TestMethod]
        public void TestLargeBound()
        {
            int bound = 1000;
            List<int> actual = Sieve.GetPrimes(bound);
            Assert.AreEqual(168, actual.Count);
            foreach (int value in actual)
            {
                Assert.IsTrue(value <= bound);
                Assert.IsTrue(IsPrime(value));
            }
        }

        [TestMethod]
        public void TestRandomBounds()
        {
            Random random = new Random(42);
            for (int i = 0; i < 10; i++)
            {
                int bound = random.Next(2, 501);
                List<int> actual = Sieve.GetPrimes(bound);
                int previous = 1;
                foreach (int value in actual)
                {
                    Assert.IsTrue(value > previous);
                    Assert.IsTrue(IsPrime(value));
                    Assert.IsTrue(value <= bound);
                    previous = value;
                }
            }
        }
    }
}