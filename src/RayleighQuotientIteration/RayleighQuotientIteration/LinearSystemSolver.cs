namespace RayleighQuotientIteration
{
    public static class LinearSystemSolver
    {
        public static double[] Solve(double[, ] a, double[] b)
        {
            int n = a.GetLength(0);
            double[, ] A = new double[n, n];
            double[] B = new double[n];
            for (int i = 0; i < n; i++)
            {
                B[i] = b[i];
                for (int j = 0; j < n; j++)
                {
                    A[i, j] = a[i, j];
                }
            }

            for (int i = 0; i < n; i++)
            {
                int pivotRow = i;
                double maxVal = System.Math.Abs(A[i, i]);
                for (int k = i + 1; k < n; k++)
                {
                    double absVal = System.Math.Abs(A[k, i]);
                    if (absVal > maxVal)
                    {
                        maxVal = absVal;
                        pivotRow = k;
                    }
                }

                if (maxVal < 1e-12)
                {
                    double[] result = new double[n];
                    for (int j = 0; j < n; j++)
                    {
                        result[j] = B[j];
                    }

                    return result;
                }

                if (pivotRow != i)
                {
                    for (int j = 0; j < n; j++)
                    {
                        double temp = A[i, j];
                        A[i, j] = A[pivotRow, j];
                        A[pivotRow, j] = temp;
                    }

                    double tempB = B[i];
                    B[i] = B[pivotRow];
                    B[pivotRow] = tempB;
                }

                for (int k = i + 1; k < n; k++)
                {
                    double factor = A[k, i] / A[i, i];
                    for (int j = i; j < n; j++)
                    {
                        A[k, j] -= factor * A[i, j];
                    }

                    B[k] -= factor * B[i];
                }
            }

            double[] x = new double[n];
            for (int i = n - 1; i >= 0; i--)
            {
                double sum = 0.0;
                for (int j = i + 1; j < n; j++)
                {
                    sum += A[i, j] * x[j];
                }

                if (System.Math.Abs(A[i, i]) < 1e-12)
                {
                    x[i] = 0.0;
                }
                else
                {
                    x[i] = (B[i] - sum) / A[i, i];
                }
            }

            return x;
        }
    }
}