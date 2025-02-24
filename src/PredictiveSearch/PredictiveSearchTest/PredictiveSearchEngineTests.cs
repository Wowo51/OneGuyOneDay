using Microsoft.VisualStudio.TestTools.UnitTesting;
using PredictiveSearch;

namespace PredictiveSearchTest
{
    [TestClass]
    public class PredictiveSearchEngineTests
    {
        [TestMethod]
        public void InterpolatedSearch_NullArray_ReturnsMinusOne()
        {
            int[]? testArray = null;
            int result = PredictiveSearchEngine.InterpolatedSearch(testArray, 5);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void InterpolatedSearch_EmptyArray_ReturnsMinusOne()
        {
            int[] testArray = new int[0];
            int result = PredictiveSearchEngine.InterpolatedSearch(testArray, 10);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void InterpolatedSearch_SingleElementMatch_ReturnsZero()
        {
            int[] testArray = new int[]
            {
                10
            };
            int result = PredictiveSearchEngine.InterpolatedSearch(testArray, 10);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void InterpolatedSearch_SingleElementNoMatch_ReturnsMinusOne()
        {
            int[] testArray = new int[]
            {
                10
            };
            int result = PredictiveSearchEngine.InterpolatedSearch(testArray, 20);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void InterpolatedSearch_MultiElementFound_ReturnsCorrectIndex()
        {
            int[] testArray = new int[]
            {
                1,
                3,
                5,
                7,
                9
            };
            int result = PredictiveSearchEngine.InterpolatedSearch(testArray, 7);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void InterpolatedSearch_MultiElementNotFound_ReturnsMinusOne()
        {
            int[] testArray = new int[]
            {
                1,
                3,
                5,
                7,
                9
            };
            int result = PredictiveSearchEngine.InterpolatedSearch(testArray, 4);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void InterpolatedSearch_UniformArrayMatch_ReturnsZero()
        {
            int[] testArray = new int[]
            {
                2,
                2,
                2,
                2
            };
            int result = PredictiveSearchEngine.InterpolatedSearch(testArray, 2);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void InterpolatedSearch_UniformArrayNoMatch_ReturnsMinusOne()
        {
            int[] testArray = new int[]
            {
                2,
                2,
                2,
                2
            };
            int result = PredictiveSearchEngine.InterpolatedSearch(testArray, 3);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void InterpolatedSearch_LargeArrayFound_ReturnsCorrectIndex()
        {
            int[] testArray = new int[1000];
            for (int index = 0; index < 1000; index++)
            {
                testArray[index] = index * 2;
            }

            int result = PredictiveSearchEngine.InterpolatedSearch(testArray, 500);
            Assert.AreEqual(250, result);
        }

        [TestMethod]
        public void InterpolatedSearch_LargeArrayNotFound_ReturnsMinusOne()
        {
            int[] testArray = new int[1000];
            for (int index = 0; index < 1000; index++)
            {
                testArray[index] = index * 2;
            }

            int result = PredictiveSearchEngine.InterpolatedSearch(testArray, 501);
            Assert.AreEqual(-1, result);
        }
    }
}