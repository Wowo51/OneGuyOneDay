using System;
using System.Numerics;

namespace AKSPrimalityTest
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter a number: ");
            string? input = Console.ReadLine();
            if (input is null)
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            if (BigInteger.TryParse(input, out BigInteger number))
            {
                bool isPrime = AksPrimalityTest.IsPrime(number);
                Console.WriteLine(number + (isPrime ? " is prime." : " is composite."));
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
    }
}