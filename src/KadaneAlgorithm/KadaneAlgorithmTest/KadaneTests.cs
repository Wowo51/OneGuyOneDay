using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KadaneAlgorithm;

namespace KadaneAlgorithmTest
{
    [TestClass]
    public class KadaneTests
    {
        [TestMethod]
        public void TestNullInput()
        {
            int[]? input = null;
            KadaneResult result = Kadane.FindMaxSubArray(input);
            Assert.AreEqual(0, result.MaxSum);
            Assert.AreEqual(-1, result.StartIndex);
            Assert.AreEqual(-1, result.EndIndex);
            Assert.AreEqual(0, result.SubArray.Length);
        }

        [TestMethod]
        public void TestEmptyArray()
        {
            int[] input = new int[0];
            KadaneResult result = Kadane.FindMaxSubArray(input);
            Assert.AreEqual(0, result.MaxSum);
            Assert.AreEqual(-1, result.StartIndex);
            Assert.AreEqual(-1, result.EndIndex);
            Assert.AreEqual(0, result.SubArray.Length);
        }

        [TestMethod]
        public void TestSingleElement()
        {
            int[] input = new int[]
            {
                5
            };
            KadaneResult result = Kadane.FindMaxSubArray(input);
            Assert.AreEqual(5, result.MaxSum);
            Assert.AreEqual(0, result.StartIndex);
            Assert.AreEqual(0, result.EndIndex);
            CollectionAssert.AreEqual(new int[] { 5 }, result.SubArray);
        }

        [TestMethod]
        public void TestAllNegatives()
        {
            int[] input = new int[]
            {
                -3,
                -5,
                -2,
                -8
            };
            KadaneResult result = Kadane.FindMaxSubArray(input);
            Assert.AreEqual(-2, result.MaxSum);
            Assert.AreEqual(2, result.StartIndex);
            Assert.AreEqual(2, result.EndIndex);
            CollectionAssert.AreEqual(new int[] { -2 }, result.SubArray);
        }

        [TestMethod]
        public void TestTypicalCase()
        {
            int[] input = new int[]
            {
                -2,
                1,
                -3,
                4,
                -1,
                2,
                1,
                -5,
                4
            };
            KadaneResult result = Kadane.FindMaxSubArray(input);
            Assert.AreEqual(6, result.MaxSum);
            Assert.AreEqual(3, result.StartIndex);
            Assert.AreEqual(6, result.EndIndex);
            CollectionAssert.AreEqual(new int[] { 4, -1, 2, 1 }, result.SubArray);
        }

        [TestMethod]
        public void TestAllPositives()
        {
            int[] input = new int[]
            {
                1,
                2,
                3,
                4,
                5
            };
            KadaneResult result = Kadane.FindMaxSubArray(input);
            Assert.AreEqual(15, result.MaxSum);
            Assert.AreEqual(0, result.StartIndex);
            Assert.AreEqual(4, result.EndIndex);
            CollectionAssert.AreEqual(input, result.SubArray);
        }

        [TestMethod]
        public void TestLargeRandomArray()
        {
            int length = 500;
            int[] input = new int[length];
            Random random = new Random(42);
            for (int i = 0; i < length; i++)
            {
                input[i] = random.Next(-100, 101);
            }

            int bruteForceMaxSum = BruteForceMaxSum(input);
            KadaneResult result = Kadane.FindMaxSubArray(input);
            Assert.AreEqual(bruteForceMaxSum, result.MaxSum);
            int subArraySum = 0;
            for (int i = 0; i < result.SubArray.Length; i++)
            {
                subArraySum += result.SubArray[i];
            }

            Assert.AreEqual(result.MaxSum, subArraySum);
        }

        private int BruteForceMaxSum(int[] input)
        {
            if (input == null || input.Length == 0)
            {
                return 0;
            }

            int maxSum = input[0];
            for (int i = 0; i < input.Length; i++)
            {
                int currentSum = 0;
                for (int j = i; j < input.Length; j++)
                {
                    currentSum += input[j];
                    if (currentSum > maxSum)
                    {
                        maxSum = currentSum;
                    }
                }
            }

            return maxSum;
        }
    }
}