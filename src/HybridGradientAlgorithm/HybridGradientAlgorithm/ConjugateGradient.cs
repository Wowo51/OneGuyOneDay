using System;

namespace HybridGradientAlgorithm
{
    public class ConjugateGradient
    {
        public double[] Search(double[] candidate, double factor)
        {
            if (candidate == null)
            {
                return new double[0];
            }

            int length = candidate.Length;
            double[] improved = new double[length];
            for (int i = 0; i < length; i++)
            {
                improved[i] = candidate[i] - factor * candidate[i];
            }

            return improved;
        }
    }
}