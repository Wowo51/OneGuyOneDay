using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoogeSort;
using System;

namespace StoogeSortTest
{
    [TestClass]
    public class StoogeSorterTests
    {
        [TestMethod]
        public void TestSort_NullArray_DoesNotThrow()
        {
            int[] nullArray = null;
            StoogeSorter.Sort(nullArray);
            Assert.IsNull(nullArray);
        }

        [TestMethod]
        public void TestSort_EmptyArray()
        {
            int[] array = new int[0];
            StoogeSorter.Sort(array);
            CollectionAssert.AreEqual(new int[0], array);
        }

        [TestMethod]
        public void TestSort_SingleElement()
        {
            int[] array =
            {
                42
            };
            StoogeSorter.Sort(array);
            CollectionAssert.AreEqual(new int[] { 42 }, array);
        }

        [TestMethod]
        public void TestSort_TwoElements_Sorted()
        {
            int[] array =
            {
                1,
                2
            };
            StoogeSorter.Sort(array);
            CollectionAssert.AreEqual(new int[] { 1, 2 }, array);
        }

        [TestMethod]
        public void TestSort_TwoElements_Unsorted()
        {
            int[] array =
            {
                2,
                1
            };
            StoogeSorter.Sort(array);
            CollectionAssert.AreEqual(new int[] { 1, 2 }, array);
        }

        [TestMethod]
        public void TestSort_SmallArray()
        {
            int[] array =
            {
                3,
                1,
                2
            };
            StoogeSorter.Sort(array);
            CollectionAssert.AreEqual(new int[] { 1, 2, 3 }, array);
        }

        [TestMethod]
        public void TestSort_LargeRandomArray()
        {
            System.Random random = new System.Random();
            int[] array = new int[100];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(-1000, 1000);
            }

            int[] expected = (int[])array.Clone();
            System.Array.Sort(expected);
            StoogeSorter.Sort(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void TestSort_DuplicateElements()
        {
            int[] array =
            {
                5,
                3,
                5,
                3,
                5
            };
            StoogeSorter.Sort(array);
            CollectionAssert.AreEqual(new int[] { 3, 3, 5, 5, 5 }, array);
        }

        [TestMethod]
        public void TestSort_AlreadySortedArray()
        {
            int[] array =
            {
                1,
                2,
                3,
                4,
                5
            };
            StoogeSorter.Sort(array);
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5 }, array);
        }

        [TestMethod]
        public void TestSort_ReverseSortedArray()
        {
            int[] array =
            {
                5,
                4,
                3,
                2,
                1
            };
            StoogeSorter.Sort(array);
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5 }, array);
        }

        [TestMethod]
        public void TestSort_StringArray()
        {
            string[] array =
            {
                "delta",
                "alpha",
                "charlie",
                "bravo"
            };
            StoogeSorter.Sort(array);
            CollectionAssert.AreEqual(new string[] { "alpha", "bravo", "charlie", "delta" }, array);
        }

        [TestMethod]
        public void TestSort_CustomType()
        {
            ComparableWrapper[] array =
            {
                new ComparableWrapper(3),
                new ComparableWrapper(1),
                new ComparableWrapper(2)
            };
            StoogeSorter.Sort(array);
            Assert.AreEqual(1, array[0].Value);
            Assert.AreEqual(2, array[1].Value);
            Assert.AreEqual(3, array[2].Value);
        }

        private class ComparableWrapper : System.IComparable<ComparableWrapper>
        {
            public int Value;
            public ComparableWrapper(int value)
            {
                Value = value;
            }

            public int CompareTo(ComparableWrapper other)
            {
                if (other == null)
                {
                    return 1;
                }

                return Value.CompareTo(other.Value);
            }
        }
    }
}