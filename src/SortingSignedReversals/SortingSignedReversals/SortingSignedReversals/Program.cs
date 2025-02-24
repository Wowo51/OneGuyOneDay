using System;
using System.Collections.Generic;

namespace SortingSignedReversals
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<int> permutation = new List<int>
            {
                3,
                4,
                -1,
                2
            };
            List<string> steps = SignedReversalSorter.GreedySort(permutation);
            Console.WriteLine("Sorting steps:");
            for (int index = 0; index < steps.Count; index++)
            {
                Console.WriteLine(steps[index]);
            }
        }
    }
}