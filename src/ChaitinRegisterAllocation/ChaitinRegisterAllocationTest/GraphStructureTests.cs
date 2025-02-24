using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChaitinRegisterAllocation;
using System.Collections.Generic;

namespace ChaitinRegisterAllocationTest
{
    [TestClass]
    public class GraphStructureTests
    {
        [TestMethod]
        public void GraphNode_AddNeighbor_DoesNotAddSelf()
        {
            GraphNode node = new GraphNode("A", 1.0);
            node.AddNeighbor(node);
            Assert.AreEqual(0, node.Neighbors.Count);
        }

        [TestMethod]
        public void GraphNode_AddNeighbor_NoDuplicate()
        {
            GraphNode nodeA = new GraphNode("A", 1.0);
            GraphNode nodeB = new GraphNode("B", 1.0);
            nodeA.AddNeighbor(nodeB);
            nodeA.AddNeighbor(nodeB);
            Assert.AreEqual(1, nodeA.Neighbors.Count);
            Assert.AreEqual(1, nodeB.Neighbors.Count);
        }

        [TestMethod]
        public void InterferenceGraph_AddEdge_AddsBothWays()
        {
            InterferenceGraph graph = new InterferenceGraph();
            GraphNode nodeA = new GraphNode("A", 1.0);
            GraphNode nodeB = new GraphNode("B", 1.0);
            graph.AddNode(nodeA);
            graph.AddNode(nodeB);
            graph.AddEdge("A", "B");
            Assert.IsTrue(nodeA.Neighbors.Contains(nodeB));
            Assert.IsTrue(nodeB.Neighbors.Contains(nodeA));
        }

        [TestMethod]
        public void InterferenceGraph_GetNode_ReturnsCorrectNode()
        {
            InterferenceGraph graph = new InterferenceGraph();
            GraphNode nodeA = new GraphNode("A", 1.0);
            graph.AddNode(nodeA);
            GraphNode retrieved = graph.GetNode("A");
            Assert.IsNotNull(retrieved);
            Assert.AreEqual("A", retrieved.Name);
        }
    }
}