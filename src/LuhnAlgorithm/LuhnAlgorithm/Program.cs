using System;

namespace LuhnAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter a number to validate: ");
            string? input = Console.ReadLine();
            bool isValid = LuhnValidator.IsValid(input);
            Console.WriteLine("The number is " + (isValid ? "valid" : "invalid"));
        }
    }
}