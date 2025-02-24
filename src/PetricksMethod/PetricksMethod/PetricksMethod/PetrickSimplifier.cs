using System;
using System.Collections.Generic;
using System.Linq;

namespace PetricksMethod
{
    /// <summary>
    /// Implements Petrick’s method for Boolean simplification.
    /// </summary>
    public class PetrickSimplifier
    {
        public static IList<IList<string>> Simplify(IList<IList<string>>? chart)
        {
            if (chart == null || chart.Count == 0)
            {
                return new List<IList<string>>();
            }

            // If any row is null or contains no non-null implicants, return empty solution.
            foreach (IList<string>? row in chart)
            {
                if (row == null || !row.Any(item => item != null))
                {
                    return new List<IList<string>>();
                }
            }

            List<HashSet<string>> currentSolutions = new List<HashSet<string>>();
            foreach (string implicant in chart[0])
            {
                if (implicant != null)
                {
                    currentSolutions.Add(new HashSet<string> { implicant });
                }
            }

            if (currentSolutions.Count == 0)
            {
                return new List<IList<string>>();
            }

            for (int i = 1; i < chart.Count; i++)
            {
                List<HashSet<string>> newSolutions = new List<HashSet<string>>();
                foreach (HashSet<string> solution in currentSolutions)
                {
                    foreach (string implicant in chart[i])
                    {
                        if (implicant != null)
                        {
                            HashSet<string> newSet = new HashSet<string>(solution);
                            newSet.Add(implicant);
                            newSolutions.Add(newSet);
                        }
                    }
                }

                if (newSolutions.Count == 0)
                {
                    return new List<IList<string>>();
                }

                newSolutions = newSolutions.GroupBy(ns => string.Join(", ", ns.OrderBy(x => x))).Select(g => g.First()).ToList();
                int minSize = newSolutions.Min(ns => ns.Count);
                currentSolutions = newSolutions.Where(ns => ns.Count == minSize).ToList();
            }

            List<IList<string>> result = new List<IList<string>>();
            foreach (HashSet<string> solution in currentSolutions)
            {
                result.Add(solution.OrderBy(x => x).ToList());
            }

            return result;
        }
    }
}