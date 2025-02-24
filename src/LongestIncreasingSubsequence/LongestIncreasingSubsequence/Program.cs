using System;

namespace LongestIncreasingSubsequence
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            int[] sequence =
            {
                10,
                22,
                9,
                33,
                21,
                50,
                41,
                60
            };
            int[] lis = LongestIncreasingSubsequenceCalculator.FindLIS(sequence);
            System.Console.WriteLine("Longest Increasing Subsequence:");
            foreach (int value in lis)
            {
                System.Console.Write(value + " ");
            }

            System.Console.WriteLine();
        }
    }
}