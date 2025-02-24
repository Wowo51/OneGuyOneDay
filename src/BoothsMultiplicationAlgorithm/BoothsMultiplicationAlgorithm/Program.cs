using System;

namespace BoothsMultiplicationAlgorithm
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            BoothsMultiplier multiplier = new BoothsMultiplier();
            int multiplicand = -7;
            int multiplierVal = 3;
            long product = multiplier.Multiply(multiplicand, multiplierVal);
            Console.WriteLine("Product: " + product);
        }
    }
}