using System;
using System.Collections.Generic;
using System.Text;

namespace SchreierSimsAlgorithm
{
    public static class SchreierSims
    {
        public static (List<int> Base, List<Permutation> StrongGenerators) ComputeBSGS(List<Permutation> generators, int domain)
        {
            if (generators == null)
            {
                generators = new List<Permutation>();
            }

            List<int> basePoints = new List<int>();
            List<Permutation> strongGenerators = new List<Permutation>();
            List<Permutation> currentGroup = ComputeClosure(generators, domain);
            while (currentGroup.Count > 1)
            {
                int candidate = -1;
                for (int i = 0; i < domain; i++)
                {
                    bool allFix = true;
                    for (int j = 0; j < currentGroup.Count; j++)
                    {
                        int image = currentGroup[j].Apply(i);
                        if (image != i)
                        {
                            allFix = false;
                            break;
                        }
                    }

                    if (!allFix)
                    {
                        candidate = i;
                        break;
                    }
                }

                if (candidate == -1)
                {
                    break;
                }

                basePoints.Add(candidate);
                List<Permutation> stabilizer = Stabilizer(currentGroup, candidate);
                for (int i = 0; i < generators.Count; i++)
                {
                    if (generators[i].Apply(candidate) == candidate && !generators[i].IsIdentity())
                    {
                        if (!ContainsPermutation(strongGenerators, generators[i]))
                        {
                            strongGenerators.Add(generators[i]);
                        }
                    }
                }

                currentGroup = stabilizer;
            }

            return (basePoints, strongGenerators);
        }

        private static List<Permutation> ComputeClosure(List<Permutation> generators, int domain)
        {
            HashSet<string> seen = new HashSet<string>();
            List<Permutation> closure = new List<Permutation>();
            Queue<Permutation> queue = new Queue<Permutation>();
            Permutation id = Permutation.Identity(domain);
            closure.Add(id);
            queue.Enqueue(id);
            seen.Add(HashPermutation(id));
            while (queue.Count > 0)
            {
                Permutation current = queue.Dequeue();
                for (int i = 0; i < generators.Count; i++)
                {
                    Permutation newPerm = current.Compose(generators[i]);
                    string hash = HashPermutation(newPerm);
                    if (!seen.Contains(hash))
                    {
                        closure.Add(newPerm);
                        queue.Enqueue(newPerm);
                        seen.Add(hash);
                    }

                    newPerm = generators[i].Compose(current);
                    hash = HashPermutation(newPerm);
                    if (!seen.Contains(hash))
                    {
                        closure.Add(newPerm);
                        queue.Enqueue(newPerm);
                        seen.Add(hash);
                    }
                }
            }

            return closure;
        }

        private static string HashPermutation(Permutation perm)
        {
            StringBuilder sb = new StringBuilder();
            int[] mapping = perm.Mapping;
            for (int i = 0; i < mapping.Length; i++)
            {
                sb.Append(mapping[i]);
                sb.Append(",");
            }

            return sb.ToString();
        }

        private static List<Permutation> Stabilizer(List<Permutation> group, int point)
        {
            List<Permutation> stabilizer = new List<Permutation>();
            for (int i = 0; i < group.Count; i++)
            {
                if (group[i].Apply(point) == point)
                {
                    stabilizer.Add(group[i]);
                }
            }

            return stabilizer;
        }

        private static bool ContainsPermutation(List<Permutation> list, Permutation perm)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Equals(perm))
                {
                    return true;
                }
            }

            return false;
        }
    }
}