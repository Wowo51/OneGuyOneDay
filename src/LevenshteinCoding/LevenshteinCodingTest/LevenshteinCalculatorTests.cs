using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LevenshteinCoding;

namespace LevenshteinCodingTest
{
    [TestClass]
    public class LevenshteinCalculatorTests
    {
        [TestMethod]
        public void TestNullInputs()
        {
            int distance = LevenshteinCalculator.ComputeDistance(null, null);
            Assert.AreEqual(0, distance, "Distance between null strings should be 0.");
        }

        [TestMethod]
        public void TestEmptyStrings()
        {
            int distance = LevenshteinCalculator.ComputeDistance("", "");
            Assert.AreEqual(0, distance, "Distance between empty strings should be 0.");
        }

        [TestMethod]
        public void TestOneEmptyString()
        {
            int distance1 = LevenshteinCalculator.ComputeDistance("abc", "");
            Assert.AreEqual(3, distance1, "Distance should equal the length of the non-empty string.");
            int distance2 = LevenshteinCalculator.ComputeDistance("", "abc");
            Assert.AreEqual(3, distance2, "Distance should equal the length of the non-empty string.");
        }

        [TestMethod]
        public void TestSimpleCaseKittenSitting()
        {
            int distance = LevenshteinCalculator.ComputeDistance("kitten", "sitting");
            Assert.AreEqual(3, distance, "Distance between 'kitten' and 'sitting' should be 3.");
        }

        [TestMethod]
        public void TestSameStrings()
        {
            int distance = LevenshteinCalculator.ComputeDistance("abc", "abc");
            Assert.AreEqual(0, distance, "Distance between identical strings should be 0.");
        }

        [TestMethod]
        public void TestComplexCase()
        {
            int distance = LevenshteinCalculator.ComputeDistance("abcdef", "azced");
            Assert.AreEqual(3, distance, "Distance between 'abcdef' and 'azced' should be 3.");
        }

        [TestMethod]
        public void TestRandomLargeStringSameInput()
        {
            string randomString = GenerateRandomString(1000);
            int distance = LevenshteinCalculator.ComputeDistance(randomString, randomString);
            Assert.AreEqual(0, distance, "Distance between identical large random strings should be 0.");
        }

        private static string GenerateRandomString(int length)
        {
            char[] chars = new char[length];
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                chars[i] = (char)random.Next(32, 127);
            }

            return new string (chars);
        }
    }
}