using System;

namespace PigeonholeSort
{
    public static class PigeonholeSorter
    {
        public static void Sort(int[] array)
        {
            if (array == null || array.Length <= 1)
            {
                return;
            }

            int min = array[0];
            int max = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < min)
                {
                    min = array[i];
                }

                if (array[i] > max)
                {
                    max = array[i];
                }
            }

            int range = max - min + 1;
            int[] holes = new int[range];
            for (int i = 0; i < array.Length; i++)
            {
                holes[array[i] - min]++;
            }

            int index = 0;
            for (int i = 0; i < range; i++)
            {
                while (holes[i] > 0)
                {
                    array[index] = i + min;
                    index++;
                    holes[i]--;
                }
            }
        }
    }
}