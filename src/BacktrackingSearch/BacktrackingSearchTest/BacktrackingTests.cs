using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BacktrackingSearch;

namespace BacktrackingSearchTest
{
    [TestClass]
    public class BacktrackingTests
    {
        [TestMethod]
        public void TestBinaryStringLength3()
        {
            string initial = "";
            Func<string, bool> isSolution = (string candidate) => candidate.Length == 3;
            Func<string, IEnumerable<string>> getExtensions = (string candidate) =>
            {
                List<string> extensions = new List<string>();
                if (candidate.Length < 3)
                {
                    extensions.Add(candidate + "0");
                    extensions.Add(candidate + "1");
                }

                return extensions;
            };
            List<string> solutions = Backtracking.Solve<string>(initial, isSolution, getExtensions);
            List<string> expected = new List<string>
            {
                "000",
                "001",
                "010",
                "011",
                "100",
                "101",
                "110",
                "111"
            };
            CollectionAssert.AreEquivalent(expected, solutions);
        }

        [TestMethod]
        public void TestLargeBinaryStringsGeneration()
        {
            string initial = "";
            int targetLength = 10;
            Func<string, bool> isSolution = (string candidate) => candidate.Length == targetLength;
            Func<string, IEnumerable<string>> getExtensions = (string candidate) =>
            {
                List<string> extensions = new List<string>();
                if (candidate.Length < targetLength)
                {
                    extensions.Add(candidate + "0");
                    extensions.Add(candidate + "1");
                }

                return extensions;
            };
            List<string> solutions = Backtracking.Solve<string>(initial, isSolution, getExtensions);
            int expectedCount = 1024;
            Assert.AreEqual(expectedCount, solutions.Count);
        }

        [TestMethod]
        public void TestImmediateSolution()
        {
            int initial = 0;
            Func<int, bool> isSolution = (int candidate) => candidate == 0;
            Func<int, IEnumerable<int>> getExtensions = (int candidate) => new List<int>();
            List<int> solutions = Backtracking.Solve<int>(initial, isSolution, getExtensions);
            Assert.AreEqual(1, solutions.Count);
            Assert.AreEqual(0, solutions[0]);
        }

        [TestMethod]
        public void TestDescendingChain()
        {
            int initial = 5;
            Func<int, bool> isSolution = (int candidate) => candidate == 0;
            Func<int, IEnumerable<int>> getExtensions = (int candidate) =>
            {
                List<int> extensions = new List<int>();
                if (candidate > 0)
                {
                    extensions.Add(candidate - 1);
                }

                return extensions;
            };
            List<int> solutions = Backtracking.Solve<int>(initial, isSolution, getExtensions);
            Assert.AreEqual(1, solutions.Count);
            Assert.AreEqual(0, solutions[0]);
        }
    }
}