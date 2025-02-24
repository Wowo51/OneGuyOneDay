using System;
using System.Collections.Generic;

namespace AdaBoostAdaptiveBoosting
{
    public class AdaBoost
    {
        private List<DecisionStump> _weakLearners;
        private List<double> _learnerWeights;
        public AdaBoost()
        {
            _weakLearners = new List<DecisionStump>();
            _learnerWeights = new List<double>();
        }

        public void Train(double[][] X, int[] y, int numIterations)
        {
            if (X == null || y == null)
            {
                return;
            }

            // Ensure the number of feature samples matches the number of labels.
            if (X.Length != y.Length)
            {
                return;
            }

            int nSamples = X.Length;
            if (nSamples == 0)
            {
                return;
            }

            double[] weights = new double[nSamples];
            for (int i = 0; i < nSamples; i++)
            {
                weights[i] = 1.0 / nSamples;
            }

            for (int iter = 0; iter < numIterations; iter++)
            {
                DecisionStump stump = DecisionStump.GetBestStump(X, y, weights);
                double error = ComputeError(stump, X, y, weights);
                double epsilon = 1e-10;
                double alpha = 0.5 * Math.Log((1.0 - error) / (error + epsilon));
                double sumWeights = 0.0;
                for (int i = 0; i < nSamples; i++)
                {
                    int prediction = stump.Predict(X[i]);
                    weights[i] = weights[i] * Math.Exp(-alpha * y[i] * prediction);
                    sumWeights += weights[i];
                }

                for (int i = 0; i < nSamples; i++)
                {
                    weights[i] = weights[i] / sumWeights;
                }

                _weakLearners.Add(stump);
                _learnerWeights.Add(alpha);
            }
        }

        public int Predict(double[] x)
        {
            double sum = 0.0;
            for (int i = 0; i < _weakLearners.Count; i++)
            {
                sum += _learnerWeights[i] * _weakLearners[i].Predict(x);
            }

            return sum >= 0.0 ? 1 : -1;
        }

        private double ComputeError(DecisionStump stump, double[][] X, int[] y, double[] weights)
        {
            double error = 0.0;
            for (int i = 0; i < X.Length; i++)
            {
                int prediction = stump.Predict(X[i]);
                if (prediction != y[i])
                {
                    error += weights[i];
                }
            }

            return error;
        }
    }
}