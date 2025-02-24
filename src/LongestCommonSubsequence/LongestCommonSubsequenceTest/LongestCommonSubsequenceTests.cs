using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LongestCommonSubsequence;

namespace LongestCommonSubsequenceTest
{
    [TestClass]
    public class LongestCommonSubsequenceTests
    {
        [TestMethod]
        public void ComputeLCS_NullInput_ReturnsEmpty()
        {
            IEnumerable<string?> sequences = null;
            string result = LongestCommonSubsequenceSolver.ComputeLCS(sequences);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void ComputeLCS_EmptyInput_ReturnsEmpty()
        {
            IEnumerable<string?> sequences = new List<string?>();
            string result = LongestCommonSubsequenceSolver.ComputeLCS(sequences);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void ComputeLCS_SingleSequence_ReturnsSameSequence()
        {
            string input = "ABCDEF";
            IEnumerable<string?> sequences = new List<string?>
            {
                input
            };
            string result = LongestCommonSubsequenceSolver.ComputeLCS(sequences);
            Assert.AreEqual(input, result);
        }

        [TestMethod]
        public void ComputeLCS_TwoIdenticalSequences_ReturnsSequence()
        {
            string input = "ABCDEF";
            IEnumerable<string?> sequences = new List<string?>
            {
                input,
                input
            };
            string result = LongestCommonSubsequenceSolver.ComputeLCS(sequences);
            Assert.AreEqual(input, result);
        }

        [TestMethod]
        public void ComputeLCS_TwoDifferentSequences_ReturnsCorrectSubsequence()
        {
            IEnumerable<string?> sequences = new List<string?>
            {
                "ABCDEF",
                "AEDFHR"
            };
            string result = LongestCommonSubsequenceSolver.ComputeLCS(sequences);
            Assert.AreEqual("ADF", result);
        }

        [TestMethod]
        public void ComputeLCS_MultipleSequences_ReturnsCorrectSubsequence()
        {
            IEnumerable<string?> sequences = new List<string?>
            {
                "ABCDEF",
                "AEDFHR",
                "ABDF"
            };
            string result = LongestCommonSubsequenceSolver.ComputeLCS(sequences);
            Assert.AreEqual("ADF", result);
        }

        [TestMethod]
        public void ComputeLCS_InputWithNullElements_ReturnsEmpty()
        {
            IEnumerable<string?> sequences = new List<string?>
            {
                "ABC",
                null,
                "AB"
            };
            string result = LongestCommonSubsequenceSolver.ComputeLCS(sequences);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void ComputeLCS_LargeIdenticalSequences_ReturnsSameSequence()
        {
            string input = GenerateRandomString(1000);
            IEnumerable<string?> sequences = new List<string?>
            {
                input,
                input,
                input,
                input,
                input
            };
            string result = LongestCommonSubsequenceSolver.ComputeLCS(sequences);
            Assert.AreEqual(input, result);
        }

        [TestMethod]
        public void ComputeLCS_NoCommonSubsequence_ReturnsEmpty()
        {
            IEnumerable<string?> sequences = new List<string?>
            {
                "ABC",
                "DEF"
            };
            string result = LongestCommonSubsequenceSolver.ComputeLCS(sequences);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void ComputeLCS_OneEmptyString_ReturnsEmpty()
        {
            IEnumerable<string?> sequences = new List<string?>
            {
                "ABC",
                ""
            };
            string result = LongestCommonSubsequenceSolver.ComputeLCS(sequences);
            Assert.AreEqual(string.Empty, result);
        }

        private static string GenerateRandomString(int length)
        {
            StringBuilder builder = new StringBuilder(length);
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random random = new Random(42);
            for (int i = 0; i < length; i++)
            {
                builder.Append(chars[random.Next(chars.Length)]);
            }

            return builder.ToString();
        }
    }
}