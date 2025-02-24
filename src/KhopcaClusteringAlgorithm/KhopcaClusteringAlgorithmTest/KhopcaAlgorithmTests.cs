using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KhopcaClusteringAlgorithm;

namespace KhopcaClusteringAlgorithmTest
{
    [TestClass]
    public class KhopcaAlgorithmTests
    {
        [TestMethod]
        public void TestNodeDistance()
        {
            Node nodeA = new Node(0, 10, 0, 0);
            Node nodeB = new Node(1, 20, 3, 4);
            double distance = nodeA.DistanceTo(nodeB);
            Assert.AreEqual(5.0, distance, 0.001);
        }

        [TestMethod]
        public void TestAlgorithmSimple()
        {
            Node node0 = new Node(0, 10, 0, 0);
            Node node1 = new Node(1, 20, 10, 0);
            Node node2 = new Node(2, 30, 0, 10);
            // Establish a complete graph: every node is neighbor to the others.
            node0.Neighbors.Add(node1);
            node0.Neighbors.Add(node2);
            node1.Neighbors.Add(node0);
            node1.Neighbors.Add(node2);
            node2.Neighbors.Add(node0);
            node2.Neighbors.Add(node1);
            List<Node> nodes = new List<Node>
            {
                node0,
                node1,
                node2
            };
            KHOPCAAlgorithm algorithm = new KHOPCAAlgorithm(nodes);
            algorithm.RunIteration();
            // Expect node with highest weight (node2) to be elected cluster head
            Assert.AreEqual(2, node0.ClusterId);
            Assert.AreEqual(2, node1.ClusterId);
            Assert.AreEqual(2, node2.ClusterId);
            Assert.IsFalse(node0.IsClusterHead);
            Assert.IsFalse(node1.IsClusterHead);
            Assert.IsTrue(node2.IsClusterHead);
        }

        [TestMethod]
        public void TestAlgorithmNoNeighbors()
        {
            // Create nodes with no neighbor connectivity.
            Node node0 = new Node(0, 15, 0, 0);
            Node node1 = new Node(1, 10, 10, 0);
            Node node2 = new Node(2, 20, 0, 10);
            // Do not add any neighbors.
            List<Node> nodes = new List<Node>
            {
                node0,
                node1,
                node2
            };
            KHOPCAAlgorithm algorithm = new KHOPCAAlgorithm(nodes);
            algorithm.RunIteration();
            // When isolated, every node should remain its own cluster head.
            Assert.AreEqual(0, node0.ClusterId);
            Assert.AreEqual(1, node1.ClusterId);
            Assert.AreEqual(2, node2.ClusterId);
            Assert.IsTrue(node0.IsClusterHead);
            Assert.IsTrue(node1.IsClusterHead);
            Assert.IsTrue(node2.IsClusterHead);
        }

        [TestMethod]
        public void TestSimulatorAllNeighbors()
        {
            // With a high communication range, every node should detect all others.
            int numNodes = 5;
            double commRange = 500.0; // Ensures all nodes (within a 100x100 area) are in range.
            NetworkSimulator simulator = new NetworkSimulator(numNodes, commRange);
            simulator.UpdateNeighbors();
            foreach (Node node in simulator.Nodes)
            {
                Assert.AreEqual(numNodes - 1, node.Neighbors.Count);
            }
        }

        [TestMethod]
        public void TestAlgorithmChainPropagation()
        {
            // Create a chain topology: node0 <-> node1, node1 <-> node2, with no direct link between node0 and node2.
            Node node0 = new Node(0, 10, 0, 0);
            Node node1 = new Node(1, 20, 0, 0);
            Node node2 = new Node(2, 30, 0, 0);
            node0.Neighbors.Add(node1);
            node1.Neighbors.Add(node0);
            node1.Neighbors.Add(node2);
            node2.Neighbors.Add(node1);
            List<Node> nodes = new List<Node>
            {
                node0,
                node1,
                node2
            };
            KHOPCAAlgorithm algorithm = new KHOPCAAlgorithm(nodes);
            algorithm.RunIteration();
            // Cluster propagation should still elect node2 as the cluster head throughout.
            Assert.AreEqual(2, node0.ClusterId);
            Assert.AreEqual(2, node1.ClusterId);
            Assert.AreEqual(2, node2.ClusterId);
            Assert.IsFalse(node0.IsClusterHead);
            Assert.IsFalse(node1.IsClusterHead);
            Assert.IsTrue(node2.IsClusterHead);
        }
    }
}