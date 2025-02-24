using System;

namespace KadaneAlgorithm
{
    public static class Kadane
    {
        public static KadaneResult FindMaxSubArray(int[] input)
        {
            if (input == null || input.Length == 0)
            {
                return new KadaneResult
                {
                    MaxSum = 0,
                    StartIndex = -1,
                    EndIndex = -1,
                    SubArray = new int[0]
                };
            }

            int maxSoFar = input[0];
            int maxEndingHere = input[0];
            int start = 0;
            int end = 0;
            int sTemp = 0;
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] > maxEndingHere + input[i])
                {
                    maxEndingHere = input[i];
                    sTemp = i;
                }
                else
                {
                    maxEndingHere += input[i];
                }

                if (maxEndingHere > maxSoFar)
                {
                    maxSoFar = maxEndingHere;
                    start = sTemp;
                    end = i;
                }
            }

            int length = end - start + 1;
            int[] subArray = new int[length];
            Array.Copy(input, start, subArray, 0, length);
            return new KadaneResult
            {
                MaxSum = maxSoFar,
                StartIndex = start,
                EndIndex = end,
                SubArray = subArray
            };
        }
    }
}