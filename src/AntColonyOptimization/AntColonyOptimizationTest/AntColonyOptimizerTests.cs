using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AntColonyOptimization;

namespace AntColonyOptimizationTest
{
    [TestClass]
    public class AntColonyOptimizerTests
    {
        [TestMethod]
        public void TestOptimizeRouteCompleteness()
        {
            double[, ] distances = new double[, ]
            {
                {
                    0.0,
                    10.0,
                    15.0,
                    20.0
                },
                {
                    10.0,
                    0.0,
                    35.0,
                    25.0
                },
                {
                    15.0,
                    35.0,
                    0.0,
                    30.0
                },
                {
                    20.0,
                    25.0,
                    30.0,
                    0.0
                }
            };
            int numberOfAnts = 5;
            int iterations = 50;
            double alpha = 1.0;
            double beta = 2.0;
            double evaporationRate = 0.5;
            AntColonyOptimizer optimizer = new AntColonyOptimizer(distances, numberOfAnts, iterations, alpha, beta, evaporationRate);
            Tuple<List<int>, double> result = optimizer.Optimize();
            List<int> route = result.Item1;
            Assert.AreEqual(distances.GetLength(0), route.Count, "Route should include each city exactly once.");
            HashSet<int> uniqueCities = new HashSet<int>(route);
            Assert.AreEqual(distances.GetLength(0), uniqueCities.Count, "Route should have no duplicate cities.");
        }

        [TestMethod]
        public void TestOptimizeReturnsNonTrivialRouteLength()
        {
            double[, ] distances = new double[, ]
            {
                {
                    0.0,
                    10.0,
                    15.0,
                    20.0
                },
                {
                    10.0,
                    0.0,
                    35.0,
                    25.0
                },
                {
                    15.0,
                    35.0,
                    0.0,
                    30.0
                },
                {
                    20.0,
                    25.0,
                    30.0,
                    0.0
                }
            };
            int numberOfAnts = 5;
            int iterations = 50;
            double alpha = 1.0;
            double beta = 2.0;
            double evaporationRate = 0.5;
            AntColonyOptimizer optimizer = new AntColonyOptimizer(distances, numberOfAnts, iterations, alpha, beta, evaporationRate);
            Tuple<List<int>, double> result = optimizer.Optimize();
            double routeLength = result.Item2;
            Assert.IsTrue(routeLength > 0.0, "Route length should be greater than zero.");
        }

        [TestMethod]
        public void TestSyntheticLargeData()
        {
            int cities = 50;
            double[, ] distances = CreateSymmetricMatrix(cities, 1, 100, 42);
            int numberOfAnts = 10;
            int iterations = 20;
            double alpha = 1.0;
            double beta = 2.0;
            double evaporationRate = 0.5;
            AntColonyOptimizer optimizer = new AntColonyOptimizer(distances, numberOfAnts, iterations, alpha, beta, evaporationRate);
            Tuple<List<int>, double> result = optimizer.Optimize();
            List<int> route = result.Item1;
            Assert.AreEqual(cities, route.Count, "Route should include each city for synthetic large data.");
            HashSet<int> uniqueCities = new HashSet<int>(route);
            Assert.AreEqual(cities, uniqueCities.Count, "Route should have no duplicate cities for synthetic large data.");
        }

        [TestMethod]
        public void TestSingleAntWithSimpleData()
        {
            double[, ] distances = new double[, ]
            {
                {
                    0.0,
                    5.0
                },
                {
                    5.0,
                    0.0
                }
            };
            int numberOfAnts = 1;
            int iterations = 20;
            double alpha = 1.0;
            double beta = 2.0;
            double evaporationRate = 0.5;
            AntColonyOptimizer optimizer = new AntColonyOptimizer(distances, numberOfAnts, iterations, alpha, beta, evaporationRate);
            Tuple<List<int>, double> result = optimizer.Optimize();
            List<int> route = result.Item1;
            Assert.AreEqual(2, route.Count, "Route should include both cities.");
            Assert.IsTrue(route.Contains(0) && route.Contains(1), "Route should contain both cities 0 and 1.");
        }

        private double[, ] CreateSymmetricMatrix(int cities, int minDistance, int maxDistance, int seed)
        {
            double[, ] matrix = new double[cities, cities];
            Random random = new Random(seed);
            for (int i = 0; i < cities; i++)
            {
                for (int j = 0; j < cities; j++)
                {
                    if (i == j)
                    {
                        matrix[i, j] = 0.0;
                    }
                    else if (j > i)
                    {
                        double distance = minDistance + random.NextDouble() * (maxDistance - minDistance);
                        matrix[i, j] = distance;
                        matrix[j, i] = distance;
                    }
                }
            }

            return matrix;
        }
    }
}