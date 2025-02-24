using System;

namespace CombSort
{
    public static class Program
    {
        public static void Main()
        {
            int[] numbers = new int[]
            {
                64,
                34,
                25,
                12,
                22,
                11,
                90
            };
            CombSorter.Sort(numbers);
            System.Console.WriteLine("Sorted array: " + string.Join(", ", numbers));
        }
    }
}