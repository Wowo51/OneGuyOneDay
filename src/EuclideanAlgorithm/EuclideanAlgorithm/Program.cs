using System;

namespace EuclideanAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int firstNumber = 48;
            int secondNumber = 18;
            int gcd = GcdCalculator.Compute(firstNumber, secondNumber);
            Console.WriteLine("The GCD of " + firstNumber + " and " + secondNumber + " is: " + gcd);
        }
    }
}