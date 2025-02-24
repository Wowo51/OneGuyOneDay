using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StateSpaceSearch;

namespace StateSpaceSearchTest
{
    [TestClass]
    public class StateSpaceSearchAlgorithmTests
    {
        private class LinearProblem : IStateSpaceProblem<int>
        {
            public int InitialState { get; }
            public int Goal { get; }

            public LinearProblem(int initial, int goal)
            {
                InitialState = initial;
                Goal = goal;
            }

            public bool IsGoalState(int state)
            {
                return state == Goal;
            }

            public IEnumerable<int> GetNeighbors(int state)
            {
                List<int> neighbors = new List<int>();
                if (state < Goal)
                {
                    neighbors.Add(state + 1);
                }

                return neighbors;
            }

            public int GetCost(int fromState, int toState)
            {
                return 1;
            }

            public int Heuristic(int state)
            {
                return Goal - state;
            }
        }

        private class BranchingProblem : IStateSpaceProblem<int>
        {
            public int InitialState { get; }
            public int Goal { get; }

            public BranchingProblem(int initial, int goal)
            {
                InitialState = initial;
                Goal = goal;
            }

            public bool IsGoalState(int state)
            {
                return state == Goal;
            }

            public IEnumerable<int> GetNeighbors(int state)
            {
                List<int> neighbors = new List<int>();
                if (state < Goal)
                {
                    if (state + 1 <= Goal)
                    {
                        neighbors.Add(state + 1);
                    }

                    if (state + 2 <= Goal)
                    {
                        neighbors.Add(state + 2);
                    }
                }

                return neighbors;
            }

            public int GetCost(int fromState, int toState)
            {
                return 1;
            }

            public int Heuristic(int state)
            {
                return Goal - state;
            }
        }

        private class NoSolutionProblem : IStateSpaceProblem<int>
        {
            public int InitialState { get; }

            public NoSolutionProblem(int initial)
            {
                InitialState = initial;
            }

            public bool IsGoalState(int state)
            {
                return false;
            }

            public IEnumerable<int> GetNeighbors(int state)
            {
                return new List<int>();
            }

            public int GetCost(int fromState, int toState)
            {
                return 1;
            }

            public int Heuristic(int state)
            {
                return 1;
            }
        }

        [TestMethod]
        public void TestInitialStateIsGoal()
        {
            LinearProblem problem = new LinearProblem(5, 5);
            List<int> path = StateSpaceSearchAlgorithm.Search(problem);
            Assert.IsNotNull(path);
            Assert.AreEqual(1, path.Count);
            Assert.AreEqual(5, path[0]);
        }

        [TestMethod]
        public void TestSimpleLinearPath()
        {
            LinearProblem problem = new LinearProblem(0, 5);
            List<int> path = StateSpaceSearchAlgorithm.Search(problem);
            Assert.IsNotNull(path);
            Assert.AreEqual(6, path.Count);
            for (int index = 0; index < path.Count; index++)
            {
                Assert.AreEqual(index, path[index]);
            }
        }

        [TestMethod]
        public void TestNoSolution()
        {
            NoSolutionProblem problem = new NoSolutionProblem(0);
            List<int> path = StateSpaceSearchAlgorithm.Search(problem);
            Assert.IsNotNull(path);
            Assert.AreEqual(0, path.Count);
        }

        [TestMethod]
        public void TestBranchingSearch()
        {
            BranchingProblem problem = new BranchingProblem(0, 7);
            List<int> path = StateSpaceSearchAlgorithm.Search(problem);
            Assert.IsNotNull(path);
            Assert.IsTrue(path.Count > 0);
            Assert.AreEqual(0, path[0]);
            Assert.AreEqual(7, path[path.Count - 1]);
            for (int i = 1; i < path.Count; i++)
            {
                int diff = path[i] - path[i - 1];
                Assert.IsTrue(diff == 1 || diff == 2, "Each step must be 1 or 2");
            }
        }

        [TestMethod]
        public void TestLargeLinearProblem()
        {
            LinearProblem problem = new LinearProblem(0, 1000);
            List<int> path = StateSpaceSearchAlgorithm.Search(problem);
            Assert.IsNotNull(path);
            Assert.AreEqual(1001, path.Count);
            Assert.AreEqual(0, path[0]);
            Assert.AreEqual(1000, path[path.Count - 1]);
        }
    }
}