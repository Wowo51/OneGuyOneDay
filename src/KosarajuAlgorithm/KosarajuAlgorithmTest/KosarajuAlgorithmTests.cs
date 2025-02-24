using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KosarajuAlgorithm;

namespace KosarajuAlgorithmTest
{
    [TestClass]
    public class KosarajuAlgorithmTests
    {
        [TestMethod]
        public void TestNullGraph()
        {
            List<List<int>> sccs = KosarajuSccFinder.FindStronglyConnectedComponents(null);
            Assert.AreEqual(0, sccs.Count);
        }

        [TestMethod]
        public void TestEmptyGraph()
        {
            Graph graph = new Graph();
            List<List<int>> sccs = KosarajuSccFinder.FindStronglyConnectedComponents(graph);
            Assert.AreEqual(0, sccs.Count);
        }

        [TestMethod]
        public void TestSingleNodeGraph()
        {
            Graph graph = new Graph();
            graph.AddEdge(1, 1);
            List<List<int>> sccs = KosarajuSccFinder.FindStronglyConnectedComponents(graph);
            Assert.AreEqual(1, sccs.Count);
            Assert.AreEqual(1, sccs[0].Count);
            CollectionAssert.AreEquivalent(new List<int> { 1 }, sccs[0]);
        }

        [TestMethod]
        public void TestTwoCycleGraph()
        {
            Graph graph = new Graph();
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 1);
            List<List<int>> sccs = KosarajuSccFinder.FindStronglyConnectedComponents(graph);
            Assert.AreEqual(1, sccs.Count);
            CollectionAssert.AreEquivalent(new List<int> { 1, 2 }, sccs[0]);
        }

        [TestMethod]
        public void TestChainGraph()
        {
            Graph graph = new Graph();
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            List<List<int>> sccs = KosarajuSccFinder.FindStronglyConnectedComponents(graph);
            Assert.AreEqual(3, sccs.Count);
            foreach (List<int> component in sccs)
            {
                Assert.AreEqual(1, component.Count);
            }
        }

        [TestMethod]
        public void TestCycleGraph()
        {
            Graph graph = new Graph();
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 1);
            List<List<int>> sccs = KosarajuSccFinder.FindStronglyConnectedComponents(graph);
            Assert.AreEqual(1, sccs.Count);
            CollectionAssert.AreEquivalent(new List<int> { 1, 2, 3 }, sccs[0]);
        }

        [TestMethod]
        public void TestLargeCyclicGraph()
        {
            Graph graph = new Graph();
            int n = 1000;
            for (int i = 0; i < n; i++)
            {
                graph.AddEdge(i, (i + 1) % n);
            }

            List<List<int>> sccs = KosarajuSccFinder.FindStronglyConnectedComponents(graph);
            Assert.AreEqual(1, sccs.Count);
            Assert.AreEqual(n, sccs[0].Count);
            List<int> expected = new List<int>();
            for (int i = 0; i < n; i++)
            {
                expected.Add(i);
            }

            CollectionAssert.AreEquivalent(expected, sccs[0]);
        }

        [TestMethod]
        public void TestLargeGraphMultipleCycles()
        {
            Graph graph = new Graph();
            int groupCount = 20;
            int groupSize = 10;
            int totalVertices = groupCount * groupSize;
            for (int i = 0; i < groupCount; i++)
            {
                int baseIndex = i * groupSize;
                for (int j = 0; j < groupSize; j++)
                {
                    int from = baseIndex + j;
                    int to = baseIndex + ((j + 1) % groupSize);
                    graph.AddEdge(from, to);
                }

                if (i < groupCount - 1)
                {
                    graph.AddEdge(baseIndex + groupSize - 1, (i + 1) * groupSize);
                }
            }

            List<List<int>> sccs = KosarajuSccFinder.FindStronglyConnectedComponents(graph);
            Assert.AreEqual(groupCount, sccs.Count);
            int sumVertices = 0;
            foreach (List<int> component in sccs)
            {
                sumVertices += component.Count;
            }

            Assert.AreEqual(totalVertices, sumVertices);
        }
    }
}