using Microsoft.VisualStudio.TestTools.UnitTesting;
using KthLargest;
using System;

namespace KthLargestTest
{
    [TestClass]
    public class KthLargestFinderTest
    {
        [TestMethod]
        public void TestNullSequence()
        {
            int? result = KthLargestFinder.FindKthLargest(null, 1);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestEmptyArray()
        {
            int[] arr = new int[0];
            int? result = KthLargestFinder.FindKthLargest(arr, 1);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestInvalidK_Zero()
        {
            int[] arr = new int[]
            {
                1,
                2,
                3
            };
            int? result = KthLargestFinder.FindKthLargest(arr, 0);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestInvalidK_GreaterThanLength()
        {
            int[] arr = new int[]
            {
                1,
                2,
                3
            };
            int? result = KthLargestFinder.FindKthLargest(arr, 4);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestSingleElement()
        {
            int[] arr = new int[]
            {
                10
            };
            int? result = KthLargestFinder.FindKthLargest(arr, 1);
            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Value);
        }

        [TestMethod]
        public void TestMultipleElements()
        {
            int[] arr = new int[]
            {
                3,
                1,
                5,
                2,
                4
            };
            int? result = KthLargestFinder.FindKthLargest(arr, 2);
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Value);
        }

        [TestMethod]
        public void TestDuplicates()
        {
            int[] arr = new int[]
            {
                3,
                3,
                1,
                2,
                2
            };
            int? result = KthLargestFinder.FindKthLargest(arr, 3);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Value);
        }

        [TestMethod]
        public void TestLargeRandomArray()
        {
            System.Random random = new System.Random(0);
            int size = 1000;
            int[] arr = new int[size];
            for (int i = 0; i < size; i++)
            {
                arr[i] = random.Next(-100000, 100000);
            }

            int k = 10;
            int? result = KthLargestFinder.FindKthLargest(arr, k);
            System.Array.Sort(arr);
            int expected = arr[arr.Length - k];
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod]
        public void TestKEqualsLength()
        {
            int[] arr = new int[]
            {
                5,
                3,
                1,
                2,
                4
            };
            int? result = KthLargestFinder.FindKthLargest(arr, arr.Length);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Value);
        }

        [TestMethod]
        public void TestAllIdenticalElements()
        {
            int[] arr = new int[]
            {
                7,
                7,
                7,
                7
            };
            int? result = KthLargestFinder.FindKthLargest(arr, 3);
            Assert.IsNotNull(result);
            Assert.AreEqual(7, result.Value);
        }

        [TestMethod]
        public void TestInvalidK_Negative()
        {
            int[] arr = new int[]
            {
                1,
                2,
                3
            };
            int? result = KthLargestFinder.FindKthLargest(arr, -1);
            Assert.IsNull(result);
        }
    }
}