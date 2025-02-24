using System;

namespace SlowSort
{
    public class Slowsorter
    {
        public static void Sort<T>(T[] array)
            where T : IComparable<T>
        {
            if (array == null)
            {
                return;
            }

            Slowsort(array, 0, array.Length - 1);
        }

        private static void Slowsort<T>(T[] array, int i, int j)
            where T : IComparable<T>
        {
            if (i >= j)
            {
                return;
            }

            int mid = (i + j) / 2;
            Slowsort(array, i, mid);
            Slowsort(array, mid + 1, j);
            if (array[j].CompareTo(array[mid]) < 0)
            {
                T temp = array[j];
                array[j] = array[mid];
                array[mid] = temp;
            }

            Slowsort(array, i, j - 1);
        }
    }
}