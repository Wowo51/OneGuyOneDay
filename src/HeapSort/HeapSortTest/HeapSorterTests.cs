using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using HeapSort;

namespace HeapSortTest
{
    [TestClass]
    public class HeapSorterTests
    {
        [TestMethod]
        public void Test_NullInput()
        {
            List<int>? list = null;
            HeapSorter.Sort<int>(list);
            Assert.IsNull(list);
        }

        [TestMethod]
        public void Test_EmptyList()
        {
            List<int> list = new List<int>();
            HeapSorter.Sort<int>(list);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void Test_SingleElement()
        {
            List<int> list = new List<int>
            {
                42
            };
            HeapSorter.Sort<int>(list);
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(42, list[0]);
        }

        [TestMethod]
        public void Test_SortedList()
        {
            List<int> list = new List<int>
            {
                10,
                7,
                9,
                5,
                3
            };
            HeapSorter.Sort<int>(list);
            List<int> expected = new List<int>
            {
                3,
                5,
                7,
                9,
                10
            };
            CollectionAssert.AreEqual(expected, list);
        }

        [TestMethod]
        public void Test_RandomList()
        {
            List<int> list = new List<int>();
            Random random = new Random(0);
            for (int i = 0; i < 1000; i++)
            {
                list.Add(random.Next(-10000, 10000));
            }

            List<int> expected = new List<int>(list);
            expected.Sort();
            HeapSorter.Sort<int>(list);
            CollectionAssert.AreEqual(expected, list);
        }

        [TestMethod]
        public void Test_StringList()
        {
            List<string> list = new List<string>
            {
                "c",
                "a",
                "b",
                "e",
                "d"
            };
            HeapSorter.Sort<string>(list);
            List<string> expected = new List<string>
            {
                "a",
                "b",
                "c",
                "d",
                "e"
            };
            CollectionAssert.AreEqual(expected, list);
        }

        [TestMethod]
        public void Test_ListWithDuplicates()
        {
            List<int> list = new List<int>
            {
                5,
                3,
                8,
                3,
                9,
                5,
                1,
                1,
                3
            };
            HeapSorter.Sort<int>(list);
            List<int> expected = new List<int>
            {
                1,
                1,
                3,
                3,
                3,
                5,
                5,
                8,
                9
            };
            CollectionAssert.AreEqual(expected, list);
        }
    }
}