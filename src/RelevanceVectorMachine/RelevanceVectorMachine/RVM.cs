using System;

namespace RelevanceVectorMachine
{
    public class RelevanceVectorMachine
    {
        private double[]? Weights;
        private double Bias;
        private double[][]? TrainingData;
        private const double Gamma = 0.5;
        public void Train(double[][] trainingData, double[] targets)
        {
            if (trainingData == null || targets == null || trainingData.Length == 0 || targets.Length != trainingData.Length)
            {
                return;
            }

            int sampleCount = trainingData.Length;
            int dimension = trainingData[0].Length;
            for (int i = 0; i < sampleCount; i++)
            {
                if (trainingData[i] == null || trainingData[i].Length != dimension)
                {
                    return;
                }
            }

            TrainingData = trainingData;
            Weights = new double[sampleCount];
            for (int i = 0; i < sampleCount; i++)
            {
                Weights[i] = 0.1;
            }

            Bias = 0.0;
            int iterations = 1000;
            double learningRate = 0.01;
            for (int iter = 0; iter < iterations; iter++)
            {
                double[] weightGradients = new double[sampleCount];
                double biasGradient = 0.0;
                for (int i = 0; i < sampleCount; i++)
                {
                    double[] sample = trainingData[i];
                    double dot = Bias;
                    for (int j = 0; j < sampleCount; j++)
                    {
                        dot += Weights[j] * Kernel(trainingData[j], sample);
                    }

                    double prediction = 1.0 / (1.0 + Math.Exp(-dot));
                    double error = targets[i] - prediction;
                    for (int j = 0; j < sampleCount; j++)
                    {
                        weightGradients[j] += Kernel(trainingData[j], sample) * error;
                    }

                    biasGradient += error;
                }

                for (int j = 0; j < sampleCount; j++)
                {
                    Weights[j] += learningRate * weightGradients[j] / sampleCount;
                }

                Bias += learningRate * biasGradient / sampleCount;
            }

            for (int i = 0; i < sampleCount; i++)
            {
                if (Math.Abs(Weights[i]) < 1e-3)
                {
                    Weights[i] = 0.0;
                }
            }
        }

        public double PredictProbability(double[] input)
        {
            if (input == null || Weights == null || TrainingData == null)
            {
                return 0.0;
            }

            int dimension = TrainingData[0].Length;
            if (input.Length != dimension)
            {
                return 0.0;
            }

            double sum = Bias;
            int sampleCount = TrainingData.Length;
            for (int i = 0; i < sampleCount; i++)
            {
                sum += Weights[i] * Kernel(input, TrainingData[i]);
            }

            return 1.0 / (1.0 + Math.Exp(-sum));
        }

        private static double Kernel(double[] x, double[] y)
        {
            if (x == null || y == null || x.Length != y.Length)
            {
                return 0.0;
            }

            double sumSquares = 0.0;
            int length = x.Length;
            for (int i = 0; i < length; i++)
            {
                double diff = x[i] - y[i];
                sumSquares += diff * diff;
            }

            return Math.Exp(-Gamma * sumSquares);
        }
    }
}