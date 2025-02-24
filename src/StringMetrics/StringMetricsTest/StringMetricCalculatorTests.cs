using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringMetrics;
using System;

namespace StringMetricsTest
{
    [TestClass]
    public class StringMetricCalculatorTests
    {
        [TestMethod]
        public void Test_ComputeLevenshteinDistance_BothEmpty()
        {
            int distance = StringMetricCalculator.ComputeLevenshteinDistance("", "");
            Assert.AreEqual(0, distance, "Distance between two empty strings should be 0.");
        }

        [TestMethod]
        public void Test_ComputeLevenshteinDistance_FirstNull()
        {
            int distance = StringMetricCalculator.ComputeLevenshteinDistance(null, "abc");
            Assert.AreEqual(3, distance, "Distance when first string is null should be length of second string.");
        }

        [TestMethod]
        public void Test_ComputeLevenshteinDistance_SecondNull()
        {
            int distance = StringMetricCalculator.ComputeLevenshteinDistance("abc", null);
            Assert.AreEqual(3, distance, "Distance when second string is null should be length of first string.");
        }

        [TestMethod]
        public void Test_ComputeLevenshteinDistance_SimpleCase()
        {
            int distance = StringMetricCalculator.ComputeLevenshteinDistance("kitten", "sitting");
            Assert.AreEqual(3, distance, "Levenshtein distance between 'kitten' and 'sitting' is 3.");
        }

        [TestMethod]
        public void Test_ComputeNormalizedSimilarity_Identical()
        {
            double similarity = StringMetricCalculator.ComputeNormalizedSimilarity("example", "example");
            Assert.AreEqual(1.0, similarity, "Normalized similarity of identical strings should be 1.0.");
        }

        [TestMethod]
        public void Test_ComputeNormalizedSimilarity_BothNull()
        {
            double similarity = StringMetricCalculator.ComputeNormalizedSimilarity(null, null);
            Assert.AreEqual(1.0, similarity, "Normalized similarity when both strings are null should be 1.0.");
        }

        [TestMethod]
        public void Test_ComputeNormalizedSimilarity_Different()
        {
            double similarity = StringMetricCalculator.ComputeNormalizedSimilarity("abc", "def");
            Assert.AreEqual(0.0, similarity, "Normalized similarity between 'abc' and 'def' should be 0.0.");
        }

        [TestMethod]
        public void Test_ComputeNormalizedSimilarity_OneEmpty()
        {
            double similarity1 = StringMetricCalculator.ComputeNormalizedSimilarity("", "abc");
            Assert.AreEqual(0.0, similarity1, "Normalized similarity when first string is empty and second is non-empty should be 0.0.");
            double similarity2 = StringMetricCalculator.ComputeNormalizedSimilarity("abc", "");
            Assert.AreEqual(0.0, similarity2, "Normalized similarity when second string is empty and first is non-empty should be 0.0.");
        }

        [TestMethod]
        public void Test_RandomStringSimilarityRange()
        {
            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                int length1 = random.Next(0, 50);
                int length2 = random.Next(0, 50);
                string s1 = GenerateRandomString(length1, random);
                string s2 = GenerateRandomString(length2, random);
                double similarity = StringMetricCalculator.ComputeNormalizedSimilarity(s1, s2);
                Assert.IsTrue(similarity >= 0.0 && similarity <= 1.0, "Normalized similarity should be in range [0,1].");
            }
        }

        private static string GenerateRandomString(int length, Random random)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] buffer = new char[length];
            for (int i = 0; i < length; i++)
            {
                buffer[i] = chars[random.Next(chars.Length)];
            }

            return new string (buffer);
        }
    }
}