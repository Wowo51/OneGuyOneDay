using System;
using System.Collections.Generic;

namespace ChienSearch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Example polynomial: f(x) = 1 + 0*x + 1*x^2, representing x^2 + 1.
            // In GF(5), the roots of x^2 + 1 are 2 and 3.
            int[] polynomial = new int[]
            {
                1,
                0,
                1
            };
            int modulus = 5;
            List<int> roots = ChienSearchAlgorithm.FindRoots(polynomial, modulus);
            Console.WriteLine("Found roots in GF(" + modulus.ToString() + "): " + String.Join(", ", roots));
        }
    }
}