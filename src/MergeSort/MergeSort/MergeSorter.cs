using System;
using System.Collections.Generic;

namespace MergeSort
{
    public static class MergeSorter
    {
        public static List<T> Sort<T>(List<T> list)
            where T : IComparable<T>
        {
            if (list == null)
            {
                return new List<T>();
            }

            if (list.Count <= 1)
            {
                return new List<T>(list);
            }

            int middle = list.Count / 2;
            List<T> left = list.GetRange(0, middle);
            List<T> right = list.GetRange(middle, list.Count - middle);
            List<T> sortedLeft = Sort(left);
            List<T> sortedRight = Sort(right);
            return Merge(sortedLeft, sortedRight);
        }

        public static List<T> Merge<T>(List<T> left, List<T> right)
            where T : IComparable<T>
        {
            List<T> result = new List<T>();
            int leftIndex = 0;
            int rightIndex = 0;
            while (leftIndex < left.Count && rightIndex < right.Count)
            {
                if (left[leftIndex].CompareTo(right[rightIndex]) <= 0)
                {
                    result.Add(left[leftIndex]);
                    leftIndex++;
                }
                else
                {
                    result.Add(right[rightIndex]);
                    rightIndex++;
                }
            }

            while (leftIndex < left.Count)
            {
                result.Add(left[leftIndex]);
                leftIndex++;
            }

            while (rightIndex < right.Count)
            {
                result.Add(right[rightIndex]);
                rightIndex++;
            }

            return result;
        }
    }
}