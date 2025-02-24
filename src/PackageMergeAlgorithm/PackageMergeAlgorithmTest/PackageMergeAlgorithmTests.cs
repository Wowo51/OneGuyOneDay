using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PackageMergeAlgorithm;

namespace PackageMergeAlgorithmTest
{
    [TestClass]
    public class PackageMergeAlgorithmTests
    {
        [TestMethod]
        public void TestOptimize_NullInput_ReturnsEmptyArray()
        {
            int[]? input = null;
            int[] result = PackageMergeOptimizer.Optimize(input, 10);
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(0, result.Length, "Result length must be 0.");
        }

        [TestMethod]
        public void TestOptimize_EmptyArray_ReturnsEmptyArray()
        {
            int[] input = new int[0];
            int[] result = PackageMergeOptimizer.Optimize(input, 10);
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(0, result.Length, "Result length must be 0.");
        }

        [TestMethod]
        public void TestOptimize_SingleElement_ReturnsCodeLengthOne()
        {
            int[] input = new int[]
            {
                42
            };
            int[] result = PackageMergeOptimizer.Optimize(input, 5);
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(1, result.Length, "Result length must equal input length.");
            Assert.AreEqual(1, result[0], "For a single element, code length should be 1.");
        }

        [TestMethod]
        public void TestOptimize_NoAdjustmentNeeded()
        {
            int[] input = new int[]
            {
                5,
                9,
                12,
                13,
                16,
                45
            };
            int maxLength = 15;
            int[] result = PackageMergeOptimizer.Optimize(input, maxLength);
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(input.Length, result.Length, "Result length must equal input length.");
            int maxFound = 0;
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] > maxFound)
                {
                    maxFound = result[i];
                }
            }

            Assert.IsTrue(maxFound <= maxLength, "Max code length should be less than or equal to maxLength.");
        }

        [TestMethod]
        public void TestOptimize_AdjustmentNeeded()
        {
            int[] input = new int[]
            {
                1,
                1,
                1,
                1,
                1,
                1,
                1,
                1,
                1,
                1
            };
            int maxLength = 2;
            int[] result = PackageMergeOptimizer.Optimize(input, maxLength);
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(input.Length, result.Length, "Result length must equal input length.");
            for (int i = 0; i < result.Length; i++)
            {
                Assert.IsTrue(result[i] <= maxLength, "Each code length must be <= maxLength.");
                Assert.IsTrue(result[i] > 0, "Each code length should be > 0.");
            }
        }

        [TestMethod]
        public void TestOptimize_LargeSyntheticData()
        {
            int largeSize = 1000;
            int[] input = new int[largeSize];
            Random randomGenerator = new Random(0);
            for (int i = 0; i < largeSize; i++)
            {
                input[i] = randomGenerator.Next(1, 1001);
            }

            int maxLength = 15;
            int[] result = PackageMergeOptimizer.Optimize(input, maxLength);
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(largeSize, result.Length, "Result length must equal input length.");
            for (int i = 0; i < largeSize; i++)
            {
                Assert.IsTrue(result[i] > 0, "Code length must be positive.");
                Assert.IsTrue(result[i] <= maxLength, "Each code length must be <= maxLength.");
            }
        }
    }
}