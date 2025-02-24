using System;

namespace HybridBfgsMethod
{
    public static class VectorMath
    {
        public static double Dot(double[] a, double[] b)
        {
            int length = a.Length;
            double sum = 0.0;
            for (int i = 0; i < length; i++)
            {
                sum += a[i] * b[i];
            }

            return sum;
        }

        public static double Norm(double[] a)
        {
            double sum = 0.0;
            for (int i = 0; i < a.Length; i++)
            {
                sum += a[i] * a[i];
            }

            return Math.Sqrt(sum);
        }

        public static double[] Add(double[] a, double[] b)
        {
            int length = a.Length;
            double[] result = new double[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = a[i] + b[i];
            }

            return result;
        }

        public static double[] Subtract(double[] a, double[] b)
        {
            int length = a.Length;
            double[] result = new double[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = a[i] - b[i];
            }

            return result;
        }

        public static double[] Scale(double[] a, double scalar)
        {
            int length = a.Length;
            double[] result = new double[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = a[i] * scalar;
            }

            return result;
        }
    }
}