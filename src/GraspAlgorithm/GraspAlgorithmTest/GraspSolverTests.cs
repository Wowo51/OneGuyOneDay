using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraspAlgorithm;

namespace GraspAlgorithmTest
{
    [TestClass]
    public class GraspSolverTests
    {
        [TestMethod]
        public void Solve_ReturnsTarget()
        {
            int maxIterations = 100;
            int target = 42;
            int min = 0;
            int max = 84;
            GraspSolver solver = new GraspSolver(maxIterations, target, min, max);
            int solution = solver.Solve();
            Assert.AreEqual(target, solution);
        }

        [TestMethod]
        public void Solve_ReturnsMinWhenTargetIsMin()
        {
            int maxIterations = 100;
            int target = 0;
            int min = 0;
            int max = 50;
            GraspSolver solver = new GraspSolver(maxIterations, target, min, max);
            int solution = solver.Solve();
            Assert.AreEqual(min, solution);
        }

        [TestMethod]
        public void Solve_ReturnsMaxWhenTargetIsMax()
        {
            int maxIterations = 100;
            int target = 50;
            int min = 0;
            int max = 50;
            GraspSolver solver = new GraspSolver(maxIterations, target, min, max);
            int solution = solver.Solve();
            Assert.AreEqual(max, solution);
        }

        [TestMethod]
        public void Solve_MultipleTimesReturnTarget()
        {
            int maxIterations = 100;
            int target = 42;
            int min = 0;
            int max = 84;
            for (int i = 0; i < 50; i++)
            {
                GraspSolver solver = new GraspSolver(maxIterations, target, min, max);
                int solution = solver.Solve();
                Assert.AreEqual(target, solution);
            }
        }

        [TestMethod]
        public void Solve_PerformanceTestWithLargeIterations()
        {
            int maxIterations = 10000;
            int target = 42;
            int min = 0;
            int max = 84;
            GraspSolver solver = new GraspSolver(maxIterations, target, min, max);
            int solution = solver.Solve();
            Assert.AreEqual(target, solution);
        }
    }
}