using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrigramSearch;

namespace TrigramSearchTest
{
    [TestClass]
    public class TrigramHelperTests
    {
        [TestMethod]
        public void Test_GetTrigrams_Null_ReturnsEmpty()
        {
            HashSet<string> result = TrigramHelper.GetTrigrams(null);
            Assert.AreEqual(0, result.Count, "Null input should return an empty set.");
        }

        [TestMethod]
        public void Test_GetTrigrams_EmptyString_ReturnsEmpty()
        {
            HashSet<string> result = TrigramHelper.GetTrigrams("");
            Assert.AreEqual(0, result.Count, "Empty string should return an empty set.");
        }

        [TestMethod]
        public void Test_GetTrigrams_SingleCharacter_ReturnsThatCharacter()
        {
            HashSet<string> result = TrigramHelper.GetTrigrams("a");
            Assert.AreEqual(1, result.Count, "Single character should return one element.");
            Assert.IsTrue(result.Contains("a"));
        }

        [TestMethod]
        public void Test_GetTrigrams_ExactThreeCharacters_ReturnsWholeString()
        {
            HashSet<string> result = TrigramHelper.GetTrigrams("abc");
            Assert.AreEqual(1, result.Count, "Three characters should return the string itself as one trigram.");
            Assert.IsTrue(result.Contains("abc"));
        }

        [TestMethod]
        public void Test_GetTrigrams_MultipleCharacters_ReturnsTrigrams()
        {
            HashSet<string> result = TrigramHelper.GetTrigrams("abcd");
            Assert.AreEqual(2, result.Count, "Four characters should return two trigrams.");
            Assert.IsTrue(result.Contains("abc"));
            Assert.IsTrue(result.Contains("bcd"));
        }

        [TestMethod]
        public void Test_GetTrigrams_TwoCharacters_ReturnsTwoCharacter()
        {
            HashSet<string> result = TrigramHelper.GetTrigrams("ab");
            Assert.AreEqual(1, result.Count, "Two characters should return one element.");
            Assert.IsTrue(result.Contains("ab"));
        }

        [TestMethod]
        public void Test_GetTrigrams_SpacesOnly_ReturnsEmpty()
        {
            HashSet<string> result = TrigramHelper.GetTrigrams("   ");
            Assert.AreEqual(0, result.Count, "String with only spaces should return an empty set after trimming.");
        }
    }
}