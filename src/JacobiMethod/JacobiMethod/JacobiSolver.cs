using System;

namespace JacobiMethod
{
    public static class JacobiSolver
    {
        public static double[] Solve(double[, ] A, double[] b, double tolerance, int maxIterations)
        {
            if (A == null || b == null)
            {
                return new double[0];
            }

            int n = b.Length;
            if (A.GetLength(0) != n || A.GetLength(1) != n)
            {
                return new double[n];
            }

            double[] x = new double[n];
            for (int i = 0; i < n; i++)
            {
                x[i] = 0.0;
            }

            for (int iteration = 0; iteration < maxIterations; iteration++)
            {
                double[] newX = new double[n];
                for (int i = 0; i < n; i++)
                {
                    double sum = 0.0;
                    for (int j = 0; j < n; j++)
                    {
                        if (j != i)
                        {
                            sum += A[i, j] * x[j];
                        }
                    }

                    newX[i] = (b[i] - sum) / A[i, i];
                }

                double normDiff = 0.0;
                for (int i = 0; i < n; i++)
                {
                    normDiff += Math.Abs(newX[i] - x[i]);
                }

                if (normDiff < tolerance)
                {
                    return newX;
                }

                x = newX;
            }

            return x;
        }
    }
}