using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeamStackSearch;

namespace BeamStackSearchTest
{
    [TestClass]
    public class BeamStackSearchAlgorithmTests
    {
        [TestMethod]
        public void Search_NullInitialState_ReturnsEmptyPath()
        {
            BeamStackSearchAlgorithm<string> algorithm = new BeamStackSearchAlgorithm<string>(1);
            List<string> result = algorithm.Search((string? )null, state => false, state => new List<string>(), state => 0);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Search_InitialStateIsGoal_ReturnsInitialState()
        {
            BeamStackSearchAlgorithm<int> algorithm = new BeamStackSearchAlgorithm<int>(2);
            List<int> result = algorithm.Search(42, state => state == 42, state => new List<int> { state + 1 }, state => state);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(42, result[0]);
        }

        [TestMethod]
        public void Search_FindPathThroughSuccessors_ReturnsValidPath()
        {
            BeamStackSearchAlgorithm<int> algorithm = new BeamStackSearchAlgorithm<int>(1);
            List<int> GetSuccessors(int state)
            {
                if (state < 10)
                {
                    return new List<int>
                    {
                        state + 1,
                        state + 2
                    };
                }

                return new List<int>();
            }

            bool IsGoal(int state)
            {
                return state == 10;
            }

            double ScoreFunc(int state)
            {
                return state;
            }

            List<int> result = algorithm.Search(1, IsGoal, GetSuccessors, ScoreFunc);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains(10));
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(10, result[result.Count - 1]);
        }

        [TestMethod]
        public void Search_NoSolution_ReturnsEmptyPath()
        {
            BeamStackSearchAlgorithm<int> algorithm = new BeamStackSearchAlgorithm<int>(2);
            List<int> GetSuccessors(int state)
            {
                return new List<int>();
            }

            bool IsGoal(int state)
            {
                return state == 1;
            }

            double ScoreFunc(int state)
            {
                return state;
            }

            List<int> result = algorithm.Search(0, IsGoal, GetSuccessors, ScoreFunc);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Search_LargeDataSet_ReturnsSolution()
        {
            BeamStackSearchAlgorithm<int> algorithm = new BeamStackSearchAlgorithm<int>(2);
            List<int> GetSuccessors(int state)
            {
                if (state < 50)
                {
                    return new List<int>
                    {
                        state + 1,
                        state + 2
                    };
                }

                return new List<int>();
            }

            bool IsGoal(int state)
            {
                return state == 50;
            }

            double ScoreFunc(int state)
            {
                return state;
            }

            List<int> result = algorithm.Search(0, IsGoal, GetSuccessors, ScoreFunc);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count >= 2);
            Assert.AreEqual(0, result[0]);
            Assert.AreEqual(50, result[result.Count - 1]);
            for (int i = 1; i < result.Count; i++)
            {
                int diff = result[i] - result[i - 1];
                Assert.IsTrue(diff == 1 || diff == 2, "Each step difference must be either 1 or 2");
            }
        }
    }
}