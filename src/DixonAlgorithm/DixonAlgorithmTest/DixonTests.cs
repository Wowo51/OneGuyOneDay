using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DixonAlgorithm;
using System.Collections.Generic;

namespace DixonAlgorithmTest
{
    [TestClass]
    public class DixonTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            TestContext.WriteLine("Starting test: " + TestContext.TestName);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            TestContext.WriteLine("Finished test: " + TestContext.TestName);
        }

        [TestMethod]
        public void TestTrivialCases()
        {
            TestContext.WriteLine("Running TestTrivialCases");
            BigInteger nZero = new BigInteger(0);
            (BigInteger factor1, BigInteger factor2) = Dixon.Factor(nZero);
            Assert.AreEqual(nZero, factor1 * factor2, "0 factorization invalid");
            BigInteger nOne = new BigInteger(1);
            (BigInteger factor1One, BigInteger factor2One) = Dixon.Factor(nOne);
            Assert.AreEqual(nOne, factor1One * factor2One, "1 factorization invalid");
            BigInteger nNegative = new BigInteger(-7);
            (BigInteger f1Neg, BigInteger f2Neg) = Dixon.Factor(nNegative);
            Assert.AreEqual(nNegative, f1Neg * f2Neg, "Negative factorization invalid");
        }

        [TestMethod]
        public void TestEvenNumbers()
        {
            TestContext.WriteLine("Running TestEvenNumbers");
            BigInteger n = new BigInteger(16);
            (BigInteger f1, BigInteger f2) = Dixon.Factor(n);
            Assert.AreEqual(n, f1 * f2, "16 factorization invalid");
            Assert.AreEqual(new BigInteger(2), f1, "Factor for an even number is not 2");
        }

        [TestMethod]
        public void TestOddCompositeNumbers()
        {
            TestContext.WriteLine("Running TestOddCompositeNumbers");
            BigInteger n = new BigInteger(15);
            (BigInteger f1, BigInteger f2) = Dixon.Factor(n);
            Assert.AreEqual(n, f1 * f2, "15 factorization invalid");
            Assert.IsTrue(f1 != n && f2 != n, "15 factorization should be nontrivial");
        }

        [TestMethod]
        public void TestPrimeNumber()
        {
            TestContext.WriteLine("Running TestPrimeNumber");
            BigInteger n = new BigInteger(7);
            (BigInteger f1, BigInteger f2) = Dixon.Factor(n);
            Assert.AreEqual(n, f1 * f2, "Prime factorization invalid");
            Assert.IsTrue(f1 == n || f2 == n, "For prime, one factor should be n");
        }

        [TestMethod]
        public void TestManyCompositeNumbers()
        {
            TestContext.WriteLine("Running TestManyCompositeNumbers");
            int[] primes = new int[]
            {
                3,
                5,
                7,
                11,
                13,
                17,
                19
            };
            for (int i = 0; i < primes.Length; i++)
            {
                for (int j = i; j < primes.Length; j++)
                {
                    BigInteger n = new BigInteger(primes[i]) * new BigInteger(primes[j]);
                    (BigInteger f1, BigInteger f2) = Dixon.Factor(n);
                    TestContext.WriteLine("Testing n = " + n.ToString() + " with factors: " + f1.ToString() + ", " + f2.ToString());
                    Assert.AreEqual(n, f1 * f2, $"Factorization of {n} is invalid");
                    if (n >= new BigInteger(4))
                    {
                        Assert.IsTrue(f1 != new BigInteger(1) && f2 != new BigInteger(1), $"Factorization of {n} should be nontrivial");
                    }
                }
            }
        }
    }
}