using System;

namespace PancakeSorting
{
    public class PancakeSorter
    {
        public void Sort(int[] arr)
        {
            if (arr == null)
            {
                return;
            }

            int n = arr.Length;
            for (int curr_size = n; curr_size > 1; curr_size--)
            {
                int maxIndex = FindMaxIndex(arr, curr_size);
                if (maxIndex != curr_size - 1)
                {
                    if (maxIndex != 0)
                    {
                        Flip(arr, maxIndex);
                    }

                    Flip(arr, curr_size - 1);
                }
            }
        }

        private int FindMaxIndex(int[] arr, int n)
        {
            int maxIndex = 0;
            for (int i = 1; i < n; i++)
            {
                if (arr[i] > arr[maxIndex])
                {
                    maxIndex = i;
                }
            }

            return maxIndex;
        }

        private void Flip(int[] arr, int i)
        {
            int start = 0;
            while (start < i)
            {
                int temp = arr[start];
                arr[start] = arr[i];
                arr[i] = temp;
                start++;
                i--;
            }
        }
    }
}