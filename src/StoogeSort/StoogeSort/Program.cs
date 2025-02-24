using System;

namespace StoogeSort
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[] array = new int[]
            {
                2,
                4,
                5,
                3,
                1,
                6
            };
            Console.WriteLine("Unsorted: " + string.Join(", ", array));
            StoogeSorter.Sort(array);
            Console.WriteLine("Sorted: " + string.Join(", ", array));
        }
    }
}