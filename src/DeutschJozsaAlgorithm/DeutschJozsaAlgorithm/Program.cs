using System;

namespace DeutschJozsaAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Example usage with a constant function: always false.
            Func<bool[], bool> constantFunction = delegate (bool[] input)
            {
                return false;
            };
            string constantResult = DeutschJozsa.GetFunctionType(constantFunction, 3);
            Console.WriteLine("Constant function is " + constantResult);
            // Example usage with a balanced function for n=1.
            // For n=1, a balanced function: f(false)=false, f(true)=true.
            Func<bool[], bool> balancedFunction = delegate (bool[] input)
            {
                return input[0];
            };
            string balancedResult = DeutschJozsa.GetFunctionType(balancedFunction, 1);
            Console.WriteLine("Balanced function is " + balancedResult);
        }
    }
}