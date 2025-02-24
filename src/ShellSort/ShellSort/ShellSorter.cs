using System;
using System.Collections.Generic;

namespace ShellSort
{
    public static class ShellSorter
    {
        public static void Sort<T>(T[] array, IComparer<T> comparer)
        {
            if (array == null)
            {
                return;
            }

            if (comparer == null)
            {
                comparer = Comparer<T>.Default;
            }

            int n = array.Length;
            int gap = n / 2;
            while (gap > 0)
            {
                for (int i = gap; i < n; i++)
                {
                    T temp = array[i];
                    int j = i;
                    while (j >= gap && comparer.Compare(array[j - gap], temp) > 0)
                    {
                        array[j] = array[j - gap];
                        j -= gap;
                    }

                    array[j] = temp;
                }

                gap /= 2;
            }
        }

        public static void Sort<T>(T[] array)
            where T : IComparable<T>
        {
            Sort(array, Comparer<T>.Default);
        }
    }
}