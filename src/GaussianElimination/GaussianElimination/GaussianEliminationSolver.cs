using System;

namespace GaussianElimination
{
    public class GaussianEliminationSolver
    {
        public static double[] Solve(double[, ] A, double[] b)
        {
            int n = A.GetLength(0);
            if (n != A.GetLength(1) || n != b.Length)
            {
                return new double[0];
            }

            double[, ] matrix = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = A[i, j];
                }
            }

            double[] vector = new double[n];
            for (int i = 0; i < n; i++)
            {
                vector[i] = b[i];
            }

            // Forward elimination with partial pivoting
            for (int k = 0; k < n - 1; k++)
            {
                int maxRow = k;
                double maxVal = Math.Abs(matrix[k, k]);
                for (int i = k + 1; i < n; i++)
                {
                    double currentVal = Math.Abs(matrix[i, k]);
                    if (currentVal > maxVal)
                    {
                        maxVal = currentVal;
                        maxRow = i;
                    }
                }

                if (maxRow != k)
                {
                    for (int j = 0; j < n; j++)
                    {
                        double temp = matrix[k, j];
                        matrix[k, j] = matrix[maxRow, j];
                        matrix[maxRow, j] = temp;
                    }

                    double tmp = vector[k];
                    vector[k] = vector[maxRow];
                    vector[maxRow] = tmp;
                }

                if (Math.Abs(matrix[k, k]) < 1e-12)
                {
                    return new double[0];
                }

                for (int i = k + 1; i < n; i++)
                {
                    double factor = matrix[i, k] / matrix[k, k];
                    for (int j = k; j < n; j++)
                    {
                        matrix[i, j] -= factor * matrix[k, j];
                    }

                    vector[i] -= factor * vector[k];
                }
            }

            double[] x = new double[n];
            if (Math.Abs(matrix[n - 1, n - 1]) < 1e-12)
            {
                return new double[0];
            }

            x[n - 1] = vector[n - 1] / matrix[n - 1, n - 1];
            for (int i = n - 2; i >= 0; i--)
            {
                double sum = 0.0;
                for (int j = i + 1; j < n; j++)
                {
                    sum += matrix[i, j] * x[j];
                }

                if (Math.Abs(matrix[i, i]) < 1e-12)
                {
                    return new double[0];
                }

                x[i] = (vector[i] - sum) / matrix[i, i];
            }

            return x;
        }
    }
}