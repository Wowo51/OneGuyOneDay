using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuineMcCluskeyAlgorithm;

namespace QuineMcCluskeyAlgorithmTest
{
    [TestClass]
    public class QuineMcCluskeyTests
    {
        [TestMethod]
        public void TestToBinaryString()
        {
            string expected = "0101";
            string actual = QuineMcCluskey.ToBinaryString(5, 4);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSimplifyEmpty()
        {
            List<int> minterms = new List<int>();
            List<int> dontCares = new List<int>();
            List<string> result = QuineMcCluskey.Simplify(minterms, dontCares, 3);
            Assert.AreEqual(0, result.Count, "Expected no simplified terms for empty input.");
        }

        [TestMethod]
        public void TestSimplifySingleMinterm()
        {
            List<int> minterms = new List<int>
            {
                2
            };
            List<int> dontCares = new List<int>();
            List<string> result = QuineMcCluskey.Simplify(minterms, dontCares, 3);
            Assert.AreEqual(1, result.Count, "Expected one simplified term for a single minterm.");
            Assert.AreEqual("010", result[0], "Simplified term should match binary representation of 2 in 3 bits.");
        }

        [TestMethod]
        public void TestSimplifyTwoMintermsCombine()
        {
            List<int> minterms = new List<int>
            {
                1,
                3
            };
            List<int> dontCares = new List<int>();
            List<string> result = QuineMcCluskey.Simplify(minterms, dontCares, 2);
            Assert.AreEqual(1, result.Count, "Expected one combined implicant for two minterms.");
            Assert.AreEqual("-1", result[0], "Combined implicant should be '-1'.");
        }

        [TestMethod]
        public void TestSimplifyWithDontCares()
        {
            List<int> minterms = new List<int>
            {
                1,
                3
            };
            List<int> dontCares = new List<int>
            {
                2
            };
            List<string> result = QuineMcCluskey.Simplify(minterms, dontCares, 3);
            Assert.AreEqual(1, result.Count, "Expected one essential prime implicant when using dont cares.");
            Assert.AreEqual("0-1", result[0], "Expected implicant '0-1' covering minterms 1 and 3.");
        }

        [TestMethod]
        public void TestSimplifyRandomCoverage()
        {
            int variableCount = 4;
            List<int> minterms = new List<int>();
            Random rng = new Random(1234);
            while (minterms.Count < 8)
            {
                int candidate = rng.Next(0, 16);
                if (!minterms.Contains(candidate))
                {
                    minterms.Add(candidate);
                }
            }

            List<int> dontCares = new List<int>();
            List<string> simplified = QuineMcCluskey.Simplify(minterms, dontCares, variableCount);
            foreach (int m in minterms)
            {
                string binary = QuineMcCluskey.ToBinaryString(m, variableCount);
                bool covered = false;
                foreach (string term in simplified)
                {
                    if (Covers(term, binary))
                    {
                        covered = true;
                        break;
                    }
                }

                Assert.IsTrue(covered, "Minterm " + m.ToString() + " (" + binary + ") is not covered by any implicant.");
            }
        }

        private bool Covers(string pattern, string binary)
        {
            if (pattern.Length != binary.Length)
            {
                return false;
            }

            for (int i = 0; i < pattern.Length; i++)
            {
                char p = pattern[i];
                char b = binary[i];
                if (p != '-' && p != b)
                {
                    return false;
                }
            }

            return true;
        }
    }
}