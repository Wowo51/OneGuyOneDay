using System;

namespace SimulatedAnnealing
{
    public class SimulatedAnnealingAlgorithm
    {
        private readonly Random _random;
        public double InitialTemperature { get; set; }
        public double CoolingRate { get; set; }
        public int Iterations { get; set; }

        public SimulatedAnnealingAlgorithm()
        {
            _random = new Random();
            InitialTemperature = 100.0;
            CoolingRate = 0.95;
            Iterations = 1000;
        }

        private double ObjectiveFunction(double x)
        {
            return x * x;
        }

        private double GetNeighbor(double current)
        {
            double step = (_random.NextDouble() * 2.0) - 1.0;
            return current + step;
        }

        public double Execute()
        {
            double currentSolution = _random.NextDouble() * 20.0 - 10.0;
            double bestSolution = currentSolution;
            double temperature = InitialTemperature;
            for (int i = 0; i < Iterations; i++)
            {
                double candidate = GetNeighbor(currentSolution);
                double delta = ObjectiveFunction(candidate) - ObjectiveFunction(currentSolution);
                if (delta < 0.0 || _random.NextDouble() < Math.Exp(-delta / temperature))
                {
                    currentSolution = candidate;
                    if (ObjectiveFunction(currentSolution) < ObjectiveFunction(bestSolution))
                    {
                        bestSolution = currentSolution;
                    }
                }

                temperature = temperature * CoolingRate;
            }

            return bestSolution;
        }
    }
}