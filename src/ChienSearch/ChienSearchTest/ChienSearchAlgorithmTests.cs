using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ChienSearch;

namespace ChienSearchTest
{
    [TestClass]
    public class ChienSearchAlgorithmTests
    {
        [TestMethod]
        public void TestFindRoots_KnownPolynomial()
        {
            int[] polynomial = new int[]
            {
                1,
                0,
                1
            };
            int modulus = 5;
            List<int> roots = ChienSearchAlgorithm.FindRoots(polynomial, modulus);
            List<int> expected = new List<int>
            {
                2,
                3
            };
            Assert.AreEqual(expected.Count, roots.Count, "Root count mismatch.");
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], roots[i], $"Mismatch at index {i}.");
            }
        }

        [TestMethod]
        public void TestEvaluatePolynomial_KnownValues()
        {
            int[] polynomial = new int[]
            {
                1,
                0,
                1
            };
            int modulus = 5;
            int[] expectedValues = new int[]
            {
                1,
                2,
                0,
                0,
                2
            };
            for (int x = 0; x < 5; x++)
            {
                int value = ChienSearchAlgorithm.EvaluatePolynomial(polynomial, x, modulus);
                Assert.AreEqual(expectedValues[x], value, $"Failed at x = {x}");
            }
        }

        [TestMethod]
        public void TestEmptyCoefficients()
        {
            int[] polynomial = new int[]
            {
            };
            int modulus = 7;
            int value = ChienSearchAlgorithm.EvaluatePolynomial(polynomial, 3, modulus);
            Assert.AreEqual(0, value, "Empty coefficients should evaluate to 0.");
        }

        [TestMethod]
        public void TestNullCoefficients()
        {
            int modulus = 7;
            int value = ChienSearchAlgorithm.EvaluatePolynomial(null, 3, modulus);
            Assert.AreEqual(0, value, "Null coefficients should evaluate to 0.");
        }

        [TestMethod]
        public void TestFindRoots_NoRoots()
        {
            int[] polynomial = new int[]
            {
                1
            };
            int modulus = 7;
            List<int> roots = ChienSearchAlgorithm.FindRoots(polynomial, modulus);
            Assert.AreEqual(0, roots.Count, "Constant non-zero polynomial must have no roots.");
        }

        [TestMethod]
        public void TestLargePolynomial()
        {
            int modulus = 97;
            int length = 100;
            int[] polynomial = new int[length];
            Random random = new Random(0);
            for (int i = 0; i < length; i++)
            {
                polynomial[i] = random.Next(-100, 101);
            }

            for (int x = 0; x < modulus; x++)
            {
                int expectedValue = ExpectedEvaluatePolynomial(polynomial, x, modulus);
                int actualValue = ChienSearchAlgorithm.EvaluatePolynomial(polynomial, x, modulus);
                Assert.AreEqual(expectedValue, actualValue, $"Mismatch at x = {x}");
            }
        }

        private int ExpectedEvaluatePolynomial(int[] coefficients, int x, int modulus)
        {
            int result = 0;
            int power = 1;
            for (int i = 0; i < coefficients.Length; i++)
            {
                int term = (coefficients[i] * power) % modulus;
                if (term < 0)
                {
                    term += modulus;
                }

                result = (result + term) % modulus;
                power = (power * x) % modulus;
            }

            if (result < 0)
            {
                result += modulus;
            }

            return result;
        }

        [TestMethod]
        public void TestFindRoots_AllRoots()
        {
            int[] polynomial = new int[]
            {
                0,
                0,
                0
            };
            int modulus = 11;
            List<int> roots = ChienSearchAlgorithm.FindRoots(polynomial, modulus);
            Assert.AreEqual(modulus, roots.Count, "Zero polynomial should have all possible roots.");
            for (int i = 0; i < modulus; i++)
            {
                Assert.AreEqual(i, roots[i], $"Mismatch at candidate {i}.");
            }
        }
    }
}