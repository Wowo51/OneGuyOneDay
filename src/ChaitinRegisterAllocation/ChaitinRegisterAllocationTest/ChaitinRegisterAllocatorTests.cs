using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ChaitinRegisterAllocation;

namespace ChaitinRegisterAllocationTest
{
    [TestClass]
    public class ChaitinRegisterAllocatorTests
    {
        [TestMethod]
        public void AllocateRegisters_SingleNode_AssignsRegisterZero()
        {
            InterferenceGraph graph = new InterferenceGraph();
            GraphNode nodeA = new GraphNode("A", 1.0);
            graph.AddNode(nodeA);
            ChaitinRegisterAllocator allocator = new ChaitinRegisterAllocator();
            Dictionary<string, int> allocation = allocator.AllocateRegisters(graph, 1);
            Assert.AreEqual(1, allocation.Count);
            Assert.IsTrue(allocation.ContainsKey("A"));
            Assert.AreEqual(0, allocation["A"]);
        }

        [TestMethod]
        public void AllocateRegisters_NoInterference_MultipleNodes_AssignsSameRegister()
        {
            InterferenceGraph graph = new InterferenceGraph();
            GraphNode nodeA = new GraphNode("A", 1.0);
            GraphNode nodeB = new GraphNode("B", 1.0);
            graph.AddNode(nodeA);
            graph.AddNode(nodeB);
            ChaitinRegisterAllocator allocator = new ChaitinRegisterAllocator();
            Dictionary<string, int> allocation = allocator.AllocateRegisters(graph, 1);
            Assert.AreEqual(2, allocation.Count);
            Assert.AreEqual(0, allocation["A"]);
            Assert.AreEqual(0, allocation["B"]);
        }

        [TestMethod]
        public void AllocateRegisters_WithInterference_TwoNodes_KEqualsTwo_AssignsDifferentRegisters()
        {
            InterferenceGraph graph = new InterferenceGraph();
            GraphNode nodeA = new GraphNode("A", 1.0);
            GraphNode nodeB = new GraphNode("B", 1.0);
            graph.AddNode(nodeA);
            graph.AddNode(nodeB);
            nodeA.AddNeighbor(nodeB);
            ChaitinRegisterAllocator allocator = new ChaitinRegisterAllocator();
            Dictionary<string, int> allocation = allocator.AllocateRegisters(graph, 2);
            Assert.AreEqual(2, allocation.Count);
            int regA = allocation["A"];
            int regB = allocation["B"];
            Assert.IsTrue(regA >= 0 && regA < 2);
            Assert.IsTrue(regB >= 0 && regB < 2);
            Assert.AreNotEqual(regA, regB);
        }

        [TestMethod]
        public void AllocateRegisters_WithInterference_TwoNodes_KEqualsOne_OneSpilled()
        {
            InterferenceGraph graph = new InterferenceGraph();
            GraphNode nodeA = new GraphNode("A", 1.0);
            GraphNode nodeB = new GraphNode("B", 1.0);
            graph.AddNode(nodeA);
            graph.AddNode(nodeB);
            nodeA.AddNeighbor(nodeB);
            ChaitinRegisterAllocator allocator = new ChaitinRegisterAllocator();
            Dictionary<string, int> allocation = allocator.AllocateRegisters(graph, 1);
            Assert.AreEqual(2, allocation.Count);
            int regA = allocation["A"];
            int regB = allocation["B"];
            Assert.IsTrue((regA == 0 && regB == -1) || (regA == -1 && regB == 0));
        }

        [TestMethod]
        public void AllocateRegisters_ChainGraph_AssignsAlternatingRegisters()
        {
            InterferenceGraph graph = new InterferenceGraph();
            GraphNode nodeA = new GraphNode("A", 1.0);
            GraphNode nodeB = new GraphNode("B", 1.0);
            GraphNode nodeC = new GraphNode("C", 1.0);
            GraphNode nodeD = new GraphNode("D", 1.0);
            graph.AddNode(nodeA);
            graph.AddNode(nodeB);
            graph.AddNode(nodeC);
            graph.AddNode(nodeD);
            nodeA.AddNeighbor(nodeB);
            nodeB.AddNeighbor(nodeC);
            nodeC.AddNeighbor(nodeD);
            ChaitinRegisterAllocator allocator = new ChaitinRegisterAllocator();
            Dictionary<string, int> allocation = allocator.AllocateRegisters(graph, 2);
            Assert.AreEqual(4, allocation.Count);
            Assert.AreNotEqual(allocation["A"], allocation["B"]);
            Assert.AreNotEqual(allocation["B"], allocation["C"]);
            Assert.AreNotEqual(allocation["C"], allocation["D"]);
            foreach (KeyValuePair<string, int> kvp in allocation)
            {
                Assert.IsTrue(kvp.Value == 0 || kvp.Value == 1 || kvp.Value == -1);
            }
        }

        [TestMethod]
        public void AllocateRegisters_NullGraph_ReturnsEmptyDictionary()
        {
            ChaitinRegisterAllocator allocator = new ChaitinRegisterAllocator();
            Dictionary<string, int> allocation = allocator.AllocateRegisters(null, 1);
            Assert.AreEqual(0, allocation.Count);
        }

        [TestMethod]
        public void AllocateRegisters_RandomLargeGraph_NoInterference_UsingOneRegister()
        {
            InterferenceGraph graph = new InterferenceGraph();
            int nodeCount = 100;
            List<GraphNode> nodes = new List<GraphNode>();
            for (int i = 0; i < nodeCount; i++)
            {
                GraphNode node = new GraphNode("N" + i.ToString(), i + 1.0);
                nodes.Add(node);
                graph.AddNode(node);
            }

            ChaitinRegisterAllocator allocator = new ChaitinRegisterAllocator();
            Dictionary<string, int> allocation = allocator.AllocateRegisters(graph, 1);
            Assert.AreEqual(nodeCount, allocation.Count);
            foreach (KeyValuePair<string, int> kvp in allocation)
            {
                Assert.AreEqual(0, kvp.Value);
            }
        }

        [TestMethod]
        public void AllocateRegisters_RandomLargeGraph_WithRandomInterference_KEquals10()
        {
            InterferenceGraph graph = new InterferenceGraph();
            int nodeCount = 50;
            List<GraphNode> nodes = new List<GraphNode>();
            for (int i = 0; i < nodeCount; i++)
            {
                GraphNode node = new GraphNode("N" + i.ToString(), 1.0);
                nodes.Add(node);
                graph.AddNode(node);
            }

            System.Random random = new System.Random(12345);
            for (int i = 0; i < nodeCount; i++)
            {
                for (int j = i + 1; j < nodeCount; j++)
                {
                    if (random.NextDouble() < 0.2)
                    {
                        nodes[i].AddNeighbor(nodes[j]);
                    }
                }
            }

            ChaitinRegisterAllocator allocator = new ChaitinRegisterAllocator();
            Dictionary<string, int> allocation = allocator.AllocateRegisters(graph, 10);
            foreach (GraphNode node in nodes)
            {
                int assigned = node.AssignedRegister.HasValue ? node.AssignedRegister.Value : -1;
                if (assigned != -1)
                {
                    foreach (GraphNode neighbor in node.Neighbors)
                    {
                        if (neighbor.AssignedRegister.HasValue && neighbor.AssignedRegister.Value == assigned)
                        {
                            Assert.Fail("Neighbor conflict detected for node " + node.Name);
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void AllocateRegisters_KZero_AllSpilled()
        {
            InterferenceGraph graph = new InterferenceGraph();
            GraphNode nodeA = new GraphNode("A", 1.0);
            GraphNode nodeB = new GraphNode("B", 1.0);
            graph.AddNode(nodeA);
            graph.AddNode(nodeB);
            nodeA.AddNeighbor(nodeB);
            ChaitinRegisterAllocator allocator = new ChaitinRegisterAllocator();
            Dictionary<string, int> allocation = allocator.AllocateRegisters(graph, 0);
            Assert.AreEqual(2, allocation.Count);
            Assert.AreEqual(-1, allocation["A"]);
            Assert.AreEqual(-1, allocation["B"]);
        }

        [TestMethod]
        public void AllocateRegisters_NegativeK_AllSpilled()
        {
            InterferenceGraph graph = new InterferenceGraph();
            GraphNode nodeA = new GraphNode("A", 1.0);
            GraphNode nodeB = new GraphNode("B", 1.0);
            graph.AddNode(nodeA);
            graph.AddNode(nodeB);
            nodeA.AddNeighbor(nodeB);
            ChaitinRegisterAllocator allocator = new ChaitinRegisterAllocator();
            Dictionary<string, int> allocation = allocator.AllocateRegisters(graph, -1);
            Assert.AreEqual(2, allocation.Count);
            Assert.AreEqual(-1, allocation["A"]);
            Assert.AreEqual(-1, allocation["B"]);
        }
    }
}