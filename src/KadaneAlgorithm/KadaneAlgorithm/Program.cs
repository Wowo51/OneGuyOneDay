using System;

namespace KadaneAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] input = new int[]
            {
                -2,
                1,
                -3,
                4,
                -1,
                2,
                1,
                -5,
                4
            };
            KadaneResult result = Kadane.FindMaxSubArray(input);
            Console.WriteLine("Max Sum: " + result.MaxSum);
            Console.WriteLine("Subarray: " + string.Join(", ", result.SubArray));
        }
    }
}