using System;

namespace TridiagonalMatrixAlgorithm
{
    public static class ThomasAlgorithmSolver
    {
        public static double[]? Solve(double[] lower, double[] main, double[] upper, double[] d)
        {
            if (lower == null || main == null || upper == null || d == null || d.Length != main.Length || lower.Length != main.Length - 1 || upper.Length != main.Length - 1)
            {
                return null;
            }

            int n = main.Length;
            double[] bCopy = new double[n];
            double[] dCopy = new double[n];
            for (int i = 0; i < n; i++)
            {
                bCopy[i] = main[i];
                dCopy[i] = d[i];
            }

            for (int i = 1; i < n; i++)
            {
                if (bCopy[i - 1] == 0.0)
                {
                    return null;
                }

                double w = lower[i - 1] / bCopy[i - 1];
                bCopy[i] = bCopy[i] - w * upper[i - 1];
                dCopy[i] = dCopy[i] - w * dCopy[i - 1];
            }

            if (bCopy[n - 1] == 0.0)
            {
                return null;
            }

            double[] x = new double[n];
            x[n - 1] = dCopy[n - 1] / bCopy[n - 1];
            for (int i = n - 2; i >= 0; i--)
            {
                if (bCopy[i] == 0.0)
                {
                    return null;
                }

                x[i] = (dCopy[i] - upper[i] * x[i + 1]) / bCopy[i];
            }

            return x;
        }
    }
}