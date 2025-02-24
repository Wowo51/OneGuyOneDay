namespace OdlyzkoSchonhageAlgorithm
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;

    public class Program
    {
        public static void Main(string[] args)
        {
            OdlyzkoSchonhageCalculator calculator = new OdlyzkoSchonhageCalculator();
            // Use the efficient method to compute zeros as specified by the Odlyzko–Schönhage algorithm.
            List<Complex> zeros = calculator.ComputeZerosEfficient(14.0, 24.0, 10);
            foreach (Complex zero in zeros)
            {
                Console.WriteLine("Zero: " + zero);
            }
        }
    }
}