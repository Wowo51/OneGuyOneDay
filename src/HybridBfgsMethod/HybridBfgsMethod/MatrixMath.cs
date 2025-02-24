using System;

namespace HybridBfgsMethod
{
    public static class MatrixMath
    {
        public static double[, ] IdentityMatrix(int n)
        {
            double[, ] identity = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    identity[i, j] = (i == j) ? 1.0 : 0.0;
                }
            }

            return identity;
        }

        public static double[, ] Subtract(double[, ] A, double[, ] B)
        {
            int rows = A.GetLength(0);
            int cols = A.GetLength(1);
            double[, ] result = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = A[i, j] - B[i, j];
                }
            }

            return result;
        }

        public static double[, ] Add(double[, ] A, double[, ] B)
        {
            int rows = A.GetLength(0);
            int cols = A.GetLength(1);
            double[, ] result = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = A[i, j] + B[i, j];
                }
            }

            return result;
        }

        public static double[, ] Scale(double[, ] A, double scalar)
        {
            int rows = A.GetLength(0);
            int cols = A.GetLength(1);
            double[, ] result = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = A[i, j] * scalar;
                }
            }

            return result;
        }

        public static double[, ] Multiply(double[, ] A, double[, ] B)
        {
            int rowsA = A.GetLength(0);
            int colsA = A.GetLength(1);
            int colsB = B.GetLength(1);
            double[, ] result = new double[rowsA, colsB];
            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    double sum = 0.0;
                    for (int k = 0; k < colsA; k++)
                    {
                        sum += A[i, k] * B[k, j];
                    }

                    result[i, j] = sum;
                }
            }

            return result;
        }

        public static double[] Multiply(double[, ] A, double[] v)
        {
            int rows = A.GetLength(0);
            int cols = A.GetLength(1);
            double[] result = new double[rows];
            for (int i = 0; i < rows; i++)
            {
                double sum = 0.0;
                for (int j = 0; j < cols; j++)
                {
                    sum += A[i, j] * v[j];
                }

                result[i] = sum;
            }

            return result;
        }

        public static double[, ] OuterProduct(double[] u, double[] v)
        {
            int lenU = u.Length;
            int lenV = v.Length;
            double[, ] result = new double[lenU, lenV];
            for (int i = 0; i < lenU; i++)
            {
                for (int j = 0; j < lenV; j++)
                {
                    result[i, j] = u[i] * v[j];
                }
            }

            return result;
        }
    }
}