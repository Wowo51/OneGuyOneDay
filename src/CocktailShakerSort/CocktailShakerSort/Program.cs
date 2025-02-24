using System;

namespace CocktailShakerSort
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[] numbers = new int[]
            {
                5,
                3,
                8,
                4,
                2,
                7,
                1,
                6
            };
            Console.WriteLine("Original array: " + string.Join(", ", numbers));
            CocktailSorter.CocktailShakerSort(numbers);
            Console.WriteLine("Sorted array: " + string.Join(", ", numbers));
        }
    }
}