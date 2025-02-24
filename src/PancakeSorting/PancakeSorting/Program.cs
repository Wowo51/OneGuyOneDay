using System;

namespace PancakeSorting
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            int[] array = new int[]
            {
                3,
                6,
                1,
                2,
                4,
                5
            };
            Console.WriteLine("Original array: " + string.Join(", ", array));
            PancakeSorter sorter = new PancakeSorter();
            sorter.Sort(array);
            Console.WriteLine("Sorted array: " + string.Join(", ", array));
        }
    }
}