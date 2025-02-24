using Microsoft.VisualStudio.TestTools.UnitTesting;
using FordFulkersonAlgorithm;

namespace FordFulkersonAlgorithmTest
{
    [TestClass]
    public class FordFulkersonTests
    {
        [TestMethod]
        public void TestSingleEdge()
        {
            FordFulkerson solver = new FordFulkerson();
            Graph graph = new Graph(2);
            graph.AddEdge(0, 1, 10);
            int maxFlow = solver.ComputeMaxFlow(graph, 0, 1);
            Assert.AreEqual(10, maxFlow);
        }

        [TestMethod]
        public void TestDisconnectedGraph()
        {
            FordFulkerson solver = new FordFulkerson();
            Graph graph = new Graph(3);
            graph.AddEdge(0, 1, 5);
            int maxFlow = solver.ComputeMaxFlow(graph, 0, 2);
            Assert.AreEqual(0, maxFlow);
        }

        [TestMethod]
        public void TestMultiplePaths()
        {
            FordFulkerson solver = new FordFulkerson();
            Graph graph = new Graph(6);
            graph.AddEdge(0, 1, 16);
            graph.AddEdge(0, 2, 13);
            graph.AddEdge(1, 2, 10);
            graph.AddEdge(2, 1, 4);
            graph.AddEdge(1, 3, 12);
            graph.AddEdge(2, 4, 14);
            graph.AddEdge(3, 2, 9);
            graph.AddEdge(3, 5, 20);
            graph.AddEdge(4, 3, 7);
            graph.AddEdge(4, 5, 4);
            int maxFlow = solver.ComputeMaxFlow(graph, 0, 5);
            Assert.AreEqual(23, maxFlow);
        }

        [TestMethod]
        public void TestChainGraph()
        {
            FordFulkerson solver = new FordFulkerson();
            int vertexCount = 4;
            Graph graph = new Graph(vertexCount);
            int[] capacities = new int[]
            {
                5,
                10,
                15
            };
            for (int i = 0; i < vertexCount - 1; i++)
            {
                graph.AddEdge(i, i + 1, capacities[i]);
            }

            int maxFlow = solver.ComputeMaxFlow(graph, 0, vertexCount - 1);
            Assert.AreEqual(5, maxFlow);
        }

        [TestMethod]
        public void TestParallelEdges()
        {
            FordFulkerson solver = new FordFulkerson();
            Graph graph = new Graph(2);
            graph.AddEdge(0, 1, 5);
            graph.AddEdge(0, 1, 5);
            int maxFlow = solver.ComputeMaxFlow(graph, 0, 1);
            Assert.AreEqual(10, maxFlow);
        }

        [TestMethod]
        public void TestZeroCapacityEdge()
        {
            FordFulkerson solver = new FordFulkerson();
            Graph graph = new Graph(2);
            graph.AddEdge(0, 1, 0);
            int maxFlow = solver.ComputeMaxFlow(graph, 0, 1);
            Assert.AreEqual(0, maxFlow);
        }

        [TestMethod]
        public void TestSelfLoop()
        {
            FordFulkerson solver = new FordFulkerson();
            Graph graph = new Graph(2);
            // A self-loop should not contribute to the flow.
            graph.AddEdge(0, 0, 100);
            graph.AddEdge(0, 1, 50);
            int maxFlow = solver.ComputeMaxFlow(graph, 0, 1);
            Assert.AreEqual(50, maxFlow);
        }

        [TestMethod]
        public void TestLargeLayeredGraph()
        {
            FordFulkerson solver = new FordFulkerson();
            int intermediateCount = 50;
            int totalVertices = intermediateCount + 2; // source and sink
            Graph graph = new Graph(totalVertices);
            // Connect source (0) to each intermediate node and each intermediate to sink.
            for (int i = 1; i <= intermediateCount; i++)
            {
                graph.AddEdge(0, i, 10);
                graph.AddEdge(i, totalVertices - 1, 10);
            }

            int maxFlow = solver.ComputeMaxFlow(graph, 0, totalVertices - 1);
            Assert.AreEqual(intermediateCount * 10, maxFlow);
        }

        [TestMethod]
        public void TestLargeThreeLayerGraph()
        {
            FordFulkerson solver = new FordFulkerson();
            int n = 10;
            int totalVertices = 2 + 2 * n; // source, sink, two intermediate layers
            Graph graph = new Graph(totalVertices);
            // Source to first layer.
            for (int i = 1; i <= n; i++)
            {
                graph.AddEdge(0, i, 10);
            }

            // Full bipartite connection from first layer to second layer with capacity 1.
            for (int i = 1; i <= n; i++)
            {
                for (int j = n + 1; j <= 2 * n; j++)
                {
                    graph.AddEdge(i, j, 1);
                }
            }

            // Second layer to sink.
            for (int j = n + 1; j <= 2 * n; j++)
            {
                graph.AddEdge(j, totalVertices - 1, 10);
            }

            int maxFlow = solver.ComputeMaxFlow(graph, 0, totalVertices - 1);
            Assert.AreEqual(100, maxFlow);
        }
    }
}