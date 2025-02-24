using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BubbleSort;

namespace BubbleSortTest
{
    [TestClass]
    public class BubbleSorterTests
    {
        [TestMethod]
        public void TestNullArray()
        {
            int[]? testArray = null;
            BubbleSorter.Sort<int>(testArray);
            Assert.IsNull(testArray);
        }

        [TestMethod]
        public void TestEmptyArray()
        {
            int[] array = new int[0];
            BubbleSorter.Sort<int>(array);
            Assert.AreEqual(0, array.Length);
        }

        [TestMethod]
        public void TestSingleElementArray()
        {
            int[] array = new int[]
            {
                42
            };
            BubbleSorter.Sort<int>(array);
            Assert.AreEqual(1, array.Length);
            Assert.AreEqual(42, array[0]);
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
            BubbleSorter.Sort<int>(array);
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5 }, array);
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
            BubbleSorter.Sort<int>(array);
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5 }, array);
        }

        [TestMethod]
        public void TestRandomArray()
        {
            Random random = new Random();
            int length = 100;
            int[] array = new int[length];
            for (int i = 0; i < length; i++)
            {
                array[i] = random.Next(-1000, 1000);
            }

            int[] expected = new int[length];
            for (int i = 0; i < length; i++)
            {
                expected[i] = array[i];
            }

            Array.Sort(expected);
            BubbleSorter.Sort<int>(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void TestStringArray()
        {
            string[] array = new string[]
            {
                "zebra",
                "apple",
                "mango",
                "banana"
            };
            BubbleSorter.Sort<string>(array);
            CollectionAssert.AreEqual(new string[] { "apple", "banana", "mango", "zebra" }, array);
        }

        [TestMethod]
        public void TestLargeRandomArray()
        {
            Random random = new Random();
            int length = 1000;
            int[] array = new int[length];
            for (int i = 0; i < length; i++)
            {
                array[i] = random.Next(-10000, 10000);
            }

            int[] expected = new int[length];
            for (int i = 0; i < length; i++)
            {
                expected[i] = array[i];
            }

            Array.Sort(expected);
            BubbleSorter.Sort<int>(array);
            CollectionAssert.AreEqual(expected, array);
        }
    }
}