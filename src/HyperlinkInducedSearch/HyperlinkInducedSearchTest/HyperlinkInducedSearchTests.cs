using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using HyperlinkInducedSearch;

namespace HyperlinkInducedSearchTest
{
    [TestClass]
    public class HyperlinkInducedSearchTests
    {
        private const double Tolerance = 1e-4;
        [TestMethod]
        public void TestGraphBasicOperations()
        {
            HyperlinkInducedSearch.Graph graph = new HyperlinkInducedSearch.Graph();
            graph.AddEdge("A", "B");
            graph.AddEdge("A", "C");
            List<string> nodes = graph.GetNodes();
            Assert.IsTrue(nodes.Contains("A"), "Graph should contain node A");
            Assert.IsTrue(nodes.Contains("B"), "Graph should contain node B");
            Assert.IsTrue(nodes.Contains("C"), "Graph should contain node C");
            List<string> outgoingA = graph.GetOutgoing("A");
            Assert.AreEqual(2, outgoingA.Count, "Node A should have 2 outgoing links");
            CollectionAssert.Contains(outgoingA, "B");
            CollectionAssert.Contains(outgoingA, "C");
            List<string> incomingB = graph.GetIncoming("B");
            Assert.AreEqual(1, incomingB.Count, "Node B should have 1 incoming link");
            CollectionAssert.Contains(incomingB, "A");
            List<string> incomingC = graph.GetIncoming("C");
            CollectionAssert.Contains(incomingC, "A");
        }

        [TestMethod]
        public void TestGraphEmpty()
        {
            HyperlinkInducedSearch.Graph graph = new HyperlinkInducedSearch.Graph();
            List<string> nodes = graph.GetNodes();
            Assert.AreEqual(0, nodes.Count, "Graph should be empty");
            List<string> outgoing = graph.GetOutgoing("NonExistent");
            Assert.AreEqual(0, outgoing.Count, "Outgoing from non-existent node should be empty");
            List<string> incoming = graph.GetIncoming("NonExistent");
            Assert.AreEqual(0, incoming.Count, "Incoming from non-existent node should be empty");
        }

        [TestMethod]
        public void TestHITSAlgorithmWithSimpleGraph()
        {
            HyperlinkInducedSearch.Graph graph = new HyperlinkInducedSearch.Graph();
            graph.AddEdge("A", "B");
            graph.AddEdge("A", "C");
            graph.AddEdge("B", "C");
            graph.AddEdge("C", "A");
            graph.AddEdge("D", "C");
            graph.AddEdge("D", "B");
            Dictionary<string, double> hubs;
            Dictionary<string, double> authorities;
            int iterations = 10;
            HITSAlgorithm.Compute(graph, iterations, out hubs, out authorities);
            List<string> nodes = graph.GetNodes();
            Assert.AreEqual(nodes.Count, hubs.Count, "Hubs count should equal number of nodes");
            Assert.AreEqual(nodes.Count, authorities.Count, "Authorities count should equal number of nodes");
            // Node D has no incoming links so its authority score should be 0.
            Assert.AreEqual(0.0, authorities["D"], Tolerance, "Authority score for D should be 0");
            double hubNorm = Math.Sqrt(hubs.Values.Sum(score => score * score));
            double authorityNorm = Math.Sqrt(authorities.Values.Sum(score => score * score));
            Assert.AreEqual(1.0, hubNorm, Tolerance, "Hub vector should be normalized to unit length");
            Assert.AreEqual(1.0, authorityNorm, Tolerance, "Authority vector should be normalized to unit length");
            // For this graph we expect C to have the highest authority followed by B then A.
            Assert.IsTrue(authorities["C"] > authorities["B"], "Authority of C should be greater than B");
            Assert.IsTrue(authorities["B"] > authorities["A"], "Authority of B should be greater than A");
        }

        [TestMethod]
        public void TestHITSAlgorithmWithRandomGraph()
        {
            HyperlinkInducedSearch.Graph graph = new HyperlinkInducedSearch.Graph();
            int nodeCount = 100;
            List<string> nodes = new List<string>();
            for (int i = 0; i < nodeCount; i++)
            {
                nodes.Add(i.ToString());
            }

            // Generate synthetic test data with random edges.
            Random random = new Random(42);
            for (int i = 0; i < nodeCount * 3; i++)
            {
                int sourceIndex = random.Next(0, nodeCount);
                int targetIndex = random.Next(0, nodeCount);
                if (sourceIndex != targetIndex)
                {
                    graph.AddEdge(nodes[sourceIndex], nodes[targetIndex]);
                }
            }

            Dictionary<string, double> hubs;
            Dictionary<string, double> authorities;
            int iterations = 20;
            HITSAlgorithm.Compute(graph, iterations, out hubs, out authorities);
            double hubNorm = Math.Sqrt(hubs.Values.Sum(score => score * score));
            double authorityNorm = Math.Sqrt(authorities.Values.Sum(score => score * score));
            Assert.AreEqual(1.0, hubNorm, Tolerance, "Hub vector should be normalized to unit length");
            Assert.AreEqual(1.0, authorityNorm, Tolerance, "Authority vector should be normalized to unit length");
            // Verify all scores are non-negative.
            foreach (KeyValuePair<string, double> pair in hubs)
            {
                Assert.IsTrue(pair.Value >= 0.0, "Hub score should be non-negative");
            }

            foreach (KeyValuePair<string, double> pair in authorities)
            {
                Assert.IsTrue(pair.Value >= 0.0, "Authority score should be non-negative");
            }
        }
    }
}