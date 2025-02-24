using System;
using System.Collections.Generic;

namespace QuickSortDivide
{
    public static class QuickSorter
    {
        public static List<T> QuickSort<T>(List<T> list)
            where T : IComparable<T>
        {
            if (list == null)
            {
                return new List<T>();
            }

            if (list.Count < 2)
            {
                return list;
            }

            T pivot = list[0];
            List<T> left = new List<T>();
            List<T> right = new List<T>();
            for (int i = 1; i < list.Count; i++)
            {
                T current = list[i];
                if (current.CompareTo(pivot) < 0)
                {
                    left.Add(current);
                }
                else
                {
                    right.Add(current);
                }
            }

            List<T> sorted = new List<T>();
            List<T> sortedLeft = QuickSort(left);
            List<T> sortedRight = QuickSort(right);
            sorted.AddRange(sortedLeft);
            sorted.Add(pivot);
            sorted.AddRange(sortedRight);
            return sorted;
        }
    }
}