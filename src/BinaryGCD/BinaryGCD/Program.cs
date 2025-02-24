using System;

namespace BinaryGCD
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                int n1;
                int n2;
                if (int.TryParse(args[0], out n1) && int.TryParse(args[1], out n2))
                {
                    int result = GcdCalculator.BinaryGcd(n1, n2);
                    Console.WriteLine("GCD(" + n1 + ", " + n2 + ") = " + result);
                }
                else
                {
                    Console.WriteLine("Please provide two integers.");
                }
            }
            else
            {
                Console.WriteLine("Using sample values: 48, 18");
                int sample1 = 48;
                int sample2 = 18;
                int result = GcdCalculator.BinaryGcd(sample1, sample2);
                Console.WriteLine("GCD(" + sample1 + ", " + sample2 + ") = " + result);
            }
        }
    }
}