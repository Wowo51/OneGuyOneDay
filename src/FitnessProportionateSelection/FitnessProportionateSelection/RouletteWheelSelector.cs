using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace FitnessProportionateSelection
{
    public static class RouletteWheelSelector
    {
        [return: MaybeNull]
        public static T Select<T>(IEnumerable<T> population, Func<T, double> fitnessSelector, Random random)
        {
            if (population == null || fitnessSelector == null || random == null)
            {
                return default(T);
            }

            List<T> individuals = new List<T>(population);
            if (individuals.Count == 0)
            {
                return default(T);
            }

            double totalFitness = 0.0;
            foreach (T individual in individuals)
            {
                double fitness = fitnessSelector(individual);
                totalFitness += fitness;
            }

            if (Math.Abs(totalFitness) < 1e-10)
            {
                return individuals[random.Next(individuals.Count)];
            }

            double threshold = random.NextDouble() * totalFitness;
            double cumulative = 0.0;
            foreach (T individual in individuals)
            {
                cumulative += fitnessSelector(individual);
                if (cumulative >= threshold)
                {
                    return individual;
                }
            }

            return individuals[individuals.Count - 1];
        }
    }
}