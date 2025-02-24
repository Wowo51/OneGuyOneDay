using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LongestCommonSubstring;

namespace LongestCommonSubstringTest
{
    [TestClass]
    public class LongestCommonSubstringTests
    {
        [TestMethod]
        public void TestNullInput()
        {
            string[] inputs = null;
            List<string> result = LongestCommonSubstringFinder.FindLongestCommonSubstrings(inputs);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestSingleInput()
        {
            string[] inputs = new string[]
            {
                "abc"
            };
            List<string> result = LongestCommonSubstringFinder.FindLongestCommonSubstrings(inputs);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestEmptyStrings()
        {
            string[] inputs = new string[]
            {
                "",
                ""
            };
            List<string> result = LongestCommonSubstringFinder.FindLongestCommonSubstrings(inputs);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestCommonSubstring()
        {
            string[] inputs = new string[]
            {
                "ababc",
                "babca"
            };
            List<string> result = LongestCommonSubstringFinder.FindLongestCommonSubstrings(inputs);
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(new List<string> { "babc" }, result);
        }

        [TestMethod]
        public void TestMultipleCommonSubstrings()
        {
            string[] inputs = new string[]
            {
                "abc",
                "cba"
            };
            List<string> result = LongestCommonSubstringFinder.FindLongestCommonSubstrings(inputs);
            Assert.IsNotNull(result);
            CollectionAssert.AreEquivalent(new List<string> { "a", "b", "c" }, result);
        }

        [TestMethod]
        public void TestLargeSyntheticData()
        {
            StringBuilder builder1 = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            string common = "COMMON_SUBSTRING";
            for (int i = 0; i < 1000; i++)
            {
                builder1.Append("A");
                builder2.Append("B");
            }

            string first = builder1.ToString() + common + builder1.ToString();
            string second = builder2.ToString() + common + builder2.ToString();
            string[] inputs = new string[]
            {
                first,
                second
            };
            List<string> result = LongestCommonSubstringFinder.FindLongestCommonSubstrings(inputs);
            Assert.IsNotNull(result);
            CollectionAssert.Contains(result, common);
        }

        // Additional tests for thorough coverage
        [TestMethod]
        public void TestNoCommonSubstring()
        {
            string[] inputs = new string[]
            {
                "abc",
                "def"
            };
            List<string> result = LongestCommonSubstringFinder.FindLongestCommonSubstrings(inputs);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestMoreThanTwoInputs()
        {
            string[] inputs = new string[]
            {
                "abcdef",
                "abcxyz",
                "abc123"
            };
            List<string> result = LongestCommonSubstringFinder.FindLongestCommonSubstrings(inputs);
            Assert.IsNotNull(result);
            CollectionAssert.Contains(result, "abc");
        }

        [TestMethod]
        public void TestWithNullElement()
        {
            string[] inputs = new string[]
            {
                "abc",
                null,
                "ab"
            };
            List<string> result = LongestCommonSubstringFinder.FindLongestCommonSubstrings(inputs);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestCaseSensitivity()
        {
            string[] inputs = new string[]
            {
                "ABC",
                "abc"
            };
            List<string> result = LongestCommonSubstringFinder.FindLongestCommonSubstrings(inputs);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestOverlappingCommonSubstring()
        {
            string[] inputs = new string[]
            {
                "aaa",
                "aa"
            };
            List<string> result = LongestCommonSubstringFinder.FindLongestCommonSubstrings(inputs);
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(new List<string> { "aa" }, result);
        }
    }
}