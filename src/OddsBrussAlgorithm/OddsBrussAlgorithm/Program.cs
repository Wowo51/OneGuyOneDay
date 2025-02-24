using System;
using System.Collections.Generic;

namespace OddsBrussAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            double[] probabilities = new double[]
            {
                0.3,
                0.4,
                0.2,
                0.9,
                0.1
            };
            List<bool> outcomes = new List<bool>();
            Random random = new Random();
            for (int i = 0; i < probabilities.Length; i++)
            {
                double p = probabilities[i];
                bool isSuccess = random.NextDouble() < p;
                outcomes.Add(isSuccess);
                Console.WriteLine("Candidate " + i + ": " + (isSuccess ? "Distinguished" : "Not distinguished"));
            }

            OddsAlgorithm algorithm = new OddsAlgorithm(probabilities);
            int selectedIndex = algorithm.Run(outcomes);
            Console.WriteLine("Selected candidate index: " + selectedIndex);
        }
    }
}