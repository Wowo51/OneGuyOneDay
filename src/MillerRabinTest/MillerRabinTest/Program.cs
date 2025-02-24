using System;
using System.Numerics;

namespace MillerRabinTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter a number to test for primality:");
            string? input = Console.ReadLine();
            if (input == null || !BigInteger.TryParse(input, out BigInteger number))
            {
                Console.WriteLine("Invalid number input!");
                return;
            }

            bool isPrime = MillerRabin.IsProbablyPrime(number, 10);
            Console.WriteLine($"{number} is {(isPrime ? "probably prime" : "composite")}");
        }
    }
}