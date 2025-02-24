using System.Collections.Generic;

namespace UnionMergeUnique
{
    public static class UnionMergeUtility
    {
        public static List<T> UnionMerge<T>(IEnumerable<T> first, IEnumerable<T> second)
        {
            if (first == null)
            {
                first = new List<T>();
            }

            if (second == null)
            {
                second = new List<T>();
            }

            HashSet<T> result = new HashSet<T>();
            foreach (T item in first)
            {
                result.Add(item);
            }

            foreach (T item in second)
            {
                result.Add(item);
            }

            return new List<T>(result);
        }
    }
}