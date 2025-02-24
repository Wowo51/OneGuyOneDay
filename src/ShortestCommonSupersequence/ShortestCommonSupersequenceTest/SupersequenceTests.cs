using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShortestCommonSupersequence;

namespace ShortestCommonSupersequenceTest
{
    [TestClass]
    public class SupersequenceTests
    {
        [TestMethod]
        public void TestEmptyInput_NullAndEmptyArray()
        {
            string resultNull = SupersequenceSolver.ShortestCommonSupersequence(null);
            Assert.AreEqual(string.Empty, resultNull);
            string[] emptyArray = new string[0];
            string resultEmpty = SupersequenceSolver.ShortestCommonSupersequence(emptyArray);
            Assert.AreEqual(string.Empty, resultEmpty);
        }

        [TestMethod]
        public void TestSingleSequence()
        {
            string input = "abc";
            string result = SupersequenceSolver.ShortestCommonSupersequence(input);
            Assert.AreEqual(input, result);
        }

        [TestMethod]
        public void TestTwoSequences_Simple()
        {
            string sequence1 = "abac";
            string sequence2 = "cab";
            string expected = "cabac";
            string result = SupersequenceSolver.ShortestCommonSupersequence(sequence1, sequence2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestSequence_WithEmptyString()
        {
            string sequence1 = "";
            string sequence2 = "test";
            string result = SupersequenceSolver.ShortestCommonSupersequence(sequence1, sequence2);
            Assert.AreEqual("test", result);
            result = SupersequenceSolver.ShortestCommonSupersequence("test", "");
            Assert.AreEqual("test", result);
        }

        [TestMethod]
        public void TestIdenticalSequences()
        {
            string sequence = "abc";
            string result = SupersequenceSolver.ShortestCommonSupersequence(sequence, sequence);
            Assert.AreEqual(sequence, result);
        }

        [TestMethod]
        public void TestLargeRandomInput()
        {
            int length1 = 1000;
            int length2 = 1000;
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            Random rnd = new Random(0);
            for (int i = 0; i < length1; i++)
            {
                sb1.Append(alphabet[rnd.Next(alphabet.Length)]);
            }

            for (int j = 0; j < length2; j++)
            {
                sb2.Append(alphabet[rnd.Next(alphabet.Length)]);
            }

            string s1 = sb1.ToString();
            string s2 = sb2.ToString();
            string result = SupersequenceSolver.ShortestCommonSupersequence(s1, s2);
            Assert.IsTrue(IsSubsequence(s1, result));
            Assert.IsTrue(IsSubsequence(s2, result));
            Assert.IsTrue(result.Length <= s1.Length + s2.Length);
        }

        [TestMethod]
        public void TestMultipleSequences()
        {
            string s1 = "ab";
            string s2 = "bc";
            string s3 = "ca";
            string result = SupersequenceSolver.ShortestCommonSupersequence(s1, s2, s3);
            Assert.IsTrue(IsSubsequence(s1, result));
            Assert.IsTrue(IsSubsequence(s2, result));
            Assert.IsTrue(IsSubsequence(s3, result));
            Assert.IsTrue(result.Length <= s1.Length + s2.Length + s3.Length);
        }

        private static bool IsSubsequence(string subsequence, string sequence)
        {
            int index = 0;
            for (int i = 0; i < sequence.Length && index < subsequence.Length; i++)
            {
                if (sequence[i] == subsequence[index])
                {
                    index++;
                }
            }

            return index == subsequence.Length;
        }
    }
}