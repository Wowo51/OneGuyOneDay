using System;

namespace RandomSearch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RandomSearchAlgorithm algorithm = new RandomSearchAlgorithm();
            double[] lowerBounds = new double[]
            {
                -10.0,
                -10.0
            };
            double[] upperBounds = new double[]
            {
                10.0,
                10.0
            };
            int iterations = 1000;
            System.Func<double[], double> objective = delegate (double[] candidate)
            {
                return candidate[0] * candidate[0] + candidate[1] * candidate[1];
            };
            RandomSearchResult result = algorithm.Optimize(lowerBounds, upperBounds, objective, iterations);
            System.Console.WriteLine("Best candidate: " + string.Join(", ", result.Candidate));
            System.Console.WriteLine("Objective value: " + result.ObjectiveValue);
        }
    }
}