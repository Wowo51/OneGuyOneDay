using System;

namespace GeneExpressionProgramming
{
    public class Program
    {
        public static void Main(string[] args)
        {
            GEPAlgorithm algorithm = new GEPAlgorithm(20, 10, 0.1, 50);
            algorithm.Evolve();
            Chromosome best = algorithm.Population[0];
            for (int i = 1; i < algorithm.Population.Count; i++)
            {
                if (algorithm.Population[i].Fitness > best.Fitness)
                {
                    best = algorithm.Population[i];
                }
            }

            Console.WriteLine("Best Chromosome: " + best.Gene + " Fitness: " + best.Fitness);
        }
    }
}