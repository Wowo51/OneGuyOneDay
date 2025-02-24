using Microsoft.VisualStudio.TestTools.UnitTesting;
using BellmanFordAlgorithm;

namespace BellmanFordAlgorithmTest
{
    [TestClass]
    public class BellmanFordSolverTests
    {
        [TestMethod]
        public void TestValidGraph()
        {
            Graph graph = new Graph(5);
            graph.AddEdge(0, 1, 6);
            graph.AddEdge(0, 2, 7);
            graph.AddEdge(1, 2, 8);
            graph.AddEdge(1, 3, 5);
            graph.AddEdge(1, 4, -4);
            graph.AddEdge(2, 3, -3);
            graph.AddEdge(2, 4, 9);
            graph.AddEdge(3, 1, -2);
            graph.AddEdge(4, 3, 7);
            graph.AddEdge(4, 0, 2);
            int[] distances;
            int[] predecessors;
            bool success = BellmanFordSolver.Solve(graph, 0, out distances, out predecessors);
            Assert.IsTrue(success, "Solver should succeed for valid graph.");
            int[] expectedDistances = new int[]
            {
                0,
                2,
                7,
                4,
                -2
            };
            int[] expectedPredecessors = new int[]
            {
                -1,
                3,
                0,
                2,
                1
            };
            Assert.AreEqual(expectedDistances.Length, distances.Length, "Distances array length mismatch.");
            Assert.AreEqual(expectedPredecessors.Length, predecessors.Length, "Predecessors array length mismatch.");
            for (int i = 0; i < expectedDistances.Length; i++)
            {
                Assert.AreEqual(expectedDistances[i], distances[i], $"Distance mismatch at vertex {i}.");
            }

            for (int i = 0; i < expectedPredecessors.Length; i++)
            {
                Assert.AreEqual(expectedPredecessors[i], predecessors[i], $"Predecessor mismatch at vertex {i}.");
            }
        }

        [TestMethod]
        public void TestNullGraph()
        {
            int[] distances;
            int[] predecessors;
            bool success = BellmanFordSolver.Solve(null, 0, out distances, out predecessors);
            Assert.IsFalse(success, "Solver should fail for null graph.");
            Assert.AreEqual(0, distances.Length, "Distances array should be empty for null graph.");
            Assert.AreEqual(0, predecessors.Length, "Predecessors array should be empty for null graph.");
        }

        [TestMethod]
        public void TestNegativeCycleGraph()
        {
            Graph graph = new Graph(2);
            graph.AddEdge(0, 1, 1);
            graph.AddEdge(1, 0, -2);
            int[] distances;
            int[] predecessors;
            bool success = BellmanFordSolver.Solve(graph, 0, out distances, out predecessors);
            Assert.IsFalse(success, "Solver should fail when graph contains a negative weight cycle.");
        }

        [TestMethod]
        public void TestLargeChainGraph()
        {
            int vertexCount = 1000;
            Graph graph = new Graph(vertexCount);
            for (int i = 0; i < vertexCount - 1; i++)
            {
                graph.AddEdge(i, i + 1, 1);
            }

            int[] distances;
            int[] predecessors;
            bool success = BellmanFordSolver.Solve(graph, 0, out distances, out predecessors);
            Assert.IsTrue(success, "Solver should succeed for large chain graph.");
            Assert.AreEqual(vertexCount, distances.Length, "Distances array length mismatch.");
            Assert.AreEqual(vertexCount, predecessors.Length, "Predecessors array length mismatch.");
            Assert.AreEqual(0, distances[0], "Source distance should be 0.");
            Assert.AreEqual(-1, predecessors[0], "Source predecessor should be -1.");
            for (int i = 1; i < vertexCount; i++)
            {
                Assert.AreEqual(i, distances[i], $"Distance mismatch at vertex {i}.");
                Assert.AreEqual(i - 1, predecessors[i], $"Predecessor mismatch at vertex {i}.");
            }
        }

        [TestMethod]
        public void TestDisconnectedGraph()
        {
            Graph graph = new Graph(4);
            graph.AddEdge(0, 1, 5);
            // Vertices 2 and 3 remain disconnected.
            int[] distances;
            int[] predecessors;
            bool success = BellmanFordSolver.Solve(graph, 0, out distances, out predecessors);
            Assert.IsTrue(success, "Solver should succeed even if graph is disconnected.");
            Assert.AreEqual(4, distances.Length, "Distances array length mismatch.");
            Assert.AreEqual(4, predecessors.Length, "Predecessors array length mismatch.");
            Assert.AreEqual(0, distances[0], "Source distance should be 0.");
            Assert.AreEqual(5, distances[1], "Distance to vertex 1 should be 5.");
            Assert.AreEqual(int.MaxValue, distances[2], "Distance for disconnected vertex 2 should be int.MaxValue.");
            Assert.AreEqual(int.MaxValue, distances[3], "Distance for disconnected vertex 3 should be int.MaxValue.");
            Assert.AreEqual(-1, predecessors[0], "Source predecessor should be -1.");
            Assert.AreEqual(0, predecessors[1], "Predecessor of vertex 1 should be 0.");
            Assert.AreEqual(-1, predecessors[2], "Predecessor of disconnected vertex 2 should be -1.");
            Assert.AreEqual(-1, predecessors[3], "Predecessor of disconnected vertex 3 should be -1.");
        }

        [TestMethod]
        public void TestSingleVertexGraph()
        {
            Graph graph = new Graph(1);
            int[] distances;
            int[] predecessors;
            bool success = BellmanFordSolver.Solve(graph, 0, out distances, out predecessors);
            Assert.IsTrue(success, "Solver should succeed for single vertex graph.");
            Assert.AreEqual(1, distances.Length, "Distances array length mismatch.");
            Assert.AreEqual(1, predecessors.Length, "Predecessors array length mismatch.");
            Assert.AreEqual(0, distances[0], "Source distance should be 0.");
            Assert.AreEqual(-1, predecessors[0], "Source predecessor should be -1.");
        }

        [TestMethod]
        public void TestSelfLoopNegativeEdge()
        {
            Graph graph = new Graph(1);
            graph.AddEdge(0, 0, -1);
            int[] distances;
            int[] predecessors;
            bool success = BellmanFordSolver.Solve(graph, 0, out distances, out predecessors);
            Assert.IsFalse(success, "Solver should fail for self-loop negative edge.");
        }

        [TestMethod]
        public void TestSelfLoopPositiveEdge()
        {
            Graph graph = new Graph(1);
            graph.AddEdge(0, 0, 5);
            int[] distances;
            int[] predecessors;
            bool success = BellmanFordSolver.Solve(graph, 0, out distances, out predecessors);
            Assert.IsTrue(success, "Solver should succeed for self-loop positive edge.");
            Assert.AreEqual(0, distances[0], "Source distance should remain 0.");
            Assert.AreEqual(-1, predecessors[0], "Source predecessor should remain -1.");
        }
    }
}