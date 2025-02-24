using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PigeonholeSort;

namespace PigeonholeSortTest
{
    [TestClass]
    public class PigeonholeSorterTests
    {
        [TestMethod]
        public void TestNullArray()
        {
            int[]? array = null;
            PigeonholeSorter.Sort(array);
            Assert.IsNull(array);
        }

        [TestMethod]
        public void TestEmptyArray()
        {
            int[] array = new int[0];
            PigeonholeSorter.Sort(array);
            Assert.AreEqual(0, array.Length);
        }

        [TestMethod]
        public void TestSingleElement()
        {
            int[] array = new int[]
            {
                42
            };
            int[] expected = new int[]
            {
                42
            };
            PigeonholeSorter.Sort(array);
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
            PigeonholeSorter.Sort(array);
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
            PigeonholeSorter.Sort(array);
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
                2,
                1
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
            PigeonholeSorter.Sort(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void TestArrayWithNegativeNumbers()
        {
            int[] array = new int[]
            {
                -5,
                3,
                0,
                -2,
                1
            };
            int[] expected = new int[]
            {
                -5,
                -2,
                0,
                1,
                3
            };
            PigeonholeSorter.Sort(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void TestLargeRandomArray()
        {
            const int size = 10000;
            int[] array = new int[size];
            int seed = 42;
            System.Random random = new System.Random(seed);
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(-10000, 10000);
            }

            PigeonholeSorter.Sort(array);
            for (int i = 0; i < size - 1; i++)
            {
                Assert.IsTrue(array[i] <= array[i + 1], "Array is not sorted at index " + i);
            }
        }
    }
}