using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using PrimsAlgorithm;

namespace PrimsAlgorithmTest
{
    [TestClass]
    public class PrimsAlgorithmTests
    {
        [TestMethod]
        public void TestEmptyGraph()
        {
            Graph graph = new Graph();
            List<Edge> mstEdges = PrimAlgorithm.ComputeMST(graph);
            Assert.AreEqual(0, mstEdges.Count);
        }

        [TestMethod]
        public void TestSingleVertexGraph()
        {
            Graph graph = new Graph();
            Vertex vertex = new Vertex(1);
            graph.AddVertex(vertex);
            List<Edge> mstEdges = PrimAlgorithm.ComputeMST(graph);
            Assert.AreEqual(0, mstEdges.Count);
        }

        [TestMethod]
        public void TestTwoVerticesGraph()
        {
            Graph graph = new Graph();
            Vertex vertex1 = new Vertex(1);
            Vertex vertex2 = new Vertex(2);
            Edge edge = new Edge(vertex1, vertex2, 5.0);
            graph.AddEdge(edge);
            List<Edge> mstEdges = PrimAlgorithm.ComputeMST(graph);
            Assert.AreEqual(1, mstEdges.Count);
            Assert.IsTrue(mstEdges.Contains(edge));
        }

        [TestMethod]
        public void TestSmallGraphMST()
        {
            Graph graph = new Graph();
            Vertex v1 = new Vertex(1);
            Vertex v2 = new Vertex(2);
            Vertex v3 = new Vertex(3);
            Vertex v4 = new Vertex(4);
            graph.AddVertex(v1);
            graph.AddVertex(v2);
            graph.AddVertex(v3);
            graph.AddVertex(v4);
            Edge edge12 = new Edge(v1, v2, 1.0);
            Edge edge13 = new Edge(v1, v3, 3.0);
            Edge edge23 = new Edge(v2, v3, 2.0);
            Edge edge34 = new Edge(v3, v4, 1.0);
            Edge edge24 = new Edge(v2, v4, 4.0);
            graph.AddEdge(edge12);
            graph.AddEdge(edge13);
            graph.AddEdge(edge23);
            graph.AddEdge(edge34);
            graph.AddEdge(edge24);
            List<Edge> mstEdges = PrimAlgorithm.ComputeMST(graph);
            Assert.AreEqual(3, mstEdges.Count);
            double totalWeight = 0;
            foreach (Edge e in mstEdges)
            {
                totalWeight += e.Weight;
            }

            Assert.AreEqual(4.0, totalWeight, 0.0001);
        }

        [TestMethod]
        public void TestLargeRandomGraphMST()
        {
            Graph graph = new Graph();
            int vertexCount = 20;
            List<Vertex> vertices = new List<Vertex>();
            for (int i = 1; i <= vertexCount; i++)
            {
                Vertex vertex = new Vertex(i);
                vertices.Add(vertex);
                graph.AddVertex(vertex);
            }

            Random random = new Random(12345);
            for (int i = 0; i < vertexCount; i++)
            {
                for (int j = i + 1; j < vertexCount; j++)
                {
                    double weight = random.NextDouble() * 100;
                    Edge edge = new Edge(vertices[i], vertices[j], weight);
                    graph.AddEdge(edge);
                }
            }

            List<Edge> mstEdges = PrimAlgorithm.ComputeMST(graph);
            Assert.AreEqual(vertexCount - 1, mstEdges.Count);
        }

        [TestMethod]
        public void TestGraphWithMultipleComponents()
        {
            Graph graph = new Graph();
            Vertex v1 = new Vertex(1);
            Vertex v2 = new Vertex(2);
            Vertex v3 = new Vertex(3);
            Vertex v4 = new Vertex(4);
            graph.AddEdge(new Edge(v1, v2, 1.0));
            graph.AddEdge(new Edge(v3, v4, 1.0));
            List<Edge> mstEdges = PrimAlgorithm.ComputeMST(graph);
            Assert.AreEqual(1, mstEdges.Count);
        }

        [TestMethod]
        public void TestNullGraph()
        {
            List<Edge> mstEdges = PrimAlgorithm.ComputeMST(null);
            Assert.AreEqual(0, mstEdges.Count);
        }

        [TestMethod]
        public void TestGraphWithNegativeWeights()
        {
            Graph graph = new Graph();
            Vertex v1 = new Vertex(1);
            Vertex v2 = new Vertex(2);
            Vertex v3 = new Vertex(3);
            graph.AddVertex(v1);
            graph.AddVertex(v2);
            graph.AddVertex(v3);
            Edge e1 = new Edge(v1, v2, -5.0);
            Edge e2 = new Edge(v2, v3, 2.0);
            Edge e3 = new Edge(v1, v3, 3.0);
            graph.AddEdge(e1);
            graph.AddEdge(e2);
            graph.AddEdge(e3);
            List<Edge> mstEdges = PrimAlgorithm.ComputeMST(graph);
            double totalWeight = 0;
            foreach (Edge e in mstEdges)
            {
                totalWeight += e.Weight;
            }

            Assert.AreEqual(2, mstEdges.Count);
            Assert.AreEqual(-3.0, totalWeight, 0.0001);
        }

        [TestMethod]
        public void TestGraphWithDuplicateEdges()
        {
            Graph graph = new Graph();
            Vertex v1 = new Vertex(1);
            Vertex v2 = new Vertex(2);
            graph.AddVertex(v1);
            graph.AddVertex(v2);
            Edge e1 = new Edge(v1, v2, 5.0);
            Edge e2 = new Edge(v1, v2, 3.0);
            graph.AddEdge(e1);
            graph.AddEdge(e2);
            List<Edge> mstEdges = PrimAlgorithm.ComputeMST(graph);
            Assert.AreEqual(1, mstEdges.Count);
            Assert.IsTrue(mstEdges.Contains(e2));
        }

        [TestMethod]
        public void TestGraphWithSelfLoop()
        {
            Graph graph = new Graph();
            Vertex v1 = new Vertex(1);
            graph.AddVertex(v1);
            Edge selfLoop = new Edge(v1, v1, 10.0);
            graph.AddEdge(selfLoop);
            List<Edge> mstEdges = PrimAlgorithm.ComputeMST(graph);
            Assert.AreEqual(0, mstEdges.Count);
        }
    }
}