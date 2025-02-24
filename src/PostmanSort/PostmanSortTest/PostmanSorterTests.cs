using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PostmanSort;

namespace PostmanSortTest
{
    [TestClass]
    public class PostmanSorterTests
    {
        [TestMethod]
        public void Sort_NullArray_NoChange()
        {
            string? []? data = null;
            PostmanSorter.Sort(data);
            Assert.IsNull(data);
        }

        [TestMethod]
        public void Sort_EmptyArray_NoChange()
        {
            string? [] data = new string? [0];
            PostmanSorter.Sort(data);
            Assert.AreEqual(0, data.Length);
        }

        [TestMethod]
        public void Sort_SingleElementArray_NoChange()
        {
            string? [] data = new string? []
            {
                "apple"
            };
            PostmanSorter.Sort(data);
            Assert.AreEqual(1, data.Length);
            Assert.AreEqual("apple", data[0]);
        }

        [TestMethod]
        public void Sort_UnsortedArray_SortsCorrectly()
        {
            string? [] data = new string? []
            {
                "delta",
                "alpha",
                "charlie",
                "bravo"
            };
            PostmanSorter.Sort(data);
            string? [] expected = new string? []
            {
                "alpha",
                "bravo",
                "charlie",
                "delta"
            };
            CollectionAssert.AreEqual(expected, data);
        }

        [TestMethod]
        public void Sort_ArrayWithNullElements_SortsNullsFirst()
        {
            string? [] data = new string? []
            {
                "banana",
                null,
                "apple",
                null,
                "cherry"
            };
            PostmanSorter.Sort(data);
            string? [] expected = new string? []
            {
                null,
                null,
                "apple",
                "banana",
                "cherry"
            };
            CollectionAssert.AreEqual(expected, data);
        }

        [TestMethod]
        public void Sort_LargeDataSet_SortsCorrectly()
        {
            const int count = 1000;
            string? [] data = new string? [count];
            System.Random random = new System.Random(42);
            for (int i = 0; i < count; i++)
            {
                int length = random.Next(1, 10);
                char[] chars = new char[length];
                for (int j = 0; j < length; j++)
                {
                    chars[j] = (char)random.Next(97, 123);
                }

                data[i] = new string (chars);
            }

            string? [] expected = (string? [])data.Clone();
            System.Array.Sort(expected, System.StringComparer.Ordinal);
            PostmanSorter.Sort(data);
            CollectionAssert.AreEqual(expected, data);
        }
    }
}