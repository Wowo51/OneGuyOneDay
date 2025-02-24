using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using KruskalAlgorithm;

namespace KruskalAlgorithmTest
{
    [TestClass]
    public class KruskalMSTTests
    {
        [TestMethod]
        public void Test_SimpleGraphMST()
        {
            int vertices = 4;
            Graph graph = new Graph(vertices);
            graph.AddEdge(0, 1, 10);
            graph.AddEdge(0, 2, 6);
            graph.AddEdge(0, 3, 5);
            graph.AddEdge(1, 3, 15);
            graph.AddEdge(2, 3, 4);
            List<Edge> mst = KruskalMST.FindMinimumSpanningTree(graph);
            Assert.AreEqual(vertices - 1, mst.Count, "MST should have vertices-1 edges.");
            int totalWeight = 0;
            for (int i = 0; i < mst.Count; i++)
            {
                totalWeight += mst[i].Weight;
            }

            Assert.AreEqual(19, totalWeight, "Total weight of MST should be 19.");
        }

        [TestMethod]
        public void Test_LargeConnectedGraphMST()
        {
            int vertices = 100;
            Graph graph = new Graph(vertices);
            // Create a connected spanning tree.
            for (int i = 0; i < vertices - 1; i++)
            {
                graph.AddEdge(i, i + 1, 1);
            }

            // Add extra random edges.
            Random rng = new Random(12345);
            for (int j = 0; j < 500; j++)
            {
                int u = rng.Next(0, vertices);
                int v = rng.Next(0, vertices);
                if (u != v)
                {
                    int weight = rng.Next(1, 20);
                    graph.AddEdge(u, v, weight);
                }
            }

            List<Edge> mst = KruskalMST.FindMinimumSpanningTree(graph);
            Assert.AreEqual(vertices - 1, mst.Count, "MST should have vertices-1 edges for a connected graph.");
        }
    }

    [TestClass]
    public class DisjointSetTests
    {
        [TestMethod]
        public void Test_UnionFind()
        {
            DisjointSet ds = new DisjointSet(5);
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(i, ds.Find(i), "Each element should initially be its own parent.");
            }

            ds.Union(0, 1);
            Assert.AreEqual(ds.Find(0), ds.Find(1), "0 and 1 should be connected.");
            ds.Union(1, 2);
            Assert.AreEqual(ds.Find(0), ds.Find(2), "0 and 2 should be connected.");
            Assert.AreNotEqual(ds.Find(0), ds.Find(3), "0 and 3 should not be connected.");
            ds.Union(3, 4);
            Assert.AreEqual(ds.Find(3), ds.Find(4), "3 and 4 should be connected.");
            ds.Union(2, 3);
            Assert.AreEqual(ds.Find(0), ds.Find(4), "All elements should be connected after union.");
        }
    }

    [TestClass]
    public class EdgeTests
    {
        [TestMethod]
        public void Test_EdgeToString()
        {
            Edge edge = new Edge(0, 1, 5);
            string expected = "(0 -- 5 --> 1)";
            Assert.AreEqual(expected, edge.ToString(), "Edge string representation is incorrect.");
        }
    }
}