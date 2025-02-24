using System;

namespace BubbleSort
{
    public static class BubbleSorter
    {
        public static void Sort<T>(T[] array)
            where T : IComparable<T>
        {
            if (array == null)
            {
                return;
            }

            int length = array.Length;
            for (int i = 0; i < length - 1; i++)
            {
                for (int j = 0; j < length - i - 1; j++)
                {
                    if (array[j].CompareTo(array[j + 1]) > 0)
                    {
                        T temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }
    }
}