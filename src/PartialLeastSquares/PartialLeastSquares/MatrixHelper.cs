using System;

namespace PartialLeastSquares
{
    public static class MatrixHelper
    {
        public static double[][] Clone(double[][] matrix)
        {
            int rows = matrix.Length;
            double[][] result = new double[rows][];
            for (int i = 0; i < rows; i++)
            {
                int cols = (matrix[i] != null) ? matrix[i].Length : 0;
                result[i] = new double[cols];
                for (int j = 0; j < cols; j++)
                {
                    result[i][j] = matrix[i][j];
                }
            }

            return result;
        }

        public static double[][] Transpose(double[][] matrix)
        {
            if (matrix == null || matrix.Length == 0)
            {
                return new double[0][];
            }

            int rows = matrix.Length;
            int cols = matrix[0].Length;
            double[][] result = new double[cols][];
            for (int i = 0; i < cols; i++)
            {
                result[i] = new double[rows];
                for (int j = 0; j < rows; j++)
                {
                    result[i][j] = matrix[j][i];
                }
            }

            return result;
        }

        public static double[][] Multiply(double[][] A, double[][] B)
        {
            if (A == null || B == null || A.Length == 0 || B.Length == 0)
            {
                return new double[0][];
            }

            int m = A.Length;
            int n = A[0].Length;
            int p = B[0].Length;
            double[][] result = new double[m][];
            for (int i = 0; i < m; i++)
            {
                result[i] = new double[p];
                for (int j = 0; j < p; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < n; k++)
                    {
                        sum += A[i][k] * B[k][j];
                    }

                    result[i][j] = sum;
                }
            }

            return result;
        }

        public static double[] MultiplyVector(double[][] A, double[] vector)
        {
            if (A == null || vector == null || A.Length == 0)
            {
                return new double[0];
            }

            int m = A.Length;
            int n = A[0].Length;
            double[] result = new double[m];
            for (int i = 0; i < m; i++)
            {
                double sum = 0;
                for (int j = 0; j < n; j++)
                {
                    sum += A[i][j] * vector[j];
                }

                result[i] = sum;
            }

            return result;
        }

        public static double Dot(double[] a, double[] b)
        {
            if (a == null || b == null || a.Length != b.Length)
            {
                return 0;
            }

            double sum = 0;
            for (int i = 0; i < a.Length; i++)
            {
                sum += a[i] * b[i];
            }

            return sum;
        }

        public static double Norm(double[] vector)
        {
            return Math.Sqrt(Dot(vector, vector));
        }

        public static double[][] OuterProduct(double[] a, double[] b)
        {
            int m = a.Length;
            int n = b.Length;
            double[][] result = new double[m][];
            for (int i = 0; i < m; i++)
            {
                result[i] = new double[n];
                for (int j = 0; j < n; j++)
                {
                    result[i][j] = a[i] * b[j];
                }
            }

            return result;
        }

        public static double[][] Subtract(double[][] A, double[][] B)
        {
            if (A == null || B == null)
            {
                return new double[0][];
            }

            int m = A.Length;
            int n = A[0].Length;
            double[][] result = new double[m][];
            for (int i = 0; i < m; i++)
            {
                result[i] = new double[n];
                for (int j = 0; j < n; j++)
                {
                    result[i][j] = A[i][j] - B[i][j];
                }
            }

            return result;
        }

        public static double[][] Inverse(double[][] matrix)
        {
            int n = matrix.Length;
            double[][] augmented = new double[n][];
            for (int i = 0; i < n; i++)
            {
                augmented[i] = new double[2 * n];
                for (int j = 0; j < n; j++)
                {
                    augmented[i][j] = matrix[i][j];
                }

                for (int j = n; j < 2 * n; j++)
                {
                    augmented[i][j] = (i == (j - n)) ? 1.0 : 0.0;
                }
            }

            for (int i = 0; i < n; i++)
            {
                double pivot = augmented[i][i];
                if (Math.Abs(pivot) < 1e-10)
                {
                    pivot = 1e-10;
                }

                for (int j = 0; j < 2 * n; j++)
                {
                    augmented[i][j] /= pivot;
                }

                for (int k = 0; k < n; k++)
                {
                    if (k != i)
                    {
                        double factor = augmented[k][i];
                        for (int j = 0; j < 2 * n; j++)
                        {
                            augmented[k][j] -= factor * augmented[i][j];
                        }
                    }
                }
            }

            double[][] inverse = new double[n][];
            for (int i = 0; i < n; i++)
            {
                inverse[i] = new double[n];
                for (int j = 0; j < n; j++)
                {
                    inverse[i][j] = augmented[i][j + n];
                }
            }

            return inverse;
        }
    }
}