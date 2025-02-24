using System;
using System.Collections.Generic;

namespace SteinhausJohnsonTrotter
{
    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine("Steinhaus-Johnson-Trotter Permutations:");
            int n = 3;
            IEnumerable<int[]> permutations = SteinhausJohnsonTrotterAlgorithm.Generate(n);
            foreach (int[] permutation in permutations)
            {
                Console.WriteLine(string.Join(" ", permutation));
            }
        }
    }
}