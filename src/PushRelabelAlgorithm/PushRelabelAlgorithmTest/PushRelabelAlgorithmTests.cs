using Microsoft.VisualStudio.TestTools.UnitTesting;
using PushRelabelAlgorithm;

namespace PushRelabelAlgorithmTest
{
    [TestClass]
    public class PushRelabelAlgorithmTests
    {
        [TestMethod]
        public void TestSimpleFlow()
        {
            int vertexCount = 4;
            Graph graph = new Graph(vertexCount);
            graph.AddEdge(0, 1, 100);
            graph.AddEdge(1, 3, 50);
            graph.AddEdge(0, 2, 50);
            graph.AddEdge(2, 3, 100);
            int maxFlow = PushRelabel.GetMaxFlow(graph, 0, 3);
            Assert.AreEqual(100, maxFlow);
        }

        [TestMethod]
        public void TestNoFlow()
        {
            Graph graph = new Graph(3);
            graph.AddEdge(1, 2, 10);
            int maxFlow = PushRelabel.GetMaxFlow(graph, 0, 2);
            Assert.AreEqual(0, maxFlow);
        }

        [TestMethod]
        public void TestChainGraph()
        {
            int vertices = 100;
            Graph graph = new Graph(vertices);
            for (int i = 0; i < vertices - 1; i++)
            {
                graph.AddEdge(i, i + 1, 10);
            }

            int maxFlow = PushRelabel.GetMaxFlow(graph, 0, vertices - 1);
            Assert.AreEqual(10, maxFlow);
        }

        [TestMethod]
        public void TestBipartiteLayeredGraph()
        {
            int layer1Count = 10;
            int layer2Count = 10;
            int totalVertices = 2 + layer1Count + layer2Count;
            Graph graph = new Graph(totalVertices);
            int source = 0;
            int sink = totalVertices - 1;
            for (int i = 1; i <= layer1Count; i++)
            {
                graph.AddEdge(source, i, 100);
            }

            for (int i = layer1Count + 1; i <= layer1Count + layer2Count; i++)
            {
                graph.AddEdge(i, sink, 100);
            }

            for (int i = 1; i <= layer1Count; i++)
            {
                for (int j = layer1Count + 1; j <= layer1Count + layer2Count; j++)
                {
                    graph.AddEdge(i, j, 50);
                }
            }

            int maxFlow = PushRelabel.GetMaxFlow(graph, source, sink);
            Assert.AreEqual(1000, maxFlow);
        }
    }
}