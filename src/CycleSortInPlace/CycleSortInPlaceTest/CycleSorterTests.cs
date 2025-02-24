using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CycleSortInPlace;

namespace CycleSortInPlaceTest
{
    [TestClass]
    public class CycleSorterTests
    {
        [TestMethod]
        public void TestSort_NullArray_DoesNotThrow()
        {
            int[] array = null;
            CycleSorter.Sort(array);
            Assert.IsNull(array);
        }

        [TestMethod]
        public void TestSort_SingleElement()
        {
            int[] array = new int[]
            {
                42
            };
            CycleSorter.Sort(array);
            Assert.AreEqual(42, array[0]);
        }

        [TestMethod]
        public void TestSort_EmptyArray()
        {
            int[] array = new int[0];
            CycleSorter.Sort(array);
            Assert.AreEqual(0, array.Length);
        }

        [TestMethod]
        public void TestSort_AlreadySorted()
        {
            int[] array = new int[]
            {
                1,
                2,
                3,
                4,
                5
            };
            CycleSorter.Sort(array);
            for (int index = 1; index < array.Length; index++)
            {
                Assert.IsTrue(array[index - 1] <= array[index]);
            }
        }

        [TestMethod]
        public void TestSort_Unsorted()
        {
            int[] array = new int[]
            {
                5,
                3,
                1,
                4,
                2
            };
            CycleSorter.Sort(array);
            for (int index = 1; index < array.Length; index++)
            {
                Assert.IsTrue(array[index - 1] <= array[index]);
            }
        }

        [TestMethod]
        public void TestSort_WithDuplicates()
        {
            int[] array = new int[]
            {
                3,
                1,
                2,
                3,
                2,
                1
            };
            CycleSorter.Sort(array);
            for (int index = 1; index < array.Length; index++)
            {
                Assert.IsTrue(array[index - 1] <= array[index]);
            }
        }

        [TestMethod]
        public void TestSort_LargeArray()
        {
            const int size = 10000;
            int[] array = new int[size];
            Random random = new Random(42);
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(-100000, 100000);
            }

            int[] expected = new int[size];
            Array.Copy(array, expected, size);
            Array.Sort(expected);
            CycleSorter.Sort(array);
            for (int i = 0; i < size; i++)
            {
                Assert.AreEqual(expected[i], array[i]);
            }
        }

        [TestMethod]
        public void TestSort_StringArray()
        {
            string[] array = new string[]
            {
                "banana",
                "apple",
                "cherry",
                "date"
            };
            CycleSorter.Sort(array);
            string[] expected = new string[]
            {
                "apple",
                "banana",
                "cherry",
                "date"
            };
            CollectionAssert.AreEqual(expected, array);
        }
    }
}