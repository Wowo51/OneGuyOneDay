using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using KargersAlgorithm;

namespace KargersAlgorithmTest
{
    [TestClass]
    public class GraphTests
    {
        [TestMethod]
        public void TestGraphCloneEqual()
        {
            List<int> vertices = new List<int>
            {
                1,
                2,
                3
            };
            List<Edge> edges = new List<Edge>
            {
                new Edge(1, 2),
                new Edge(2, 3)
            };
            Graph original = new Graph(vertices, edges);
            Graph clone = original.Clone();
            Assert.AreEqual(original.Vertices.Count, clone.Vertices.Count);
            Assert.AreEqual(original.Edges.Count, clone.Edges.Count);
            for (int i = 0; i < original.Edges.Count; i++)
            {
                Assert.AreEqual(original.Edges[i].Vertex1, clone.Edges[i].Vertex1);
                Assert.AreEqual(original.Edges[i].Vertex2, clone.Edges[i].Vertex2);
            }
        }

        [TestMethod]
        public void TestGraphCloneDeepCopy()
        {
            List<int> vertices = new List<int>
            {
                1,
                2,
                3
            };
            List<Edge> edges = new List<Edge>
            {
                new Edge(1, 2),
                new Edge(2, 3)
            };
            Graph original = new Graph(vertices, edges);
            Graph clone = original.Clone();
            clone.Vertices.Add(4);
            clone.Edges.Add(new Edge(3, 4));
            Assert.AreEqual(3, original.Vertices.Count);
            Assert.AreEqual(2, original.Edges.Count);
        }

        [TestMethod]
        public void TestSingleVertexGraph()
        {
            List<int> vertices = new List<int>
            {
                1
            };
            List<Edge> edges = new List<Edge>();
            Graph graph = new Graph(vertices, edges);
            int cut = KargerAlgorithm.FindMinCut(graph);
            Assert.AreEqual(0, cut);
        }

        [TestMethod]
        public void TestTwoVertexNoEdgeGraph()
        {
            List<int> vertices = new List<int>
            {
                1,
                2
            };
            List<Edge> edges = new List<Edge>();
            Graph graph = new Graph(vertices, edges);
            int cut = KargerAlgorithm.FindMinCut(graph);
            Assert.AreEqual(0, cut);
        }

        [TestMethod]
        public void TestFindMinCutZeroIterations()
        {
            List<int> vertices = new List<int>
            {
                1,
                2,
                3,
                4
            };
            List<Edge> edges = new List<Edge>
            {
                new Edge(1, 2),
                new Edge(1, 3),
                new Edge(1, 4),
                new Edge(2, 3),
                new Edge(2, 4),
                new Edge(3, 4)
            };
            Graph graph = new Graph(vertices, edges);
            int cut = KargerAlgorithm.FindMinCut(graph, 0);
            Assert.AreEqual(int.MaxValue, cut);
        }
    }
}