using System.Collections.Generic;

namespace BuzenAlgorithm
{
    public static class BuzenCalculator
    {
        public static double ComputeNormalizationConstant(int K, IList<double> weights)
        {
            if (weights == null)
            {
                return K == 0 ? 1.0 : 0.0;
            }

            if (K < 0)
            {
                return 0.0;
            }

            int count = weights.Count;
            double[] G = new double[K + 1];
            G[0] = 1.0;
            for (int i = 0; i < count; i++)
            {
                for (int k = 1; k <= K; k++)
                {
                    G[k] += weights[i] * G[k - 1];
                }
            }

            return G[K];
        }
    }
}