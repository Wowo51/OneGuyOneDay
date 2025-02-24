using System;

namespace UniformBinarySearch
{
    public static class UniformBinarySearchAlgorithm
    {
        public static int Search(int[] array, int key)
        {
            if (array == null)
            {
                return -1;
            }

            int n = array.Length;
            if (n == 0)
            {
                return -1;
            }

            int pos = 0;
            int power = 1 << (31 - CountLeadingZeros((uint)n));
            while (power > 0)
            {
                int candidate = pos + power;
                if (candidate < n && array[candidate] <= key)
                {
                    pos = candidate;
                }

                power >>= 1;
            }

            return array[pos] == key ? pos : -1;
        }

        private static int CountLeadingZeros(uint x)
        {
            int count = 0;
            for (int i = 31; i >= 0; i--)
            {
                if ((x & (1u << i)) != 0)
                {
                    break;
                }

                count++;
            }

            return count;
        }
    }
}