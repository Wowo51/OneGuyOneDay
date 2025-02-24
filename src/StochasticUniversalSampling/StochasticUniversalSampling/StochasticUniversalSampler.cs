using System;
using System.Collections.Generic;

namespace StochasticUniversalSampling
{
    public static class StochasticUniversalSampler
    {
        public static List<T> Select<T>(IList<T> population, Func<T, double> fitnessSelector, int numberToSelect)
        {
            if (population == null || fitnessSelector == null || numberToSelect <= 0)
            {
                return new List<T>();
            }

            int populationCount = population.Count;
            if (populationCount == 0)
            {
                return new List<T>();
            }

            double totalFitness = 0.0;
            for (int i = 0; i < populationCount; i++)
            {
                totalFitness += fitnessSelector(population[i]);
            }

            // If total fitness is not positive, fall back to uniform selection.
            if (totalFitness <= 0.0)
            {
                List<T> uniformSelection = new List<T>();
                Random randomUniform = new Random();
                for (int j = 0; j < numberToSelect; j++)
                {
                    int index = randomUniform.Next(0, populationCount);
                    uniformSelection.Add(population[index]);
                }

                return uniformSelection;
            }

            double pointerSpacing = totalFitness / numberToSelect;
            Random randomPointer = new Random();
            double startPointer = randomPointer.NextDouble() * pointerSpacing;
            List<double> pointers = new List<double>();
            for (int k = 0; k < numberToSelect; k++)
            {
                pointers.Add(startPointer + k * pointerSpacing);
            }

            List<T> selected = new List<T>();
            double cumulativeFitness = 0.0;
            int iPopulation = 0;
            for (int iPointer = 0; iPointer < pointers.Count; iPointer++)
            {
                double target = pointers[iPointer];
                while (iPopulation < populationCount && cumulativeFitness < target)
                {
                    cumulativeFitness += fitnessSelector(population[iPopulation]);
                    iPopulation++;
                }

                if (iPopulation > 0)
                {
                    selected.Add(population[iPopulation - 1]);
                }
                else
                {
                    selected.Add(population[0]);
                }
            }

            return selected;
        }
    }
}