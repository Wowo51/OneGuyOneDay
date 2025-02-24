using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BitonicSorter;

namespace BitonicSorterTest
{
    [TestClass]
    public class BitonicSorterTests
    {
        private static void AssertSorted<T>(T[] array, bool ascending)
            where T : IComparable<T>
        {
            for (int i = 1; i < array.Length; i++)
            {
                if (ascending)
                {
                    Assert.IsTrue(array[i - 1].CompareTo(array[i]) <= 0);
                }
                else
                {
                    Assert.IsTrue(array[i - 1].CompareTo(array[i]) >= 0);
                }
            }
        }

        [TestMethod]
        public void TestNullArrayDoesNotThrowException()
        {
            int[]? array = null;
            BitonicSorter.BitonicSorter.Sort(array);
            Assert.IsNull(array);
        }

        [TestMethod]
        public void TestEmptyArray()
        {
            int[] array = new int[0];
            BitonicSorter.BitonicSorter.Sort(array);
            Assert.AreEqual(0, array.Length);
        }

        [TestMethod]
        public void TestSingleElementArray()
        {
            int[] array = new int[]
            {
                42
            };
            BitonicSorter.BitonicSorter.Sort(array);
            Assert.AreEqual(42, array[0]);
        }

        [TestMethod]
        public void TestAscendingSort()
        {
            int[] array = new int[]
            {
                3,
                7,
                4,
                8,
                6,
                2,
                1,
                5
            };
            BitonicSorter.BitonicSorter.Sort(array, true);
            AssertSorted(array, true);
        }

        [TestMethod]
        public void TestDescendingSort()
        {
            int[] array = new int[]
            {
                3,
                7,
                4,
                8,
                6,
                2,
                1,
                5
            };
            BitonicSorter.BitonicSorter.Sort(array, false);
            AssertSorted(array, false);
        }

        [TestMethod]
        public void TestLargeDatasetAscending()
        {
            int size = 1024;
            int[] array = new int[size];
            Random random = new Random(0);
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(-10000, 10000);
            }

            BitonicSorter.BitonicSorter.Sort(array, true);
            AssertSorted(array, true);
        }

        [TestMethod]
        public void TestLargeDatasetDescending()
        {
            int size = 1024;
            int[] array = new int[size];
            Random random = new Random(0);
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(-10000, 10000);
            }

            BitonicSorter.BitonicSorter.Sort(array, false);
            AssertSorted(array, false);
        }

        [TestMethod]
        public void TestArrayWithDuplicates()
        {
            int[] array = new int[]
            {
                5,
                3,
                5,
                3,
                5,
                3,
                5,
                3
            };
            BitonicSorter.BitonicSorter.Sort(array, true);
            AssertSorted(array, true);
        }
    }
}