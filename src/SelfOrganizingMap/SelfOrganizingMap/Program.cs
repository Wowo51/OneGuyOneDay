using System;

namespace SelfOrganizingMap
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            double[][] inputData = new double[][]
            {
                new double[]
                {
                    1.0,
                    2.0
                },
                new double[]
                {
                    2.0,
                    3.0
                },
                new double[]
                {
                    3.0,
                    4.0
                },
                new double[]
                {
                    4.0,
                    5.0
                },
                new double[]
                {
                    5.0,
                    6.0
                }
            };
            int inputDimension = inputData[0].Length;
            int gridWidth = 5;
            int gridHeight = 5;
            int iterations = 100;
            SelfOrganizingMap som = new SelfOrganizingMap(inputDimension, gridWidth, gridHeight, iterations);
            som.Train(inputData);
            Console.WriteLine("SOM training completed.");
        }
    }
}