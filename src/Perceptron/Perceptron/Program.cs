using System;

namespace Perceptron
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            double[][] trainingInputs = new double[][]
            {
                new double[]
                {
                    0,
                    0
                },
                new double[]
                {
                    0,
                    1
                },
                new double[]
                {
                    1,
                    0
                },
                new double[]
                {
                    1,
                    1
                }
            };
            int[] labels = new int[]
            {
                0,
                0,
                0,
                1
            };
            Perceptron perceptron = new Perceptron(2, 0.1);
            int epochs = 10;
            perceptron.Train(trainingInputs, labels, epochs);
            Console.WriteLine("Perceptron predictions after training:");
            for (int i = 0; i < trainingInputs.Length; i++)
            {
                int prediction = perceptron.Predict(trainingInputs[i]);
                Console.WriteLine($"Input: [{trainingInputs[i][0]}, {trainingInputs[i][1]}] => Prediction: {prediction}");
            }
        }
    }
}