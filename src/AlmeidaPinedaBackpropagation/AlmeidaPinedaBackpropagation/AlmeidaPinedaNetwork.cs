using System;

namespace AlmeidaPinedaBackpropagation
{
    public class AlmeidaPinedaNetwork
    {
        public int InputCount;
        public int HiddenCount;
        public int OutputCount;
        private double[, ] weightsInputHidden;
        private double[, ] weightsHiddenHidden;
        private double[, ] weightsHiddenOutput;
        private Random random = new Random();
        public AlmeidaPinedaNetwork(int inputCount, int hiddenCount, int outputCount)
        {
            InputCount = inputCount;
            HiddenCount = hiddenCount;
            OutputCount = outputCount;
            weightsInputHidden = new double[inputCount, hiddenCount];
            weightsHiddenHidden = new double[hiddenCount, hiddenCount];
            weightsHiddenOutput = new double[hiddenCount, outputCount];
            InitializeWeights();
        }

        private void InitializeWeights()
        {
            for (int i = 0; i < InputCount; i++)
            {
                for (int j = 0; j < HiddenCount; j++)
                {
                    weightsInputHidden[i, j] = (random.NextDouble() * 0.2) - 0.1;
                }
            }

            for (int i = 0; i < HiddenCount; i++)
            {
                for (int j = 0; j < HiddenCount; j++)
                {
                    weightsHiddenHidden[i, j] = (random.NextDouble() * 0.2) - 0.1;
                }
            }

            for (int i = 0; i < HiddenCount; i++)
            {
                for (int k = 0; k < OutputCount; k++)
                {
                    weightsHiddenOutput[i, k] = (random.NextDouble() * 0.2) - 0.1;
                }
            }
        }

        private double Sigmoid(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-x));
        }

        private double SigmoidDerivative(double output)
        {
            return output * (1.0 - output);
        }

        private double[] ComputeHiddenState(double[] inputs)
        {
            if (inputs == null)
            {
                inputs = new double[InputCount];
            }

            double[] hidden = new double[HiddenCount];
            for (int j = 0; j < HiddenCount; j++)
            {
                hidden[j] = 0.0;
            }

            int maxIterations = 1000;
            double tolerance = 1e-6;
            for (int iteration = 0; iteration < maxIterations; iteration++)
            {
                double[] newHidden = new double[HiddenCount];
                for (int j = 0; j < HiddenCount; j++)
                {
                    double sum = 0.0;
                    for (int i = 0; i < InputCount; i++)
                    {
                        sum += inputs[i] * weightsInputHidden[i, j];
                    }

                    for (int k = 0; k < HiddenCount; k++)
                    {
                        sum += hidden[k] * weightsHiddenHidden[k, j];
                    }

                    newHidden[j] = Sigmoid(sum);
                }

                double maxDifference = 0.0;
                for (int j = 0; j < HiddenCount; j++)
                {
                    double diff = Math.Abs(newHidden[j] - hidden[j]);
                    if (diff > maxDifference)
                    {
                        maxDifference = diff;
                    }
                }

                hidden = newHidden;
                if (maxDifference < tolerance)
                {
                    break;
                }
            }

            return hidden;
        }

        public double[] Compute(double[] inputs)
        {
            if (inputs == null)
            {
                inputs = new double[InputCount];
            }

            double[] hidden = ComputeHiddenState(inputs);
            double[] outputs = new double[OutputCount];
            for (int k = 0; k < OutputCount; k++)
            {
                double sum = 0.0;
                for (int j = 0; j < HiddenCount; j++)
                {
                    sum += hidden[j] * weightsHiddenOutput[j, k];
                }

                outputs[k] = Sigmoid(sum);
            }

            return outputs;
        }

        public double[] Train(double[] inputs, double[] targets, double learningRate)
        {
            if (inputs == null)
            {
                inputs = new double[InputCount];
            }

            if (targets == null)
            {
                targets = new double[OutputCount];
            }

            double[] hidden = ComputeHiddenState(inputs);
            double[] outputs = new double[OutputCount];
            for (int k = 0; k < OutputCount; k++)
            {
                double sum = 0.0;
                for (int j = 0; j < HiddenCount; j++)
                {
                    sum += hidden[j] * weightsHiddenOutput[j, k];
                }

                outputs[k] = Sigmoid(sum);
            }

            double[] outputError = new double[OutputCount];
            for (int k = 0; k < OutputCount; k++)
            {
                double error = targets[k] - outputs[k];
                outputError[k] = error * SigmoidDerivative(outputs[k]);
            }

            double[] hiddenError = new double[HiddenCount];
            for (int j = 0; j < HiddenCount; j++)
            {
                double sum = 0.0;
                for (int k = 0; k < OutputCount; k++)
                {
                    sum += weightsHiddenOutput[j, k] * outputError[k];
                }

                hiddenError[j] = sum * SigmoidDerivative(hidden[j]);
            }

            for (int j = 0; j < HiddenCount; j++)
            {
                for (int k = 0; k < OutputCount; k++)
                {
                    weightsHiddenOutput[j, k] += learningRate * outputError[k] * hidden[j];
                }
            }

            for (int i = 0; i < InputCount; i++)
            {
                for (int j = 0; j < HiddenCount; j++)
                {
                    weightsInputHidden[i, j] += learningRate * hiddenError[j] * inputs[i];
                }
            }

            for (int j = 0; j < HiddenCount; j++)
            {
                for (int k = 0; k < HiddenCount; k++)
                {
                    weightsHiddenHidden[j, k] += learningRate * hiddenError[k] * hidden[j];
                }
            }

            return outputs;
        }
    }
}