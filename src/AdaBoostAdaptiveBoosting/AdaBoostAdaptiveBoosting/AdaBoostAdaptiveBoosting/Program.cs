using System;
using System.Collections.Generic;

namespace AdaBoostAdaptiveBoosting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            double[][] features = new double[][]
            {
                new double[]
                {
                    1.0
                },
                new double[]
                {
                    2.0
                },
                new double[]
                {
                    3.0
                },
                new double[]
                {
                    4.0
                }
            };
            int[] labels = new int[]
            {
                1,
                1,
                -1,
                -1
            };
            AdaBoost adaBoost = new AdaBoost();
            adaBoost.Train(features, labels, 10);
            double[][] testSamples = new double[][]
            {
                new double[]
                {
                    1.5
                },
                new double[]
                {
                    2.5
                },
                new double[]
                {
                    3.5
                }
            };
            for (int i = 0; i < testSamples.Length; i++)
            {
                int prediction = adaBoost.Predict(testSamples[i]);
                Console.WriteLine("Sample " + i + ": " + prediction);
            }
        }
    }
}