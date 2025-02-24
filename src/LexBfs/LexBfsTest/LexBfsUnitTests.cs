using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LexBfs;

namespace LexBfsTest
{
    [TestClass]
    public class LexBfsUnitTests
    {
        [TestMethod]
        public void TestEmptyGraph()
        {
            LexBfs.Graph<int> graph = new LexBfs.Graph<int>();
            List<int> ordering = LexBfsAlgorithm.LexBfs(graph);
            Assert.AreEqual(0, ordering.Count);
        }

        [TestMethod]
        public void TestSingleVertex()
        {
            LexBfs.Graph<int> graph = new LexBfs.Graph<int>();
            graph.AddVertex(1);
            List<int> ordering = LexBfsAlgorithm.LexBfs(graph);
            Assert.AreEqual(1, ordering.Count);
            Assert.AreEqual(1, ordering[0]);
        }

        [TestMethod]
        public void TestSimpleGraph()
        {
            LexBfs.Graph<int> graph = new LexBfs.Graph<int>();
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            List<int> ordering = LexBfsAlgorithm.LexBfs(graph);
            Assert.AreEqual(3, ordering.Count);
            CollectionAssert.AreEquivalent(new List<int> { 1, 2, 3 }, ordering);
        }

        [TestMethod]
        public void TestDisconnectedGraph()
        {
            LexBfs.Graph<int> graph = new LexBfs.Graph<int>();
            graph.AddVertex(1);
            graph.AddVertex(2);
            List<int> ordering = LexBfsAlgorithm.LexBfs(graph);
            Assert.AreEqual(2, ordering.Count);
            CollectionAssert.AreEquivalent(new List<int> { 1, 2 }, ordering);
        }

        [TestMethod]
        public void TestLargeGraph()
        {
            LexBfs.Graph<int> graph = new LexBfs.Graph<int>();
            int numVertices = 1000;
            for (int i = 0; i < numVertices; i++)
            {
                graph.AddVertex(i);
            }

            for (int i = 0; i < numVertices - 1; i++)
            {
                graph.AddEdge(i, i + 1);
            }

            List<int> ordering = LexBfsAlgorithm.LexBfs(graph);
            Assert.AreEqual(numVertices, ordering.Count);
            HashSet<int> verticesSet = new HashSet<int>(ordering);
            Assert.AreEqual(numVertices, verticesSet.Count);
        }
    }
}