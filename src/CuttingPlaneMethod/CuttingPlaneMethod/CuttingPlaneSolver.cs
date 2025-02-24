using System;
using System.Collections.Generic;

namespace CuttingPlaneMethod
{
    public class CuttingPlaneSolver
    {
        public void Solve()
        {
            double tolerance = 1e-6;
            int maxIterations = 100;
            double target = 10.0;
            FeasibleRegion region = new FeasibleRegion(0.0, target);
            List<string> cuts = new List<string>();
            for (int iteration = 0; iteration < maxIterations; iteration++)
            {
                double candidate = region.GetCandidateSolution();
                string cut = "x >= " + candidate.ToString("F6");
                region.AddCut(candidate);
                cuts.Add(cut);
                Console.WriteLine("Iteration " + iteration + ": candidate = " + candidate.ToString("F6"));
                Console.WriteLine("Added cut: " + cut);
                if (Math.Abs(target - region.LowerBound) < tolerance)
                {
                    Console.WriteLine("Converged to optimal solution: " + region.LowerBound.ToString("F6"));
                    break;
                }
            }
        }
    }
}