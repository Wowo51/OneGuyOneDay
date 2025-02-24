using System;

namespace PowerIteration
{
    public static class PowerIterationAlgorithm
    {
        public static (double eigenvalue, double[] eigenvector) Compute(double[, ] matrix, double[] initial, double tolerance, int maxIterations)
        {
            if (matrix == null || initial == null)
            {
                return (0.0, new double[0]);
            }

            if (matrix.GetLength(1) != initial.Length || matrix.GetLength(0) != matrix.GetLength(1))
            {
                return (0.0, new double[0]);
            }

            double[] v = Normalize(initial);
            double eigenvalue = 0.0;
            for (int i = 0; i < maxIterations; i++)
            {
                double[] next = MultiplyMatrixVector(matrix, v);
                if (IsZeroVector(next))
                {
                    return (0.0, new double[0]);
                }

                double[] nextNormalized = Normalize(next);
                eigenvalue = RayleighQuotient(matrix, nextNormalized);
                double diff = DifferenceNorm(v, nextNormalized);
                v = nextNormalized;
                if (diff < tolerance)
                {
                    break;
                }
            }

            return (eigenvalue, v);
        }

        private static double[] MultiplyMatrixVector(double[, ] matrix, double[] vector)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            double[] result = new double[rows];
            for (int i = 0; i < rows; i++)
            {
                double sum = 0.0;
                for (int j = 0; j < cols; j++)
                {
                    sum += matrix[i, j] * vector[j];
                }

                result[i] = sum;
            }

            return result;
        }

        private static double[] Normalize(double[] vector)
        {
            double norm = 0.0;
            for (int i = 0; i < vector.Length; i++)
            {
                norm += vector[i] * vector[i];
            }

            norm = System.Math.Sqrt(norm);
            if (norm == 0.0)
            {
                return vector;
            }

            double[] normalized = new double[vector.Length];
            for (int i = 0; i < vector.Length; i++)
            {
                normalized[i] = vector[i] / norm;
            }

            return normalized;
        }

        private static double RayleighQuotient(double[, ] matrix, double[] vector)
        {
            int n = matrix.GetLength(0);
            double numerator = 0.0;
            double denominator = 0.0;
            for (int i = 0; i < n; i++)
            {
                double temp = 0.0;
                for (int j = 0; j < n; j++)
                {
                    temp += matrix[i, j] * vector[j];
                }

                numerator += vector[i] * temp;
                denominator += vector[i] * vector[i];
            }

            if (denominator == 0.0)
            {
                return 0.0;
            }

            return numerator / denominator;
        }

        private static double DifferenceNorm(double[] vector1, double[] vector2)
        {
            double sum = 0.0;
            for (int i = 0; i < vector1.Length; i++)
            {
                double diff = vector1[i] - vector2[i];
                sum += diff * diff;
            }

            return System.Math.Sqrt(sum);
        }

        private static bool IsZeroVector(double[] vector)
        {
            for (int i = 0; i < vector.Length; i++)
            {
                if (vector[i] != 0.0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}