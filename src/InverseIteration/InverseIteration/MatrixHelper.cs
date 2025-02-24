using System;

namespace InverseIteration
{
    public static class MatrixHelper
    {
        public static double[, ] CopyMatrix(double[, ] matrix)
        {
            if (matrix == null)
            {
                return new double[0, 0];
            }

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            double[, ] copy = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    copy[i, j] = matrix[i, j];
                }
            }

            return copy;
        }

        public static void SubtractFromDiagonal(double[, ] matrix, double value)
        {
            int n = Math.Min(matrix.GetLength(0), matrix.GetLength(1));
            for (int i = 0; i < n; i++)
            {
                matrix[i, i] -= value;
            }
        }

        public static double[] Solve(double[, ] M, double[] b)
        {
            int mRows = M.GetLength(0);
            int mCols = M.GetLength(1);
            int n = b.Length;
            if (mRows != n || mCols != n)
            {
                return new double[0];
            }

            double[, ] A = CopyMatrix(M);
            double[] x = new double[n];
            double[] bCopy = new double[n];
            for (int i = 0; i < n; i++)
            {
                bCopy[i] = b[i];
            }

            for (int i = 0; i < n; i++)
            {
                int pivot = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (Math.Abs(A[j, i]) > Math.Abs(A[pivot, i]))
                    {
                        pivot = j;
                    }
                }

                if (pivot != i)
                {
                    for (int k = i; k < n; k++)
                    {
                        double temp = A[i, k];
                        A[i, k] = A[pivot, k];
                        A[pivot, k] = temp;
                    }

                    double tempB = bCopy[i];
                    bCopy[i] = bCopy[pivot];
                    bCopy[pivot] = tempB;
                }

                if (Math.Abs(A[i, i]) < 1e-12)
                {
                    A[i, i] = 1e-12;
                }

                for (int j = i + 1; j < n; j++)
                {
                    double factor = A[j, i] / A[i, i];
                    for (int k = i; k < n; k++)
                    {
                        A[j, k] -= factor * A[i, k];
                    }

                    bCopy[j] -= factor * bCopy[i];
                }
            }

            for (int i = n - 1; i >= 0; i--)
            {
                double sum = 0;
                for (int j = i + 1; j < n; j++)
                {
                    sum += A[i, j] * x[j];
                }

                if (Math.Abs(A[i, i]) < 1e-12)
                {
                    A[i, i] = 1e-12;
                }

                x[i] = (bCopy[i] - sum) / A[i, i];
            }

            return x;
        }

        public static double Norm(double[] v)
        {
            double sum = 0;
            for (int i = 0; i < v.Length; i++)
            {
                sum += v[i] * v[i];
            }

            return Math.Sqrt(sum);
        }

        public static double[] Normalize(double[] v)
        {
            double norm = Norm(v);
            if (norm < 1e-12)
            {
                return v;
            }

            double[] result = new double[v.Length];
            for (int i = 0; i < v.Length; i++)
            {
                result[i] = v[i] / norm;
            }

            return result;
        }

        public static double Distance(double[] v1, double[] v2)
        {
            if (v1.Length != v2.Length)
            {
                return double.MaxValue;
            }

            double sum = 0;
            for (int i = 0; i < v1.Length; i++)
            {
                double diff = v1[i] - v2[i];
                sum += diff * diff;
            }

            return Math.Sqrt(sum);
        }
    }
}