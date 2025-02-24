using System;

namespace SupportVectorMachine
{
    public class SVMClassifier
    {
        private double[] weights;
        private double bias;
        private double learningRate;
        private double regularizationParam;
        private int iterations;
        public SVMClassifier(int featureCount, double learningRate, double regularizationParam, int iterations)
        {
            this.weights = new double[featureCount];
            this.bias = 0;
            this.learningRate = learningRate;
            this.regularizationParam = regularizationParam;
            this.iterations = iterations;
        }

        public void Train(double[][] X, int[] y)
        {
            int nSamples = X.Length;
            int nFeatures = X[0].Length;
            for (int iter = 0; iter < this.iterations; iter++)
            {
                for (int i = 0; i < nSamples; i++)
                {
                    double decision = DotProduct(this.weights, X[i]) + this.bias;
                    if (y[i] * decision < 1)
                    {
                        for (int j = 0; j < nFeatures; j++)
                        {
                            this.weights[j] = this.weights[j] - this.learningRate * (this.weights[j] - this.regularizationParam * y[i] * X[i][j]);
                        }

                        this.bias = this.bias - this.learningRate * (-this.regularizationParam * y[i]);
                    }
                    else
                    {
                        for (int j = 0; j < nFeatures; j++)
                        {
                            this.weights[j] = this.weights[j] - this.learningRate * this.weights[j];
                        }
                    }
                }
            }
        }

        public int Predict(double[] x)
        {
            double decision = DotProduct(this.weights, x) + this.bias;
            return decision >= 0 ? 1 : -1;
        }

        private static double DotProduct(double[] vector, double[] data)
        {
            double result = 0;
            for (int i = 0; i < vector.Length; i++)
            {
                result += vector[i] * data[i];
            }

            return result;
        }

        public double[] GetWeights()
        {
            return this.weights;
        }

        public double GetBias()
        {
            return this.bias;
        }
    }
}