using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomHillClimb;

namespace RandomHillClimbTest
{
    [TestClass]
    public class RandomRestartHillClimberTests
    {
        [TestMethod]
        public void TestNoRestartsReturnsInitial()
        {
            RandomRestartHillClimber<int> climber = new RandomRestartHillClimber<int>();
            int initialValue = 5;
            int result = climber.FindOptimalSolution(() => initialValue, (int x) => new List<int>(), (int x) => (double)x, 0, 10);
            Assert.AreEqual(initialValue, result);
        }

        [TestMethod]
        public void TestSimpleHillClimbReachesOptimal()
        {
            RandomRestartHillClimber<int> climber = new RandomRestartHillClimber<int>();
            int result = climber.FindOptimalSolution(() => 5, (int x) => (x < 10) ? new List<int> { x + 1 } : new List<int>(), (int x) => (double)x, 1, 10);
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void TestMultipleRestartsBestSolutionSelected()
        {
            RandomRestartHillClimber<int> climber = new RandomRestartHillClimber<int>();
            int[] seeds = new int[]
            {
                8,
                11,
                7
            };
            int seedIndex = 0;
            int result = climber.FindOptimalSolution(() =>
            {
                int value = seeds[seedIndex];
                seedIndex++;
                return value;
            }, (int x) => (x < 10) ? new List<int> { x + 1 } : new List<int>(), (int x) => (double)x, 3, 10);
            Assert.AreEqual(11, result);
        }

        [TestMethod]
        public void TestNeighborGeneratorReturnsNull()
        {
            RandomRestartHillClimber<int> climber = new RandomRestartHillClimber<int>();
            int result = climber.FindOptimalSolution(() => 7, (int x) => null, (int x) => (double)x, 1, 10);
            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void TestLargeHillClimbReachesMaxLocal()
        {
            RandomRestartHillClimber<int> climber = new RandomRestartHillClimber<int>();
            int result = climber.FindOptimalSolution(() => 0, (int x) =>
            {
                List<int> neighbors = new List<int>();
                if (x < 1000)
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        if (x + i <= 1000)
                        {
                            neighbors.Add(x + i);
                        }
                    }
                }

                return neighbors;
            }, (int x) => (double)x, 1, 300);
            Assert.AreEqual(1000, result);
        }

        [TestMethod]
        public void TestMultipleRestartsWithLargeDataSet()
        {
            System.Random random = new System.Random(42);
            int bestExpected = 1000;
            int numRestarts = 50;
            int maxIterations = 1000;
            RandomRestartHillClimber<int> climber = new RandomRestartHillClimber<int>();
            int result = climber.FindOptimalSolution(() => random.Next(0, 900), (int x) =>
            {
                List<int> list = new List<int>();
                if (x < bestExpected)
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        int candidate = x + i;
                        if (candidate <= bestExpected)
                        {
                            list.Add(candidate);
                        }
                    }
                }

                return list;
            }, (int x) => (double)x, numRestarts, maxIterations);
            Assert.AreEqual(bestExpected, result);
        }
    }
}