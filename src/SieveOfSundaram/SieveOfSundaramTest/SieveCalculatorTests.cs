using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using SieveOfSundaram;

namespace SieveOfSundaramTest
{
    [TestClass]
    public class SieveCalculatorTests
    {
        [TestMethod]
        public void GeneratePrimes_NegativeInput_ReturnsEmptyList()
        {
            List<int> primes = SieveCalculator.GeneratePrimes(-10);
            Assert.AreEqual(0, primes.Count, "Expected empty list for negative input");
        }

        [TestMethod]
        public void GeneratePrimes_InputLessThan2_ReturnsEmptyList()
        {
            List<int> primes = SieveCalculator.GeneratePrimes(1);
            Assert.AreEqual(0, primes.Count, "Expected empty list when input is less than 2");
        }

        [TestMethod]
        public void GeneratePrimes_InputEquals0_ReturnsEmptyList()
        {
            List<int> primes = SieveCalculator.GeneratePrimes(0);
            Assert.AreEqual(0, primes.Count, "Expected empty list when input is 0");
        }

        [TestMethod]
        public void GeneratePrimes_InputEquals2_ReturnsOnly2()
        {
            List<int> primes = SieveCalculator.GeneratePrimes(2);
            Assert.AreEqual(1, primes.Count, "Expected one prime when input equals 2");
            Assert.AreEqual(2, primes[0], "Expected the prime to be 2 when input equals 2");
        }

        [TestMethod]
        public void GeneratePrimes_InputEquals3_ReturnsTwoAndThree()
        {
            List<int> primes = SieveCalculator.GeneratePrimes(3);
            CollectionAssert.AreEqual(new List<int> { 2, 3 }, primes, "Expected primes [2, 3] when input equals 3");
        }

        [TestMethod]
        public void GeneratePrimes_InputEquals10_ReturnsPrimesUpTo10()
        {
            List<int> primes = SieveCalculator.GeneratePrimes(10);
            CollectionAssert.AreEqual(new List<int> { 2, 3, 5, 7 }, primes, "Expected primes [2, 3, 5, 7] when input equals 10");
        }

        [TestMethod]
        public void GeneratePrimes_InputEquals100_ReturnsCorrectPrimes()
        {
            List<int> primes = SieveCalculator.GeneratePrimes(100);
            List<int> expected = new List<int>
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
                47,
                53,
                59,
                61,
                67,
                71,
                73,
                79,
                83,
                89,
                97
            };
            CollectionAssert.AreEqual(expected, primes, "Unexpected primes for input 100");
        }

        [TestMethod]
        public void GeneratePrimes_RandomLargeInput_CorrectnessTest()
        {
            List<int> primes = SieveCalculator.GeneratePrimes(1000);
            int previous = 0;
            foreach (int prime in primes)
            {
                Assert.IsTrue(prime <= 1000, $"Found prime {{prime}} greater than 1000");
                Assert.IsTrue(prime > previous, "Primes are not in ascending order");
                previous = prime;
            }
        }

        [TestMethod]
        public void GeneratePrimes_RandomInput_PrimeValidationTest()
        {
            int max = 500;
            List<int> primes = SieveCalculator.GeneratePrimes(max);
            int previous = 0;
            foreach (int prime in primes)
            {
                Assert.IsTrue(prime <= max, $"Found prime {{prime}} greater than max {{max}}");
                Assert.IsTrue(prime > previous, "Primes are not in ascending order");
                Assert.IsTrue(IsPrime(prime), $"Number {{prime}} is not prime");
                previous = prime;
            }
        }

        private bool IsPrime(int number)
        {
            if (number < 2)
            {
                return false;
            }

            int boundary = (int)Math.Sqrt(number);
            for (int i = 2; i <= boundary; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}