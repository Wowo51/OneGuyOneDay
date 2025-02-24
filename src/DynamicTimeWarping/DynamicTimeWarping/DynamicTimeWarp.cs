using System;

namespace DynamicTimeWarping
{
    public static class DTW
    {
        public static double Compute(double[] sequence1, double[] sequence2)
        {
            if (sequence1 == null)
            {
                sequence1 = new double[0];
            }

            if (sequence2 == null)
            {
                sequence2 = new double[0];
            }

            int n = sequence1.Length;
            int m = sequence2.Length;
            double[, ] dtw = new double[n + 1, m + 1];
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= m; j++)
                {
                    dtw[i, j] = double.PositiveInfinity;
                }
            }

            dtw[0, 0] = 0;
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    double cost = Math.Abs(sequence1[i - 1] - sequence2[j - 1]);
                    double minimum = dtw[i - 1, j];
                    if (dtw[i, j - 1] < minimum)
                    {
                        minimum = dtw[i, j - 1];
                    }

                    if (dtw[i - 1, j - 1] < minimum)
                    {
                        minimum = dtw[i - 1, j - 1];
                    }

                    dtw[i, j] = cost + minimum;
                }
            }

            return dtw[n, m];
        }
    }
}