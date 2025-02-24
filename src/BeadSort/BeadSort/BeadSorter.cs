using System;

namespace BeadSort
{
    public static class BeadSorter
    {
        public static void Sort(int[] array)
        {
            if (array.Length == 0)
            {
                return;
            }

            int n = array.Length;
            int max = 0;
            for (int i = 0; i < n; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }
            }

            if (max == 0)
            {
                return;
            }

            bool[, ] beads = new bool[n, max];
            for (int i = 0; i < n; i++)
            {
                int beadsCount = array[i];
                for (int j = 0; j < beadsCount; j++)
                {
                    beads[i, j] = true;
                }
            }

            for (int j = 0; j < max; j++)
            {
                int sum = 0;
                for (int i = 0; i < n; i++)
                {
                    if (beads[i, j])
                    {
                        sum++;
                        beads[i, j] = false;
                    }
                }

                for (int i = n - sum; i < n; i++)
                {
                    beads[i, j] = true;
                }
            }

            for (int i = 0; i < n; i++)
            {
                int count = 0;
                for (int j = 0; j < max; j++)
                {
                    if (beads[i, j])
                    {
                        count++;
                    }
                }

                array[i] = count;
            }
        }
    }
}