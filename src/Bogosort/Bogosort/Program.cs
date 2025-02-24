using System;

namespace Bogosort
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[] array = new int[]
            {
                3,
                1,
                2
            };
            BogosortAlgorithm.Sort(array);
            System.Console.WriteLine("Sorted array: " + string.Join(", ", array));
        }
    }
}