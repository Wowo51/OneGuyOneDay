using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlphaBetaPruning;

namespace AlphaBetaPruningTest
{
    [TestClass]
    public class AlphaBetaPrunerTests
    {
        [TestMethod]
        public void Test_NullNode_ReturnsZero()
        {
            int result = AlphaBetaPruner.AlphaBeta(null, int.MinValue, int.MaxValue, true);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Test_TerminalNode_ReturnsValue()
        {
            TestGameTreeNode node = new TestGameTreeNode
            {
                IsTerminal = true,
                Value = 42
            };
            int result = AlphaBetaPruner.AlphaBeta(node, int.MinValue, int.MaxValue, true);
            Assert.AreEqual(42, result);
        }

        [TestMethod]
        public void Test_TwoLevelMaximizing()
        {
            TestGameTreeNode root = new TestGameTreeNode
            {
                IsTerminal = false
            };
            TestGameTreeNode child1 = new TestGameTreeNode
            {
                IsTerminal = true,
                Value = 3
            };
            TestGameTreeNode child2 = new TestGameTreeNode
            {
                IsTerminal = true,
                Value = 5
            };
            root.Children.Add(child1);
            root.Children.Add(child2);
            int result = AlphaBetaPruner.AlphaBeta(root, int.MinValue, int.MaxValue, true);
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void Test_MixedTree()
        {
            // Tree structure:
            //            Root (maximizing)
            //           /          \
            //      ChildA(minimizing)   ChildB(minimizing)
            //      /    \                   /       \
            //     3      12               8         2
            TestGameTreeNode root = new TestGameTreeNode
            {
                IsTerminal = false
            };
            TestGameTreeNode childA = new TestGameTreeNode
            {
                IsTerminal = false
            };
            TestGameTreeNode childB = new TestGameTreeNode
            {
                IsTerminal = false
            };
            TestGameTreeNode leaf1 = new TestGameTreeNode
            {
                IsTerminal = true,
                Value = 3
            };
            TestGameTreeNode leaf2 = new TestGameTreeNode
            {
                IsTerminal = true,
                Value = 12
            };
            TestGameTreeNode leaf3 = new TestGameTreeNode
            {
                IsTerminal = true,
                Value = 8
            };
            TestGameTreeNode leaf4 = new TestGameTreeNode
            {
                IsTerminal = true,
                Value = 2
            };
            childA.Children.Add(leaf1);
            childA.Children.Add(leaf2);
            childB.Children.Add(leaf3);
            childB.Children.Add(leaf4);
            root.Children.Add(childA);
            root.Children.Add(childB);
            int result = AlphaBetaPruner.AlphaBeta(root, int.MinValue, int.MaxValue, true);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Test_LargeBalancedTree()
        {
            int leafCounter = 1;
            TestGameTreeNode root = GenerateBalancedTree(4, 3, true, ref leafCounter);
            int expected = ComputeMinimax(root, true);
            int result = AlphaBetaPruner.AlphaBeta(root, int.MinValue, int.MaxValue, true);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_EmptyNonTerminal_Maximizing()
        {
            TestGameTreeNode node = new TestGameTreeNode
            {
                IsTerminal = false
            };
            int result = AlphaBetaPruner.AlphaBeta(node, int.MinValue, int.MaxValue, true);
            Assert.AreEqual(int.MinValue, result);
        }

        [TestMethod]
        public void Test_EmptyNonTerminal_Minimizing()
        {
            TestGameTreeNode node = new TestGameTreeNode
            {
                IsTerminal = false
            };
            int result = AlphaBetaPruner.AlphaBeta(node, int.MinValue, int.MaxValue, false);
            Assert.AreEqual(int.MaxValue, result);
        }

        private TestGameTreeNode GenerateBalancedTree(int depth, int branchingFactor, bool maximizing, ref int leafCounter)
        {
            TestGameTreeNode node = new TestGameTreeNode();
            if (depth == 0)
            {
                node.IsTerminal = true;
                node.Value = leafCounter;
                leafCounter = leafCounter + 1;
                return node;
            }
            else
            {
                node.IsTerminal = false;
                for (int i = 0; i < branchingFactor; i++)
                {
                    TestGameTreeNode child = GenerateBalancedTree(depth - 1, branchingFactor, !maximizing, ref leafCounter);
                    node.Children.Add(child);
                }

                return node;
            }
        }

        private int ComputeMinimax(IGameTreeNode node, bool maximizing)
        {
            if (node.IsTerminal)
            {
                return node.Value;
            }

            if (maximizing)
            {
                int best = int.MinValue;
                foreach (IGameTreeNode child in node.GetChildren())
                {
                    int score = ComputeMinimax(child, false);
                    if (score > best)
                    {
                        best = score;
                    }
                }

                return best;
            }
            else
            {
                int best = int.MaxValue;
                foreach (IGameTreeNode child in node.GetChildren())
                {
                    int score = ComputeMinimax(child, true);
                    if (score < best)
                    {
                        best = score;
                    }
                }

                return best;
            }
        }
    }

    public class TestGameTreeNode : IGameTreeNode
    {
        public bool IsTerminal { get; set; }
        public int Value { get; set; }
        public List<IGameTreeNode> Children { get; set; } = new List<IGameTreeNode>();

        public IEnumerable<IGameTreeNode> GetChildren()
        {
            return Children;
        }
    }
}