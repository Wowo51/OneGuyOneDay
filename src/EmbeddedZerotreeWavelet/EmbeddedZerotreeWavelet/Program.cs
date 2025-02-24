using System;

namespace EmbeddedZerotreeWavelet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            double[, ] testData = new double[2, 2];
            testData[0, 0] = 1.5;
            testData[0, 1] = -2.3;
            testData[1, 0] = 0.5;
            testData[1, 1] = -0.5;
            string encoded = EZWProcessor.Encode(testData);
            double[, ] decoded = EZWProcessor.Decode(encoded, testData.GetLength(0), testData.GetLength(1));
            Console.WriteLine("Encoded: " + encoded);
            Console.WriteLine("Decoded matrix:");
            for (int i = 0; i < decoded.GetLength(0); i++)
            {
                for (int j = 0; j < decoded.GetLength(1); j++)
                {
                    Console.Write(decoded[i, j] + " ");
                }

                Console.WriteLine();
            }
        }
    }
}