using System;
using System.Collections.Generic;
using System.Linq;

namespace TruncationSelection
{
    public class TruncationSelector
    {
        public List<Individual> Select(IList<Individual> individuals, double fraction)
        {
            if (individuals == null)
            {
                return new List<Individual>();
            }

            if (fraction <= 0.0 || fraction > 1.0)
            {
                return new List<Individual>();
            }

            List<Individual> sortedIndividuals = individuals.OrderByDescending(individual => individual.Fitness).ToList();
            int count = (int)Math.Ceiling(sortedIndividuals.Count * fraction);
            return sortedIndividuals.Take(count).ToList();
        }
    }
}