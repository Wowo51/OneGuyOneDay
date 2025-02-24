using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using LocalSearch;

namespace LocalSearchTest
{
    [TestClass]
    public class LocalSearchAlgorithmTests
    {
        [TestMethod]
        public void TestNullObjective()
        {
            int initial = 10;
            // When objective is null, the algorithm should return the initial solution.
            int result = LocalSearchAlgorithm.Search(initial, null, (int x) => new List<int> { 5, 6 });
            Assert.AreEqual(initial, result);
        }

        [TestMethod]
        public void TestNullGetNeighbors()
        {
            int initial = 10;
            double Objective(int x)
            {
                return x;
            }

            // When getNeighbors is null, the algorithm returns the initial solution.
            int result = LocalSearchAlgorithm.Search(initial, Objective, null);
            Assert.AreEqual(initial, result);
        }

        [TestMethod]
        public void TestNoBetterNeighbor()
        {
            int initial = 5;
            double Objective(int x)
            {
                return x;
            }

            IEnumerable<int> GetNeighbors(int x)
            {
                // Neighbors do not have a lower objective value than initial.
                return new List<int>
                {
                    10,
                    20,
                    5
                };
            }

            int result = LocalSearchAlgorithm.Search(initial, Objective, GetNeighbors);
            Assert.AreEqual(initial, result);
        }

        [TestMethod]
        public void TestIterativeImprovementMinimization()
        {
            int initial = 10;
            // Objective: minimize (x-3)^2. The optimal solution is x = 3.
            double Objective(int x)
            {
                return (x - 3) * (x - 3);
            }

            IEnumerable<int> GetNeighbors(int x)
            {
                // Generate two neighbors: one decrement and one increment.
                return new List<int>
                {
                    x - 1,
                    x + 1
                };
            }

            int result = LocalSearchAlgorithm.Search(initial, Objective, GetNeighbors);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void TestCustomIsBetterForMaximization()
        {
            int initial = 1;
            // Objective: maximize x.
            double Objective(int x)
            {
                return x;
            }

            IEnumerable<int> GetNeighbors(int x)
            {
                // Provide a static list; only 3 is a better candidate.
                return new List<int>
                {
                    3,
                    1
                };
            }

            // Custom isBetter delegate for maximization.
            bool IsBetter(double currentValue, double candidateValue)
            {
                return candidateValue > currentValue;
            }

            int result = LocalSearchAlgorithm.Search(initial, Objective, GetNeighbors, IsBetter);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void TestLargeNeighborGeneration()
        {
            int initial = 100;
            // Objective: minimize (x-50)^2. The optimal solution is x = 50.
            double Objective(int x)
            {
                return (x - 50) * (x - 50);
            }

            IEnumerable<int> GetNeighbors(int x)
            {
                // Synthetically generate 101 neighbors from x-50 to x+50.
                List<int> neighbors = new List<int>();
                for (int i = x - 50; i <= x + 50; i++)
                {
                    neighbors.Add(i);
                }

                return neighbors;
            }

            int result = LocalSearchAlgorithm.Search(initial, Objective, GetNeighbors);
            Assert.AreEqual(50, result);
        }
    }
}