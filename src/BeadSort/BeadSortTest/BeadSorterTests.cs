using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeadSort;
using System;

namespace BeadSortTest
{
    [TestClass]
    public class BeadSorterTests
    {
        [TestMethod]
        public void TestEmptyArray()
        {
            int[] array = new int[0];
            BeadSorter.Sort(array);
            Assert.AreEqual(0, array.Length);
        }

        [TestMethod]
        public void TestSingleElement()
        {
            int[] array = new int[]
            {
                5
            };
            BeadSorter.Sort(array);
            Assert.AreEqual(5, array[0]);
        }

        [TestMethod]
        public void TestSortedArray()
        {
            int[] array = new int[]
            {
                1,
                3,
                4,
                5,
                7
            };
            BeadSorter.Sort(array);
            CollectionAssert.AreEqual(new int[] { 1, 3, 4, 5, 7 }, array);
        }

        [TestMethod]
        public void TestReverseArray()
        {
            int[] array = new int[]
            {
                7,
                5,
                4,
                3,
                1
            };
            BeadSorter.Sort(array);
            CollectionAssert.AreEqual(new int[] { 1, 3, 4, 5, 7 }, array);
        }

        [TestMethod]
        public void TestRandomArray()
        {
            int length = 1000;
            int[] array = new int[length];
            System.Random random = new System.Random(0);
            for (int i = 0; i < length; i++)
            {
                array[i] = random.Next(0, 101);
            }

            int[] expected = new int[length];
            for (int i = 0; i < length; i++)
            {
                expected[i] = array[i];
            }

            Array.Sort(expected);
            BeadSorter.Sort(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void TestAllEqualElements()
        {
            int[] array = new int[]
            {
                5,
                5,
                5
            };
            BeadSorter.Sort(array);
            CollectionAssert.AreEqual(new int[] { 5, 5, 5 }, array);
        }

        [TestMethod]
        public void TestArrayWithZeros()
        {
            int[] array = new int[]
            {
                0,
                0,
                0,
                0,
                0
            };
            BeadSorter.Sort(array);
            CollectionAssert.AreEqual(new int[] { 0, 0, 0, 0, 0 }, array);
        }
    }
}