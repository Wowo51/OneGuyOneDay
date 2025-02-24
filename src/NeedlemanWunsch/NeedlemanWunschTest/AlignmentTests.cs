using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeedlemanWunsch;

namespace NeedlemanWunschTest
{
    [TestClass]
    public class AlignmentTests
    {
        private static string RemoveGaps(string input)
        {
            return input.Replace("-", "");
        }

        [TestMethod]
        public void TestIdenticalSequences()
        {
            string sequence = "AAAA";
            int match = 1;
            int mismatch = -1;
            int gap = -1;
            NeedlemanWunsch.AlignmentResult result = NeedlemanWunschAlgorithm.Align(sequence, sequence, match, mismatch, gap);
            Assert.AreEqual(sequence, RemoveGaps(result.AlignedSequence1));
            Assert.AreEqual(sequence, RemoveGaps(result.AlignedSequence2));
            Assert.AreEqual(sequence.Length * match, result.Score);
        }

        [TestMethod]
        public void TestEmptySequenceFirst()
        {
            // Tests when the second sequence is empty.
            string sequence1 = "ACGT";
            string sequence2 = "";
            int match = 1;
            int mismatch = -1;
            int gap = -1;
            NeedlemanWunsch.AlignmentResult result = NeedlemanWunschAlgorithm.Align(sequence1, sequence2, match, mismatch, gap);
            Assert.AreEqual(sequence1, RemoveGaps(result.AlignedSequence1));
            Assert.AreEqual(sequence2, RemoveGaps(result.AlignedSequence2));
            Assert.AreEqual(sequence1.Length * gap, result.Score);
        }

        [TestMethod]
        public void TestBothEmpty()
        {
            string sequence1 = "";
            string sequence2 = "";
            int match = 1;
            int mismatch = -1;
            int gap = -1;
            NeedlemanWunsch.AlignmentResult result = NeedlemanWunschAlgorithm.Align(sequence1, sequence2, match, mismatch, gap);
            Assert.AreEqual(sequence1, result.AlignedSequence1);
            Assert.AreEqual(sequence2, result.AlignedSequence2);
            Assert.AreEqual(0, result.Score);
        }

        [TestMethod]
        public void TestNullInputs()
        {
            int match = 1;
            int mismatch = -1;
            int gap = -1;
            NeedlemanWunsch.AlignmentResult result = NeedlemanWunschAlgorithm.Align(null, null, match, mismatch, gap);
            Assert.AreEqual(string.Empty, result.AlignedSequence1);
            Assert.AreEqual(string.Empty, result.AlignedSequence2);
            Assert.AreEqual(0, result.Score);
        }

        [TestMethod]
        public void TestMainExample()
        {
            string sequence1 = "GATTACA";
            string sequence2 = "GCATGCU";
            int match = 1;
            int mismatch = -1;
            int gap = -1;
            NeedlemanWunsch.AlignmentResult result = NeedlemanWunschAlgorithm.Align(sequence1, sequence2, match, mismatch, gap);
            Assert.AreEqual(sequence1, RemoveGaps(result.AlignedSequence1));
            Assert.AreEqual(sequence2, RemoveGaps(result.AlignedSequence2));
            Assert.AreEqual(0, result.Score);
            Assert.AreEqual("G-ATTACA", result.AlignedSequence1);
            Assert.AreEqual("GCA-TGCU", result.AlignedSequence2);
        }

        [TestMethod]
        public void TestMismatchSequences()
        {
            string sequence1 = "ACGT";
            string sequence2 = "TGCA";
            int match = 1;
            int mismatch = -1;
            int gap = -1;
            NeedlemanWunsch.AlignmentResult result = NeedlemanWunschAlgorithm.Align(sequence1, sequence2, match, mismatch, gap);
            Assert.AreEqual(sequence1, RemoveGaps(result.AlignedSequence1));
            Assert.AreEqual(sequence2, RemoveGaps(result.AlignedSequence2));
        }

        [TestMethod]
        public void TestSyntheticLongSequences()
        {
            int length1 = 100;
            int length2 = 120;
            string sequence1 = GenerateRandomSequence(length1, 42);
            string sequence2 = GenerateRandomSequence(length2, 43);
            int match = 2;
            int mismatch = -1;
            int gap = -2;
            NeedlemanWunsch.AlignmentResult result = NeedlemanWunschAlgorithm.Align(sequence1, sequence2, match, mismatch, gap);
            Assert.AreEqual(sequence1, RemoveGaps(result.AlignedSequence1));
            Assert.AreEqual(sequence2, RemoveGaps(result.AlignedSequence2));
            Assert.AreEqual(result.AlignedSequence1.Length, result.AlignedSequence2.Length);
        }

        [TestMethod]
        public void TestSingleCharacterSequences()
        {
            string sequence1 = "A";
            string sequence2 = "T";
            int match = 1;
            int mismatch = -1;
            int gap = -1;
            NeedlemanWunsch.AlignmentResult result = NeedlemanWunschAlgorithm.Align(sequence1, sequence2, match, mismatch, gap);
            Assert.AreEqual(sequence1, RemoveGaps(result.AlignedSequence1));
            Assert.AreEqual(sequence2, RemoveGaps(result.AlignedSequence2));
            // Diagonal move yields a mismatch score of -1.
            Assert.AreEqual(-1, result.Score);
            Assert.AreEqual(result.AlignedSequence1.Length, result.AlignedSequence2.Length);
        }

        [TestMethod]
        public void TestEmptySequenceSecond()
        {
            // Tests when the first sequence is empty.
            string sequence1 = "";
            string sequence2 = "ACGT";
            int match = 1;
            int mismatch = -1;
            int gap = -1;
            NeedlemanWunsch.AlignmentResult result = NeedlemanWunschAlgorithm.Align(sequence1, sequence2, match, mismatch, gap);
            Assert.AreEqual(sequence1, RemoveGaps(result.AlignedSequence1));
            Assert.AreEqual(sequence2, RemoveGaps(result.AlignedSequence2));
            Assert.AreEqual(sequence2.Length * gap, result.Score);
            Assert.AreEqual(result.AlignedSequence1.Length, result.AlignedSequence2.Length);
        }

        private string GenerateRandomSequence(int length, int seed)
        {
            StringBuilder sb = new StringBuilder();
            Random random = new Random(seed);
            char[] bases = new char[]
            {
                'A',
                'C',
                'G',
                'T'
            };
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(bases.Length);
                sb.Append(bases[index]);
            }

            return sb.ToString();
        }
    }
}