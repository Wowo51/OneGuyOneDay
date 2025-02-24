using System;

namespace BitonicSorter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[] data = new int[]
            {
                3,
                7,
                4,
                8,
                6,
                2,
                1,
                5
            };
            BitonicSorter.Sort(data, true);
            Console.WriteLine("Sorted array:");
            for (int i = 0; i < data.Length; i++)
            {
                Console.Write(data[i] + " ");
            }

            Console.WriteLine();
        }
    }
}