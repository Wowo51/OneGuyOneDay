using System;
using System.Collections.Generic;

namespace BacktrackingSearch
{
    public static class Backtracking
    {
        public static List<T> Solve<T>(T initial, Func<T, bool> isSolution, Func<T, IEnumerable<T>> getExtensions)
        {
            List<T> solutions = new List<T>();
            Backtrack(initial, isSolution, getExtensions, solutions);
            return solutions;
        }

        private static void Backtrack<T>(T candidate, Func<T, bool> isSolution, Func<T, IEnumerable<T>> getExtensions, List<T> solutions)
        {
            if (isSolution(candidate))
            {
                solutions.Add(candidate);
            }
            else
            {
                IEnumerable<T> extensions = getExtensions(candidate);
                foreach (T next in extensions)
                {
                    Backtrack(next, isSolution, getExtensions, solutions);
                }
            }
        }
    }
}