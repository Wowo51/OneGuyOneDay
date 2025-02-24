using System;

namespace Perceptron
{
    public class Perceptron
    {
        private double[] _weights;
        public double Bias { get; private set; }
        public double LearningRate { get; }
        public int NumInputs { get; }

        public Perceptron(int numInputs, double learningRate)
        {
            NumInputs = numInputs;
            LearningRate = learningRate;
            _weights = new double[numInputs];
            Bias = 0.0;
        }

        public int Predict(double[] inputs)
        {
            if (inputs == null || inputs.Length != NumInputs)
            {
                return 0;
            }

            double sum = Bias;
            for (int i = 0; i < NumInputs; i++)
            {
                sum += _weights[i] * inputs[i];
            }

            return sum >= 0.0 ? 1 : 0;
        }

        public void Train(double[][] trainingInputs, int[] labels, int epochs)
        {
            if (trainingInputs == null || labels == null)
            {
                return;
            }

            for (int epoch = 0; epoch < epochs; epoch++)
            {
                for (int i = 0; i < trainingInputs.Length; i++)
                {
                    if (trainingInputs[i] == null || trainingInputs[i].Length != NumInputs)
                    {
                        continue;
                    }

                    int prediction = Predict(trainingInputs[i]);
                    int error = labels[i] - prediction;
                    for (int j = 0; j < NumInputs; j++)
                    {
                        _weights[j] += LearningRate * error * trainingInputs[i][j];
                    }

                    Bias += LearningRate * error;
                }
            }
        }
    }
}