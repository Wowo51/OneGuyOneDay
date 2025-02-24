using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KraussWildcardAlgorithm;

namespace KraussWildcardAlgorithmTest
{
    [TestClass]
    public class WildcardMatcherTests
    {
        [TestMethod]
        public void TestExactMatch()
        {
            string text = "hello";
            string pattern = "hello";
            bool result = WildcardMatcher.IsMatch(text, pattern);
            Assert.IsTrue(result, "Exact match should succeed.");
        }

        [TestMethod]
        public void TestSimpleMismatch()
        {
            string text = "hello";
            string pattern = "world";
            bool result = WildcardMatcher.IsMatch(text, pattern);
            Assert.IsFalse(result, "Mismatch should fail.");
        }

        [TestMethod]
        public void TestQuestionMarkWildcard()
        {
            string text = "hello";
            string pattern = "h?llo";
            bool result = WildcardMatcher.IsMatch(text, pattern);
            Assert.IsTrue(result, "Pattern with '?' should match a single character.");
        }

        [TestMethod]
        public void TestAsteriskWildcard()
        {
            string text = "hello";
            string pattern = "he*o";
            bool result = WildcardMatcher.IsMatch(text, pattern);
            Assert.IsTrue(result, "Pattern with '*' matching middle characters should succeed.");
        }

        [TestMethod]
        public void TestMixedWildcardsSuccess()
        {
            string text = "abcdef";
            string pattern = "a*e?";
            bool result = WildcardMatcher.IsMatch(text, pattern);
            Assert.IsTrue(result, "Pattern with mixed wildcards should match.");
        }

        [TestMethod]
        public void TestMixedWildcardsFailure()
        {
            string text = "abcdef";
            string pattern = "a*d?x";
            bool result = WildcardMatcher.IsMatch(text, pattern);
            Assert.IsFalse(result, "Pattern with mixed wildcards should fail.");
        }

        [TestMethod]
        public void TestNullParameters()
        {
            string? text = null;
            string? pattern = "a";
            bool result = WildcardMatcher.IsMatch(text, pattern);
            Assert.IsFalse(result, "Null text should return false.");
            text = "a";
            pattern = null;
            result = WildcardMatcher.IsMatch(text, pattern);
            Assert.IsFalse(result, "Null pattern should return false.");
            text = null;
            pattern = null;
            result = WildcardMatcher.IsMatch(text, pattern);
            Assert.IsFalse(result, "Both parameters null should return false.");
        }

        [TestMethod]
        public void TestEmptyStrings()
        {
            string text = "";
            string pattern = "";
            bool result = WildcardMatcher.IsMatch(text, pattern);
            Assert.IsTrue(result, "Empty text and empty pattern should match.");
            text = "";
            pattern = "*";
            result = WildcardMatcher.IsMatch(text, pattern);
            Assert.IsTrue(result, "Empty text should match '*' pattern.");
            text = "";
            pattern = "?";
            result = WildcardMatcher.IsMatch(text, pattern);
            Assert.IsFalse(result, "Empty text should not match '?' pattern.");
        }

        [TestMethod]
        public void TestOnlyAsterisk()
        {
            string text = "anything";
            string pattern = "*";
            bool result = WildcardMatcher.IsMatch(text, pattern);
            Assert.IsTrue(result, "Only '*' should match any text.");
        }

        [TestMethod]
        public void TestLeadingAndTrailingAsterisk()
        {
            string text = "wildcard";
            string pattern = "*card";
            bool result = WildcardMatcher.IsMatch(text, pattern);
            Assert.IsTrue(result, "Pattern starting with '*' should match ending substring.");
        }

        [TestMethod]
        public void TestLargeTextExactMatch()
        {
            int length = 3000;
            string text = new string ('a', length);
            string pattern = new string ('a', length);
            bool result = WildcardMatcher.IsMatch(text, pattern);
            Assert.IsTrue(result, "Large text exact match should succeed.");
        }

        [TestMethod]
        public void TestLargeTextWildcard()
        {
            int length = 3000;
            string text = new string ('a', length);
            string pattern = "a*a";
            bool result = WildcardMatcher.IsMatch(text, pattern);
            Assert.IsTrue(result, "Large text wildcard match should succeed.");
        }
    }
}