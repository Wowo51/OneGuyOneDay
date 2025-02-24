using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FibonacciSearchTechnique;

namespace FibonacciSearchTechniqueTest
{
    [TestClass]
    public class FibonacciSearchTests
    {
        [TestMethod]
        public void Search_NullArray_ReturnsMinusOne()
        {
            int result = FibonacciSearch.Search<int>(null, 10);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Search_EmptyArray_ReturnsMinusOne()
        {
            int[] array = new int[0];
            int result = FibonacciSearch.Search(array, 10);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Search_SingleElement_Found()
        {
            int[] array = new int[]
            {
                42
            };
            int result = FibonacciSearch.Search(array, 42);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Search_SingleElement_NotFound()
        {
            int[] array = new int[]
            {
                42
            };
            int result = FibonacciSearch.Search(array, 10);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Search_MultipleElements_Found()
        {
            int[] array = new int[]
            {
                1,
                3,
                5,
                7,
                9
            };
            int result = FibonacciSearch.Search(array, 7);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Search_MultipleElements_NotFound()
        {
            int[] array = new int[]
            {
                1,
                3,
                5,
                7,
                9
            };
            int result = FibonacciSearch.Search(array, 4);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Search_FindingFirstAndLast()
        {
            int[] array = new int[]
            {
                10,
                20,
                30,
                40,
                50
            };
            int resultFirst = FibonacciSearch.Search(array, 10);
            int resultLast = FibonacciSearch.Search(array, 50);
            Assert.AreEqual(0, resultFirst);
            Assert.AreEqual(4, resultLast);
        }

        [TestMethod]
        public void Search_CustomType_Test()
        {
            string[] array = new string[]
            {
                "apple",
                "banana",
                "cherry"
            };
            int result = FibonacciSearch.Search(array, "banana");
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Search_LargeArray_FoundAndNotFound()
        {
            int size = 10000;
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = i;
            }

            int resultFound = FibonacciSearch.Search(array, 5678);
            Assert.AreEqual(5678, resultFound);
            int resultNotFound = FibonacciSearch.Search(array, -10);
            Assert.AreEqual(-1, resultNotFound);
        }

        [TestMethod]
        public void Search_DuplicateElements_Test()
        {
            int[] array = new int[]
            {
                1,
                2,
                2,
                2,
                3
            };
            int result = FibonacciSearch.Search(array, 2);
            Assert.IsTrue(result >= 1 && result <= 3);
        }

        [TestMethod]
        public void Search_AllEqualElements_Test()
        {
            int[] array = new int[]
            {
                5,
                5,
                5,
                5,
                5
            };
            int result = FibonacciSearch.Search(array, 5);
            Assert.IsTrue(result >= 0 && result < array.Length);
        }

        [TestMethod]
        public void Search_CustomClass_Test()
        {
            TestComparable[] array = new TestComparable[]
            {
                new TestComparable(1),
                new TestComparable(3),
                new TestComparable(5),
                new TestComparable(7),
                new TestComparable(9)
            };
            TestComparable target = new TestComparable(7);
            int result = FibonacciSearch.Search(array, target);
            Assert.AreEqual(3, result);
        }

        public class TestComparable : IComparable<TestComparable>
        {
            public int Value;
            public TestComparable(int value)
            {
                this.Value = value;
            }

            public int CompareTo(TestComparable other)
            {
                return this.Value.CompareTo(other.Value);
            }
        }
    }
}