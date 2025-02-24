using System;
using System.Collections.Generic;

namespace LogitBoost
{
    public interface IWeakLearner
    {
        void Fit(double[][] X, double[] target, double[] weights);
        double Predict(double[] x);
    }

    public class DecisionStump : IWeakLearner
    {
        public int FeatureIndex { get; set; }
        public double Threshold { get; set; }
        public double LeftValue { get; set; }
        public double RightValue { get; set; }
        public double ConstantValue { get; set; }

        public DecisionStump()
        {
            FeatureIndex = -1;
            Threshold = 0.0;
            LeftValue = 0.0;
            RightValue = 0.0;
            ConstantValue = 0.0;
        }

        public void Fit(double[][] X, double[] target, double[] weights)
        {
            if (X == null || target == null || weights == null)
            {
                return;
            }

            int samples = X.Length;
            if (samples == 0)
            {
                return;
            }

            int features = X[0].Length;
            double bestError = double.PositiveInfinity;
            int bestFeature = -1;
            double bestThreshold = 0.0;
            double bestLeftValue = 0.0;
            double bestRightValue = 0.0;
            double bestConstant = 0.0;
            double totalWeight = 0.0;
            double totalSum = 0.0;
            double totalSumSq = 0.0;
            for (int i = 0; i < samples; i++)
            {
                totalWeight += weights[i];
                totalSum += weights[i] * target[i];
                totalSumSq += weights[i] * target[i] * target[i];
            }

            double constant = (totalWeight > 0.0) ? totalSum / totalWeight : 0.0;
            double trivialError = (totalWeight > 0.0) ? (totalSumSq - (totalSum * totalSum) / totalWeight) : 0.0;
            bestError = trivialError;
            bestFeature = -1;
            bestConstant = constant;
            for (int f = 0; f < features; f++)
            {
                int[] indices = new int[samples];
                for (int i = 0; i < samples; i++)
                {
                    indices[i] = i;
                }

                Array.Sort(indices, delegate (int i1, int i2)
                {
                    return X[i1][f].CompareTo(X[i2][f]);
                });
                double[] cumW = new double[samples];
                double[] cumSum = new double[samples];
                double[] cumSumSq = new double[samples];
                cumW[0] = weights[indices[0]];
                cumSum[0] = weights[indices[0]] * target[indices[0]];
                cumSumSq[0] = weights[indices[0]] * target[indices[0]] * target[indices[0]];
                for (int j = 1; j < samples; j++)
                {
                    int idx = indices[j];
                    cumW[j] = cumW[j - 1] + weights[idx];
                    cumSum[j] = cumSum[j - 1] + weights[idx] * target[idx];
                    cumSumSq[j] = cumSumSq[j - 1] + weights[idx] * target[idx] * target[idx];
                }

                double totalW = cumW[samples - 1];
                double totalS = cumSum[samples - 1];
                double totalSsq = cumSumSq[samples - 1];
                for (int j = 0; j < samples - 1; j++)
                {
                    double currentVal = X[indices[j]][f];
                    double nextVal = X[indices[j + 1]][f];
                    if (currentVal == nextVal)
                    {
                        continue;
                    }

                    double thresholdCandidate = (currentVal + nextVal) / 2.0;
                    double leftW = cumW[j];
                    double rightW = totalW - leftW;
                    if (leftW == 0.0 || rightW == 0.0)
                    {
                        continue;
                    }

                    double leftSum = cumSum[j];
                    double rightSum = totalS - leftSum;
                    double leftError = cumSumSq[j] - (leftSum * leftSum) / leftW;
                    double rightError = (totalSsq - cumSumSq[j]) - (rightSum * rightSum) / rightW;
                    double error = leftError + rightError;
                    if (error < bestError)
                    {
                        bestError = error;
                        bestFeature = f;
                        bestThreshold = thresholdCandidate;
                        bestLeftValue = leftSum / leftW;
                        bestRightValue = rightSum / rightW;
                    }
                }
            }

            if (bestFeature == -1)
            {
                this.FeatureIndex = -1;
                this.ConstantValue = bestConstant;
            }
            else
            {
                this.FeatureIndex = bestFeature;
                this.Threshold = bestThreshold;
                this.LeftValue = bestLeftValue;
                this.RightValue = bestRightValue;
            }
        }

        public double Predict(double[] x)
        {
            if (x == null)
            {
                return 0.0;
            }

            if (this.FeatureIndex == -1)
            {
                return this.ConstantValue;
            }

            if (x[this.FeatureIndex] <= this.Threshold)
            {
                return this.LeftValue;
            }
            else
            {
                return this.RightValue;
            }
        }
    }

    public class LogitBoostAlgorithm
    {
        private List<IWeakLearner> _learners;
        private int _numIterations;
        public LogitBoostAlgorithm()
        {
            this._learners = new List<IWeakLearner>();
            this._numIterations = 0;
        }

        public void Fit(double[][] X, int[] y, int numIterations)
        {
            if (X == null || y == null)
            {
                return;
            }

            int n = X.Length;
            if (n == 0 || y.Length != n)
            {
                return;
            }

            double[] F = new double[n];
            for (int i = 0; i < n; i++)
            {
                F[i] = 0.0;
            }

            for (int m = 0; m < numIterations; m++)
            {
                double[] p = new double[n];
                double[] w = new double[n];
                double[] z = new double[n];
                for (int i = 0; i < n; i++)
                {
                    double expF = Math.Exp(F[i]);
                    p[i] = expF / (1.0 + expF);
                    w[i] = p[i] * (1.0 - p[i]);
                    if (Math.Abs(w[i]) < 1e-10)
                    {
                        z[i] = 0.0;
                    }
                    else
                    {
                        z[i] = (y[i] - p[i]) / w[i];
                    }
                }

                DecisionStump stump = new DecisionStump();
                stump.Fit(X, z, w);
                for (int i = 0; i < n; i++)
                {
                    double prediction = stump.Predict(X[i]);
                    F[i] = F[i] + 0.5 * prediction;
                }

                this._learners.Add(stump);
            }

            this._numIterations = numIterations;
        }

        public double PredictProbability(double[] x)
        {
            if (x == null)
            {
                return 0.0;
            }

            double F = 0.0;
            foreach (IWeakLearner learner in this._learners)
            {
                F += 0.5 * learner.Predict(x);
            }

            double expF = Math.Exp(F);
            return expF / (1.0 + expF);
        }

        public int Predict(double[] x)
        {
            double probability = this.PredictProbability(x);
            return (probability >= 0.5) ? 1 : 0;
        }
    }
}