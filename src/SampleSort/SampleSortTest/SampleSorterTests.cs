using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using SampleSort;

namespace SampleSortTest
{
    [TestClass]
    public class SampleSorterTests
    {
        [TestMethod]
        public void TestNullCollection()
        {
            List<int> result = SampleSorter.Sort<int>(null, Comparer<int>.Default);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestEmptyList()
        {
            List<int> empty = new List<int>();
            List<int> result = SampleSorter.Sort(empty, Comparer<int>.Default);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestSingleElement()
        {
            List<int> single = new List<int>
            {
                42
            };
            List<int> result = SampleSorter.Sort(single, Comparer<int>.Default);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(42, result[0]);
        }

        [TestMethod]
        public void TestSortedList()
        {
            List<int> sortedInput = new List<int>
            {
                1,
                2,
                3,
                4,
                5
            };
            List<int> result = SampleSorter.Sort(sortedInput, Comparer<int>.Default);
            CollectionAssert.AreEqual(sortedInput, result);
        }

        [TestMethod]
        public void TestReverseList()
        {
            List<int> reverse = new List<int>
            {
                5,
                4,
                3,
                2,
                1
            };
            List<int> result = SampleSorter.Sort(reverse, Comparer<int>.Default);
            CollectionAssert.AreEqual(new List<int> { 1, 2, 3, 4, 5 }, result);
        }

        [TestMethod]
        public void TestDuplicates()
        {
            List<int> duplicates = new List<int>
            {
                3,
                1,
                2,
                1,
                3
            };
            List<int> result = SampleSorter.Sort(duplicates, Comparer<int>.Default);
            CollectionAssert.AreEqual(new List<int> { 1, 1, 2, 3, 3 }, result);
        }

        [TestMethod]
        public void TestLargeRandomList()
        {
            const int size = 1000;
            List<int> randomList = new List<int>(size);
            Random random = new Random(0);
            for (int i = 0; i < size; i++)
            {
                randomList.Add(random.Next(0, 10000));
            }

            List<int> result = SampleSorter.Sort(randomList, Comparer<int>.Default);
            for (int i = 1; i < result.Count; i++)
            {
                Assert.IsTrue(result[i - 1] <= result[i]);
            }
        }
    }
}