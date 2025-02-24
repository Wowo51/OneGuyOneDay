using System;

namespace BaileyBorweinPlouffe
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter the position of the binary digit (n) after the binary point of pi:");
            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input) || !int.TryParse(input, out int n) || n < 1)
            {
                Console.WriteLine("Invalid input. Please enter a positive integer.");
                return;
            }

            int binaryDigit = BBPFormula.ComputeBinaryDigit(n);
            Console.WriteLine("The binary digit at position {0} is {1}.", n, binaryDigit);
        }
    }
}