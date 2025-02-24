using System;
using System.Collections.Generic;

namespace EvolutionStrategy
{
    public class EvolutionStrategyAlgorithm
    {
        private const int PopulationSize = 20;
        private const int ParameterCount = 5;
        private const int Generations = 100;
        private const double MutationStrength = 0.1;
        private readonly Random random = new Random();
        private readonly List<Individual> population;
        public EvolutionStrategyAlgorithm()
        {
            population = new List<Individual>();
            for (int i = 0; i < PopulationSize; i++)
            {
                double[] parameters = new double[ParameterCount];
                for (int j = 0; j < ParameterCount; j++)
                {
                    parameters[j] = random.NextDouble() * 10 - 5;
                }

                Individual ind = new Individual(parameters);
                ind.Evaluate();
                population.Add(ind);
            }
        }

        public void Run()
        {
            for (int generation = 0; generation < Generations; generation++)
            {
                for (int i = 0; i < PopulationSize; i++)
                {
                    Individual parent = population[i];
                    double[] offspringParameters = new double[ParameterCount];
                    for (int j = 0; j < ParameterCount; j++)
                    {
                        offspringParameters[j] = parent.Parameters[j] + MutationStrength * NextGaussian();
                    }

                    Individual offspring = new Individual(offspringParameters);
                    offspring.Evaluate();
                    if (offspring.Fitness > parent.Fitness)
                    {
                        population[i] = offspring;
                    }
                }
            }

            Individual best = GetBestIndividual();
            Console.WriteLine("Best fitness: " + best.Fitness);
            Console.Write("Best parameters:");
            for (int i = 0; i < best.Parameters.Length; i++)
            {
                Console.Write(" " + best.Parameters[i]);
            }

            Console.WriteLine();
        }

        private double NextGaussian()
        {
            double u1 = random.NextDouble();
            double u2 = random.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            return randStdNormal;
        }

        private Individual GetBestIndividual()
        {
            Individual best = population[0];
            for (int i = 1; i < population.Count; i++)
            {
                if (population[i].Fitness > best.Fitness)
                {
                    best = population[i];
                }
            }

            return best;
        }
    }
}