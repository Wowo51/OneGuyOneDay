using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using UnicodeCollationAlgorithm;

namespace UnicodeCollationAlgorithmTest
{
    [TestClass]
    public class UnicodeCollationAlgorithmTests
    {
        [TestMethod]
        public void Compare_NullStrings_ReturnsZero()
        {
            int result = UnicodeCollation.Compare(null, null);
            Assert.AreEqual(0, result, "Comparing two null strings should return 0.");
        }

        [TestMethod]
        public void Compare_NullAndEmpty_ReturnsZero()
        {
            int result1 = UnicodeCollation.Compare(null, "");
            int result2 = UnicodeCollation.Compare("", null);
            Assert.AreEqual(0, result1, "Comparing null and empty should return 0.");
            Assert.AreEqual(0, result2, "Comparing empty and null should return 0.");
        }

        [TestMethod]
        public void Compare_IdenticalStrings_ReturnsZero()
        {
            int result = UnicodeCollation.Compare("test", "test");
            Assert.AreEqual(0, result, "Identical strings should compare equal.");
        }

        [TestMethod]
        public void Compare_IgnoresCase_ReturnsZero()
        {
            int result = UnicodeCollation.Compare("Test", "test");
            Assert.AreEqual(0, result, "Comparison should ignore case differences.");
        }

        [TestMethod]
        public void Compare_IgnoresDiacritics_ReturnsZero()
        {
            string accented = "a\u0300";
            int result = UnicodeCollation.Compare(accented, "a");
            Assert.AreEqual(0, result, "Comparison should ignore non-spacing marks (diacritics).");
        }

        [TestMethod]
        public void Compare_DifferentStrings_ReturnsNonZero()
        {
            int result = UnicodeCollation.Compare("apple", "banana");
            Assert.IsTrue(result < 0, "Expected 'apple' to be less than 'banana'.");
            int reverseResult = UnicodeCollation.Compare("banana", "apple");
            Assert.IsTrue(reverseResult > 0, "Expected 'banana' to be greater than 'apple'.");
        }

        [TestMethod]
        public void Compare_SyntheticRandomStrings_ReturnsSelfComparisonZero()
        {
            Random randomGenerator = new Random(42);
            for (int i = 0; i < 100; i++)
            {
                string randomString = GenerateRandomString(randomGenerator, 10);
                int result = UnicodeCollation.Compare(randomString, randomString);
                Assert.AreEqual(0, result, "A string should compare equal to itself.");
            }
        }

        [TestMethod]
        public void Sort_RandomStrings_ProducesSortedList()
        {
            Random randomGenerator = new Random(101);
            List<string> randomStrings = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                string randomString = GenerateRandomString(randomGenerator, 15);
                randomStrings.Add(randomString);
            }

            randomStrings.Sort((string a, string b) => UnicodeCollation.Compare(a, b));
            for (int i = 1; i < randomStrings.Count; i++)
            {
                int cmp = UnicodeCollation.Compare(randomStrings[i - 1], randomStrings[i]);
                Assert.IsTrue(cmp <= 0, "List is not sorted in non-decreasing order.");
            }
        }

        private static string GenerateRandomString(Random randomGenerator, int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] resultChars = new char[length];
            for (int i = 0; i < length; i++)
            {
                int index = randomGenerator.Next(chars.Length);
                resultChars[i] = chars[index];
            }

            return new string (resultChars);
        }
    }
}