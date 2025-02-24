using System;
using System.Numerics;
using LenstraEllipticFactorization;

namespace LenstraEllipticFactorization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter an integer greater than 1 to factor using Lenstra's Elliptic Curve Factorization: ");
            string input = Console.ReadLine() ?? string.Empty;
            BigInteger number;
            if (BigInteger.TryParse(input, out number))
            {
                if (number < 2)
                {
                    Console.WriteLine("Please enter an integer greater than 1.");
                    return;
                }

                BigInteger factor = LenstraFactorization.Factor(number);
                if (factor == number)
                {
                    Console.WriteLine("No nontrivial factor found. The number might be prime.");
                }
                else
                {
                    Console.WriteLine("Found factor: " + factor);
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
    }
}