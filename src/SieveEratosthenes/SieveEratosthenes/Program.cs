using System;
using System.Collections.Generic;

namespace SieveEratosthenes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int bound = 100;
            List<int> primes = Sieve.GetPrimes(bound);
            Console.WriteLine("Prime numbers up to " + bound + ":");
            Console.WriteLine(string.Join(", ", primes));
        }
    }
}