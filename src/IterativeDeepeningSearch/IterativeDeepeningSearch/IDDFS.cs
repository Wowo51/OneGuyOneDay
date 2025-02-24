using System;
using System.Collections.Generic;

namespace IterativeDeepeningSearch
{
    public static class IDDFS
    {
        public static List<T>? Search<T>(T start, Func<T, IEnumerable<T>> childrenSelector, Predicate<T> goalTest)
        {
            int depth = 0;
            while (true)
            {
                List<T>? result = DepthLimitedSearch(start, childrenSelector, goalTest, depth);
                if (result != null)
                {
                    return result;
                }

                depth++;
            }
        }

        private static List<T>? DepthLimitedSearch<T>(T node, Func<T, IEnumerable<T>> childrenSelector, Predicate<T> goalTest, int depth)
        {
            if (goalTest(node))
            {
                return new List<T>
                {
                    node
                };
            }

            if (depth == 0)
            {
                return null;
            }

            foreach (T child in childrenSelector(node))
            {
                List<T>? result = DepthLimitedSearch(child, childrenSelector, goalTest, depth - 1);
                if (result != null)
                {
                    result.Insert(0, node);
                    return result;
                }
            }

            return null;
        }
    }
}