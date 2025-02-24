using System;

namespace PostmanSort
{
    public static class PostmanSorter
    {
        public static void Sort(string[] array)
        {
            if (array == null || array.Length <= 1)
            {
                return;
            }

            MSDRadixSort(array, 0, array.Length - 1, 0);
        }

        private static void MSDRadixSort(string[] array, int lo, int hi, int d)
        {
            if (lo >= hi)
            {
                return;
            }

            int radix = 256;
            int[] count = new int[radix + 2];
            int i;
            // Count frequency of each character (or -1 if end-of-string)
            for (i = lo; i <= hi; i++)
            {
                int c = CharAt(array[i], d);
                count[c + 2]++;
            }

            // Transform counts to indices
            for (i = 0; i < radix + 1; i++)
            {
                count[i + 1] += count[i];
            }

            string[] aux = new string[hi - lo + 1];
            for (i = lo; i <= hi; i++)
            {
                int c = CharAt(array[i], d);
                aux[count[c + 1]++] = array[i];
            }

            for (i = lo; i <= hi; i++)
            {
                array[i] = aux[i - lo];
            }

            // Recursively sort for each character value
            for (i = 0; i < radix; i++)
            {
                MSDRadixSort(array, lo + count[i], lo + count[i + 1] - 1, d + 1);
            }
        }

        private static int CharAt(string s, int d)
        {
            if (s == null || d >= s.Length)
            {
                return -1;
            }

            return (int)s[d];
        }
    }
}