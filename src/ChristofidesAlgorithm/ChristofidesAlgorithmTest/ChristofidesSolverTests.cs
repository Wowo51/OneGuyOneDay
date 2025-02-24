using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChristofidesAlgorithm;

namespace ChristofidesAlgorithmTest
{
    [TestClass]
    public class ChristofidesSolverTests
    {
        private bool IsValidCircuit(List<int> circuit, int cityCount)
        {
            if (circuit.Count != cityCount + 1)
            {
                return false;
            }

            if (circuit[0] != circuit[circuit.Count - 1])
            {
                return false;
            }

            HashSet<int> visited = new HashSet<int>();
            for (int i = 0; i < circuit.Count - 1; i++)
            {
                visited.Add(circuit[i]);
            }

            return visited.Count == cityCount;
        }

        private double[, ] GenerateSymmetricMatrix(int n, double min, double max, int seed)
        {
            Random random = new Random(seed);
            double[, ] matrix = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    if (i == j)
                    {
                        matrix[i, j] = 0;
                    }
                    else
                    {
                        double value = random.NextDouble() * (max - min) + min;
                        matrix[i, j] = value;
                        matrix[j, i] = value;
                    }
                }
            }

            return matrix;
        }

        [TestMethod]
        public void TestEmptyMatrix()
        {
            double[, ] distances = new double[0, 0];
            List<int> result = ChristofidesSolver.SolveTSP(distances);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestSingleCity()
        {
            double[, ] distances = new double[1, 1]
            {
                {
                    0
                }
            };
            List<int> result = ChristofidesSolver.SolveTSP(distances);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(0, result[0]);
        }

        [TestMethod]
        public void TestTwoCities()
        {
            double[, ] distances = new double[2, 2]
            {
                {
                    0,
                    1
                },
                {
                    1,
                    0
                }
            };
            List<int> result = ChristofidesSolver.SolveTSP(distances);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(result[0], result[result.Count - 1]);
            HashSet<int> cities = new HashSet<int>
            {
                result[0],
                result[1]
            };
            Assert.AreEqual(2, cities.Count);
        }

        [TestMethod]
        public void TestFourCities()
        {
            double[, ] distances = new double[4, 4]
            {
                {
                    0,
                    10,
                    15,
                    20
                },
                {
                    10,
                    0,
                    35,
                    25
                },
                {
                    15,
                    35,
                    0,
                    30
                },
                {
                    20,
                    25,
                    30,
                    0
                }
            };
            List<int> result = ChristofidesSolver.SolveTSP(distances);
            Assert.IsTrue(IsValidCircuit(result, 4));
        }

        [TestMethod]
        public void TestLargeGraph()
        {
            int cityCount = 20;
            double[, ] distances = GenerateSymmetricMatrix(cityCount, 1, 100, 42);
            List<int> result = ChristofidesSolver.SolveTSP(distances);
            Assert.IsTrue(IsValidCircuit(result, cityCount));
        }
    }
}