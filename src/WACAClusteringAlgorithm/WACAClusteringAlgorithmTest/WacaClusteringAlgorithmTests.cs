using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WACAClusteringAlgorithm;

namespace WACAClusteringAlgorithmTest
{
    [TestClass]
    public class WacaClusteringAlgorithmTests
    {
        [TestMethod]
        public void TestEmptyInput()
        {
            WacaClusteringAlgorithm algorithm = new WacaClusteringAlgorithm();
            List<Node> nodes = new List<Node>();
            List<Cluster> clusters = algorithm.ComputeClusters(nodes, 1);
            Assert.IsNotNull(clusters);
            Assert.AreEqual(0, clusters.Count);
        }

        [TestMethod]
        public void TestSingleNode()
        {
            WacaClusteringAlgorithm algorithm = new WacaClusteringAlgorithm();
            Node node = new Node(1);
            List<Node> nodes = new List<Node>
            {
                node
            };
            List<Cluster> clusters = algorithm.ComputeClusters(nodes, 1);
            Assert.AreEqual(1, clusters.Count);
            Assert.AreEqual(1, clusters[0].Nodes.Count);
            Assert.AreEqual(1, clusters[0].Nodes[0].Id);
        }

        [TestMethod]
        public void TestTwoConnectedNodes()
        {
            WacaClusteringAlgorithm algorithm = new WacaClusteringAlgorithm();
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            node1.Neighbors.Add(2);
            node2.Neighbors.Add(1);
            List<Node> nodes = new List<Node>
            {
                node1,
                node2
            };
            List<Cluster> clusters = algorithm.ComputeClusters(nodes, 1);
            Assert.AreEqual(1, clusters.Count);
            Assert.AreEqual(2, clusters[0].Nodes.Count);
        }

        [TestMethod]
        public void TestTwoDisconnectedNodes()
        {
            WacaClusteringAlgorithm algorithm = new WacaClusteringAlgorithm();
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            List<Node> nodes = new List<Node>
            {
                node1,
                node2
            };
            List<Cluster> clusters = algorithm.ComputeClusters(nodes, 1);
            Assert.AreEqual(2, clusters.Count);
        }

        [TestMethod]
        public void TestChainGraphOneHop()
        {
            WacaClusteringAlgorithm algorithm = new WacaClusteringAlgorithm();
            List<Node> nodes = GenerateChain(5);
            // For a chain of 5 nodes with maxHops = 1:
            // Expected clusters: [nodes 1,2], [nodes 3,4], [node 5]
            List<Cluster> clusters = algorithm.ComputeClusters(nodes, 1);
            Assert.AreEqual(3, clusters.Count);
        }

        [TestMethod]
        public void TestChainGraphTwoHop()
        {
            WacaClusteringAlgorithm algorithm = new WacaClusteringAlgorithm();
            List<Node> nodes = GenerateChain(5);
            // For a chain of 5 nodes with maxHops = 2:
            // Expected clusters: [nodes 1,2,3] and [nodes 4,5]
            List<Cluster> clusters = algorithm.ComputeClusters(nodes, 2);
            Assert.AreEqual(2, clusters.Count);
            Assert.AreEqual(3, clusters[0].Nodes.Count);
            Assert.AreEqual(2, clusters[1].Nodes.Count);
        }

        [TestMethod]
        public void TestHierarchicalClusters()
        {
            WacaClusteringAlgorithm algorithm = new WacaClusteringAlgorithm();
            List<Node> nodes = GenerateChain(3);
            // For a chain of 3 nodes:
            // With maxHops = 1: Expected clusters: [nodes 1,2] and [node 3] => 2 clusters
            // With maxHops = 2: Expected cluster: [nodes 1,2,3] => 1 cluster
            List<int> thresholds = new List<int>
            {
                1,
                2
            };
            List<List<Cluster>> hierarchicalClusters = algorithm.ComputeHierarchicalClusters(nodes, thresholds);
            Assert.AreEqual(2, hierarchicalClusters.Count);
            Assert.AreEqual(2, hierarchicalClusters[0].Count);
            Assert.AreEqual(1, hierarchicalClusters[1].Count);
        }

        [TestMethod]
        public void TestCompleteGraph()
        {
            WacaClusteringAlgorithm algorithm = new WacaClusteringAlgorithm();
            List<Node> nodes = new List<Node>();
            for (int i = 1; i <= 4; i++)
            {
                Node node = new Node(i);
                nodes.Add(node);
            }

            // Create a complete graph: each node is connected to every other node.
            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = 0; j < nodes.Count; j++)
                {
                    if (i != j)
                    {
                        nodes[i].Neighbors.Add(nodes[j].Id);
                    }
                }
            }

            List<Cluster> clusters = algorithm.ComputeClusters(nodes, 1);
            Assert.AreEqual(1, clusters.Count);
            Assert.AreEqual(4, clusters[0].Nodes.Count);
        }

        [TestMethod]
        public void TestLargeChainGraph()
        {
            WacaClusteringAlgorithm algorithm = new WacaClusteringAlgorithm();
            int count = 1000;
            List<Node> nodes = GenerateChain(count);
            // For maxHops = 1, clusters are expected to group nodes in pairs.
            int expectedClustersOneHop = (count % 2 == 0) ? count / 2 : (count / 2 + 1);
            List<Cluster> clustersOneHop = algorithm.ComputeClusters(nodes, 1);
            Assert.AreEqual(expectedClustersOneHop, clustersOneHop.Count);
            // For maxHops = 2, clusters are expected to group nodes in triplets.
            int expectedClustersTwoHop = (count % 3 == 0) ? count / 3 : (count / 3 + 1);
            List<Cluster> clustersTwoHop = algorithm.ComputeClusters(nodes, 2);
            Assert.AreEqual(expectedClustersTwoHop, clustersTwoHop.Count);
        }

        private static List<Node> GenerateChain(int count)
        {
            List<Node> nodes = new List<Node>();
            for (int i = 1; i <= count; i++)
            {
                Node node = new Node(i);
                nodes.Add(node);
            }

            for (int i = 0; i < count; i++)
            {
                if (i > 0)
                {
                    nodes[i].Neighbors.Add(nodes[i - 1].Id);
                }

                if (i < count - 1)
                {
                    nodes[i].Neighbors.Add(nodes[i + 1].Id);
                }
            }

            return nodes;
        }
    }
}