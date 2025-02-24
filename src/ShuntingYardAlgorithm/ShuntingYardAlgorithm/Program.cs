using System;

namespace ShuntingYardAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter infix expression: ");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Empty expression provided.");
                return;
            }

            string postfix = ShuntingYardConverter.ConvertToPostfix(input);
            Console.WriteLine("Postfix: " + postfix);
        }
    }
}