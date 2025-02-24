using System;
using System.Collections.Generic;
using System.Linq;

namespace EspressoHeuristicMinimizer
{
    public class EspressoMinimizer
    {
        public List<BooleanCube> Minimize(List<BooleanCube> cubes)
        {
            if (cubes == null)
            {
                return new List<BooleanCube>();
            }

            List<BooleanCube> primes = GetPrimeImplicants(cubes);
            List<BooleanCube> cover = new List<BooleanCube>();
            List<BooleanCube> remaining = new List<BooleanCube>(cubes);
            while (remaining.Count > 0)
            {
                int bestCount = -1;
                BooleanCube? chosen = null;
                foreach (BooleanCube prime in primes)
                {
                    int count = CountCover(prime, remaining);
                    if (count > bestCount)
                    {
                        bestCount = count;
                        chosen = prime;
                    }
                }

                if (chosen == null)
                {
                    break;
                }

                cover.Add(chosen);
                remaining = remaining.Where(cube => !chosen.Covers(cube)).ToList();
            }

            return cover;
        }

        private int CountCover(BooleanCube implicant, List<BooleanCube> cubes)
        {
            int count = 0;
            foreach (BooleanCube cube in cubes)
            {
                if (implicant.Covers(cube))
                {
                    count++;
                }
            }

            return count;
        }

        public List<BooleanCube> GetPrimeImplicants(List<BooleanCube> cubes)
        {
            List<BooleanCube> current = new List<BooleanCube>(cubes);
            List<BooleanCube> next;
            bool mergedAtLeastOnce = true;
            List<BooleanCube> primeImplicants = new List<BooleanCube>();
            while (mergedAtLeastOnce)
            {
                mergedAtLeastOnce = false;
                next = new List<BooleanCube>();
                bool[] merged = new bool[current.Count];
                for (int i = 0; i < current.Count; i++)
                {
                    for (int j = i + 1; j < current.Count; j++)
                    {
                        if (current[i].CanMerge(current[j]))
                        {
                            BooleanCube? mergedCube = current[i].Merge(current[j]);
                            if (mergedCube != null && !next.Contains(mergedCube))
                            {
                                next.Add(mergedCube);
                            }

                            merged[i] = true;
                            merged[j] = true;
                            mergedAtLeastOnce = true;
                        }
                    }
                }

                for (int i = 0; i < current.Count; i++)
                {
                    if (!merged[i] && !primeImplicants.Contains(current[i]))
                    {
                        primeImplicants.Add(current[i]);
                    }
                }

                current = next;
            }

            foreach (BooleanCube cube in current)
            {
                if (!primeImplicants.Contains(cube))
                {
                    primeImplicants.Add(cube);
                }
            }

            return primeImplicants;
        }
    }
}