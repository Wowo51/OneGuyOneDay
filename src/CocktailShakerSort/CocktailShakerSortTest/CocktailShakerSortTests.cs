using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CocktailShakerSort;

namespace CocktailShakerSortTest
{
    [TestClass]
    public class CocktailShakerSortTests
    {
        [TestMethod]
        public void Test_NullArray()
        {
            int[]? array = null;
            // Calling the sort on a null array should not throw and leave array as null.
            CocktailSorter.CocktailShakerSort(array);
            Assert.IsNull(array);
        }

        [TestMethod]
        public void Test_EmptyArray()
        {
            int[] array = new int[0];
            CocktailSorter.CocktailShakerSort(array);
            Assert.AreEqual(0, array.Length);
        }

        [TestMethod]
        public void Test_SingleElement()
        {
            int[] array = new int[]
            {
                5
            };
            CocktailSorter.CocktailShakerSort(array);
            Assert.AreEqual(5, array[0]);
        }

        [TestMethod]
        public void Test_SortedArray()
        {
            int[] array = new int[]
            {
                1,
                2,
                3,
                4,
                5
            };
            CocktailSorter.CocktailShakerSort(array);
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5 }, array);
        }

        [TestMethod]
        public void Test_UnsortedArray()
        {
            int[] array = new int[]
            {
                5,
                3,
                1,
                4,
                2
            };
            CocktailSorter.CocktailShakerSort(array);
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5 }, array);
        }

        [TestMethod]
        public void Test_ReversedArray()
        {
            int[] array = new int[]
            {
                9,
                7,
                5,
                3,
                1
            };
            CocktailSorter.CocktailShakerSort(array);
            CollectionAssert.AreEqual(new int[] { 1, 3, 5, 7, 9 }, array);
        }

        [TestMethod]
        public void Test_ArrayWithDuplicates()
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
            CocktailSorter.CocktailShakerSort(array);
            CollectionAssert.AreEqual(new int[] { 1, 1, 2, 2, 3, 3 }, array);
        }

        [TestMethod]
        public void Test_LargeRandomArray()
        {
            int size = 10000;
            int[] array = new int[size];
            int[] expected = new int[size];
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                int value = random.Next(-10000, 10000);
                array[i] = value;
                expected[i] = value;
            }

            Array.Sort(expected);
            CocktailSorter.CocktailShakerSort(array);
            CollectionAssert.AreEqual(expected, array);
        }
    }
}