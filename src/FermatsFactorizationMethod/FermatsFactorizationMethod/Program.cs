using System;

namespace FermatsFactorizationMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a positive odd integer: ");
            string input = Console.ReadLine() ?? "";
            long number;
            if (!Int64.TryParse(input, out number) || number < 1)
            {
                Console.WriteLine("Invalid input. Please enter a positive integer.");
                return;
            }

            if (number % 2 == 0)
            {
                Console.WriteLine("Input must be an odd integer for Fermat's factorization method.");
                return;
            }

            (long factor1, long factor2) = FermatFactorization.Factorize(number);
            Console.WriteLine("Factors: {0} and {1}", factor1, factor2);
        }
    }
}