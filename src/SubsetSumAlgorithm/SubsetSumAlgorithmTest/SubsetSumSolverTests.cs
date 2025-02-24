using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubsetSumAlgorithm;

namespace SubsetSumAlgorithmTest
{
    [TestClass]
    public class SubsetSumSolverTests
    {
        [TestMethod]
        public void HasSubsetSum_NullArray_ReturnsFalse()
        {
            int[]? numbers = null;
            bool result = SubsetSumSolver.HasSubsetSum(numbers, 10);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HasSubsetSum_EmptyArray_TargetZero_ReturnsTrue()
        {
            int[] numbers = new int[0];
            bool result = SubsetSumSolver.HasSubsetSum(numbers, 0);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HasSubsetSum_EmptyArray_NonZeroTarget_ReturnsFalse()
        {
            int[] numbers = new int[0];
            bool result = SubsetSumSolver.HasSubsetSum(numbers, 10);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HasSubsetSum_SingleElementEqualsTarget_ReturnsTrue()
        {
            int[] numbers = new int[]
            {
                5
            };
            bool result = SubsetSumSolver.HasSubsetSum(numbers, 5);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HasSubsetSum_SingleElementNotTarget_ReturnsFalse()
        {
            int[] numbers = new int[]
            {
                5
            };
            bool result = SubsetSumSolver.HasSubsetSum(numbers, 3);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HasSubsetSum_MultipleElements_TargetFound_ReturnsTrue()
        {
            int[] numbers = new int[]
            {
                3,
                34,
                4,
                12,
                5,
                2
            };
            bool result = SubsetSumSolver.HasSubsetSum(numbers, 9);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HasSubsetSum_MultipleElements_TargetNotFound_ReturnsFalse()
        {
            int[] numbers = new int[]
            {
                3,
                34,
                4,
                12,
                5,
                2
            };
            bool result = SubsetSumSolver.HasSubsetSum(numbers, 30);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HasSubsetSum_RandomLargeTest_TargetSumExists_ReturnsTrue()
        {
            int[] numbers = new int[20];
            for (int i = 0; i < 20; i++)
            {
                numbers[i] = i + 1;
            }

            bool result = SubsetSumSolver.HasSubsetSum(numbers, 100);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HasSubsetSum_RandomLargeTest_TargetSumNotExists_ReturnsFalse()
        {
            int[] numbers = new int[20];
            for (int i = 0; i < 20; i++)
            {
                numbers[i] = i + 1;
            }

            bool result = SubsetSumSolver.HasSubsetSum(numbers, 211);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HasSubsetSum_NegativeNumbers_ReturnsCorrectResults()
        {
            int[] numbers = new int[]
            {
                -7,
                -3,
                -2,
                5,
                8
            };
            bool result = SubsetSumSolver.HasSubsetSum(numbers, 0);
            Assert.IsTrue(result);
        }
    }
}