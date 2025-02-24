using System;

namespace SmoothGamerSort
{
    public static class SmoothSorter
    {
        public static void SmoothGamerSort<T>(T[] array)
            where T : IComparable<T>
        {
            if (array == null)
            {
                return;
            }

            int length = array.Length;
            for (int i = 1; i < length; i++)
            {
                T key = array[i];
                int j = i - 1;
                while (j >= 0 && array[j].CompareTo(key) > 0)
                {
                    array[j + 1] = array[j];
                    j--;
                }

                array[j + 1] = key;
            }
        }
    }
}