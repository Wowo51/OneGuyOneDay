using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using BytePairEncoding;

namespace BytePairEncodingTest
{
    [TestClass]
    public class BytePairEncoderTests
    {
        [TestMethod]
        public void TestEncode_NullInput_ReturnsEmptyList()
        {
            List<string> result = BytePairEncoder.Encode(null, 5);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestEncode_EmptyString_ReturnsEmptyList()
        {
            List<string> result = BytePairEncoder.Encode(string.Empty, 5);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestEncode_NoMergeOperations_ReturnsSeparatedCharacters()
        {
            string input = "abc";
            int mergeOperations = 0;
            List<string> result = BytePairEncoder.Encode(input, mergeOperations);
            CollectionAssert.AreEqual(new List<string> { "a", "b", "c" }, result);
            string resultString = BytePairEncoder.EncodeToString(input, mergeOperations);
            Assert.AreEqual("a b c", resultString);
        }

        [TestMethod]
        public void TestEncode_SingleMergeOperation_MergesCorrectly()
        {
            string input = "abab";
            int mergeOperations = 1;
            List<string> result = BytePairEncoder.Encode(input, mergeOperations);
            CollectionAssert.AreEqual(new List<string> { "ab", "ab" }, result);
            string resultString = BytePairEncoder.EncodeToString(input, mergeOperations);
            Assert.AreEqual("ab ab", resultString);
        }

        [TestMethod]
        public void TestEncode_MultipleMergeOperations_MergesCorrectly()
        {
            string input = "ababab";
            int mergeOperations = 2;
            List<string> result = BytePairEncoder.Encode(input, mergeOperations);
            // After first merge: "a", "b", "a", "b", "a", "b" becomes ["ab", "ab", "ab"].
            // After second merge: merging the first two "ab" tokens leads to ["abab", "ab"].
            CollectionAssert.AreEqual(new List<string> { "abab", "ab" }, result);
            string resultString = BytePairEncoder.EncodeToString(input, mergeOperations);
            Assert.AreEqual("abab ab", resultString);
        }

        [TestMethod]
        public void TestEncode_NoMergePossible_ReturnsOriginalTokens()
        {
            // For a string with all unique adjacent pairs, no merge will occur
            string input = "abcd";
            int mergeOperations = 5;
            List<string> result = BytePairEncoder.Encode(input, mergeOperations);
            CollectionAssert.AreEqual(new List<string> { "a", "b", "c", "d" }, result);
            string resultString = BytePairEncoder.EncodeToString(input, mergeOperations);
            Assert.AreEqual("a b c d", resultString);
        }

        [TestMethod]
        public void TestEncode_LargeInput_PerformanceTest()
        {
            // Generate a large input string using synthetic data generation.
            int repeatCount = 1000;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < repeatCount; i++)
            {
                sb.Append("abab ");
            }

            string input = sb.ToString().Trim();
            int mergeOperations = 10;
            List<string> result = BytePairEncoder.Encode(input, mergeOperations);
            // Expect the merged tokens count to be less than the original character count.
            Assert.IsTrue(result.Count < input.Length);
        }

        [TestMethod]
        public void TestEncode_NegativeMergeOperations_ReturnsSeparatedCharacters()
        {
            string input = "abc";
            int mergeOperations = -1;
            List<string> result = BytePairEncoder.Encode(input, mergeOperations);
            CollectionAssert.AreEqual(new List<string> { "a", "b", "c" }, result);
            string resultString = BytePairEncoder.EncodeToString(input, mergeOperations);
            Assert.AreEqual("a b c", resultString);
        }
    }
}