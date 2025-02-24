using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinimaxAlgorithm;
using System.Collections.Generic;
using System;

namespace MinimaxAlgorithmTest
{
    public class TestGameState : IGameState
    {
        public bool IsTerminal { get; set; }
        public int Value { get; set; }
        public List<IGameState> Successors { get; set; }

        public TestGameState(bool isTerminal, int value)
        {
            IsTerminal = isTerminal;
            Value = value;
            Successors = new List<IGameState>();
        }

        public int Evaluate()
        {
            return Value;
        }

        public IEnumerable<IGameState> GetSuccessors()
        {
            return Successors;
        }
    }

    [TestClass]
    public class MinimaxSolverTests
    {
        private MinimaxSolver solver = null !;
        [TestInitialize]
        public void Setup()
        {
            solver = new MinimaxSolver();
        }

        [TestMethod]
        public void TerminalStateTest()
        {
            TestGameState terminalState = new TestGameState(true, 10);
            MinimaxResult result = solver.Minimax(terminalState, true, 3);
            Assert.AreEqual(10, result.Score, "Terminal node evaluation should match its value.");
            Assert.IsNull(result.BestMove, "Terminal node must have null BestMove.");
        }

        [TestMethod]
        public void MaximizingChoiceTest()
        {
            // Create a tree where the maximizing player should choose the higher score.
            TestGameState root = new TestGameState(false, 0);
            TestGameState child1 = new TestGameState(true, 5);
            TestGameState child2 = new TestGameState(true, 15);
            root.Successors.Add(child1);
            root.Successors.Add(child2);
            MinimaxResult result = solver.Minimax(root, true, 1);
            Assert.AreEqual(15, result.Score, "Maximizing should select the child with highest score.");
            Assert.AreEqual(child2, result.BestMove, "Best move should be the child with the highest score.");
        }

        [TestMethod]
        public void MinimizingChoiceTest()
        {
            // Create a tree where the minimizing player should choose the lower score.
            TestGameState root = new TestGameState(false, 0);
            TestGameState child1 = new TestGameState(true, 20);
            TestGameState child2 = new TestGameState(true, 5);
            root.Successors.Add(child1);
            root.Successors.Add(child2);
            MinimaxResult result = solver.Minimax(root, false, 1);
            Assert.AreEqual(5, result.Score, "Minimizing should select the child with lowest score.");
            Assert.AreEqual(child2, result.BestMove, "Best move should be the child with the lowest score.");
        }

        [TestMethod]
        public void LargeTreeTest()
        {
            // Generate a synthetic binary tree to thoroughly test recursion and performance.
            int depth = 4;
            TestGameState root = GenerateTree(true, depth, 0);
            MinimaxResult result = solver.Minimax(root, true, depth);
            Assert.IsNotNull(result, "Result from synthetic large tree should not be null.");
        }

        [TestMethod]
        public void DepthZeroNonTerminalTest()
        {
            // Test that a non-terminal state with depth zero returns its immediate evaluation.
            TestGameState state = new TestGameState(false, 100);
            state.Successors.Add(new TestGameState(true, 200));
            MinimaxResult result = solver.Minimax(state, true, 0);
            Assert.AreEqual(100, result.Score, "When depth is zero, the evaluation of the current state should be returned.");
            Assert.IsNull(result.BestMove, "When depth is zero, BestMove should be null.");
        }

        private TestGameState GenerateTree(bool isMaximizing, int depth, int baseScore)
        {
            if (depth <= 0)
            {
                return new TestGameState(true, baseScore);
            }

            TestGameState node = new TestGameState(false, 0);
            // Create two children with slight variations in their base scores.
            TestGameState child1 = GenerateTree(!isMaximizing, depth - 1, baseScore + 1);
            TestGameState child2 = GenerateTree(!isMaximizing, depth - 1, baseScore - 1);
            node.Successors.Add(child1);
            node.Successors.Add(child2);
            return node;
        }
    }
}