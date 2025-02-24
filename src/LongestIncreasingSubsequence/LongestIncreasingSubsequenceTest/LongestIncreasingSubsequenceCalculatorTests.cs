using Microsoft.VisualStudio.TestTools.UnitTesting;
using LongestIncreasingSubsequence;

namespace LongestIncreasingSubsequenceTest
{
    [TestClass]
    public class LongestIncreasingSubsequenceCalculatorTests
    {
        [TestMethod]
        public void FindLIS_NullInput_ReturnsEmpty()
        {
            int[]? input = null;
            int[] result = LongestIncreasingSubsequenceCalculator.FindLIS(input);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void FindLIS_EmptyArray_ReturnsEmpty()
        {
            int[] input = new int[0];
            int[] result = LongestIncreasingSubsequenceCalculator.FindLIS(input);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void FindLIS_SingleElement_ReturnsSameElement()
        {
            int[] input = new int[]
            {
                42
            };
            int[] result = LongestIncreasingSubsequenceCalculator.FindLIS(input);
            CollectionAssert.AreEqual(new int[] { 42 }, result);
        }

        [TestMethod]
        public void FindLIS_SampleSequence_ReturnsCorrectSubsequence()
        {
            int[] input = new int[]
            {
                10,
                22,
                9,
                33,
                21,
                50,
                41,
                60
            };
            int[] expected = new int[]
            {
                10,
                22,
                33,
                50,
                60
            };
            int[] result = LongestIncreasingSubsequenceCalculator.FindLIS(input);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void FindLIS_AllEqualElements_ReturnsSingleElement()
        {
            int[] input = new int[]
            {
                5,
                5,
                5,
                5
            };
            int[] result = LongestIncreasingSubsequenceCalculator.FindLIS(input);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(5, result[0]);
        }

        [TestMethod]
        public void FindLIS_StrictlyIncreasing_ReturnsSameArray()
        {
            int[] input = new int[]
            {
                1,
                2,
                3,
                4,
                5
            };
            int[] result = LongestIncreasingSubsequenceCalculator.FindLIS(input);
            CollectionAssert.AreEqual(input, result);
        }

        [TestMethod]
        public void FindLIS_StrictlyDecreasing_ReturnsFirstElement()
        {
            int[] input = new int[]
            {
                5,
                4,
                3,
                2,
                1
            };
            int[] result = LongestIncreasingSubsequenceCalculator.FindLIS(input);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(5, result[0]);
        }

        [TestMethod]
        public void FindLIS_RandomLargeData_ReturnsValidSubsequence()
        {
            System.Random random = new System.Random(12345);
            int size = 1000;
            int[] input = new int[size];
            for (int i = 0; i < size; i++)
            {
                input[i] = random.Next(0, 10000);
            }

            int[] result = LongestIncreasingSubsequenceCalculator.FindLIS(input);
            for (int i = 1; i < result.Length; i++)
            {
                Assert.IsTrue(result[i - 1] < result[i]);
            }

            int lastIndex = -1;
            for (int i = 0; i < result.Length; i++)
            {
                bool found = false;
                for (int j = lastIndex + 1; j < input.Length; j++)
                {
                    if (input[j] == result[i])
                    {
                        lastIndex = j;
                        found = true;
                        break;
                    }
                }

                Assert.IsTrue(found);
            }
        }
    }
}