using Microsoft.VisualStudio.TestTools.UnitTesting;
using PancakeSorting;

namespace PancakeSortingTest
{
    [TestClass]
    public class PancakeSorterTests
    {
        [TestMethod]
        public void TestSort_WithNullArray()
        {
            PancakeSorter sorter = new PancakeSorter();
            sorter.Sort(null);
            // Expect no exception when sorting a null array.
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestSort_WithEmptyArray()
        {
            PancakeSorter sorter = new PancakeSorter();
            int[] array = new int[0];
            sorter.Sort(array);
            Assert.AreEqual(0, array.Length);
        }

        [TestMethod]
        public void TestSort_WithSingleElement()
        {
            PancakeSorter sorter = new PancakeSorter();
            int[] array = new int[]
            {
                42
            };
            int[] expected = new int[]
            {
                42
            };
            sorter.Sort(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void TestSort_AlreadySortedArray()
        {
            PancakeSorter sorter = new PancakeSorter();
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
            sorter.Sort(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void TestSort_UnsortedArray()
        {
            PancakeSorter sorter = new PancakeSorter();
            int[] array = new int[]
            {
                3,
                1,
                4,
                2
            };
            int[] expected = new int[]
            {
                1,
                2,
                3,
                4
            };
            sorter.Sort(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void TestSort_WithDuplicates()
        {
            PancakeSorter sorter = new PancakeSorter();
            int[] array = new int[]
            {
                1,
                3,
                3,
                2,
                1
            };
            int[] expected = new int[]
            {
                1,
                1,
                2,
                3,
                3
            };
            sorter.Sort(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void TestSort_LargeRandomArray()
        {
            PancakeSorter sorter = new PancakeSorter();
            const int size = 1000;
            int[] array = new int[size];
            System.Random random = new System.Random(12345);
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(0, 10000);
            }

            int[] expected = new int[size];
            System.Array.Copy(array, expected, size);
            System.Array.Sort(expected);
            sorter.Sort(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void TestSort_DescendingArray()
        {
            PancakeSorter sorter = new PancakeSorter();
            int[] array = new int[]
            {
                9,
                7,
                5,
                3,
                1
            };
            int[] expected = new int[]
            {
                1,
                3,
                5,
                7,
                9
            };
            sorter.Sort(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void TestSort_WithNegativeNumbers()
        {
            PancakeSorter sorter = new PancakeSorter();
            int[] array = new int[]
            {
                -5,
                -1,
                -3,
                0,
                -2
            };
            int[] expected = new int[]
            {
                -5,
                -3,
                -2,
                -1,
                0
            };
            sorter.Sort(array);
            CollectionAssert.AreEqual(expected, array);
        }
    }
}