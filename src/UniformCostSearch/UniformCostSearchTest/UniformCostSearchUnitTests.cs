using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniformCostSearch;

namespace UniformCostSearchTest
{
    [TestClass]
    public class UniformCostSearchUnitTests
    {
        [TestMethod]
        public void TestSimpleLinearPath()
        {
            string start = "A";
            string goal = "C";
            Dictionary<string, List<(string, double)>> graph = new Dictionary<string, List<(string, double)>>();
            graph.Add("A", new List<(string, double)> { ("B", 1.0) });
            graph.Add("B", new List<(string, double)> { ("C", 1.0) });
            graph.Add("C", new List<(string, double)>());
            List<string>? result = UniformCostSearchAlgorithm.FindPath<string>(start, (string x) => x == goal, (string x) => graph.ContainsKey(x) ? graph[x] : new List<(string, double)>());
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            CollectionAssert.AreEqual(new List<string> { "A", "B", "C" }, result);
        }

        [TestMethod]
        public void TestAlternatePaths()
        {
            string start = "A";
            string goal = "D";
            Dictionary<string, List<(string, double)>> graph = new Dictionary<string, List<(string, double)>>();
            graph.Add("A", new List<(string, double)> { ("B", 1.0), ("C", 2.0) });
            graph.Add("B", new List<(string, double)> { ("D", 10.0) });
            graph.Add("C", new List<(string, double)> { ("D", 1.0) });
            graph.Add("D", new List<(string, double)>());
            List<string>? result = UniformCostSearchAlgorithm.FindPath<string>(start, (string x) => x == goal, (string x) => graph.ContainsKey(x) ? graph[x] : new List<(string, double)>());
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(new List<string> { "A", "C", "D" }, result);
        }

        [TestMethod]
        public void TestNoPath()
        {
            string start = "A";
            string goal = "Z";
            Dictionary<string, List<(string, double)>> graph = new Dictionary<string, List<(string, double)>>();
            graph.Add("A", new List<(string, double)> { ("B", 1.0) });
            graph.Add("B", new List<(string, double)>());
            List<string>? result = UniformCostSearchAlgorithm.FindPath<string>(start, (string x) => x == goal, (string x) => graph.ContainsKey(x) ? graph[x] : new List<(string, double)>());
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestLargeGridGraph()
        {
            int gridSize = 20;
            string start = "0,0";
            string goal = (gridSize - 1) + "," + (gridSize - 1);
            Dictionary<string, List<(string, double)>> graph = new Dictionary<string, List<(string, double)>>();
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    string node = i + "," + j;
                    List<(string, double)> neighbors = new List<(string, double)>();
                    if (i + 1 < gridSize)
                    {
                        neighbors.Add(((i + 1) + "," + j, 1.0));
                    }

                    if (j + 1 < gridSize)
                    {
                        neighbors.Add((i + "," + (j + 1), 1.0));
                    }

                    graph.Add(node, neighbors);
                }
            }

            List<string>? result = UniformCostSearchAlgorithm.FindPath<string>(start, (string x) => x == goal, (string x) => graph.ContainsKey(x) ? graph[x] : new List<(string, double)>());
            Assert.IsNotNull(result);
            Assert.AreEqual(2 * gridSize - 1, result.Count);
        }
    }
}