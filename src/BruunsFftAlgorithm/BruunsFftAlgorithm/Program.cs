using System;
using System.Numerics;

namespace BruunsFftAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] coefficients = new double[]
            {
                1.0,
                2.0,
                3.0,
                4.0
            };
            System.Numerics.Complex[] fftResult = BruunsFft.Compute(coefficients);
            Console.WriteLine("FFT result:");
            for (int i = 0; i < fftResult.Length; i++)
            {
                Console.WriteLine(fftResult[i]);
            }
        }
    }
}