using System;

namespace LPBoost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LPBoostAlgorithm algorithm = new LPBoostAlgorithm();
            double[] sampleData = new double[]
            {
                1.0,
                2.0,
                3.0
            };
            algorithm.Train(sampleData);
            Console.WriteLine("LPBoost training completed.");
        }
    }
}