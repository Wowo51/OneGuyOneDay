using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TopologicalSort;

namespace TopologicalSortTest
{
    [TestClass]
    public class TopologicalSorterTests
    {
        [TestMethod]
        public void TestEmptyGraph()
        {
            Dictionary<string, IEnumerable<string>> graph = new Dictionary<string, IEnumerable<string>>();
            List<string> sorted;
            bool result = TopologicalSorter.TrySort<string>(graph, out sorted);
            Assert.IsTrue(result);
            Assert.AreEqual(0, sorted.Count);
        }

        [TestMethod]
        public void TestNullGraph()
        {
            List<string> sorted;
            bool result = TopologicalSorter.TrySort<string>(null, out sorted);
            Assert.IsFalse(result);
            Assert.AreEqual(0, sorted.Count);
        }

        [TestMethod]
        public void TestValidGraph()
        {
            // Graph: A -> (B, C), B -> D, C -> D.
            Dictionary<string, IEnumerable<string>> dependencyGraph = new Dictionary<string, IEnumerable<string>>();
            dependencyGraph["A"] = new List<string>();
            dependencyGraph["B"] = new List<string>
            {
                "A"
            };
            dependencyGraph["C"] = new List<string>
            {
                "A"
            };
            dependencyGraph["D"] = new List<string>
            {
                "B",
                "C"
            };
            List<string> sorted;
            bool result = TopologicalSorter.TrySort<string>(dependencyGraph, out sorted);
            Assert.IsTrue(result);
            int indexA = sorted.IndexOf("A");
            int indexB = sorted.IndexOf("B");
            int indexC = sorted.IndexOf("C");
            int indexD = sorted.IndexOf("D");
            Assert.IsTrue(indexA < indexB, "A should come before B");
            Assert.IsTrue(indexA < indexC, "A should come before C");
            Assert.IsTrue(indexB < indexD, "B should come before D");
            Assert.IsTrue(indexC < indexD, "C should come before D");
        }

        [TestMethod]
        public void TestGraphWithCycle()
        {
            // Cycle: A -> C, B -> A, C -> B.
            Dictionary<string, IEnumerable<string>> dependencyGraph = new Dictionary<string, IEnumerable<string>>();
            dependencyGraph["A"] = new List<string>
            {
                "C"
            };
            dependencyGraph["B"] = new List<string>
            {
                "A"
            };
            dependencyGraph["C"] = new List<string>
            {
                "B"
            };
            List<string> sorted;
            bool result = TopologicalSorter.TrySort<string>(dependencyGraph, out sorted);
            Assert.IsFalse(result);
            List<string> sortedCycle = TopologicalSorter.Sort<string>(dependencyGraph);
            Assert.AreEqual(0, sortedCycle.Count, "Cycle detected: sorted list must be empty");
        }

        [TestMethod]
        public void TestLargeGraph()
        {
            // Build a chain of 1000 nodes: 0 -> 1, 1 -> 2, ..., 998 -> 999.
            Dictionary<int, IEnumerable<int>> dependencyGraph = new Dictionary<int, IEnumerable<int>>();
            dependencyGraph[0] = new List<int>();
            for (int i = 1; i < 1000; i++)
            {
                dependencyGraph[i] = new List<int>
                {
                    i - 1
                };
            }

            List<int> sorted;
            bool result = TopologicalSorter.TrySort<int>(dependencyGraph, out sorted);
            Assert.IsTrue(result);
            Assert.AreEqual(1000, sorted.Count);
            for (int index = 0; index < 1000; index++)
            {
                Assert.AreEqual(index, sorted[index], "Nodes should be in ascending order");
            }
        }

        [TestMethod]
        public void TestNodeWithNullDependencyList()
        {
            // Test a node whose dependency list is explicitly null.
            Dictionary<string, IEnumerable<string>> dependencyGraph = new Dictionary<string, IEnumerable<string>>();
            dependencyGraph["A"] = null;
            List<string> sorted;
            bool result = TopologicalSorter.TrySort<string>(dependencyGraph, out sorted);
            Assert.IsTrue(result);
            Assert.AreEqual(1, sorted.Count);
            Assert.AreEqual("A", sorted[0]);
        }

        [TestMethod]
        public void TestSelfDependentNode()
        {
            // Test a node that depends on itself.
            Dictionary<string, IEnumerable<string>> dependencyGraph = new Dictionary<string, IEnumerable<string>>();
            dependencyGraph["A"] = new List<string>
            {
                "A"
            };
            List<string> sorted;
            bool result = TopologicalSorter.TrySort<string>(dependencyGraph, out sorted);
            Assert.IsFalse(result);
            Assert.AreEqual(0, sorted.Count);
            List<string> sortedUsingSort = TopologicalSorter.Sort<string>(dependencyGraph);
            Assert.AreEqual(0, sortedUsingSort.Count, "Self dependency cycle should return empty sorted list");
        }

        [TestMethod]
        public void TestDisjointGraph()
        {
            // Test a disjoint graph with two independent parts.
            Dictionary<string, IEnumerable<string>> dependencyGraph = new Dictionary<string, IEnumerable<string>>();
            dependencyGraph["A"] = new List<string>(); // independent node
            dependencyGraph["C"] = new List<string>(); // prerequisite for B
            dependencyGraph["B"] = new List<string>
            {
                "C"
            };
            List<string> sorted;
            bool result = TopologicalSorter.TrySort<string>(dependencyGraph, out sorted);
            Assert.IsTrue(result);
            Assert.AreEqual(3, sorted.Count);
            int indexC = sorted.IndexOf("C");
            int indexB = sorted.IndexOf("B");
            Assert.IsTrue(indexC < indexB, "C should come before B in disjoint graph");
        }
    }
}