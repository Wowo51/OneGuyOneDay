using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GnomeSort;

namespace GnomeSortTest
{
    [TestClass]
    public class GnomeSorterTests
    {
        [TestMethod]
        public void TestNullArray()
        {
            int[]? array = null;
            GnomeSorter.Sort(array);
            Assert.IsNull(array);
        }

        [TestMethod]
        public void TestEmptyArray()
        {
            int[] array = new int[0];
            GnomeSorter.Sort(array);
            Assert.AreEqual(0, array.Length);
        }

        [TestMethod]
        public void TestSingleElement()
        {
            int[] array = new int[]
            {
                42
            };
            GnomeSorter.Sort(array);
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
            int[] expected = new int[]
            {
                1,
                2,
                3,
                4,
                5
            };
            GnomeSorter.Sort(array);
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
            GnomeSorter.Sort(array);
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
            GnomeSorter.Sort(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void TestRandomLargeArray()
        {
            int size = 1000;
            int[] array = new int[size];
            int[] expected = new int[size];
            Random random = new Random(42);
            for (int i = 0; i < size; i++)
            {
                int value = random.Next(-1000, 1000);
                array[i] = value;
                expected[i] = value;
            }

            Array.Sort(expected);
            GnomeSorter.Sort(array);
            CollectionAssert.AreEqual(expected, array);
        }
    }
}