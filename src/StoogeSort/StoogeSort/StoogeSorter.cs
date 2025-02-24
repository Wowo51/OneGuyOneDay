using System;

namespace StoogeSort
{
    public static class StoogeSorter
    {
        public static void Sort<T>(T[] array)
            where T : IComparable<T>
        {
            if (array == null || array.Length < 2)
            {
                return;
            }

            StoogeSortInternal(array, 0, array.Length - 1);
        }

        private static void StoogeSortInternal<T>(T[] array, int i, int j)
            where T : IComparable<T>
        {
            if (array[i].CompareTo(array[j]) > 0)
            {
                T temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }

            if ((j - i + 1) > 2)
            {
                int t = (j - i + 1) / 3;
                StoogeSortInternal(array, i, j - t);
                StoogeSortInternal(array, i + t, j);
                StoogeSortInternal(array, i, j - t);
            }
        }
    }
}