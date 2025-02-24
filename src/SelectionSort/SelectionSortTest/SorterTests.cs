using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SelectionSort;

namespace SelectionSortTest
{
    [TestClass]
    public class SorterTests
    {
        [TestMethod]
        public void Test_SmallArray_SortsCorrectly()
        {
            int[] unsorted = new int[]
            {
                64,
                25,
                12,
                22,
                11
            };
            int[] expected = new int[]
            {
                11,
                12,
                22,
                25,
                64
            };
            Sorter.SelectionSort(unsorted);
            CollectionAssert.AreEqual(expected, unsorted);
        }

        [TestMethod]
        public void Test_NullArray_NoException()
        {
            int[]? nullArray = null;
            try
            {
                Sorter.SelectionSort(nullArray);
            }
            catch (Exception ex)
            {
                Assert.Fail("No exception expected, but got: " + ex.Message);
            }
        }

        [TestMethod]
        public void Test_EmptyArray_NoChange()
        {
            int[] empty = new int[0];
            int[] expected = new int[0];
            Sorter.SelectionSort(empty);
            CollectionAssert.AreEqual(expected, empty);
        }

        [TestMethod]
        public void Test_RandomLargeArray_Sorted()
        {
            int size = 1000;
            int[] randomArray = new int[size];
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                randomArray[i] = random.Next(0, 10000);
            }

            Sorter.SelectionSort(randomArray);
            for (int i = 0; i < size - 1; i++)
            {
                Assert.IsTrue(randomArray[i] <= randomArray[i + 1], "Array is not sorted at index " + i);
            }
        }
    }
}