using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BoruvkasAlgorithm;

namespace BoruvkasAlgorithmTest
{
    [TestClass]
    public class BoruvkaTests
    {
        [TestMethod]
        public void Test_BasicGraph_MST()
        {
            Graph graph = new Graph(4);
            graph.AddEdge(0, 1, 10);
            graph.AddEdge(0, 2, 6);
            graph.AddEdge(0, 3, 5);
            graph.AddEdge(1, 3, 15);
            graph.AddEdge(2, 3, 4);
            List<Edge> mst = Boruvka.ComputeMST(graph);
            Assert.AreEqual(3, mst.Count, "MST should contain 3 edges for a connected graph with 4 vertices.");
            double totalWeight = 0;
            foreach (Edge edge in mst)
            {
                totalWeight += edge.Weight;
            }

            Assert.AreEqual(19, totalWeight, 0.001, "MST total weight should be 19.");
        }

        [TestMethod]
        public void Test_SingleVertexGraph()
        {
            Graph graph = new Graph(1);
            List<Edge> mst = Boruvka.ComputeMST(graph);
            Assert.AreEqual(0, mst.Count, "MST for a single vertex graph should be empty.");
        }

        [TestMethod]
        public void Test_DisconnectedGraph()
        {
            Graph graph = new Graph(4);
            graph.AddEdge(0, 1, 1);
            graph.AddEdge(2, 3, 2);
            List<Edge> mst = Boruvka.ComputeMST(graph);
            Assert.AreEqual(2, mst.Count, "MST for a disconnected graph should contain one edge per connected component (total 2 edges).");
            bool foundEdge1 = false;
            bool foundEdge2 = false;
            foreach (Edge edge in mst)
            {
                if (Math.Abs(edge.Weight - 1) < 0.001)
                {
                    foundEdge1 = true;
                }

                if (Math.Abs(edge.Weight - 2) < 0.001)
                {
                    foundEdge2 = true;
                }
            }

            Assert.IsTrue(foundEdge1 && foundEdge2, "MST should contain both expected edges with weights 1 and 2.");
        }

        [TestMethod]
        public void Test_LargeRandomGraph()
        {
            int vertices = 100;
            Graph graph = new Graph(vertices);
            Random random = new Random(42);
            double chainWeight = 0;
            for (int i = 0; i < vertices - 1; i++)
            {
                double weight = random.NextDouble() * 10 + 1;
                chainWeight += weight;
                graph.AddEdge(i, i + 1, weight);
            }

            for (int i = 0; i < 200; i++)
            {
                int src = random.Next(vertices);
                int dest = random.Next(vertices);
                if (src != dest)
                {
                    double weight = random.NextDouble() * 10 + 1;
                    graph.AddEdge(src, dest, weight);
                }
            }

            List<Edge> mst = Boruvka.ComputeMST(graph);
            Assert.AreEqual(vertices - 1, mst.Count, "MST should contain vertices - 1 edges for a connected graph.");
            double totalMSTWeight = 0;
            foreach (Edge edge in mst)
            {
                totalMSTWeight += edge.Weight;
            }

            Assert.IsTrue(totalMSTWeight <= chainWeight, "MST total weight should be less than or equal to the chain spanning tree weight.");
        }
    }
}