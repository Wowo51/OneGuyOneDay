using System;

namespace StochasticTunnelingAlgorithm
{
    public class StochasticTunneling
    {
        private double gamma;
        private Random random;
        public StochasticTunneling(double gamma)
        {
            this.gamma = gamma;
            this.random = new Random();
        }

        public double TransformObjective(double objective, double bestObjective)
        {
            // Transform the objective using a tunneling transformation:
            // T(f) = 1 - exp(-gamma * (f - bestObjective))
            return 1.0 - Math.Exp(-this.gamma * (objective - bestObjective));
        }

        public double[] Optimize(Func<double[], double> objectiveFunc, double[] initialGuess, int iterations)
        {
            double[] currentSolution = new double[initialGuess.Length];
            Array.Copy(initialGuess, currentSolution, initialGuess.Length);
            double bestObjective = objectiveFunc(currentSolution);
            double[] bestSolution = new double[currentSolution.Length];
            Array.Copy(currentSolution, bestSolution, currentSolution.Length);
            for (int i = 0; i < iterations; i++)
            {
                double[] candidate = new double[currentSolution.Length];
                for (int j = 0; j < currentSolution.Length; j++)
                {
                    candidate[j] = currentSolution[j] + (this.random.NextDouble() - 0.5);
                }

                double candidateObjective = objectiveFunc(candidate);
                double currentTransformed = TransformObjective(objectiveFunc(currentSolution), bestObjective);
                double candidateTransformed = TransformObjective(candidateObjective, bestObjective);
                if (candidateTransformed < currentTransformed)
                {
                    Array.Copy(candidate, currentSolution, candidate.Length);
                    if (candidateObjective < bestObjective)
                    {
                        bestObjective = candidateObjective;
                        Array.Copy(candidate, bestSolution, candidate.Length);
                    }
                }
            }

            return bestSolution;
        }
    }
}