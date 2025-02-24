using System;

namespace EllipsoidMethod
{
    public static class MatrixHelper
    {
        public static double[] MultiplyMatrixVector(double[, ] matrix, double[] vector)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            double[] result = new double[rows];
            for (int i = 0; i < rows; i++)
            {
                result[i] = 0.0;
                for (int j = 0; j < cols; j++)
                {
                    result[i] += matrix[i, j] * vector[j];
                }
            }

            return result;
        }

        public static double InnerProduct(double[] vector1, double[] vector2)
        {
            int length = vector1.Length;
            double result = 0.0;
            for (int i = 0; i < length; i++)
            {
                result += vector1[i] * vector2[i];
            }

            return result;
        }

        public static double[, ] SubtractMatrices(double[, ] a, double[, ] b)
        {
            int rows = a.GetLength(0);
            int cols = a.GetLength(1);
            double[, ] result = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = a[i, j] - b[i, j];
                }
            }

            return result;
        }

        public static double[, ] ScaleMatrix(double[, ] matrix, double factor)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            double[, ] result = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = matrix[i, j] * factor;
                }
            }

            return result;
        }

        public static double[, ] OuterProduct(double[] vector1, double[] vector2)
        {
            int length = vector1.Length;
            double[, ] result = new double[length, length];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    result[i, j] = vector1[i] * vector2[j];
                }
            }

            return result;
        }
    }
}