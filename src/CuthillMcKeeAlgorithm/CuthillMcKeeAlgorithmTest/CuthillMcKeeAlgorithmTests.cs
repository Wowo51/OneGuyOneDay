using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using CuthillMcKeeAlgorithm;

namespace CuthillMcKeeAlgorithmTest
{
    [TestClass]
    public class CuthillMcKeeTests
    {
        [TestMethod]
        public void TestComputeOrdering_NullGraph()
        {
            int[] result = CuthillMcKee.ComputeOrdering(null);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void TestComputeOrdering_SingleVertex()
        {
            List<IList<int>> graph = new List<IList<int>>();
            graph.Add(new List<int>());
            int[] result = CuthillMcKee.ComputeOrdering(graph);
            CollectionAssert.AreEqual(new int[] { 0 }, result);
        }

        [TestMethod]
        public void TestComputeOrdering_MultipleComponents()
        {
            List<IList<int>> graph = new List<IList<int>>
            {
                new List<int>
                {
                    1,
                    2
                },
                new List<int>
                {
                    0
                },
                new List<int>
                {
                    0
                },
                new List<int>
                {
                    4
                },
                new List<int>
                {
                    3
                }
            };
            int[] result = CuthillMcKee.ComputeOrdering(graph);
            int[] expected = new int[]
            {
                1,
                0,
                2,
                3,
                4
            };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestComputeOrderingFromMatrix_Null()
        {
            int[] result = CuthillMcKee.ComputeOrderingFromMatrix(null);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void TestComputeOrderingFromMatrix_NonSquare()
        {
            int[, ] matrix = new int[2, 3];
            int[] result = CuthillMcKee.ComputeOrderingFromMatrix(matrix);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void TestComputeOrderingFromMatrix_SimpleMatrix()
        {
            int[, ] matrix = new int[4, 4]
            {
                {
                    0,
                    1,
                    1,
                    0
                },
                {
                    1,
                    0,
                    1,
                    0
                },
                {
                    1,
                    1,
                    0,
                    1
                },
                {
                    0,
                    0,
                    1,
                    0
                }
            };
            int[] result = CuthillMcKee.ComputeOrderingFromMatrix(matrix);
            int[] expected = new int[]
            {
                3,
                2,
                0,
                1
            };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestComputeOrdering_LargeRandomGraph()
        {
            const int numVertices = 100;
            List<IList<int>> graph = new List<IList<int>>();
            for (int i = 0; i < numVertices; i++)
            {
                graph.Add(new List<int>());
            }

            Random random = new Random(42);
            for (int i = 0; i < numVertices; i++)
            {
                for (int j = i + 1; j < numVertices; j++)
                {
                    if (random.NextDouble() < 0.05)
                    {
                        graph[i].Add(j);
                        graph[j].Add(i);
                    }
                }
            }

            int[] result = CuthillMcKee.ComputeOrdering(graph);
            Assert.AreEqual(numVertices, result.Length);
            int[] sortedResult = (int[])result.Clone();
            Array.Sort(sortedResult);
            List<int> expected = new List<int>();
            for (int i = 0; i < numVertices; i++)
            {
                expected.Add(i);
            }

            CollectionAssert.AreEqual(expected.ToArray(), sortedResult);
        }
    }
}