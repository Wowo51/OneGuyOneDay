using System;
using System.Collections.Generic;

namespace AntColonyOptimization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            double[, ] distances = new double[, ]
            {
                {
                    0.0,
                    2.0,
                    9.0,
                    10.0,
                    7.0
                },
                {
                    2.0,
                    0.0,
                    6.0,
                    4.0,
                    3.0
                },
                {
                    9.0,
                    6.0,
                    0.0,
                    8.0,
                    5.0
                },
                {
                    10.0,
                    4.0,
                    8.0,
                    0.0,
                    6.0
                },
                {
                    7.0,
                    3.0,
                    5.0,
                    6.0,
                    0.0
                }
            };
            int numberOfAnts = 10;
            int iterations = 100;
            double alpha = 1.0;
            double beta = 2.0;
            double evaporationRate = 0.5;
            AntColonyOptimizer optimizer = new AntColonyOptimizer(distances, numberOfAnts, iterations, alpha, beta, evaporationRate);
            Tuple<List<int>, double> bestSolution = optimizer.Optimize();
            Console.WriteLine("Best Route: " + string.Join(" -> ", bestSolution.Item1));
            Console.WriteLine("Best Route Length: " + bestSolution.Item2);
        }
    }
}