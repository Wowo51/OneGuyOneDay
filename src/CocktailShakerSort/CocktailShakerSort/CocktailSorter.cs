using System;

namespace CocktailShakerSort
{
    public static class CocktailSorter
    {
        public static void CocktailShakerSort(int[] array)
        {
            if (array == null)
            {
                return;
            }

            int start = 0;
            int end = array.Length - 1;
            bool swapped = true;
            while (swapped)
            {
                swapped = false;
                for (int i = start; i < end; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        int temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        swapped = true;
                    }
                }

                if (!swapped)
                {
                    break;
                }

                swapped = false;
                end--;
                for (int i = end - 1; i >= start; i--)
                {
                    if (array[i] > array[i + 1])
                    {
                        int temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        swapped = true;
                    }
                }

                start++;
            }
        }
    }
}