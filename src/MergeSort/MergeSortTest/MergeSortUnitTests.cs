using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MergeSort;

namespace MergeSortTest
{
    [TestClass]
    public class MergeSortUnitTests
    {
        [TestMethod]
        public void TestSort_NullInput_ReturnsEmptyList()
        {
            List<int>? input = null;
            List<int> result = MergeSorter.Sort(input);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestSort_EmptyList_ReturnsEmptyList()
        {
            List<int> input = new List<int>();
            List<int> result = MergeSorter.Sort(input);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestSort_SingleElement_ReturnsSameElement()
        {
            List<int> input = new List<int>()
            {
                42
            };
            List<int> result = MergeSorter.Sort(input);
            CollectionAssert.AreEqual(input, result);
        }

        [TestMethod]
        public void TestSort_MultipleElements_SortsCorrectly()
        {
            List<int> input = new List<int>()
            {
                5,
                3,
                1,
                4,
                2
            };
            List<int> expected = new List<int>()
            {
                1,
                2,
                3,
                4,
                5
            };
            List<int> result = MergeSorter.Sort(input);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestSort_WithDuplicates_SortsCorrectly()
        {
            List<int> input = new List<int>()
            {
                2,
                1,
                2,
                1
            };
            List<int> expected = new List<int>()
            {
                1,
                1,
                2,
                2
            };
            List<int> result = MergeSorter.Sort(input);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestSort_RandomLargeList_SortsCorrectly()
        {
            List<int> input = new List<int>();
            List<int> expected = new List<int>();
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                int value = random.Next(0, 10000);
                input.Add(value);
                expected.Add(value);
            }

            expected.Sort();
            List<int> result = MergeSorter.Sort(input);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestMerge_TwoSortedLists_MergedCorrectly()
        {
            List<int> left = new List<int>()
            {
                1,
                3,
                5
            };
            List<int> right = new List<int>()
            {
                2,
                4,
                6
            };
            List<int> expected = new List<int>()
            {
                1,
                2,
                3,
                4,
                5,
                6
            };
            List<int> result = MergeSorter.Merge(left, right);
            CollectionAssert.AreEqual(expected, result);
        }
    }
}