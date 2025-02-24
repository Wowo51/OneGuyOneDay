using Microsoft.VisualStudio.TestTools.UnitTesting;
using JumpSearch;
using System;

namespace JumpSearchTest
{
    [TestClass]
    public class JumpSearchAlgorithmTests
    {
        [TestMethod]
        public void Test_NullArray_ReturnsMinusOne()
        {
            int result = JumpSearchAlgorithm.Search<int>(null, 5);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Test_EmptyArray_ReturnsMinusOne()
        {
            int[] array = new int[0];
            int result = JumpSearchAlgorithm.Search<int>(array, 10);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Test_TargetNotFound_ReturnsMinusOne()
        {
            int[] array = new int[]
            {
                1,
                3,
                5,
                7,
                9
            };
            int result = JumpSearchAlgorithm.Search<int>(array, 4);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Test_TargetFound_ReturnsIndex()
        {
            int[] array = new int[]
            {
                1,
                3,
                5,
                7,
                9
            };
            int result = JumpSearchAlgorithm.Search<int>(array, 7);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Test_FirstElement_ReturnsIndexZero()
        {
            int[] array = new int[]
            {
                2,
                4,
                6,
                8,
                10
            };
            int result = JumpSearchAlgorithm.Search<int>(array, 2);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Test_LastElement_ReturnsLastIndex()
        {
            int[] array = new int[]
            {
                2,
                4,
                6,
                8,
                10
            };
            int result = JumpSearchAlgorithm.Search<int>(array, 10);
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void Test_SingleElementMatch_ReturnsZero()
        {
            int[] array = new int[]
            {
                5
            };
            int result = JumpSearchAlgorithm.Search<int>(array, 5);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Test_DuplicateElements_ReturnsFirstOccurrence()
        {
            int[] array = new int[]
            {
                1,
                2,
                2,
                2,
                3
            };
            int result = JumpSearchAlgorithm.Search<int>(array, 2);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Test_LargeDataSet_ReturnsCorrectIndex()
        {
            System.Int32 dataSize = 10000;
            int[] array = new int[dataSize];
            for (int index = 0; index < dataSize; index++)
            {
                array[index] = index;
            }

            int target = dataSize / 2;
            int result = JumpSearchAlgorithm.Search<int>(array, target);
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void Test_NegativeNumbers_ReturnsCorrectIndex()
        {
            int[] array = new int[]
            {
                -20,
                -10,
                0,
                10,
                20
            };
            int result = JumpSearchAlgorithm.Search<int>(array, -10);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Test_AllElementsIdentical_ReturnsFirstOccurrence()
        {
            int[] array = new int[]
            {
                7,
                7,
                7,
                7,
                7
            };
            int result = JumpSearchAlgorithm.Search<int>(array, 7);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Test_StringArray_ReturnsCorrectIndex()
        {
            string[] array = new string[]
            {
                "apple",
                "banana",
                "cherry",
                "date",
                "fig"
            };
            int result = JumpSearchAlgorithm.Search<string>(array, "cherry");
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void Test_TargetTooSmall_ReturnsMinusOne()
        {
            int[] array = new int[]
            {
                1,
                2,
                3,
                4,
                5
            };
            int result = JumpSearchAlgorithm.Search<int>(array, 0);
            Assert.AreEqual(-1, result);
        }
    }
}