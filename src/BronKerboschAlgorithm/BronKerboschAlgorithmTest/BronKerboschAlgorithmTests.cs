using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using BronKerboschAlgorithm;

namespace BronKerboschAlgorithmTest
{
    [TestClass]
    public class BronKerboschAlgorithmTests
    {
        [TestMethod]
        public void TestEmptyGraph()
        {
            Graph graph = new Graph();
            List<List<int>> cliques = BronKerbosch.FindMaximalCliques(graph);
            // For an empty graph, the algorithm returns one clique: the empty set.
            Assert.AreEqual(1, cliques.Count);
            Assert.AreEqual(0, cliques[0].Count);
        }

        [TestMethod]
        public void TestSingleVertexGraph()
        {
            Graph graph = new Graph();
            graph.AddVertex(1);
            List<List<int>> cliques = BronKerbosch.FindMaximalCliques(graph);
            Assert.AreEqual(1, cliques.Count);
            CollectionAssert.AreEquivalent(new List<int> { 1 }, cliques[0]);
        }

        [TestMethod]
        public void TestCompleteGraphTriangle()
        {
            Graph graph = new Graph();
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(2, 3);
            List<List<int>> cliques = BronKerbosch.FindMaximalCliques(graph);
            // In a complete triangle, the maximal clique is the entire set of vertices.
            Assert.AreEqual(1, cliques.Count);
            CollectionAssert.AreEquivalent(new List<int> { 1, 2, 3 }, cliques[0]);
        }

        [TestMethod]
        public void TestDisconnectedGraphOfTriangles()
        {
            Graph graph = new Graph();
            // First triangle: vertices 1,2,3
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(1, 3);
            // Second triangle: vertices 4,5,6
            graph.AddEdge(4, 5);
            graph.AddEdge(5, 6);
            graph.AddEdge(4, 6);
            List<List<int>> cliques = BronKerbosch.FindMaximalCliques(graph);
            Assert.AreEqual(2, cliques.Count);
            bool firstTriangleFound = cliques.Any(delegate (List<int> clique)
            {
                return new HashSet<int>(clique).SetEquals(new HashSet<int> { 1, 2, 3 });
            });
            bool secondTriangleFound = cliques.Any(delegate (List<int> clique)
            {
                return new HashSet<int>(clique).SetEquals(new HashSet<int> { 4, 5, 6 });
            });
            Assert.IsTrue(firstTriangleFound && secondTriangleFound);
        }

        [TestMethod]
        public void TestGraphWithTriangleAndEdge()
        {
            Graph graph = new Graph();
            // Create a triangle: vertices 1,2,3
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(2, 3);
            // Add an extra edge connecting vertex 3 to vertex 4
            graph.AddEdge(3, 4);
            List<List<int>> cliques = BronKerbosch.FindMaximalCliques(graph);
            // Expected maximal cliques: {1,2,3} and {3,4}
            Assert.AreEqual(2, cliques.Count);
            bool triangleFound = cliques.Any(delegate (List<int> clique)
            {
                return new HashSet<int>(clique).SetEquals(new HashSet<int> { 1, 2, 3 });
            });
            bool edgeCliqueFound = cliques.Any(delegate (List<int> clique)
            {
                return new HashSet<int>(clique).SetEquals(new HashSet<int> { 3, 4 });
            });
            Assert.IsTrue(triangleFound && edgeCliqueFound);
        }

        [TestMethod]
        public void TestGraphWithIsolatedVertex()
        {
            Graph graph = new Graph();
            // Add one isolated vertex.
            graph.AddVertex(1);
            // Also add an edge between two other vertices.
            graph.AddEdge(2, 3);
            List<List<int>> cliques = BronKerbosch.FindMaximalCliques(graph);
            // Expected maximal cliques: {1} and {2,3}
            Assert.AreEqual(2, cliques.Count);
            bool isolatedFound = cliques.Any(delegate (List<int> clique)
            {
                return new HashSet<int>(clique).SetEquals(new HashSet<int> { 1 });
            });
            bool pairFound = cliques.Any(delegate (List<int> clique)
            {
                return new HashSet<int>(clique).SetEquals(new HashSet<int> { 2, 3 });
            });
            Assert.IsTrue(isolatedFound && pairFound);
        }

        [TestMethod]
        public void TestSyntheticLargeGraph()
        {
            Graph graph = new Graph();
            // Generate a complete graph with 5 vertices.
            List<int> vertices = new List<int>
            {
                1,
                2,
                3,
                4,
                5
            };
            for (int i = 0; i < vertices.Count; i++)
            {
                for (int j = i + 1; j < vertices.Count; j++)
                {
                    graph.AddEdge(vertices[i], vertices[j]);
                }
            }

            List<List<int>> cliques = BronKerbosch.FindMaximalCliques(graph);
            // A complete graph has a single maximal clique containing all vertices.
            Assert.AreEqual(1, cliques.Count);
            CollectionAssert.AreEquivalent(vertices, cliques[0]);
        }
    }
}