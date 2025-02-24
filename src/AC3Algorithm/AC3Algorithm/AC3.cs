using System;
using System.Collections.Generic;

namespace AC3Algorithm
{
    public static class AC3
    {
        public static bool EnforceArcConsistency<T>(CSP<T> csp)
        {
            Queue<Tuple<string, string>> queue = new Queue<Tuple<string, string>>();
            foreach (string variable in csp.Variables)
            {
                List<string> neighbours = csp.Neighbors[variable];
                foreach (string neighbor in neighbours)
                {
                    queue.Enqueue(new Tuple<string, string>(variable, neighbor));
                }
            }

            while (queue.Count > 0)
            {
                Tuple<string, string> arc = queue.Dequeue();
                string xi = arc.Item1;
                string xj = arc.Item2;
                if (Revise(csp, xi, xj))
                {
                    if (csp.Domains[xi].Count == 0)
                    {
                        return false;
                    }

                    foreach (string xk in csp.Neighbors[xi])
                    {
                        if (xk != xj)
                        {
                            queue.Enqueue(new Tuple<string, string>(xk, xi));
                        }
                    }
                }
            }

            return true;
        }

        private static bool Revise<T>(CSP<T> csp, string xi, string xj)
        {
            bool revised = false;
            List<T> domainXi = new List<T>(csp.Domains[xi]);
            foreach (T value in domainXi)
            {
                bool consistent = false;
                foreach (T neighborValue in csp.Domains[xj])
                {
                    Tuple<string, string> key = new Tuple<string, string>(xi, xj);
                    if (csp.Constraints.ContainsKey(key))
                    {
                        AC3Algorithm.Constraint<T> constraintFunction = csp.Constraints[key];
                        if (constraintFunction(value, neighborValue))
                        {
                            consistent = true;
                            break;
                        }
                    }
                    else
                    {
                        consistent = true;
                        break;
                    }
                }

                if (!consistent)
                {
                    csp.Domains[xi].Remove(value);
                    revised = true;
                }
            }

            return revised;
        }
    }
}