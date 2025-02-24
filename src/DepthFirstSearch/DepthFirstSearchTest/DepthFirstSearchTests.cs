using Microsoft.VisualStudio.TestTools.UnitTesting;
using DepthFirstSearch;
using System.Collections.Generic;

namespace DepthFirstSearchTest
{
    [TestClass]
    public class DepthFirstSearchTests
    {
        [TestMethod]
        public void TestSimpleGraph()
        {
            Graph<int> graph = new Graph<int>();
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(2, 4);
            graph.AddEdge(2, 5);
            graph.AddEdge(3, 6);
            graph.AddEdge(3, 7);
            List<int> result = DepthFirstSearchAlgorithm.DFS<int>(graph, 1);
            List<int> expected = new List<int>
            {
                1,
                2,
                4,
                5,
                3,
                6,
                7
            };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestGraphWithCycle()
        {
            Graph<string> graph = new Graph<string>();
            graph.AddEdge("A", "B");
            graph.AddEdge("B", "C");
            graph.AddEdge("C", "A");
            List<string> result = DepthFirstSearchAlgorithm.DFS<string>(graph, "A");
            List<string> expected = new List<string>
            {
                "A",
                "B",
                "C"
            };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestNullGraph()
        {
            List<int> result = DepthFirstSearchAlgorithm.DFS<int>(null, 1);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestLargeGraph()
        {
            Graph<int> graph = new Graph<int>();
            int nodeCount = 100;
            for (int i = 1; i <= nodeCount; i++)
            {
                if ((2 * i) <= nodeCount)
                {
                    graph.AddEdge(i, 2 * i);
                }

                if ((2 * i + 1) <= nodeCount)
                {
                    graph.AddEdge(i, 2 * i + 1);
                }
            }

            List<int> result = DepthFirstSearchAlgorithm.DFS<int>(graph, 1);
            Assert.AreEqual(nodeCount, result.Count);
        }

        [TestMethod]
        public void TestSingleVertex()
        {
            Graph<int> graph = new Graph<int>();
            // Adding self-loop to simulate a graph with a single vertex.
            graph.AddEdge(1, 1);
            List<int> result = DepthFirstSearchAlgorithm.DFS<int>(graph, 1);
            List<int> expected = new List<int>
            {
                1
            };
            CollectionAssert.AreEqual(expected, result);
        }
    }
}