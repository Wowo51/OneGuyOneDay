using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BidirectionalSearch;

namespace BidirectionalSearchTest
{
    [TestClass]
    public class BidirectionalSearchTests
    {
        [TestMethod]
        public void TestNullGraph()
        {
            List<string> result = BidirectionalSearchAlgorithm.FindShortestPath(null, "A", "B");
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestNullStart()
        {
            Graph graph = new Graph();
            graph.AddEdge("A", "B");
            List<string> result = BidirectionalSearchAlgorithm.FindShortestPath(graph, null, "B");
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestNullGoal()
        {
            Graph graph = new Graph();
            graph.AddEdge("A", "B");
            List<string> result = BidirectionalSearchAlgorithm.FindShortestPath(graph, "A", null);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestStartEqualGoal()
        {
            Graph graph = new Graph();
            graph.AddNode("A");
            List<string> result = BidirectionalSearchAlgorithm.FindShortestPath(graph, "A", "A");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("A", result[0]);
        }

        [TestMethod]
        public void TestBasicPath()
        {
            Graph graph = new Graph();
            graph.AddEdge("A", "B");
            graph.AddEdge("A", "C");
            graph.AddEdge("B", "D");
            graph.AddEdge("C", "D");
            graph.AddEdge("D", "E");
            List<string> expected = new List<string>
            {
                "A",
                "B",
                "D",
                "E"
            };
            List<string> result = BidirectionalSearchAlgorithm.FindShortestPath(graph, "A", "E");
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestNoPath()
        {
            Graph graph = new Graph();
            graph.AddNode("A");
            graph.AddNode("B");
            List<string> result = BidirectionalSearchAlgorithm.FindShortestPath(graph, "A", "B");
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestLargeGraph()
        {
            Graph graph = new Graph();
            int nodeCount = 1000;
            for (int i = 0; i < nodeCount; i++)
            {
                graph.AddNode(i.ToString());
            }

            for (int i = 0; i < nodeCount - 1; i++)
            {
                graph.AddEdge(i.ToString(), (i + 1).ToString());
            }

            List<string> result = BidirectionalSearchAlgorithm.FindShortestPath(graph, "0", (nodeCount - 1).ToString());
            Assert.AreEqual(nodeCount, result.Count);
            for (int i = 0; i < nodeCount; i++)
            {
                Assert.AreEqual(i.ToString(), result[i]);
            }
        }

        [TestMethod]
        public void TestCycleGraph()
        {
            Graph graph = new Graph();
            graph.AddEdge("A", "B");
            graph.AddEdge("B", "C");
            graph.AddEdge("C", "D");
            graph.AddEdge("D", "A");
            List<string> result = BidirectionalSearchAlgorithm.FindShortestPath(graph, "A", "C");
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("A", result[0]);
            Assert.AreEqual("B", result[1]);
            Assert.AreEqual("C", result[2]);
        }
    }
}