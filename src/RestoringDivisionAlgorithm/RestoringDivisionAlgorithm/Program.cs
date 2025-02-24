using System;

namespace RestoringDivisionAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            uint dividend = 37U;
            uint divisor = 5U;
            (uint quotient, uint remainder) = RestoringDivider.Divide(dividend, divisor);
            Console.WriteLine("Dividend: " + dividend);
            Console.WriteLine("Divisor: " + divisor);
            Console.WriteLine("Quotient: " + quotient);
            Console.WriteLine("Remainder: " + remainder);
        }
    }
}