using System;

namespace PostmanSort
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            string[] data = new string[]
            {
                "delta",
                "alpha",
                "charlie",
                "bravo"
            };
            PostmanSorter.Sort(data);
            foreach (string item in data)
            {
                Console.WriteLine(item);
            }
        }
    }
}