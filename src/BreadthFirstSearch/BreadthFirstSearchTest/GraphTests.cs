using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BreadthFirstSearch;

namespace BreadthFirstSearchTest
{
    [TestClass]
    public class GraphTests
    {
        [TestMethod]
        public void Test_BFS_ReturnsNull_WhenStartNotFound()
        {
            BreadthFirstSearch.Graph<string> graph = new BreadthFirstSearch.Graph<string>();
            graph.AddUndirectedEdge("A", "B");
            List<string>? path = graph.BreadthFirstSearchPath("X", "B");
            Assert.IsNull(path);
        }

        [TestMethod]
        public void Test_BFS_ReturnsNull_WhenGoalNotFound()
        {
            BreadthFirstSearch.Graph<string> graph = new BreadthFirstSearch.Graph<string>();
            graph.AddUndirectedEdge("A", "B");
            List<string>? path = graph.BreadthFirstSearchPath("A", "X");
            Assert.IsNull(path);
        }

        [TestMethod]
        public void Test_BFS_SimpleDirectEdge()
        {
            BreadthFirstSearch.Graph<string> graph = new BreadthFirstSearch.Graph<string>();
            graph.AddUndirectedEdge("A", "B");
            List<string>? path = graph.BreadthFirstSearchPath("A", "B");
            Assert.IsNotNull(path);
            Assert.AreEqual(2, path.Count);
            Assert.AreEqual("A", path[0]);
            Assert.AreEqual("B", path[1]);
        }

        [TestMethod]
        public void Test_BFS_MultiplePaths_ReturnsValidShortest()
        {
            BreadthFirstSearch.Graph<string> graph = new BreadthFirstSearch.Graph<string>();
            graph.AddUndirectedEdge("A", "B");
            graph.AddUndirectedEdge("A", "C");
            graph.AddUndirectedEdge("B", "D");
            graph.AddUndirectedEdge("C", "D");
            graph.AddUndirectedEdge("D", "E");
            List<string>? path = graph.BreadthFirstSearchPath("A", "E");
            Assert.IsNotNull(path);
            // Expected shortest path: A -> (B or C) -> D -> E, so length is 4.
            Assert.AreEqual(4, path.Count);
            Assert.AreEqual("A", path[0]);
            Assert.AreEqual("E", path[path.Count - 1]);
        }

        [TestMethod]
        public void Test_BFS_SameStartAndGoal()
        {
            BreadthFirstSearch.Graph<string> graph = new BreadthFirstSearch.Graph<string>();
            graph.AddUndirectedEdge("A", "B");
            List<string>? path = graph.BreadthFirstSearchPath("A", "A");
            Assert.IsNotNull(path);
            Assert.AreEqual(1, path.Count);
            Assert.AreEqual("A", path[0]);
        }

        [TestMethod]
        public void Test_BFS_LargeChainGraph()
        {
            BreadthFirstSearch.Graph<int> graph = new BreadthFirstSearch.Graph<int>();
            const int chainLength = 100;
            for (int i = 0; i < chainLength; i++)
            {
                graph.AddUndirectedEdge(i, i + 1);
            }

            List<int>? path = graph.BreadthFirstSearchPath(0, chainLength);
            Assert.IsNotNull(path);
            // The chain has chainLength+1 nodes (from 0 to chainLength).
            Assert.AreEqual(chainLength + 1, path.Count);
            Assert.AreEqual(0, path[0]);
            Assert.AreEqual(chainLength, path[path.Count - 1]);
        }

        [TestMethod]
        public void Test_BFS_CycleGraph_ReturnsShortestPath()
        {
            BreadthFirstSearch.Graph<string> graph = new BreadthFirstSearch.Graph<string>();
            graph.AddUndirectedEdge("A", "B");
            graph.AddUndirectedEdge("B", "C");
            graph.AddUndirectedEdge("C", "A");
            List<string>? path = graph.BreadthFirstSearchPath("A", "C");
            Assert.IsNotNull(path);
            // The shortest path from A to C should be [A, C]
            Assert.AreEqual(2, path.Count);
            Assert.AreEqual("A", path[0]);
            Assert.AreEqual("C", path[1]);
        }
    }
}