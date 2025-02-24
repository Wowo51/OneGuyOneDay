using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BoyerMooreAlgorithm;

namespace BoyerMooreAlgorithmTest
{
    [TestClass]
    public class BoyerMooreAlgorithmTests
    {
        [TestMethod]
        public void Search_NullText_ReturnsMinusOne()
        {
            int result = BoyerMoore.Search(null, "pattern");
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Search_NullPattern_ReturnsMinusOne()
        {
            int result = BoyerMoore.Search("text", null);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Search_EmptyPattern_ReturnsZero()
        {
            int result = BoyerMoore.Search("abc", "");
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Search_PatternAtStart_ReturnsZero()
        {
            int result = BoyerMoore.Search("abcdef", "abc");
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Search_PatternInMiddle_ReturnsCorrectIndex()
        {
            int result = BoyerMoore.Search("abcdef", "cd");
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void Search_PatternAtEnd_ReturnsCorrectIndex()
        {
            int result = BoyerMoore.Search("abcdef", "def");
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Search_PatternNotFound_ReturnsMinusOne()
        {
            int result = BoyerMoore.Search("abcdef", "xyz");
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Search_MultipleOccurrences_ReturnsFirstOccurrence()
        {
            int result = BoyerMoore.Search("abcabcabc", "abc");
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Search_LargeStringTest_ReturnsCorrectIndex()
        {
            System.Random random = new System.Random(42);
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            StringBuilder builder = new StringBuilder();
            int length = 10000;
            for (int i = 0; i < length; i++)
            {
                int charIndex = random.Next(0, alphabet.Length);
                builder.Append(alphabet[charIndex]);
            }

            string randomText = builder.ToString();
            string pattern = "pattern";
            int insertIndex = 5000;
            string finalText = randomText.Substring(0, insertIndex) + pattern + randomText.Substring(insertIndex);
            int result = BoyerMoore.Search(finalText, pattern);
            Assert.AreEqual(insertIndex, result);
        }
    }
}