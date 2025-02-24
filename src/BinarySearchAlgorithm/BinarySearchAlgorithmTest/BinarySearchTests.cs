using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BinarySearchAlgorithm;

namespace BinarySearchAlgorithmTest
{
    [TestClass]
    public class BinarySearchTests
    {
        [TestMethod]
        public void Search_NullArray_ReturnsNegativeOne()
        {
            int result = BinarySearch.Search<int>(null, 5);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Search_EmptyArray_ReturnsNegativeOne()
        {
            int[] array = new int[0];
            int result = BinarySearch.Search(array, 5);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Search_SingleElementFound_ReturnsIndexZero()
        {
            int[] array = new int[]
            {
                10
            };
            int result = BinarySearch.Search(array, 10);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Search_SingleElementNotFound_ReturnsNegativeOne()
        {
            int[] array = new int[]
            {
                10
            };
            int result = BinarySearch.Search(array, 5);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Search_MultipleElements_FoundIndex()
        {
            int[] array = new int[]
            {
                1,
                3,
                5,
                7,
                9
            };
            int result = BinarySearch.Search(array, 7);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Search_MultipleElements_NotFound_ReturnsNegativeOne()
        {
            int[] array = new int[]
            {
                1,
                3,
                5,
                7,
                9
            };
            int result = BinarySearch.Search(array, 4);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Search_StringArray_FoundIndex()
        {
            string[] array = new string[]
            {
                "apple",
                "banana",
                "cherry",
                "date",
                "fig"
            };
            int result = BinarySearch.Search(array, "cherry");
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void Search_StringArray_NotFound_ReturnsNegativeOne()
        {
            string[] array = new string[]
            {
                "apple",
                "banana",
                "cherry",
                "date",
                "fig"
            };
            int result = BinarySearch.Search(array, "grape");
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Search_LargeDataSet_Test()
        {
            int size = 10000;
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = i * 2;
            }

            int target = 5678;
            int expected = target / 2;
            int result = BinarySearch.Search(array, target);
            Assert.AreEqual(expected, result);
            target = 5679;
            result = BinarySearch.Search(array, target);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Search_DuplicateValues_FoundIndex()
        {
            int[] array = new int[]
            {
                1,
                2,
                2,
                2,
                3
            };
            int result = BinarySearch.Search(array, 2);
            Assert.IsTrue(result >= 1 && result <= 3);
        }

        [TestMethod]
        public void Search_RandomSortedArray_ReturnsCorrectValue()
        {
            System.Random random = new System.Random(0);
            const int size = 5000;
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(0, 100000);
            }

            System.Array.Sort(array);
            int targetIndex = random.Next(0, size);
            int target = array[targetIndex];
            int result = BinarySearch.Search(array, target);
            Assert.AreEqual(target, array[result]);
        }
    }
}