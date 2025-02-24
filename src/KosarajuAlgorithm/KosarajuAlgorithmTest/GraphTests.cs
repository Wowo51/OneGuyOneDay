using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KosarajuAlgorithm;

namespace KosarajuAlgorithmTest
{
    [TestClass]
    public class GraphTests
    {
        [TestMethod]
        public void TestAddEdge()
        {
            Graph graph = new Graph();
            graph.AddEdge(1, 2);
            Assert.IsTrue(graph.AdjacencyList.ContainsKey(1));
            CollectionAssert.Contains(graph.AdjacencyList[1], 2);
            Assert.IsTrue(graph.AdjacencyList.ContainsKey(2));
            // Since AddEdge ensures the 'to' vertex exists with an empty list, verify count is zero.
            Assert.AreEqual(0, graph.AdjacencyList[2].Count);
        }

        [TestMethod]
        public void TestReverseGraph()
        {
            Graph graph = new Graph();
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 1);
            Graph reversed = graph.Reverse();
            // In the reversed graph, the edge directions are flipped.
            CollectionAssert.Contains(reversed.AdjacencyList[2], 1);
            CollectionAssert.Contains(reversed.AdjacencyList[3], 2);
            CollectionAssert.Contains(reversed.AdjacencyList[1], 3);
        }
    }
}