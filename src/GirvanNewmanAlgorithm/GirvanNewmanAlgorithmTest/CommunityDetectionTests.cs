using Microsoft.VisualStudio.TestTools.UnitTesting;
using GirvanNewmanAlgorithm;
using System.Collections.Generic;

namespace GirvanNewmanAlgorithmTest
{
    [TestClass]
    public class CommunityDetectionTests
    {
        [TestMethod]
        public void TestSingleVertex()
        {
            Graph graph = new Graph();
            graph.AddVertex("A");
            List<List<string>> communities = CommunityDetection.GirvanNewman(graph);
            Assert.AreEqual(1, communities.Count);
            Assert.AreEqual(1, communities[0].Count);
            Assert.IsTrue(communities[0].Contains("A"));
        }

        [TestMethod]
        public void TestDisconnectedVertices()
        {
            Graph graph = new Graph();
            graph.AddVertex("A");
            graph.AddVertex("B");
            List<List<string>> communities = CommunityDetection.GirvanNewman(graph);
            Assert.AreEqual(2, communities.Count);
        }

        [TestMethod]
        public void TestBridgeEdgeRemoval()
        {
            Graph graph = new Graph();
            // Cluster 1: A-B
            graph.AddEdge("A", "B");
            // Cluster 2: C-D
            graph.AddEdge("C", "D");
            // Bridge between clusters: B-C
            graph.AddEdge("B", "C");
            List<List<string>> communities = CommunityDetection.GirvanNewman(graph);
            Assert.AreEqual(2, communities.Count);
            bool foundAB = false;
            bool foundCD = false;
            foreach (List<string> community in communities)
            {
                if (community.Contains("A") && community.Contains("B"))
                {
                    foundAB = true;
                }

                if (community.Contains("C") && community.Contains("D"))
                {
                    foundCD = true;
                }
            }

            Assert.IsTrue(foundAB);
            Assert.IsTrue(foundCD);
        }

        [TestMethod]
        public void TestTriangleGraph()
        {
            Graph graph = new Graph();
            graph.AddEdge("A", "B");
            graph.AddEdge("B", "C");
            graph.AddEdge("A", "C");
            List<List<string>> communities = CommunityDetection.GirvanNewman(graph);
            Assert.AreEqual(3, communities.Count);
        }

        [TestMethod]
        public void TestLargeSyntheticGraph()
        {
            Graph graph = new Graph();
            int clusterSize = 10;
            // Build cluster 1: vertices A0...A9 connected in a chain
            for (int i = 0; i < clusterSize; i++)
            {
                string vertex = "A" + i.ToString();
                graph.AddVertex(vertex);
            }

            for (int i = 0; i < clusterSize - 1; i++)
            {
                graph.AddEdge("A" + i.ToString(), "A" + (i + 1).ToString());
            }

            // Build cluster 2: vertices B0...B9 connected in a chain
            for (int i = 0; i < clusterSize; i++)
            {
                string vertex = "B" + i.ToString();
                graph.AddVertex(vertex);
            }

            for (int i = 0; i < clusterSize - 1; i++)
            {
                graph.AddEdge("B" + i.ToString(), "B" + (i + 1).ToString());
            }

            // Connect clusters with a single bridge edge
            graph.AddEdge("A0", "B0");
            List<List<string>> communities = CommunityDetection.GirvanNewman(graph);
            Assert.AreEqual(2, communities.Count);
        }
    }
}