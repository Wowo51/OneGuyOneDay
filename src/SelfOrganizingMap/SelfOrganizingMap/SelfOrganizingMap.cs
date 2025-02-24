using System;

namespace SelfOrganizingMap
{
    public class SelfOrganizingMap
    {
        private readonly int inputDimension;
        private readonly int width;
        private readonly int height;
        private readonly int iterations;
        private readonly Neuron[, ] neurons;
        public SelfOrganizingMap(int inputDimension, int width, int height, int iterations)
        {
            this.inputDimension = inputDimension;
            this.width = width;
            this.height = height;
            this.iterations = iterations;
            neurons = new Neuron[width, height];
            InitializeNeurons();
        }

        private void InitializeNeurons()
        {
            Random random = new Random();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    double[] weights = new double[inputDimension];
                    for (int k = 0; k < inputDimension; k++)
                    {
                        weights[k] = random.NextDouble();
                    }

                    neurons[i, j] = new Neuron(weights);
                }
            }
        }

        private double CalculateDistance(double[] vector1, double[] vector2)
        {
            double sum = 0.0;
            for (int i = 0; i < vector1.Length; i++)
            {
                double diff = vector1[i] - vector2[i];
                sum += diff * diff;
            }

            return Math.Sqrt(sum);
        }

        private (int, int) GetBestMatchingUnit(double[] input)
        {
            int bestX = 0;
            int bestY = 0;
            double minDistance = double.MaxValue;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    double distance = CalculateDistance(neurons[i, j].Weights, input);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        bestX = i;
                        bestY = j;
                    }
                }
            }

            return (bestX, bestY);
        }

        public void Train(double[][] inputData)
        {
            double initialLearningRate = 0.1;
            double timeConstant = iterations / Math.Log(Math.Max(width, height));
            for (int t = 0; t < iterations; t++)
            {
                double learningRate = initialLearningRate * Math.Exp(-((double)t / iterations));
                double radius = (Math.Max(width, height) / 2.0) * Math.Exp(-((double)t / timeConstant));
                foreach (double[] input in inputData)
                {
                    (int bmuX, int bmuY) = GetBestMatchingUnit(input);
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            double distanceToBMU = Math.Sqrt((i - bmuX) * (i - bmuX) + (j - bmuY) * (j - bmuY));
                            if (distanceToBMU <= radius)
                            {
                                double influence = Math.Exp(-((distanceToBMU * distanceToBMU) / (2 * radius * radius)));
                                for (int k = 0; k < inputDimension; k++)
                                {
                                    neurons[i, j].Weights[k] += influence * learningRate * (input[k] - neurons[i, j].Weights[k]);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}