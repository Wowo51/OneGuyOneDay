using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlowSort;

namespace SlowSortTest
{
    [TestClass]
    public class SlowSortUnitTests
    {
        [TestMethod]
        public void TestNullArray()
        {
            int[]? array = null;
            Slowsorter.Sort(array);
            Assert.IsNull(array);
        }

        [TestMethod]
        public void TestEmptyArray()
        {
            int[] array = new int[0];
            Slowsorter.Sort(array);
            Assert.AreEqual(0, array.Length);
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
            Slowsorter.Sort(array);
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
            Slowsorter.Sort(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void TestRandomArray()
        {
            int[] array = new int[]
            {
                3,
                5,
                2,
                8,
                6,
                1,
                4,
                7
            };
            int[] expected = new int[]
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8
            };
            Slowsorter.Sort(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void TestDuplicateValues()
        {
            int[] array = new int[]
            {
                4,
                2,
                5,
                2,
                3,
                3,
                1,
                4
            };
            int[] expected = new int[]
            {
                1,
                2,
                2,
                3,
                3,
                4,
                4,
                5
            };
            Slowsorter.Sort(array);
            CollectionAssert.AreEqual(expected, array);
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
            Slowsorter.Sort(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void TestLargeRandomArray()
        {
            const int size = 100;
            int[] array = new int[size];
            int[] expected = new int[size];
            System.Random random = new System.Random(0);
            for (int i = 0; i < size; i++)
            {
                int randomValue = random.Next(0, 1000);
                array[i] = randomValue;
                expected[i] = randomValue;
            }

            System.Array.Sort(expected);
            Slowsorter.Sort(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void TestAllSameElements()
        {
            int[] array = new int[]
            {
                7,
                7,
                7,
                7,
                7
            };
            int[] expected = new int[]
            {
                7,
                7,
                7,
                7,
                7
            };
            Slowsorter.Sort(array);
            CollectionAssert.AreEqual(expected, array);
        }
    }
}