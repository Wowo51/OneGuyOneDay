using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsertionSort;

namespace InsertionSortTest
{
    [TestClass]
    public class InsertionSorterTests
    {
        [TestMethod]
        public void TestSort_NullInput_DoesNotThrow()
        {
            List<int> list = null;
            try
            {
                InsertionSorter.Sort<int>(list);
            }
            catch (Exception)
            {
                Assert.Fail("Sort method threw exception on null input.");
            }
        }

        [TestMethod]
        public void TestSort_EmptyList()
        {
            List<int> list = new List<int>();
            InsertionSorter.Sort<int>(list);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void TestSort_SingleElement()
        {
            List<int> list = new List<int>
            {
                42
            };
            InsertionSorter.Sort<int>(list);
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(42, list[0]);
        }

        [TestMethod]
        public void TestSort_AlreadySorted()
        {
            List<int> list = new List<int>
            {
                1,
                2,
                3,
                4,
                5
            };
            InsertionSorter.Sort<int>(list);
            CollectionAssert.AreEqual(new List<int> { 1, 2, 3, 4, 5 }, list);
        }

        [TestMethod]
        public void TestSort_UnsortedList()
        {
            List<int> list = new List<int>
            {
                5,
                3,
                8,
                1
            };
            InsertionSorter.Sort<int>(list);
            CollectionAssert.AreEqual(new List<int> { 1, 3, 5, 8 }, list);
        }

        [TestMethod]
        public void TestSort_WithDuplicates()
        {
            List<int> list = new List<int>
            {
                5,
                3,
                3,
                2
            };
            InsertionSorter.Sort<int>(list);
            CollectionAssert.AreEqual(new List<int> { 2, 3, 3, 5 }, list);
        }

        [TestMethod]
        public void TestSort_LargeRandomList()
        {
            List<int> list = new List<int>();
            const int size = 1000;
            Random random = new Random(0);
            for (int i = 0; i < size; i++)
            {
                list.Add(random.Next(-10000, 10000));
            }

            InsertionSorter.Sort<int>(list);
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (list[i].CompareTo(list[i + 1]) > 0)
                {
                    Assert.Fail("List is not sorted at index " + i);
                }
            }
        }

        [TestMethod]
        public void TestSort_Strings()
        {
            List<string> list = new List<string>
            {
                "banana",
                "apple",
                "cherry"
            };
            InsertionSorter.Sort<string>(list);
            CollectionAssert.AreEqual(new List<string> { "apple", "banana", "cherry" }, list);
        }
    }
}