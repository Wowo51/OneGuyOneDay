using System;

namespace CombSort
{
    public class CombSorter
    {
        public static void Sort(int[] array)
        {
            int length = array.Length;
            int gap = length;
            bool swapped = false;
            const double shrink = 1.3;
            do
            {
                gap = (int)(gap / shrink);
                if (gap < 1)
                {
                    gap = 1;
                }

                swapped = false;
                for (int i = 0; i + gap < length; i++)
                {
                    if (array[i] > array[i + gap])
                    {
                        int temp = array[i];
                        array[i] = array[i + gap];
                        array[i + gap] = temp;
                        swapped = true;
                    }
                }
            }
            while (gap > 1 || swapped);
        }
    }
}