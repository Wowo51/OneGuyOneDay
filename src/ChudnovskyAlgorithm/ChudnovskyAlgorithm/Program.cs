using System;

namespace ChudnovskyAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            // Change the number of digits as needed
            int digits = 100;
            string pi = ChudnovskyCalculator.CalculatePi(digits);
            Console.WriteLine("Calculated π:");
            Console.WriteLine(pi);
        }
    }
}