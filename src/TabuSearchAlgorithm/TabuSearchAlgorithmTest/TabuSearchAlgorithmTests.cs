using Microsoft.VisualStudio.TestTools.UnitTesting;
using TabuSearchAlgorithm;

namespace TabuSearchAlgorithmTest
{
    [TestClass]
    public class TabuSearchAlgorithmTests
    {
        private static int ComputeExpected(int initial, int maxIterations)
        {
            int finalCandidate = initial + maxIterations;
            if (initial >= 0)
            {
                return initial;
            }
            else if (finalCandidate >= 0)
            {
                return 0;
            }
            else
            {
                return finalCandidate;
            }
        }

        [TestMethod]
        public void Execute_NoIterations_ReturnsInitialSolution()
        {
            TabuSearch tabuSearch = new TabuSearch(0, 1);
            int result = tabuSearch.Execute(5);
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void Execute_PositiveInitialSolution_ReturnsInitialSolution()
        {
            TabuSearch tabuSearch = new TabuSearch(10, 3);
            int initial = 10;
            int result = tabuSearch.Execute(initial);
            Assert.AreEqual(initial, result);
        }

        [TestMethod]
        public void Execute_NegativeInitialSolution_SufficientIterations_ReturnsZero()
        {
            TabuSearch tabuSearch = new TabuSearch(20, 5);
            int initial = -10;
            int result = tabuSearch.Execute(initial);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Execute_NegativeInitialSolution_InsufficientIterations_ReturnsFinalCandidate()
        {
            TabuSearch tabuSearch = new TabuSearch(5, 3);
            int initial = -10;
            // Candidate sequence: -9, -8, -7, -6, -5; expected best is -5.
            int result = tabuSearch.Execute(initial);
            Assert.AreEqual(-5, result);
        }

        [TestMethod]
        public void Execute_RandomTest_ComputeExpectedResult()
        {
            int maxIterations = 50;
            for (int initial = -110; initial < 110; initial += 20)
            {
                TabuSearch tabuSearch = new TabuSearch(maxIterations, 10);
                int result = tabuSearch.Execute(initial);
                int expected = ComputeExpected(initial, maxIterations);
                Assert.AreEqual(expected, result, "Failed for initial value: " + initial.ToString());
            }
        }
    }
}