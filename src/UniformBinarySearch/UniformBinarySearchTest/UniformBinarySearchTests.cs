using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniformBinarySearch;

namespace UniformBinarySearchTest
{
    [TestClass]
    public class UniformBinarySearchTests
    {
        [TestMethod]
        public void Test_NullArray_ReturnsMinusOne()
        {
            int result = UniformBinarySearchAlgorithm.Search(null, 10);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Test_EmptyArray_ReturnsMinusOne()
        {
            int[] array = new int[0];
            int result = UniformBinarySearchAlgorithm.Search(array, 10);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Test_SingleElement_Found()
        {
            int[] array = new int[]
            {
                5
            };
            int result = UniformBinarySearchAlgorithm.Search(array, 5);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Test_SingleElement_NotFound()
        {
            int[] array = new int[]
            {
                5
            };
            int result = UniformBinarySearchAlgorithm.Search(array, 10);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Test_MultiElement_FoundAtBeginning()
        {
            int[] array = new int[]
            {
                1,
                3,
                5,
                7,
                9
            };
            int result = UniformBinarySearchAlgorithm.Search(array, 1);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Test_MultiElement_FoundAtEnd()
        {
            int[] array = new int[]
            {
                1,
                3,
                5,
                7,
                9
            };
            int result = UniformBinarySearchAlgorithm.Search(array, 9);
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void Test_MultiElement_NotFound()
        {
            int[] array = new int[]
            {
                1,
                3,
                5,
                7,
                9
            };
            int result = UniformBinarySearchAlgorithm.Search(array, 4);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Test_MultiElement_DuplicateValues()
        {
            int[] array = new int[]
            {
                1,
                3,
                3,
                3,
                5
            };
            int result = UniformBinarySearchAlgorithm.Search(array, 3);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Test_LargeArray()
        {
            int size = 10000;
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = i * 2;
            }

            int target = 1234;
            int expectedIndex = target / 2;
            int result = UniformBinarySearchAlgorithm.Search(array, target);
            Assert.AreEqual(expectedIndex, result);
            target = 1235;
            result = UniformBinarySearchAlgorithm.Search(array, target);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Test_KeyLowerThanMin()
        {
            int[] array = new int[]
            {
                2,
                4,
                6,
                8,
                10
            };
            int result = UniformBinarySearchAlgorithm.Search(array, 1);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Test_KeyHigherThanMax()
        {
            int[] array = new int[]
            {
                2,
                4,
                6,
                8,
                10
            };
            int result = UniformBinarySearchAlgorithm.Search(array, 11);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Test_ElementFoundInMiddle()
        {
            int[] array = new int[]
            {
                2,
                4,
                6,
                8,
                10
            };
            int result = UniformBinarySearchAlgorithm.Search(array, 6);
            Assert.AreEqual(2, result);
        }
    }
}