using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TarjanSCC;

namespace TarjanSCCTest
{
    [TestClass]
    public class TarjanSCCUnitTests
    {
        [TestMethod]
        public void TestNullGraphReturnsEmpty()
        {
            Dictionary<int, List<int>> graph = null;
            List<List<int>> sccs = TarjanSCCAlgorithm.FindStronglyConnectedComponents(graph);
            Assert.IsNotNull(sccs, "SCCs should not be null.");
            Assert.AreEqual(0, sccs.Count, "Null graph should produce no SCCs.");
        }

        [TestMethod]
        public void TestEmptyGraphReturnsEmpty()
        {
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
            List<List<int>> sccs = TarjanSCCAlgorithm.FindStronglyConnectedComponents(graph);
            Assert.IsNotNull(sccs, "SCCs should not be null.");
            Assert.AreEqual(0, sccs.Count, "Empty graph should produce no SCCs.");
        }

        [TestMethod]
        public void TestSingleNodeGraph()
        {
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
            graph.Add(1, new List<int>());
            List<List<int>> sccs = TarjanSCCAlgorithm.FindStronglyConnectedComponents(graph);
            Assert.AreEqual(1, sccs.Count, "Single node graph should have one SCC.");
            CollectionAssert.AreEquivalent(new List<int> { 1 }, sccs[0]);
        }

        [TestMethod]
        public void TestDisconnectedNodesGraph()
        {
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
            graph.Add(1, new List<int>());
            graph.Add(2, new List<int>());
            List<List<int>> sccs = TarjanSCCAlgorithm.FindStronglyConnectedComponents(graph);
            Assert.AreEqual(2, sccs.Count, "Graph with two disconnected nodes should have two SCCs.");
            bool foundOne = false;
            bool foundTwo = false;
            foreach (List<int> scc in sccs)
            {
                if (scc.Count == 1 && scc.Contains(1))
                {
                    foundOne = true;
                }
                else if (scc.Count == 1 && scc.Contains(2))
                {
                    foundTwo = true;
                }
            }

            Assert.IsTrue(foundOne, "SCC containing node 1 not found.");
            Assert.IsTrue(foundTwo, "SCC containing node 2 not found.");
        }

        [TestMethod]
        public void TestBidirectionalGraph()
        {
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
            graph.Add(1, new List<int> { 2 });
            graph.Add(2, new List<int> { 1 });
            List<List<int>> sccs = TarjanSCCAlgorithm.FindStronglyConnectedComponents(graph);
            Assert.AreEqual(1, sccs.Count, "Bidirectional graph should have one SCC.");
            CollectionAssert.AreEquivalent(new List<int> { 1, 2 }, sccs[0]);
        }

        [TestMethod]
        public void TestCycleGraph()
        {
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
            graph.Add(1, new List<int> { 2 });
            graph.Add(2, new List<int> { 3 });
            graph.Add(3, new List<int> { 1 });
            List<List<int>> sccs = TarjanSCCAlgorithm.FindStronglyConnectedComponents(graph);
            Assert.AreEqual(1, sccs.Count, "Cycle graph should have one SCC.");
            CollectionAssert.AreEquivalent(new List<int> { 1, 2, 3 }, sccs[0]);
        }

        [TestMethod]
        public void TestMultipleSCCGraph()
        {
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
            graph.Add(1, new List<int> { 2 });
            graph.Add(2, new List<int> { 1, 3 });
            graph.Add(3, new List<int>());
            graph.Add(4, new List<int> { 5 });
            graph.Add(5, new List<int> { 4 });
            List<List<int>> sccs = TarjanSCCAlgorithm.FindStronglyConnectedComponents(graph);
            Assert.AreEqual(3, sccs.Count, "Graph should have three SCCs.");
            bool found12 = false;
            bool found3 = false;
            bool found45 = false;
            foreach (List<int> scc in sccs)
            {
                if (scc.Count == 2 && scc.Contains(1) && scc.Contains(2))
                {
                    found12 = true;
                }
                else if (scc.Count == 1 && scc.Contains(3))
                {
                    found3 = true;
                }
                else if (scc.Count == 2 && scc.Contains(4) && scc.Contains(5))
                {
                    found45 = true;
                }
            }

            Assert.IsTrue(found12, "SCC with nodes 1 and 2 not found.");
            Assert.IsTrue(found3, "SCC with node 3 not found.");
            Assert.IsTrue(found45, "SCC with nodes 4 and 5 not found.");
        }

        [TestMethod]
        public void TestLargeCyclicGraph()
        {
            int n = 1000;
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
            for (int i = 1; i <= n; i++)
            {
                List<int> edges = new List<int>();
                if (i < n)
                {
                    edges.Add(i + 1);
                }
                else
                {
                    edges.Add(1);
                }

                graph.Add(i, edges);
            }

            List<List<int>> sccs = TarjanSCCAlgorithm.FindStronglyConnectedComponents(graph);
            Assert.AreEqual(1, sccs.Count, "Large cyclic graph should have one SCC.");
            Assert.AreEqual(n, sccs[0].Count, "The SCC should contain all nodes.");
            for (int i = 1; i <= n; i++)
            {
                Assert.IsTrue(sccs[0].Contains(i), "SCC should contain node " + i + ".");
            }
        }
    }
}