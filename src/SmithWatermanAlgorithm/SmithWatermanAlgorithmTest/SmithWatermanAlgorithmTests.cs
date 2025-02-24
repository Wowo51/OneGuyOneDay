using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using SmithWatermanAlgorithm;

namespace SmithWatermanAlgorithmTest
{
    [TestClass]
    public class SmithWatermanAlgorithmTests
    {
        [TestMethod]
        public void TestEmptySequences()
        {
            SmithWaterman sw = new SmithWaterman();
            AlignmentResult result = sw.Align("", "", 2, -1, 2);
            Assert.AreEqual(0, result.Score);
            Assert.AreEqual("", result.AlignedSequence1);
            Assert.AreEqual("", result.AlignedSequence2);
        }

        [TestMethod]
        public void TestNullSequences()
        {
            SmithWaterman sw = new SmithWaterman();
            AlignmentResult result = sw.Align(null, null, 2, -1, 2);
            Assert.AreEqual(0, result.Score);
            Assert.AreEqual("", result.AlignedSequence1);
            Assert.AreEqual("", result.AlignedSequence2);
        }

        [TestMethod]
        public void TestIdenticalSequences()
        {
            string sequence = "ACGT";
            SmithWaterman sw = new SmithWaterman();
            AlignmentResult result = sw.Align(sequence, sequence, 2, -1, 2);
            Assert.AreEqual(sequence, result.AlignedSequence1);
            Assert.AreEqual(sequence, result.AlignedSequence2);
            Assert.AreEqual(8, result.Score);
        }

        [TestMethod]
        public void TestMismatchAlignment()
        {
            string sequence1 = "ACGT";
            string sequence2 = "AGGT";
            SmithWaterman sw = new SmithWaterman();
            AlignmentResult result = sw.Align(sequence1, sequence2, 2, -1, 2);
            Assert.AreEqual("ACGT", result.AlignedSequence1);
            Assert.AreEqual("AGGT", result.AlignedSequence2);
            Assert.AreEqual(5, result.Score);
        }

        [TestMethod]
        public void TestLargeRandomSequences()
        {
            Random random = new Random(42);
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            string allowedChars = "ACGT";
            for (int i = 0; i < 1000; i++)
            {
                int index1 = random.Next(0, allowedChars.Length);
                int index2 = random.Next(0, allowedChars.Length);
                sb1.Append(allowedChars[index1]);
                sb2.Append(allowedChars[index2]);
            }

            SmithWaterman sw = new SmithWaterman();
            AlignmentResult result = sw.Align(sb1.ToString(), sb2.ToString(), 2, -1, 2);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Score >= 0);
        }

        [TestMethod]
        public void TestOneEmptySequence()
        {
            SmithWaterman sw = new SmithWaterman();
            // First sequence non-empty, second empty.
            AlignmentResult result1 = sw.Align("ACGT", "", 2, -1, 2);
            Assert.AreEqual(0, result1.Score);
            Assert.AreEqual("", result1.AlignedSequence1);
            Assert.AreEqual("", result1.AlignedSequence2);
            // First sequence empty, second non-empty.
            AlignmentResult result2 = sw.Align("", "ACGT", 2, -1, 2);
            Assert.AreEqual(0, result2.Score);
            Assert.AreEqual("", result2.AlignedSequence1);
            Assert.AreEqual("", result2.AlignedSequence2);
        }

        [TestMethod]
        public void TestPartialAlignment()
        {
            string sequence1 = "NNACGTNN";
            string sequence2 = "XXACGTYY";
            SmithWaterman sw = new SmithWaterman();
            AlignmentResult result = sw.Align(sequence1, sequence2, 2, -1, 2);
            Assert.AreEqual(8, result.Score);
            Assert.AreEqual("ACGT", result.AlignedSequence1);
            Assert.AreEqual("ACGT", result.AlignedSequence2);
        }

        [TestMethod]
        public void TestNoCommonSubstring()
        {
            string sequence1 = "AAAA";
            string sequence2 = "TTTT";
            SmithWaterman sw = new SmithWaterman();
            AlignmentResult result = sw.Align(sequence1, sequence2, 2, -1, 2);
            Assert.AreEqual(0, result.Score);
            Assert.AreEqual("", result.AlignedSequence1);
            Assert.AreEqual("", result.AlignedSequence2);
        }
    }
}