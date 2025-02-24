using System;

namespace Bogosort
{
    public static class BogosortAlgorithm
    {
        private static readonly Random s_random = new Random();
        public static void Sort<T>(T[] array)
            where T : IComparable<T>
        {
            if (array == null || array.Length <= 1)
            {
                return;
            }

            while (!IsSorted(array))
            {
                Shuffle(array);
            }
        }

        private static bool IsSorted<T>(T[] array)
            where T : IComparable<T>
        {
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1].CompareTo(array[i]) > 0)
                {
                    return false;
                }
            }

            return true;
        }

        private static void Shuffle<T>(T[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n; i++)
            {
                int j = s_random.Next(n);
                T temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }
    }
}