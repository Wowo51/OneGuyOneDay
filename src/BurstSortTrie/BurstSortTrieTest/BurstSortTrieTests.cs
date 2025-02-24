using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BurstSortTrie;

namespace BurstSortTrieTest
{
    [TestClass]
    public class BurstSortTrieTests
    {
        [TestMethod]
        public void Test_NullInput_ReturnsEmptyList()
        {
            List<string?>? input = null;
            List<string> result = BurstSorter.Sort(input);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Test_EmptyInput_ReturnsEmptyList()
        {
            List<string?> input = new List<string?>();
            List<string> result = BurstSorter.Sort(input);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Test_SingleWord_ReturnsSameWord()
        {
            List<string?> input = new List<string?>
            {
                "apple"
            };
            List<string> result = BurstSorter.Sort(input);
            List<string> expected = input.ConvertAll(x => x ?? "");
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_MultipleWords_SortedOrder()
        {
            List<string?> input = new List<string?>
            {
                "banana",
                "apple",
                "cherry"
            };
            List<string> result = BurstSorter.Sort(input);
            List<string> expected = input.ConvertAll(x => x ?? "");
            expected.Sort(StringComparer.Ordinal);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_DuplicateWords()
        {
            List<string?> input = new List<string?>
            {
                "apple",
                "apple",
                "banana",
                "banana",
                "apple"
            };
            List<string> result = BurstSorter.Sort(input);
            List<string> expected = input.ConvertAll(x => x ?? "");
            expected.Sort(StringComparer.Ordinal);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_NullElements_TreatedAsEmpty()
        {
            List<string?> input = new List<string?>
            {
                null,
                "apple",
                null,
                "banana"
            };
            List<string> result = BurstSorter.Sort(input);
            List<string> expected = input.ConvertAll(x => x ?? "");
            expected.Sort(StringComparer.Ordinal);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_LargeRandomDataset()
        {
            Random random = new Random(42);
            List<string?> input = new List<string?>();
            int count = 1000;
            for (int i = 0; i < count; i++)
            {
                int wordLength = random.Next(1, 20);
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < wordLength; j++)
                {
                    char letter = (char)('a' + random.Next(0, 26));
                    sb.Append(letter);
                }

                input.Add(sb.ToString());
            }

            List<string> result = BurstSorter.Sort(input);
            List<string> expected = input.ConvertAll(x => x ?? "");
            expected.Sort(StringComparer.Ordinal);
            CollectionAssert.AreEqual(expected, result);
        }
    }
}