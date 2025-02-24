using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KnuthMorrisPratt;

namespace KnuthMorrisPrattTest
{
    [TestClass]
    public class KMPAlgorithmTests
    {
        [TestMethod]
        public void Search_NullText_ReturnsEmptyList()
        {
            List<int> result = KMPAlgorithm.Search(null, "pattern");
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Search_NullPattern_ReturnsEmptyList()
        {
            List<int> result = KMPAlgorithm.Search("text", null);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Search_EmptyPattern_ReturnsEmptyList()
        {
            List<int> result = KMPAlgorithm.Search("text", "");
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Search_PatternNotFound_ReturnsEmptyList()
        {
            List<int> result = KMPAlgorithm.Search("abcdef", "xyz");
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Search_SingleOccurrence_ReturnsCorrectIndex()
        {
            List<int> result = KMPAlgorithm.Search("hello", "ll");
            CollectionAssert.AreEqual(new List<int> { 2 }, result);
        }

        [TestMethod]
        public void Search_MultipleOccurrences_ReturnsCorrectIndices()
        {
            List<int> result = KMPAlgorithm.Search("ababa", "aba");
            CollectionAssert.AreEqual(new List<int> { 0, 2 }, result);
        }

        [TestMethod]
        public void Search_OverlappingOccurrences_ReturnsCorrectIndices()
        {
            List<int> result = KMPAlgorithm.Search("aaaa", "aa");
            CollectionAssert.AreEqual(new List<int> { 0, 1, 2 }, result);
        }

        [TestMethod]
        public void Search_LargeText_ReturnsCorrectCount()
        {
            String text = new String('a', 1000);
            String pattern = "aa";
            List<int> result = KMPAlgorithm.Search(text, pattern);
            Int32 expectedCount = text.Length - pattern.Length + 1;
            Assert.AreEqual(expectedCount, result.Count);
            if (result.Count > 0)
            {
                Assert.AreEqual(0, result[0]);
                Assert.AreEqual(expectedCount - 1, result[result.Count - 1]);
            }
        }

        [TestMethod]
        public void Search_EmptyText_ReturnsEmptyList()
        {
            List<int> result = KMPAlgorithm.Search("", "pattern");
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Search_TextEqualsPattern_ReturnsZero()
        {
            List<int> result = KMPAlgorithm.Search("pattern", "pattern");
            CollectionAssert.AreEqual(new List<int> { 0 }, result);
        }
    }
}