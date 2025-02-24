using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BinaryTreeSort;

namespace BinaryTreeSortTest
{
    [TestClass]
    public class BinaryTreeSortTests
    {
        [TestMethod]
        public void TestEmptyList()
        {
            List<int> unsorted = new List<int>();
            List<int> sorted = TreeSorter.Sort(unsorted);
            Assert.AreEqual(0, sorted.Count);
        }

        [TestMethod]
        public void TestSingleElement()
        {
            List<int> unsorted = new List<int>
            {
                42
            };
            List<int> sorted = TreeSorter.Sort(unsorted);
            Assert.AreEqual(1, sorted.Count);
            Assert.AreEqual(42, sorted[0]);
        }

        [TestMethod]
        public void TestSortedOrder()
        {
            List<int> unsorted = new List<int>
            {
                5,
                3,
                8,
                1,
                9,
                2
            };
            List<int> sorted = TreeSorter.Sort(unsorted);
            List<int> expected = new List<int>
            {
                1,
                2,
                3,
                5,
                8,
                9
            };
            CollectionAssert.AreEqual(expected, sorted);
        }

        [TestMethod]
        public void TestDuplicateElements()
        {
            List<int> unsorted = new List<int>
            {
                5,
                3,
                5,
                3,
                5
            };
            List<int> sorted = TreeSorter.Sort(unsorted);
            List<int> expected = new List<int>
            {
                3,
                3,
                5,
                5,
                5
            };
            CollectionAssert.AreEqual(expected, sorted);
        }

        [TestMethod]
        public void TestLargeDataset()
        {
            const int count = 1000;
            List<int> unsorted = new List<int>(count);
            Random random = new Random(12345);
            for (int i = 0; i < count; i++)
            {
                unsorted.Add(random.Next(0, 10000));
            }

            List<int> sorted = TreeSorter.Sort(unsorted);
            for (int i = 1; i < sorted.Count; i++)
            {
                Assert.IsTrue(sorted[i - 1] <= sorted[i]);
            }
        }

        [TestMethod]
        public void TestStringSorting()
        {
            List<string> unsorted = new List<string>
            {
                "banana",
                "apple",
                "cherry"
            };
            List<string> sorted = TreeSorter.Sort(unsorted);
            List<string> expected = new List<string>
            {
                "apple",
                "banana",
                "cherry"
            };
            CollectionAssert.AreEqual(expected, sorted);
        }
    }
}