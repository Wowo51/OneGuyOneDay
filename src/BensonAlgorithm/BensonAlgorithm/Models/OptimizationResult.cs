using System.Collections.Generic;

namespace BensonAlgorithm.Models
{
    public class ParetoSolution
    {
        public double[] DecisionVector { get; set; }
        public double[] ObjectiveVector { get; set; }

        public ParetoSolution(double[] decisionVector, double[] objectiveVector)
        {
            this.DecisionVector = decisionVector;
            this.ObjectiveVector = objectiveVector;
        }
    }

    public class OptimizationResult
    {
        public List<ParetoSolution> ParetoSolutions { get; set; }

        public OptimizationResult()
        {
            this.ParetoSolutions = new List<ParetoSolution>();
        }
    }
}