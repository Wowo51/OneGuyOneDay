using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using QuickSortDivide;

namespace QuickSortDivideTest
{
    [TestClass]
    public class QuickSorterTests
    {
        [TestMethod]
        public void QuickSort_NullInput_ReturnsEmptyList()
        {
            List<int>? input = null;
            List<int> result = QuickSorter.QuickSort(input);
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(0, result.Count, "Result count should be 0 for null input.");
        }

        [TestMethod]
        public void QuickSort_EmptyList_ReturnsEmptyList()
        {
            List<int> input = new List<int>();
            List<int> result = QuickSorter.QuickSort(input);
            Assert.AreEqual(0, result.Count, "Result should be empty for empty input.");
        }

        [TestMethod]
        public void QuickSort_SingleElement_ReturnsSameList()
        {
            List<int> input = new List<int>
            {
                42
            };
            List<int> result = QuickSorter.QuickSort(input);
            Assert.AreEqual(1, result.Count, "Result should have one element.");
            Assert.AreEqual(42, result[0], "Element should be 42.");
        }

        [TestMethod]
        public void QuickSort_AlreadySorted_ReturnsSameOrder()
        {
            List<int> input = new List<int>
            {
                1,
                2,
                3,
                4,
                5
            };
            List<int> expected = new List<int>
            {
                1,
                2,
                3,
                4,
                5
            };
            List<int> result = QuickSorter.QuickSort(input);
            CollectionAssert.AreEqual(expected, result, "Sorted list should remain in the same order.");
        }

        [TestMethod]
        public void QuickSort_ReverseSorted_SortsCorrectly()
        {
            List<int> input = new List<int>
            {
                5,
                4,
                3,
                2,
                1
            };
            List<int> expected = new List<int>
            {
                1,
                2,
                3,
                4,
                5
            };
            List<int> result = QuickSorter.QuickSort(input);
            CollectionAssert.AreEqual(expected, result, "Reverse sorted list should be sorted in ascending order.");
        }

        [TestMethod]
        public void QuickSort_RandomList_SortsCorrectly()
        {
            int length = 10000;
            List<int> input = new List<int>();
            Random random = new Random(0);
            for (int i = 0; i < length; i++)
            {
                input.Add(random.Next(-100000, 100000));
            }

            List<int> result = QuickSorter.QuickSort(input);
            for (int i = 1; i < result.Count; i++)
            {
                Assert.IsTrue(result[i - 1] <= result[i], "Elements are not in non-decreasing order.");
            }
        }

        [TestMethod]
        public void QuickSort_ListWithDuplicates_SortsCorrectly()
        {
            List<int> input = new List<int>
            {
                3,
                1,
                2,
                3,
                1,
                2
            };
            List<int> expected = new List<int>
            {
                1,
                1,
                2,
                2,
                3,
                3
            };
            List<int> result = QuickSorter.QuickSort(input);
            CollectionAssert.AreEqual(expected, result, "List with duplicates should be sorted correctly.");
        }
    }
}