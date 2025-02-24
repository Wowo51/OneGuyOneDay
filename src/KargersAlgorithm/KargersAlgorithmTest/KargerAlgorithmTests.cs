using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using KargersAlgorithm;

namespace KargersAlgorithmTest
{
    [TestClass]
    public class KargerAlgorithmTests
    {
        [TestMethod]
        public void TestSingleEdgeGraph()
        {
            List<int> vertices = new List<int>
            {
                1,
                2
            };
            List<Edge> edges = new List<Edge>
            {
                new Edge(1, 2)
            };
            Graph graph = new Graph(vertices, edges);
            int cut = KargerAlgorithm.FindMinCut(graph);
            Assert.AreEqual(1, cut);
        }

        [TestMethod]
        public void TestTriangleGraph()
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
                new Edge(2, 3),
                new Edge(1, 3)
            };
            Graph graph = new Graph(vertices, edges);
            int cut = KargerAlgorithm.FindMinCut(graph);
            Assert.AreEqual(2, cut);
        }

        [TestMethod]
        public void TestCompleteGraph4Iterations()
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
            int cut = KargerAlgorithm.FindMinCut(graph, 1000);
            Assert.AreEqual(3, cut);
        }

        [TestMethod]
        public void TestGraphWithKnownMinCut()
        {
            List<int> vertices = new List<int>
            {
                1,
                2,
                3,
                4,
                5,
                6
            };
            List<Edge> edges = new List<Edge>
            {
                new Edge(1, 2),
                new Edge(1, 3),
                new Edge(2, 3),
                new Edge(4, 5),
                new Edge(4, 6),
                new Edge(5, 6),
                new Edge(1, 4),
                new Edge(1, 5)
            };
            Graph graph = new Graph(vertices, edges);
            int cut = KargerAlgorithm.FindMinCut(graph, 1000);
            Assert.AreEqual(2, cut);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void TestEmptyGraphThrows()
        {
            List<int> vertices = new List<int>
            {
                1,
                2,
                3
            };
            List<Edge> edges = new List<Edge>(); // No edges
            Graph graph = new Graph(vertices, edges);
            int cut = KargerAlgorithm.FindMinCut(graph);
        }

        [TestMethod]
        [ExpectedException(typeof(System.NullReferenceException))]
        public void TestNullGraphThrows()
        {
            Graph? graph = null;
            int cut = KargerAlgorithm.FindMinCut(graph!);
        }

        [TestMethod]
        public void TestLargeRandomGraph()
        {
            List<int> vertices = new List<int>();
            List<Edge> edges = new List<Edge>();
            for (int i = 1; i <= 20; i++)
            {
                vertices.Add(i);
            }

            for (int i = 1; i <= 20; i++)
            {
                for (int j = i + 1; j <= 20; j++)
                {
                    edges.Add(new Edge(i, j));
                }
            }

            for (int i = 21; i <= 40; i++)
            {
                vertices.Add(i);
            }

            for (int i = 21; i <= 40; i++)
            {
                for (int j = i + 1; j <= 40; j++)
                {
                    edges.Add(new Edge(i, j));
                }
            }

            // Add exactly 3 crossing edges between clusters.
            edges.Add(new Edge(10, 30));
            edges.Add(new Edge(15, 25));
            edges.Add(new Edge(20, 35));
            Graph graph = new Graph(vertices, edges);
            int cut = KargerAlgorithm.FindMinCut(graph, 5000);
            Assert.AreEqual(3, cut);
        }
    }
}