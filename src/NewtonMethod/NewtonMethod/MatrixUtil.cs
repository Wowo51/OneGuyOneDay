using System;

namespace NewtonMethod
{
    public static class MatrixUtil
    {
        public static double[]? Solve(double[, ] A, double[] b)
        {
            int n = A.GetLength(0);
            if (n != A.GetLength(1) || b.Length != n)
            {
                return null;
            }

            double[, ] a = new double[n, n];
            double[] bCopy = new double[n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    a[i, j] = A[i, j];
                }

                bCopy[i] = b[i];
            }

            for (int i = 0; i < n; i++)
            {
                int pivot = i;
                double max = Math.Abs(a[i, i]);
                for (int row = i + 1; row < n; row++)
                {
                    if (Math.Abs(a[row, i]) > max)
                    {
                        max = Math.Abs(a[row, i]);
                        pivot = row;
                    }
                }

                if (Math.Abs(a[pivot, i]) < 1e-12)
                {
                    return null;
                }

                if (pivot != i)
                {
                    for (int j = 0; j < n; j++)
                    {
                        double temp = a[i, j];
                        a[i, j] = a[pivot, j];
                        a[pivot, j] = temp;
                    }

                    double tempB = bCopy[i];
                    bCopy[i] = bCopy[pivot];
                    bCopy[pivot] = tempB;
                }

                double pivotValue = a[i, i];
                for (int j = i; j < n; j++)
                {
                    a[i, j] /= pivotValue;
                }

                bCopy[i] /= pivotValue;
                for (int row = 0; row < n; row++)
                {
                    if (row != i)
                    {
                        double factor = a[row, i];
                        for (int col = i; col < n; col++)
                        {
                            a[row, col] -= factor * a[i, col];
                        }

                        bCopy[row] -= factor * bCopy[i];
                    }
                }
            }

            double[] solution = new double[n];
            for (int i = 0; i < n; i++)
            {
                solution[i] = bCopy[i];
            }

            return solution;
        }
    }
}