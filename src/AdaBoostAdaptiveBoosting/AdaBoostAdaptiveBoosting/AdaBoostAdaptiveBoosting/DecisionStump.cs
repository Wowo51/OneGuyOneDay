using System;

namespace AdaBoostAdaptiveBoosting
{
    public class DecisionStump
    {
        public int FeatureIndex { get; set; }
        public double Threshold { get; set; }
        public int Polarity { get; set; }

        public int Predict(double[] x)
        {
            if (x == null || x.Length <= FeatureIndex)
            {
                return -1;
            }

            return x[FeatureIndex] < Threshold ? Polarity : -Polarity;
        }

        public static DecisionStump GetBestStump(double[][] X, int[] y, double[] weights)
        {
            int nSamples = X.Length;
            int nFeatures = nSamples > 0 ? X[0].Length : 0;
            double bestError = Double.MaxValue;
            DecisionStump? bestStump = null;
            for (int feature = 0; feature < nFeatures; feature++)
            {
                for (int i = 0; i < nSamples; i++)
                {
                    double thresholdCandidate = X[i][feature];
                    for (int j = 0; j < 2; j++)
                    {
                        int polarity = (j == 0) ? 1 : -1;
                        double error = 0.0;
                        for (int k = 0; k < nSamples; k++)
                        {
                            int prediction = X[k][feature] < thresholdCandidate ? polarity : -polarity;
                            if (prediction != y[k])
                            {
                                error += weights[k];
                            }
                        }

                        if (error < bestError)
                        {
                            bestError = error;
                            bestStump = new DecisionStump();
                            bestStump.FeatureIndex = feature;
                            bestStump.Threshold = thresholdCandidate;
                            bestStump.Polarity = polarity;
                        }
                    }
                }
            }

            if (bestStump == null)
            {
                bestStump = new DecisionStump();
                bestStump.FeatureIndex = 0;
                bestStump.Threshold = 0.0;
                bestStump.Polarity = 1;
            }

            return bestStump!;
        }
    }
}