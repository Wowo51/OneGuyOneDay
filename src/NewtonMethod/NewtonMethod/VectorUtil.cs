using System;

namespace NewtonMethod
{
    public static class VectorUtil
    {
        public static double[]? Subtract(double[] a, double[] b)
        {
            if (a.Length != b.Length)
            {
                return null;
            }

            double[] result = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                result[i] = a[i] - b[i];
            }

            return result;
        }

        public static double Norm(double[] a)
        {
            double sum = 0;
            for (int i = 0; i < a.Length; i++)
            {
                sum += a[i] * a[i];
            }

            return Math.Sqrt(sum);
        }
    }
}