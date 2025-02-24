using System;

namespace LevenbergMarquardtAlgorithm
{
    public static class LinearAlgebra
    {
        public static double[]? SolveLinearSystem(double[, ] A, double[] b)
        {
            int n = b.Length;
            double[, ] aug = new double[n, n + 1];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    aug[i, j] = A[i, j];
                }

                aug[i, n] = b[i];
            }

            for (int i = 0; i < n; i++)
            {
                int maxRow = i;
                for (int k = i + 1; k < n; k++)
                {
                    if (Math.Abs(aug[k, i]) > Math.Abs(aug[maxRow, i]))
                    {
                        maxRow = k;
                    }
                }

                for (int j = 0; j < n + 1; j++)
                {
                    double temp = aug[i, j];
                    aug[i, j] = aug[maxRow, j];
                    aug[maxRow, j] = temp;
                }

                if (Math.Abs(aug[i, i]) < 1e-12)
                {
                    return null;
                }

                for (int k = i + 1; k < n; k++)
                {
                    double factor = aug[k, i] / aug[i, i];
                    for (int j = i; j < n + 1; j++)
                    {
                        aug[k, j] -= factor * aug[i, j];
                    }
                }
            }

            double[] x = new double[n];
            for (int i = n - 1; i >= 0; i--)
            {
                double sum = aug[i, n];
                for (int j = i + 1; j < n; j++)
                {
                    sum -= aug[i, j] * x[j];
                }

                if (Math.Abs(aug[i, i]) < 1e-12)
                {
                    return null;
                }

                x[i] = sum / aug[i, i];
            }

            return x;
        }
    }
}