using System;

namespace SelectionSort
{
    public static class Sorter
    {
        public static void SelectionSort<T>(T[] array)
            where T : System.IComparable<T>
        {
            if (array == null)
            {
                return;
            }

            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (array[j].CompareTo(array[minIndex]) < 0)
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    T temp = array[i];
                    array[i] = array[minIndex];
                    array[minIndex] = temp;
                }
            }
        }
    }
}