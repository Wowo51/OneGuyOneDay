using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShellSort;

namespace ShellSortTest
{
    [TestClass]
    public class ShellSorterTests
    {
        [TestMethod]
        public void Test_NullArray()
        {
            int[]? array = null;
            ShellSorter.Sort(array);
            ShellSorter.Sort<int>(array, null);
            Assert.IsNull(array);
        }

        [TestMethod]
        public void Test_EmptyArray()
        {
            int[] array = new int[0];
            ShellSorter.Sort(array);
            Assert.AreEqual(0, array.Length);
        }

        [TestMethod]
        public void Test_SingleElement()
        {
            int[] array = new int[]
            {
                42
            };
            ShellSorter.Sort(array);
            Assert.AreEqual(42, array[0]);
        }

        [TestMethod]
        public void Test_SortIntegersAscending()
        {
            int[] array = new int[]
            {
                5,
                3,
                8,
                1,
                4
            };
            ShellSorter.Sort(array);
            int[] expected = new int[]
            {
                1,
                3,
                4,
                5,
                8
            };
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void Test_SortStringsAscending()
        {
            string[] array = new string[]
            {
                "z",
                "a",
                "k"
            };
            ShellSorter.Sort(array);
            string[] expected = new string[]
            {
                "a",
                "k",
                "z"
            };
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void Test_SortIntegersDescending()
        {
            int[] array = new int[]
            {
                5,
                3,
                8,
                1,
                4
            };
            IComparer<int> descendingComparer = new DescendingComparer();
            ShellSorter.Sort(array, descendingComparer);
            int[] expected = new int[]
            {
                8,
                5,
                4,
                3,
                1
            };
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void Test_LargeRandomArray()
        {
            const int size = 1000;
            int[] array = new int[size];
            int[] copied = new int[size];
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                int value = random.Next(-1000, 1000);
                array[i] = value;
                copied[i] = value;
            }

            ShellSorter.Sort(array);
            Array.Sort(copied);
            CollectionAssert.AreEqual(copied, array);
        }

        private class DescendingComparer : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                return y.CompareTo(x);
            }
        }
    }
}