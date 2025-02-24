using System;

namespace NthRootAlgorithm
{
    public class Program
    {
        public static void Main()
        {
            double number = 27.0;
            int n = 3;
            double result = NthRootCalculator.NthRoot(number, n);
            Console.WriteLine("The {0}th root of {1} is approximately {2}", n, number, result);
        }
    }
}