using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HirschbergsAlgorithm;

namespace HirschbergsAlgorithmTest
{
    [TestClass]
    public class HirschbergsAlgorithmTests
    {
        [TestMethod]
        public void TestEmptySequences()
        {
            AlignmentResult result = HirschbergsAlgorithm.HirschbergsAlgorithm.Align("", "");
            Assert.AreEqual("", result.AlignedSequence1);
            Assert.AreEqual("", result.AlignedSequence2);
            Assert.AreEqual(0, result.Cost);
        }

        [TestMethod]
        public void TestFirstEmpty()
        {
            string seq1 = "";
            string seq2 = "ABC";
            AlignmentResult result = HirschbergsAlgorithm.HirschbergsAlgorithm.Align(seq1, seq2);
            Assert.AreEqual(new string ('-', seq2.Length), result.AlignedSequence1);
            Assert.AreEqual(seq2, result.AlignedSequence2);
            Assert.AreEqual(seq2.Length, result.Cost);
        }

        [TestMethod]
        public void TestSecondEmpty()
        {
            string seq1 = "ABC";
            string seq2 = "";
            AlignmentResult result = HirschbergsAlgorithm.HirschbergsAlgorithm.Align(seq1, seq2);
            Assert.AreEqual(seq1, result.AlignedSequence1);
            Assert.AreEqual(new string ('-', seq1.Length), result.AlignedSequence2);
            Assert.AreEqual(seq1.Length, result.Cost);
        }

        [TestMethod]
        public void TestSingleCharacterMatch()
        {
            AlignmentResult result = HirschbergsAlgorithm.HirschbergsAlgorithm.Align("A", "A");
            Assert.AreEqual("A", result.AlignedSequence1);
            Assert.AreEqual("A", result.AlignedSequence2);
            Assert.AreEqual(0, result.Cost);
        }

        [TestMethod]
        public void TestSingleCharacterMismatch()
        {
            AlignmentResult result = HirschbergsAlgorithm.HirschbergsAlgorithm.Align("A", "B");
            Assert.AreEqual("A", result.AlignedSequence1);
            Assert.AreEqual("B", result.AlignedSequence2);
            Assert.AreEqual(1, result.Cost);
        }

        [TestMethod]
        public void TestIdenticalSequences()
        {
            string seq1 = "ABC";
            string seq2 = "ABC";
            AlignmentResult result = HirschbergsAlgorithm.HirschbergsAlgorithm.Align(seq1, seq2);
            Assert.AreEqual(seq1, result.AlignedSequence1);
            Assert.AreEqual(seq2, result.AlignedSequence2);
            Assert.AreEqual(0, result.Cost);
        }

        [TestMethod]
        public void TestCompletelyDifferentSequences()
        {
            string seq1 = "ABC";
            string seq2 = "DEF";
            AlignmentResult result = HirschbergsAlgorithm.HirschbergsAlgorithm.Align(seq1, seq2);
            Assert.AreEqual(seq1, result.AlignedSequence1);
            Assert.AreEqual(seq2, result.AlignedSequence2);
            Assert.AreEqual(3, result.Cost);
        }

        [TestMethod]
        public void TestGeneralCase()
        {
            string seq1 = "AGTACGCA";
            string seq2 = "TATGC";
            AlignmentResult result = HirschbergsAlgorithm.HirschbergsAlgorithm.Align(seq1, seq2);
            Assert.AreEqual(result.AlignedSequence1.Length, result.AlignedSequence2.Length);
            Assert.IsTrue(result.AlignedSequence1.Length > 0);
            int calculatedCost = CalculateHammingDistance(result.AlignedSequence1, result.AlignedSequence2);
            Assert.AreEqual(calculatedCost, result.Cost);
        }

        [TestMethod]
        public void TestLargeRandomInput()
        {
            Random random = new Random(0);
            const string chars = "ACGT";
            int length1 = 1000;
            int length2 = 1000;
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            for (int i = 0; i < length1; i++)
            {
                sb1.Append(chars[random.Next(chars.Length)]);
            }

            for (int j = 0; j < length2; j++)
            {
                sb2.Append(chars[random.Next(chars.Length)]);
            }

            string seq1 = sb1.ToString();
            string seq2 = sb2.ToString();
            AlignmentResult result = HirschbergsAlgorithm.HirschbergsAlgorithm.Align(seq1, seq2);
            Assert.AreEqual(result.AlignedSequence1.Length, result.AlignedSequence2.Length);
            int calculatedCost = CalculateHammingDistance(result.AlignedSequence1, result.AlignedSequence2);
            Assert.AreEqual(calculatedCost, result.Cost);
        }

        private int CalculateHammingDistance(string s1, string s2)
        {
            if (s1.Length != s2.Length)
            {
                return -1;
            }

            int distance = 0;
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i] != s2[i])
                {
                    distance++;
                }
            }

            return distance;
        }
    }
}