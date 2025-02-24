using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CuttingPlaneMethod;

namespace CuttingPlaneMethodTest
{
    [TestClass]
    public class CuttingPlaneSolverTests
    {
        [TestMethod]
        public void Test_FeasibleRegion_GetCandidateSolution()
        {
            CuttingPlaneMethod.FeasibleRegion region = new CuttingPlaneMethod.FeasibleRegion(0.0, 10.0);
            double candidate = region.GetCandidateSolution();
            Assert.AreEqual(5.0, candidate, 1e-6, "Candidate solution should be average of bounds.");
        }

        [TestMethod]
        public void Test_FeasibleRegion_AddCut()
        {
            CuttingPlaneMethod.FeasibleRegion region = new CuttingPlaneMethod.FeasibleRegion(0.0, 10.0);
            region.AddCut(4.0);
            Assert.AreEqual(4.0, region.LowerBound, 1e-6, "LowerBound not updated to 4.0.");
            region.AddCut(2.0);
            Assert.AreEqual(4.0, region.LowerBound, 1e-6, "LowerBound should remain 4.0 after lower cut.");
            region.AddCut(6.0);
            Assert.AreEqual(6.0, region.LowerBound, 1e-6, "LowerBound not updated to 6.0.");
        }

        [TestMethod]
        public void Test_CuttingPlaneSolver_Convergence()
        {
            StringWriter outputWriter = new StringWriter();
            TextWriter originalOutput = Console.Out;
            Console.SetOut(outputWriter);
            CuttingPlaneMethod.CuttingPlaneSolver solver = new CuttingPlaneMethod.CuttingPlaneSolver();
            solver.Solve();
            Console.SetOut(originalOutput);
            string output = outputWriter.ToString();
            Assert.IsTrue(output.Contains("Converged to optimal solution:"), "Solver did not converge as expected.");
        }

        [TestMethod]
        public void Test_FeasibleRegion_RandomCandidate()
        {
            System.Random random = new System.Random();
            for (int i = 0; i < 100; i++)
            {
                double lower = random.NextDouble() * 100;
                double upper = lower + random.NextDouble() * 100;
                CuttingPlaneMethod.FeasibleRegion region = new CuttingPlaneMethod.FeasibleRegion(lower, upper);
                double candidate = region.GetCandidateSolution();
                double expected = (lower + upper) / 2.0;
                Assert.AreEqual(expected, candidate, 1e-6, "Random candidate solution incorrect.");
            }
        }

        [TestMethod]
        public void Test_FeasibleRegion_MultipleCuts()
        {
            CuttingPlaneMethod.FeasibleRegion region = new CuttingPlaneMethod.FeasibleRegion(0.0, 100.0);
            double latestCut = 0.0;
            for (int i = 0; i < 50; i++)
            {
                double nextCut = region.GetCandidateSolution();
                Assert.IsTrue(nextCut > latestCut, "Next candidate must be greater than previous lower bound.");
                region.AddCut(nextCut);
                latestCut = nextCut;
            }

            Assert.IsTrue(region.LowerBound >= latestCut, "Final lower bound not updated correctly.");
        }
    }
}