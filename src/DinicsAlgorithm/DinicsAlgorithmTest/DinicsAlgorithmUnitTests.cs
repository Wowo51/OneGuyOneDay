using Microsoft.VisualStudio.TestTools.UnitTesting;
using DinicsAlgorithm;

namespace DinicsAlgorithmTest
{
    [TestClass]
    public class DinicsAlgorithmUnitTests
    {
        [TestMethod]
        public void TestSimpleNetwork()
        {
            int nodeCount = 6;
            Dinic dinic = new Dinic(nodeCount);
            dinic.AddEdge(0, 1, 16);
            dinic.AddEdge(0, 2, 13);
            dinic.AddEdge(1, 2, 10);
            dinic.AddEdge(1, 3, 12);
            dinic.AddEdge(2, 1, 4);
            dinic.AddEdge(2, 4, 14);
            dinic.AddEdge(3, 2, 9);
            dinic.AddEdge(3, 5, 20);
            dinic.AddEdge(4, 3, 7);
            dinic.AddEdge(4, 5, 4);
            int maxFlow = dinic.MaxFlow(0, 5);
            Assert.AreEqual(23, maxFlow, "Simple network max flow should be 23.");
        }

        [TestMethod]
        public void TestDisconnectedGraph()
        {
            int nodeCount = 4;
            Dinic dinic = new Dinic(nodeCount);
            dinic.AddEdge(0, 1, 10);
            int maxFlow = dinic.MaxFlow(0, 3);
            Assert.AreEqual(0, maxFlow, "Disconnected graph should yield zero max flow.");
        }

        [TestMethod]
        public void TestSingleEdge()
        {
            int nodeCount = 2;
            Dinic dinic = new Dinic(nodeCount);
            dinic.AddEdge(0, 1, 15);
            int maxFlow = dinic.MaxFlow(0, 1);
            Assert.AreEqual(15, maxFlow, "Flow on single edge should equal edge capacity.");
        }

        [TestMethod]
        public void TestZeroCapacityEdge()
        {
            int nodeCount = 2;
            Dinic dinic = new Dinic(nodeCount);
            dinic.AddEdge(0, 1, 0);
            int maxFlow = dinic.MaxFlow(0, 1);
            Assert.AreEqual(0, maxFlow, "Edge with zero capacity should yield zero max flow.");
        }

        [TestMethod]
        public void TestLargeLayeredNetwork()
        {
            int intermediateCount = 50;
            int nodeCount = intermediateCount + 2;
            Dinic dinic = new Dinic(nodeCount);
            int capacity = 10;
            for (int i = 1; i <= intermediateCount; i = i + 1)
            {
                dinic.AddEdge(0, i, capacity);
                dinic.AddEdge(i, nodeCount - 1, capacity);
            }

            int maxFlow = dinic.MaxFlow(0, nodeCount - 1);
            int expectedFlow = intermediateCount * capacity;
            Assert.AreEqual(expectedFlow, maxFlow, "Large layered network max flow did not match expected result.");
        }

        [TestMethod]
        public void TestInvalidEdgeIgnored()
        {
            int nodeCount = 3;
            Dinic dinic = new Dinic(nodeCount);
            // Attempt to add an edge with an invalid index; should be ignored.
            dinic.AddEdge(0, 3, 10);
            dinic.AddEdge(0, 1, 5);
            dinic.AddEdge(1, 2, 5);
            int maxFlow = dinic.MaxFlow(0, 2);
            Assert.AreEqual(5, maxFlow, "Invalid edge should be ignored, yielding max flow equal to valid path capacity.");
        }
    }
}