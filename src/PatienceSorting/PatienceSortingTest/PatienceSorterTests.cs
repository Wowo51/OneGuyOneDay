using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatienceSorting;

namespace PatienceSortingTest
{
    [TestClass]
    public class PatienceSorterTests
    {
        [TestMethod]
        public void Test_NullInput_ReturnsEmptyList()
        {
            IList<int> result = PatienceSorter.Sort<int>(null);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Test_EmptyInput_ReturnsEmptyList()
        {
            List<int> input = new List<int>();
            IList<int> result = PatienceSorter.Sort<int>(input);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Test_SingleElement()
        {
            List<int> input = new List<int>
            {
                42
            };
            IList<int> result = PatienceSorter.Sort<int>(input);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(42, result[0]);
        }

        [TestMethod]
        public void Test_SortedInput()
        {
            List<int> input = new List<int>
            {
                1,
                2,
                3,
                4,
                5
            };
            IList<int> result = PatienceSorter.Sort<int>(input);
            CollectionAssert.AreEqual((ICollection)input, (ICollection)result);
        }

        [TestMethod]
        public void Test_ReverseSortedInput()
        {
            List<int> input = new List<int>
            {
                5,
                4,
                3,
                2,
                1
            };
            IList<int> result = PatienceSorter.Sort<int>(input);
            List<int> expected = new List<int>
            {
                1,
                2,
                3,
                4,
                5
            };
            CollectionAssert.AreEqual((ICollection)expected, (ICollection)result);
        }

        [TestMethod]
        public void Test_UnsortedInput()
        {
            List<int> input = new List<int>
            {
                3,
                1,
                4,
                2
            };
            IList<int> result = PatienceSorter.Sort<int>(input);
            List<int> expected = new List<int>
            {
                1,
                2,
                3,
                4
            };
            CollectionAssert.AreEqual((ICollection)expected, (ICollection)result);
        }

        [TestMethod]
        public void Test_DuplicateElements()
        {
            List<int> input = new List<int>
            {
                3,
                1,
                2,
                3,
                1
            };
            IList<int> result = PatienceSorter.Sort<int>(input);
            List<int> expected = new List<int>
            {
                1,
                1,
                2,
                3,
                3
            };
            CollectionAssert.AreEqual((ICollection)expected, (ICollection)result);
        }

        [TestMethod]
        public void Test_LargeInput_Random()
        {
            List<int> input = new List<int>();
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                int number = random.Next(0, 10000);
                input.Add(number);
            }

            List<int> expected = new List<int>(input);
            expected.Sort();
            IList<int> result = PatienceSorter.Sort<int>(input);
            CollectionAssert.AreEqual((ICollection)expected, (ICollection)result);
        }
    }
}