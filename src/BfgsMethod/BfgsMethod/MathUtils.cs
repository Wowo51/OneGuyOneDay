using System;

namespace BfgsMethod
{
    public static class MathUtils
    {
        public static double Dot(double[] a, double[] b)
        {
            double sum = 0.0;
            for (int i = 0; i < a.Length; i++)
            {
                sum += a[i] * b[i];
            }

            return sum;
        }

        public static double Norm(double[] a)
        {
            return Math.Sqrt(Dot(a, a));
        }

        public static double[] Add(double[] a, double[] b)
        {
            int n = a.Length;
            double[] result = new double[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = a[i] + b[i];
            }

            return result;
        }

        public static double[] Subtract(double[] a, double[] b)
        {
            int n = a.Length;
            double[] result = new double[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = a[i] - b[i];
            }

            return result;
        }

        public static double[] Scale(double[] a, double scalar)
        {
            int n = a.Length;
            double[] result = new double[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = a[i] * scalar;
            }

            return result;
        }

        public static double[][] IdentityMatrix(int n)
        {
            double[][] matrix = new double[n][];
            for (int i = 0; i < n; i++)
            {
                matrix[i] = new double[n];
                for (int j = 0; j < n; j++)
                {
                    matrix[i][j] = (i == j) ? 1.0 : 0.0;
                }
            }

            return matrix;
        }

        public static double[] MultiplyMatrixVector(double[][] matrix, double[] vector)
        {
            int n = vector.Length;
            double[] result = new double[n];
            for (int i = 0; i < matrix.Length; i++)
            {
                result[i] = 0.0;
                for (int j = 0; j < vector.Length; j++)
                {
                    result[i] += matrix[i][j] * vector[j];
                }
            }

            return result;
        }

        public static double[][] MultiplyMatrices(double[][] A, double[][] B)
        {
            int n = A.Length;
            int m = B[0].Length;
            double[][] result = new double[n][];
            for (int i = 0; i < n; i++)
            {
                result[i] = new double[m];
                for (int j = 0; j < m; j++)
                {
                    result[i][j] = 0.0;
                    for (int k = 0; k < A[0].Length; k++)
                    {
                        result[i][j] += A[i][k] * B[k][j];
                    }
                }
            }

            return result;
        }

        public static double[][] OuterProduct(double[] a, double[] b)
        {
            int n = a.Length;
            int m = b.Length;
            double[][] result = new double[n][];
            for (int i = 0; i < n; i++)
            {
                result[i] = new double[m];
                for (int j = 0; j < m; j++)
                {
                    result[i][j] = a[i] * b[j];
                }
            }

            return result;
        }

        public static double[][] ScaleMatrix(double[][] matrix, double scalar)
        {
            int n = matrix.Length;
            double[][] result = new double[n][];
            for (int i = 0; i < n; i++)
            {
                int m = matrix[i].Length;
                result[i] = new double[m];
                for (int j = 0; j < m; j++)
                {
                    result[i][j] = matrix[i][j] * scalar;
                }
            }

            return result;
        }

        public static double[][] AddMatrices(double[][] A, double[][] B)
        {
            int n = A.Length;
            double[][] result = new double[n][];
            for (int i = 0; i < n; i++)
            {
                int m = A[i].Length;
                result[i] = new double[m];
                for (int j = 0; j < m; j++)
                {
                    result[i][j] = A[i][j] + B[i][j];
                }
            }

            return result;
        }

        public static double[][] SubtractMatrices(double[][] A, double[][] B)
        {
            int n = A.Length;
            double[][] result = new double[n][];
            for (int i = 0; i < n; i++)
            {
                int m = A[i].Length;
                result[i] = new double[m];
                for (int j = 0; j < m; j++)
                {
                    result[i][j] = A[i][j] - B[i][j];
                }
            }

            return result;
        }
    }
}