using System;
using System.Collections.Generic;

namespace SampleSort
{
    public static class SampleSorter
    {
        public static List<T> Sort<T>(IEnumerable<T> collection, IComparer<T> comparer)
        {
            if (collection == null)
            {
                return new List<T>();
            }

            List<T> list = new List<T>(collection);
            if (list.Count <= 1)
            {
                return list;
            }

            return SampleSortRecursive(list, comparer);
        }

        private static List<T> SampleSortRecursive<T>(List<T> list, IComparer<T> comparer)
        {
            const int threshold = 32;
            if (list.Count <= threshold)
            {
                list.Sort(comparer);
                return list;
            }

            int bucketsCount = 4;
            int sampleSize = Math.Min(bucketsCount - 1, list.Count);
            List<T> sample = new List<T>();
            int step = list.Count / (sampleSize + 1);
            for (int index = step; index < list.Count && sample.Count < sampleSize; index += step)
            {
                sample.Add(list[index]);
            }

            sample.Sort(comparer);
            List<List<T>> buckets = new List<List<T>>();
            for (int i = 0; i < bucketsCount; i++)
            {
                buckets.Add(new List<T>());
            }

            for (int i = 0; i < list.Count; i++)
            {
                T item = list[i];
                int bucketIndex = 0;
                while (bucketIndex < sample.Count && comparer.Compare(item, sample[bucketIndex]) > 0)
                {
                    bucketIndex++;
                }

                buckets[bucketIndex].Add(item);
            }

            List<T> sorted = new List<T>(list.Count);
            for (int i = 0; i < bucketsCount; i++)
            {
                List<T> sortedBucket = SampleSortRecursive(buckets[i], comparer);
                sorted.AddRange(sortedBucket);
            }

            return sorted;
        }
    }
}