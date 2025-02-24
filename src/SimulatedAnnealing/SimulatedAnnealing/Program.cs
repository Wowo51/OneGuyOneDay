using System;
using SimulatedAnnealing;

namespace SimulatedAnnealing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SimulatedAnnealingAlgorithm sa = new SimulatedAnnealingAlgorithm();
            double bestSolution = sa.Execute();
            Console.WriteLine("Best solution found: " + bestSolution.ToString("F4"));
        }
    }
}