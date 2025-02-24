using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChainMatrixMultiplication;

namespace ChainMatrixMultiplicationTest
{
    [TestClass]
    public class MatrixChainMultiplicationSolverTests
    {
        [TestMethod]
        public void Test_NullInput()
        {
            int[]? dims = null;
            (int cost, string order) result = MatrixChainMultiplicationSolver.ComputeOptimalOrder(dims);
            Assert.AreEqual(0, result.cost);
            Assert.AreEqual(string.Empty, result.order);
        }

        [TestMethod]
        public void Test_SingleElement()
        {
            int[] dims = new int[]
            {
                42
            };
            (int cost, string order) result = MatrixChainMultiplicationSolver.ComputeOptimalOrder(dims);
            Assert.AreEqual(0, result.cost);
            Assert.AreEqual(string.Empty, result.order);
        }

        [TestMethod]
        public void Test_TwoMatrices()
        {
            int[] dims = new int[]
            {
                10,
                20
            };
            (int cost, string order) result = MatrixChainMultiplicationSolver.ComputeOptimalOrder(dims);
            Assert.AreEqual(0, result.cost);
            Assert.AreEqual("A1", result.order);
        }

        [TestMethod]
        public void Test_SampleInput()
        {
            int[] dims = new int[]
            {
                30,
                35,
                15,
                5,
                10,
                20,
                25
            };
            (int cost, string order) result = MatrixChainMultiplicationSolver.ComputeOptimalOrder(dims);
            Assert.AreEqual(15125, result.cost);
            Assert.AreEqual("((A1 x (A2 x A3)) x ((A4 x A5) x A6))", result.order);
        }

        [TestMethod]
        public void Test_SyntheticLargeInput()
        {
            Random random = new Random(0);
            int length = 11;
            int[] dims = new int[length];
            for (int i = 0; i < length; i++)
            {
                dims[i] = random.Next(1, 101);
            }

            (int cost, string order) result = MatrixChainMultiplicationSolver.ComputeOptimalOrder(dims);
            Assert.IsTrue(result.cost > 0, "Expected cost to be positive for chain length greater than 2");
            Assert.IsTrue(result.order.Contains("("), "Expected order string to contain parentheses");
        }
    }
}