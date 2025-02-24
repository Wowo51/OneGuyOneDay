using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarkAndSweep;

namespace MarkAndSweepTest
{
    [TestClass]
    public class GarbageCollectorTests
    {
        [TestMethod]
        public void TestMarking_AllNodesMarked()
        {
            ObjectNode root = new ObjectNode("Root");
            ObjectNode childA = new ObjectNode("ChildA");
            ObjectNode childB = new ObjectNode("ChildB");
            root.AddChild(childA);
            childA.AddChild(childB);
            GarbageCollector.Mark(new List<ObjectNode> { root });
            Assert.IsTrue(root.Marked, "Root should be marked.");
            Assert.IsTrue(childA.Marked, "ChildA should be marked.");
            Assert.IsTrue(childB.Marked, "ChildB should be marked.");
        }

        [TestMethod]
        public void TestSweep_RemovesUnmarkedNodes()
        {
            ObjectNode root = new ObjectNode("Root");
            ObjectNode child = new ObjectNode("Child");
            ObjectNode orphan = new ObjectNode("Orphan");
            root.AddChild(child);
            List<ObjectNode> heap = new List<ObjectNode>();
            heap.Add(root);
            heap.Add(child);
            heap.Add(orphan);
            GarbageCollector.Mark(new List<ObjectNode> { root });
            List<ObjectNode> surviving = GarbageCollector.Sweep(heap);
            Assert.AreEqual(2, surviving.Count, "Surviving count should be 2.");
            foreach (ObjectNode node in surviving)
            {
                Assert.IsFalse(node.Marked, "Node mark should be reset after sweep.");
            }
        }

        [TestMethod]
        public void TestMarking_WithCycle()
        {
            ObjectNode node1 = new ObjectNode("Node1");
            ObjectNode node2 = new ObjectNode("Node2");
            node1.AddChild(node2);
            node2.AddChild(node1);
            GarbageCollector.Mark(new List<ObjectNode> { node1 });
            Assert.IsTrue(node1.Marked, "Node1 should be marked.");
            Assert.IsTrue(node2.Marked, "Node2 should be marked.");
            List<ObjectNode> nodes = new List<ObjectNode>();
            nodes.Add(node1);
            nodes.Add(node2);
            GarbageCollector.Sweep(nodes);
            Assert.IsFalse(node1.Marked, "Node1 mark should be reset.");
            Assert.IsFalse(node2.Marked, "Node2 mark should be reset.");
        }

        [TestMethod]
        public void TestLargeGraph_MarkAndSweepEfficiency()
        {
            ObjectNode largeRoot = GenerateBinaryTree("Node", 0, 6);
            List<ObjectNode> allNodes = new List<ObjectNode>();
            CollectNodes(largeRoot, allNodes);
            int totalNodes = allNodes.Count;
            GarbageCollector.Mark(new List<ObjectNode> { largeRoot });
            List<ObjectNode> surviving = GarbageCollector.Sweep(allNodes);
            Assert.AreEqual(totalNodes, surviving.Count, "All nodes in the connected graph should survive.");
            foreach (ObjectNode node in surviving)
            {
                Assert.IsFalse(node.Marked, "Node mark should be reset after sweep.");
            }
        }

        private ObjectNode GenerateBinaryTree(string prefix, int currentDepth, int maxDepth)
        {
            ObjectNode node = new ObjectNode(prefix + "_" + currentDepth);
            if (currentDepth < maxDepth)
            {
                ObjectNode left = GenerateBinaryTree(prefix + "L", currentDepth + 1, maxDepth);
                ObjectNode right = GenerateBinaryTree(prefix + "R", currentDepth + 1, maxDepth);
                node.AddChild(left);
                node.AddChild(right);
            }

            return node;
        }

        private void CollectNodes(ObjectNode root, List<ObjectNode> collected)
        {
            collected.Add(root);
            foreach (ObjectNode child in root.Children)
            {
                CollectNodes(child, collected);
            }
        }
    }
}