using System;

namespace SelectionSort
{
    public static class Program
    {
        public static void Main()
        {
            int[] numbers = new int[]
            {
                64,
                25,
                12,
                22,
                11
            };
            Sorter.SelectionSort(numbers);
            System.Console.WriteLine(string.Join(", ", numbers));
        }
    }
}