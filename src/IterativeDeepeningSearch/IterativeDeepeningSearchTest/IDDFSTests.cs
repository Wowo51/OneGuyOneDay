using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IterativeDeepeningSearch;

namespace IterativeDeepeningSearchTest
{
    [TestClass]
    public class IDDFSTests
    {
        [TestMethod]
        public void Test_StartNodeIsGoal()
        {
            int start = 42;
            List<int>? result = IDDFS.Search(start, (int x) => new List<int>(), (int x) => x == 42);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(42, result[0]);
        }

        [TestMethod]
        public void Test_LinearChain()
        {
            int target = 10;
            int start = 1;
            List<int>? result = IDDFS.Search(start, (int x) =>
            {
                return (x < target) ? new List<int>
                {
                    x + 1
                }

                : new List<int>();
            }, (int x) => x == target);
            Assert.IsNotNull(result);
            List<int> expected = new List<int>();
            for (int i = 1; i <= target; i++)
            {
                expected.Add(i);
            }

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_BranchingTreeSearch()
        {
            TreeNode root = new TreeNode("A");
            TreeNode nodeB = new TreeNode("B");
            TreeNode nodeC = new TreeNode("C");
            TreeNode nodeD = new TreeNode("D");
            TreeNode nodeE = new TreeNode("E");
            root.Children.Add(nodeB);
            root.Children.Add(nodeC);
            nodeB.Children.Add(nodeD);
            nodeC.Children.Add(nodeE);
            List<TreeNode>? result = IDDFS.Search(root, (TreeNode node) => node.Children, (TreeNode node) => node.Name == "E");
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("A", result[0].Name);
            Assert.AreEqual("C", result[1].Name);
            Assert.AreEqual("E", result[2].Name);
        }

        [TestMethod]
        public void Test_LargeLinearChain()
        {
            int target = 1000;
            int start = 1;
            List<int>? result = IDDFS.Search(start, (int x) =>
            {
                return (x < target) ? new List<int>
                {
                    x + 1
                }

                : new List<int>();
            }, (int x) => x == target);
            Assert.IsNotNull(result);
            Assert.AreEqual(target, result.Count);
            for (int i = 0; i < target; i++)
            {
                Assert.AreEqual(i + 1, result[i]);
            }
        }

        private class TreeNode
        {
            public string Name;
            public List<TreeNode> Children;
            public TreeNode(string name)
            {
                Name = name;
                Children = new List<TreeNode>();
            }
        }
    }
}