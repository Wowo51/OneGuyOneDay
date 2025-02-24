using System;
using System.Collections.Generic;

namespace RandomForest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<DataPoint> trainingData = new List<DataPoint>();
            trainingData.Add(new DataPoint(new double[] { 1.0, 2.0 }, 0));
            trainingData.Add(new DataPoint(new double[] { 1.5, 2.5 }, 0));
            trainingData.Add(new DataPoint(new double[] { 3.0, 4.0 }, 1));
            trainingData.Add(new DataPoint(new double[] { 3.5, 4.5 }, 1));
            RandomForest forest = new RandomForest(5);
            forest.Train(trainingData);
            double[] sample = new double[]
            {
                2.0,
                3.0
            };
            int prediction = forest.Predict(sample);
            Console.WriteLine("Predicted label: " + prediction);
        }
    }
}