using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinimumDegreeAlgorithm;

namespace MinimumDegreeAlgorithmTest
{
    [TestClass]
    public class MinimumDegreeOrdererTests
    {
        [TestMethod]
        public void TestNullGraph()
        {
            List<HashSet<int>>? graph = null;
            int[] ordering = MinimumDegreeOrderer.ComputeOrdering(graph);
            Assert.IsNotNull(ordering);
            Assert.AreEqual(0, ordering.Length);
        }

        [TestMethod]
        public void TestEmptyGraph()
        {
            List<HashSet<int>> graph = new List<HashSet<int>>();
            int[] ordering = MinimumDegreeOrderer.ComputeOrdering(graph);
            Assert.IsNotNull(ordering);
            Assert.AreEqual(0, ordering.Length);
        }

        [TestMethod]
        public void TestSimpleGraph()
        {
            List<HashSet<int>> graph = new List<HashSet<int>>
            {
                new HashSet<int>
                {
                    1,
                    2
                },
                new HashSet<int>
                {
                    0,
                    2,
                    3
                },
                new HashSet<int>
                {
                    0,
                    1,
                    3
                },
                new HashSet<int>
                {
                    1,
                    2,
                    4
                },
                new HashSet<int>
                {
                    3
                }
            };
            int[] ordering = MinimumDegreeOrderer.ComputeOrdering(graph);
            int[] expected = new int[]
            {
                4,
                0,
                1,
                2,
                3
            };
            CollectionAssert.AreEqual(expected, ordering);
        }

        [TestMethod]
        public void TestCompleteGraph()
        {
            int n = 5;
            List<HashSet<int>> graph = new List<HashSet<int>>();
            for (int i = 0; i < n; i++)
            {
                HashSet<int> neighbors = new HashSet<int>();
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        neighbors.Add(j);
                    }
                }

                graph.Add(neighbors);
            }

            int[] ordering = MinimumDegreeOrderer.ComputeOrdering(graph);
            Assert.AreEqual(n, ordering.Length);
            for (int i = 0; i < n; i++)
            {
                Assert.AreEqual(i, ordering[i]);
            }
        }

        [TestMethod]
        public void TestRandomSparseGraph()
        {
            int n = 100;
            Random random = new Random(42);
            List<HashSet<int>> graph = new List<HashSet<int>>();
            for (int i = 0; i < n; i++)
            {
                graph.Add(new HashSet<int>());
            }

            double p = 0.05;
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (random.NextDouble() < p)
                    {
                        graph[i].Add(j);
                        graph[j].Add(i);
                    }
                }
            }

            int[] ordering = MinimumDegreeOrderer.ComputeOrdering(graph);
            Assert.AreEqual(n, ordering.Length);
            HashSet<int> seen = new HashSet<int>();
            for (int i = 0; i < ordering.Length; i++)
            {
                bool added = seen.Add(ordering[i]);
                Assert.IsTrue(added, "Duplicate vertex in ordering");
            }
        }
    }
}