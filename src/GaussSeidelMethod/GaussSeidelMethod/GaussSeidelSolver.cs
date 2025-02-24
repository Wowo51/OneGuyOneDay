using System;

namespace GaussSeidelMethod
{
    public class GaussSeidelSolver
    {
        public static double[] Solve(double[, ] matrix, double[] b, double[] initial, double tolerance, int maxIterations)
        {
            int n = initial.Length;
            if (matrix.GetLength(0) != n || matrix.GetLength(1) != n || b.Length != n)
            {
                return initial;
            }

            double[] x = new double[n];
            for (int i = 0; i < n; i++)
            {
                x[i] = initial[i];
            }

            for (int iter = 0; iter < maxIterations; iter++)
            {
                double[] next = new double[n];
                for (int i = 0; i < n; i++)
                {
                    double sigma = 0.0;
                    for (int j = 0; j < i; j++)
                    {
                        sigma += matrix[i, j] * next[j];
                    }

                    for (int j = i + 1; j < n; j++)
                    {
                        sigma += matrix[i, j] * x[j];
                    }

                    double diag = matrix[i, i];
                    if (Math.Abs(diag) < 1e-10)
                    {
                        next[i] = x[i];
                    }
                    else
                    {
                        next[i] = (b[i] - sigma) / diag;
                    }
                }

                double maxDiff = 0.0;
                for (int i = 0; i < n; i++)
                {
                    double diff = Math.Abs(next[i] - x[i]);
                    if (diff > maxDiff)
                    {
                        maxDiff = diff;
                    }
                }

                x = next;
                if (maxDiff < tolerance)
                {
                    break;
                }
            }

            return x;
        }
    }
}