using System;
using System.Collections.Generic;

namespace SieveOfSundaram
{
    public class Program
    {
        public static void Main()
        {
            Console.Write("Enter maximum number: ");
            string? input = Console.ReadLine();
            if (input == null || !int.TryParse(input, out int max))
            {
                Console.WriteLine("Invalid input");
                return;
            }

            List<int> primes = SieveCalculator.GeneratePrimes(max);
            Console.WriteLine("Primes: " + string.Join(", ", primes));
        }
    }
}