using System;
using System.Collections.Generic;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuadraticSieve;

namespace QuadraticSieveTest
{
    [TestClass]
    public class QuadraticSieveUnitTests
    {
        [TestMethod]
        public void TestMod2Solver_EmptyMatrix()
        {
            List<List<int>> matrix = new List<List<int>>();
            List<int>? result = Mod2Solver.Solve(matrix);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestMod2Solver_SingleZeroRow()
        {
            List<List<int>> matrix = new List<List<int>>
            {
                new List<int>
                {
                    0,
                    0
                }
            };
            List<int>? result = Mod2Solver.Solve(matrix);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result!.Count);
            Assert.AreEqual(1, result[0]);
        }

        [TestMethod]
        public void TestMod2Solver_DuplicateRows()
        {
            List<List<int>> matrix = new List<List<int>>
            {
                new List<int>
                {
                    1,
                    0
                },
                new List<int>
                {
                    1,
                    0
                }
            };
            List<int>? result = Mod2Solver.Solve(matrix);
            Assert.IsNotNull(result);
            // Expected dependency vector [1, 1] from subset selecting both rows.
            Assert.AreEqual(2, result!.Count);
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(1, result[1]);
        }

        [TestMethod]
        public void TestFactor_InputOne()
        {
            BigInteger n = new BigInteger(1);
            List<BigInteger> factors = QuadraticSieveAlgorithm.Factor(n);
            Assert.AreEqual(1, factors.Count);
            Assert.AreEqual(n, factors[0]);
        }

        [TestMethod]
        public void TestFactor_Prime()
        {
            BigInteger n = new BigInteger(7);
            List<BigInteger> factors = QuadraticSieveAlgorithm.Factor(n);
            Assert.AreEqual(1, factors.Count);
            Assert.AreEqual(n, factors[0]);
        }

        [TestMethod]
        public void TestFactor_PerfectSquare_Four()
        {
            BigInteger n = new BigInteger(4);
            List<BigInteger> factors = QuadraticSieveAlgorithm.Factor(n);
            BigInteger product = BigInteger.One;
            for (int i = 0; i < factors.Count; i++)
            {
                product *= factors[i];
            }

            Assert.AreEqual(n, product);
            Assert.AreEqual(2, factors.Count);
            Assert.AreEqual(new BigInteger(2), factors[0]);
            Assert.AreEqual(new BigInteger(2), factors[1]);
        }

        [TestMethod]
        public void TestFactor_PerfectSquare_121()
        {
            BigInteger n = new BigInteger(121);
            List<BigInteger> factors = QuadraticSieveAlgorithm.Factor(n);
            BigInteger product = BigInteger.One;
            for (int i = 0; i < factors.Count; i++)
            {
                product *= factors[i];
            }

            Assert.AreEqual(n, product);
            Assert.AreEqual(2, factors.Count);
            Assert.AreEqual(new BigInteger(11), factors[0]);
            Assert.AreEqual(new BigInteger(11), factors[1]);
        }

        [TestMethod]
        public void TestFactor_CompositeNonSquare_6()
        {
            BigInteger n = new BigInteger(6);
            List<BigInteger> factors = QuadraticSieveAlgorithm.Factor(n);
            BigInteger product = BigInteger.One;
            for (int i = 0; i < factors.Count; i++)
            {
                product *= factors[i];
            }

            Assert.AreEqual(n, product);
        }

        [TestMethod]
        public void TestFactor_CompositeNonSquare_91()
        {
            BigInteger n = new BigInteger(91);
            List<BigInteger> factors = QuadraticSieveAlgorithm.Factor(n);
            BigInteger product = BigInteger.One;
            for (int i = 0; i < factors.Count; i++)
            {
                product *= factors[i];
            }

            Assert.AreEqual(n, product);
        }

        [TestMethod]
        public void TestFactor_LargePrime()
        {
            BigInteger n = new BigInteger(997);
            List<BigInteger> factors = QuadraticSieveAlgorithm.Factor(n);
            Assert.AreEqual(1, factors.Count);
            Assert.AreEqual(n, factors[0]);
        }

        [TestMethod]
        public void TestMod2Solver_BigMatrix()
        {
            List<List<int>> matrix = new List<List<int>>
            {
                new List<int>
                {
                    1,
                    0,
                    1
                },
                new List<int>
                {
                    0,
                    1,
                    1
                },
                new List<int>
                {
                    1,
                    1,
                    0
                }
            };
            List<int>? dependency = Mod2Solver.Solve(matrix);
            Assert.IsNotNull(dependency);
            CollectionAssert.AreEqual(new List<int> { 1, 1, 1 }, dependency);
        }

        [TestMethod]
        public void TestFactor_Zero()
        {
            BigInteger n = new BigInteger(0);
            List<BigInteger> factors = QuadraticSieveAlgorithm.Factor(n);
            Assert.AreEqual(1, factors.Count);
            Assert.AreEqual(n, factors[0]);
        }

        [TestMethod]
        public void TestFactor_Negative()
        {
            BigInteger n = new BigInteger(-1);
            List<BigInteger> factors = QuadraticSieveAlgorithm.Factor(n);
            Assert.AreEqual(1, factors.Count);
            Assert.AreEqual(n, factors[0]);
        }

        [TestMethod]
        public void TestFactor_PowerOfTwo()
        {
            BigInteger n = new BigInteger(256);
            List<BigInteger> factors = QuadraticSieveAlgorithm.Factor(n);
            BigInteger product = BigInteger.One;
            for (int i = 0; i < factors.Count; i++)
            {
                product *= factors[i];
            }

            Assert.AreEqual(n, product);
        }

        [TestMethod]
        public void TestFactor_RandomCompositeNumbers()
        {
            Random random = new Random();
            int testCases = 10;
            int[] smallPrimes = new int[]
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
            for (int t = 0; t < testCases; t++)
            {
                int factorCount = random.Next(2, 5);
                BigInteger n = BigInteger.One;
                for (int i = 0; i < factorCount; i++)
                {
                    int prime = smallPrimes[random.Next(smallPrimes.Length)];
                    n *= prime;
                }

                List<BigInteger> factors = QuadraticSieveAlgorithm.Factor(n);
                BigInteger product = BigInteger.One;
                for (int i = 0; i < factors.Count; i++)
                {
                    product *= factors[i];
                }

                Assert.AreEqual(n, product, "Failed for composite number: " + n.ToString());
            }
        }
    }
}