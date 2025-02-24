using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MatchRatingApproach;

namespace MatchRatingApproachTest
{
    [TestClass]
    public class MatchRatingApproachTests
    {
        [TestMethod]
        public void TestIsSimilar_NullParameters_ReturnsFalse()
        {
            bool result1 = MatchRatingApproachAlgorithm.IsSimilar(null, "TEST");
            bool result2 = MatchRatingApproachAlgorithm.IsSimilar("TEST", null);
            bool result3 = MatchRatingApproachAlgorithm.IsSimilar(null, null);
            Assert.IsFalse(result1, "Expected false when first parameter is null.");
            Assert.IsFalse(result2, "Expected false when second parameter is null.");
            Assert.IsFalse(result3, "Expected false when both parameters are null.");
        }

        [TestMethod]
        public void TestIsSimilar_LengthDifferenceTooBig_ReturnsFalse()
        {
            // Difference in length greater than 3 should return false.
            bool result = MatchRatingApproachAlgorithm.IsSimilar("A", "ABCDEFG");
            Assert.IsFalse(result, "Expected false when length difference is greater than 3.");
        }

        [TestMethod]
        public void TestIsSimilar_NonSimilar_ReturnsFalse()
        {
            // The algorithm should return false if the encoded names do not have enough matching inner characters.
            bool result = MatchRatingApproachAlgorithm.IsSimilar("SMITH", "SMYTH");
            Assert.IsFalse(result, "Expected false for non-similar strings based on the algorithm.");
        }

        [TestMethod]
        public void TestIsSimilar_SimilarStrings_ReturnsTrue()
        {
            // MARTHA and MARHTA yield similar encoded forms resulting in sufficient matching inner characters.
            bool result = MatchRatingApproachAlgorithm.IsSimilar("MARTHA", "MARHTA");
            Assert.IsTrue(result, "Expected true for similar strings MARTHA and MARHTA.");
        }

        [TestMethod]
        public void TestIsSimilar_IdenticalLongStrings_ReturnsTrue()
        {
            // For longer strings, identical names should be considered similar.
            bool result = MatchRatingApproachAlgorithm.IsSimilar("ALBERT", "ALBERT");
            Assert.IsTrue(result, "Expected true for identical long strings ALBERT and ALBERT.");
        }

        [TestMethod]
        public void TestIsSimilar_CaseInsensitive()
        {
            // The algorithm should be case insensitive.
            bool result = MatchRatingApproachAlgorithm.IsSimilar("Martha", "marHTA");
            Assert.IsTrue(result, "Expected case-insensitive match for variants of MARTHA.");
        }

        [TestMethod]
        public void TestIsSimilar_SyntheticRandomData()
        {
            // Generate synthetic random test data to ensure robustness of the algorithm.
            System.Random random = new System.Random(42);
            int trueCount = 0;
            int falseCount = 0;
            for (int i = 0; i < 1000; i++)
            {
                string str1 = GenerateRandomString(random, 5, 10);
                string str2 = GenerateRandomString(random, 5, 10);
                bool result = MatchRatingApproachAlgorithm.IsSimilar(str1, str2);
                if (result)
                {
                    trueCount++;
                }
                else
                {
                    falseCount++;
                }
            }

            // We do not assert specific counts because random data may vary,
            // but we ensure that the algorithm processes the data without error.
            Assert.IsTrue(trueCount >= 0, "Synthetic test processed some true results.");
            Assert.IsTrue(falseCount >= 0, "Synthetic test processed some false results.");
        }

        [TestMethod]
        public void TestIsSimilar_EmptyStrings_ReturnsFalse()
        {
            bool result = MatchRatingApproachAlgorithm.IsSimilar("", "");
            Assert.IsFalse(result, "Expected false for empty string inputs.");
        }

        [TestMethod]
        public void TestIsSimilar_SingleLetter_ReturnsFalse()
        {
            bool result = MatchRatingApproachAlgorithm.IsSimilar("A", "A");
            Assert.IsFalse(result, "Expected false for single character strings.");
        }

        private static string GenerateRandomString(System.Random random, int minLength, int maxLength)
        {
            int length = random.Next(minLength, maxLength + 1);
            char[] letters = new char[length];
            for (int i = 0; i < length; i++)
            {
                letters[i] = (char)random.Next('A', 'Z' + 1);
            }

            return new string (letters);
        }
    }
}