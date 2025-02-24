using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmoothGamerSort;

namespace SmoothGamerSortTest
{
    [TestClass]
    public class SmoothSorterTests
    {
        [TestMethod]
        public void Test_NullArray()
        {
            int[]? array = null;
            SmoothSorter.SmoothGamerSort(array);
            Assert.IsNull(array);
        }

        [TestMethod]
        public void Test_EmptyArray()
        {
            int[] array = new int[0];
            int[] expected = new int[0];
            SmoothSorter.SmoothGamerSort(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void Test_SingleElementArray()
        {
            int[] array = new int[]
            {
                42
            };
            int[] expected = new int[]
            {
                42
            };
            SmoothSorter.SmoothGamerSort(array);
            CollectionAssert.AreEqual(expected, array);
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
            int[] expected = new int[]
            {
                1,
                2,
                3,
                4,
                5
            };
            SmoothSorter.SmoothGamerSort(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void Test_ReverseArray()
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
            SmoothSorter.SmoothGamerSort(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void Test_DuplicateElements()
        {
            int[] array = new int[]
            {
                3,
                1,
                2,
                1,
                3
            };
            int[] expected = new int[]
            {
                1,
                1,
                2,
                3,
                3
            };
            SmoothSorter.SmoothGamerSort(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void Test_LargeRandomArray()
        {
            System.Random random = new System.Random(0);
            int size = 1000;
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(-1000, 1000);
            }

            int[] expected = new int[size];
            System.Array.Copy(array, expected, size);
            System.Array.Sort(expected);
            SmoothSorter.SmoothGamerSort(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void Test_StringArray()
        {
            string[] array = new string[]
            {
                "banana",
                "apple",
                "orange"
            };
            string[] expected = new string[]
            {
                "apple",
                "banana",
                "orange"
            };
            SmoothSorter.SmoothGamerSort(array);
            CollectionAssert.AreEqual(expected, array);
        }
    }
}