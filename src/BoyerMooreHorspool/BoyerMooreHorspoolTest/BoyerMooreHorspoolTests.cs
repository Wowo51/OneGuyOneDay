using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BoyerMooreHorspool;

namespace BoyerMooreHorspoolTest
{
    [TestClass]
    public class BoyerMooreHorspoolTests
    {
        [TestMethod]
        public void Test_NullText_ReturnsMinusOne()
        {
            string? text = null;
            string pattern = "pattern";
            int result = BoyerMooreHorspoolAlgorithm.Search(text, pattern);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Test_NullPattern_ReturnsMinusOne()
        {
            string text = "some text";
            string? pattern = null;
            int result = BoyerMooreHorspoolAlgorithm.Search(text, pattern);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Test_EmptyPattern_ReturnsZero()
        {
            string text = "any text";
            string pattern = "";
            int result = BoyerMooreHorspoolAlgorithm.Search(text, pattern);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Test_PatternLongerThanText_ReturnsMinusOne()
        {
            string text = "abc";
            string pattern = "abcd";
            int result = BoyerMooreHorspoolAlgorithm.Search(text, pattern);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Test_PatternNotFound_ReturnsMinusOne()
        {
            string text = "abcdef";
            string pattern = "gh";
            int result = BoyerMooreHorspoolAlgorithm.Search(text, pattern);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Test_SingleOccurrence_ReturnsCorrectIndex()
        {
            string text = "hello world";
            string pattern = "world";
            int result = BoyerMooreHorspoolAlgorithm.Search(text, pattern);
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void Test_MultipleOccurrences_ReturnsFirstOccurrence()
        {
            string text = "abcabcabc";
            string pattern = "abc";
            int result = BoyerMooreHorspoolAlgorithm.Search(text, pattern);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Test_SyntheticLargeTextWithKnownPattern()
        {
            int length = 1000;
            char[] allowedChars = new char[]
            {
                'A',
                'B',
                'C',
                'D',
                'E',
                'F'
            };
            StringBuilder sb = new StringBuilder();
            Random randomGenerator = new Random(42);
            for (int i = 0; i < length; i++)
            {
                int index = randomGenerator.Next(0, allowedChars.Length);
                sb.Append(allowedChars[index]);
            }

            string text = sb.ToString();
            string pattern = "QWERTY";
            int insertIndex = 500;
            string modifiedText = text.Substring(0, insertIndex) + pattern + text.Substring(insertIndex);
            int result = BoyerMooreHorspoolAlgorithm.Search(modifiedText, pattern);
            Assert.AreEqual(insertIndex, result);
        }
    }
}