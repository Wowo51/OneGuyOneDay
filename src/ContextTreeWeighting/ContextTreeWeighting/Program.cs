using System;

namespace ContextTreeWeighting
{
    public class Program
    {
        public static void Main()
        {
            ContextTreeWeightingAlgorithm algorithm = new ContextTreeWeightingAlgorithm(5);
            string context = "010";
            algorithm.Update(context, 0);
            algorithm.Update(context, 1);
            double probability = algorithm.GetWeightedProbability(context);
            Console.WriteLine("Weighted Probability: " + probability);
        }
    }
}