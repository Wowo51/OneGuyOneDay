using Microsoft.VisualStudio.TestTools.UnitTesting;
using CombSort;
using System;

namespace CombSortTest
{
    [TestClass]
    public class CombSorterTests
    {
        [TestMethod]
        public void TestEmptyArray()
        {
            int[] array = new int[0];
            CombSorter.Sort(array);
            CollectionAssert.AreEqual(new int[0], array);
        }

        [TestMethod]
        public void TestSingleElementArray()
        {
            int[] array = new int[]
            {
                42
            };
            int[] expected = new int[]
            {
                42
            };
            CombSorter.Sort(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void TestAlreadySortedArray()
        {
            int[] array = new int[]
            {
                1,
                2,
                3,
                4,
                5
            };
            int[] expected = new int[]
            {
                1,
                2,
                3,
                4,
                5
            };
            CombSorter.Sort(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void TestReverseSortedArray()
        {
            int[] array = new int[]
            {
                5,
                4,
                3,
                2,
                1
            };
            int[] expected = new int[]
            {
                1,
                2,
                3,
                4,
                5
            };
            CombSorter.Sort(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void TestUnsortedArray()
        {
            int[] array = new int[]
            {
                64,
                34,
                25,
                12,
                22,
                11,
                90
            };
            int[] expected = new int[]
            {
                11,
                12,
                22,
                25,
                34,
                64,
                90
            };
            CombSorter.Sort(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void TestArrayWithDuplicates()
        {
            int[] array = new int[]
            {
                3,
                1,
                2,
                3,
                1,
                2
            };
            int[] expected = new int[]
            {
                1,
                1,
                2,
                2,
                3,
                3
            };
            CombSorter.Sort(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void TestLargeRandomArray()
        {
            int testLength = 10000;
            int[] array = new int[testLength];
            int[] expected = new int[testLength];
            Random random = new Random(0);
            for (int i = 0; i < testLength; i++)
            {
                int value = random.Next();
                array[i] = value;
                expected[i] = value;
            }

            Array.Sort(expected);
            CombSorter.Sort(array);
            CollectionAssert.AreEqual(expected, array);
        }
    }
}