using System;
using SlowSort;

namespace SlowSort
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[] numbers = new int[]
            {
                5,
                3,
                2,
                4,
                1
            };
            Console.WriteLine("Before sorting:");
            Console.WriteLine(string.Join(", ", numbers));
            Slowsorter.Sort<int>(numbers);
            Console.WriteLine("After sorting:");
            Console.WriteLine(string.Join(", ", numbers));
        }
    }
}