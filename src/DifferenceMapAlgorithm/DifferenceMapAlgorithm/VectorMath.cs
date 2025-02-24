using System;

namespace DifferenceMapAlgorithm
{
    public static class VectorMath
    {
        public static double Norm(double[] vector)
        {
            if (vector == null)
            {
                return 0.0;
            }

            double sum = 0.0;
            for (int i = 0; i < vector.Length; i++)
            {
                sum += vector[i] * vector[i];
            }

            return Math.Sqrt(sum);
        }

        public static double[] Add(double[] a, double[] b)
        {
            if (a == null || b == null || a.Length != b.Length)
            {
                return new double[0];
            }

            double[] result = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                result[i] = a[i] + b[i];
            }

            return result;
        }

        public static double[] Subtract(double[] a, double[] b)
        {
            if (a == null || b == null || a.Length != b.Length)
            {
                return new double[0];
            }

            double[] result = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                result[i] = a[i] - b[i];
            }

            return result;
        }

        public static double[] Scale(double factor, double[] vector)
        {
            if (vector == null)
            {
                return new double[0];
            }

            double[] result = new double[vector.Length];
            for (int i = 0; i < vector.Length; i++)
            {
                result[i] = factor * vector[i];
            }

            return result;
        }
    }
}