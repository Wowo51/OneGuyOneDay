using Microsoft.VisualStudio.TestTools.UnitTesting;
using GirvanNewmanAlgorithm;
using System.Collections.Generic;

namespace GirvanNewmanAlgorithmTest
{
    [TestClass]
    public class GraphTests
    {
        [TestMethod]
        public void TestAddVertex()
        {
            Graph graph = new Graph();
            graph.AddVertex("Test");
            Assert.IsTrue(graph.AdjacencyList.ContainsKey("Test"));
        }

        [TestMethod]
        public void TestAddEdge()
        {
            Graph graph = new Graph();
            graph.AddEdge("A", "B");
            Assert.IsTrue(graph.AdjacencyList["A"].Contains("B"));
            Assert.IsTrue(graph.AdjacencyList["B"].Contains("A"));
        }

        [TestMethod]
        public void TestRemoveEdge()
        {
            Graph graph = new Graph();
            graph.AddEdge("A", "B");
            graph.RemoveEdge("A", "B");
            Assert.IsFalse(graph.AdjacencyList["A"].Contains("B"));
            Assert.IsFalse(graph.AdjacencyList["B"].Contains("A"));
        }

        [TestMethod]
        public void TestGetConnectedComponents()
        {
            Graph graph = new Graph();
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");
            graph.AddEdge("A", "B");
            List<List<string>> components = graph.GetConnectedComponents();
            Assert.AreEqual(2, components.Count);
            bool foundAB = false;
            bool foundC = false;
            foreach (List<string> component in components)
            {
                if (component.Contains("A") && component.Contains("B"))
                {
                    foundAB = true;
                }

                if (component.Contains("C") && component.Count == 1)
                {
                    foundC = true;
                }
            }

            Assert.IsTrue(foundAB);
            Assert.IsTrue(foundC);
        }
    }
}