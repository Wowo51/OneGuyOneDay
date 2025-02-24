using System;

namespace BeadSort
{
    public static class Program
    {
        public static void Main()
        {
            int[] numbers = new int[]
            {
                5,
                3,
                1,
                7,
                4
            };
            BeadSorter.Sort(numbers);
            Console.WriteLine(string.Join(", ", numbers));
        }
    }
}