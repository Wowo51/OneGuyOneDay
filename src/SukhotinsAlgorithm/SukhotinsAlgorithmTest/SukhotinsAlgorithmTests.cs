using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SukhotinsAlgorithm;

namespace SukhotinsAlgorithmTest
{
    [TestClass]
    public class SukhotinsAlgorithmTests
    {
        [TestMethod]
        public void TestEmptyInput()
        {
            ClassificationResult result = SukhotinsAlgorithm.SukhotinsAlgorithm.Classify("");
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(0, result.Vowels.Count, "Vowels should be empty for empty input.");
            Assert.AreEqual(0, result.Consonants.Count, "Consonants should be empty for empty input.");
        }

        [TestMethod]
        public void TestNullInput()
        {
            ClassificationResult result = SukhotinsAlgorithm.SukhotinsAlgorithm.Classify(null);
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(0, result.Vowels.Count, "Vowels should be empty for null input.");
            Assert.AreEqual(0, result.Consonants.Count, "Consonants should be empty for null input.");
        }

        [TestMethod]
        public void TestSingleLetter()
        {
            string input = "z";
            ClassificationResult result = SukhotinsAlgorithm.SukhotinsAlgorithm.Classify(input);
            CollectionAssert.AreEquivalent(new List<char> { 'z' }, result.Vowels, "Single letter should be classified as vowel.");
            Assert.AreEqual(0, result.Consonants.Count, "Consonants should be empty for single letter input.");
        }

        [TestMethod]
        public void TestSimpleABA()
        {
            string input = "aba";
            ClassificationResult result = SukhotinsAlgorithm.SukhotinsAlgorithm.Classify(input);
            CollectionAssert.AreEquivalent(new List<char> { 'a', 'b' }, result.Vowels, "Expected vowels to include 'a' and 'b'.");
            Assert.AreEqual(0, result.Consonants.Count, "Consonants should be empty for input 'aba'.");
        }

        [TestMethod]
        public void TestBanana()
        {
            string input = "banana";
            ClassificationResult result = SukhotinsAlgorithm.SukhotinsAlgorithm.Classify(input);
            CollectionAssert.AreEquivalent(new List<char> { 'a', 'b', 'n' }, result.Vowels, "Expected vowels to include 'a', 'b', and 'n'.");
            Assert.AreEqual(0, result.Consonants.Count, "Consonants should be empty for input 'banana'.");
        }

        [TestMethod]
        public void TestPunctuationAndDigitsOnly()
        {
            string input = "1234!@#";
            ClassificationResult result = SukhotinsAlgorithm.SukhotinsAlgorithm.Classify(input);
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(0, result.Vowels.Count, "Vowels should be empty for punctuation input.");
            Assert.AreEqual(0, result.Consonants.Count, "Consonants should be empty for punctuation input.");
        }

        [TestMethod]
        public void TestLargeRandomInput()
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random(0);
            int length = 1000;
            for (int i = 0; i < length; i++)
            {
                char letter = (char)('a' + random.Next(0, 26));
                builder.Append(letter);
            }

            string input = builder.ToString();
            ClassificationResult result = SukhotinsAlgorithm.SukhotinsAlgorithm.Classify(input);
            HashSet<char> uniqueLetters = new HashSet<char>();
            foreach (char ch in input)
            {
                if (Char.IsLetter(ch))
                {
                    uniqueLetters.Add(ch);
                }
            }

            HashSet<char> union = new HashSet<char>(result.Vowels);
            union.UnionWith(result.Consonants);
            CollectionAssert.AreEquivalent(uniqueLetters.ToList(), union.ToList(), "Union of vowels and consonants should equal unique letters in input.");
        }
    }
}