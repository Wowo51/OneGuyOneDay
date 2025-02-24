using System;

namespace ConjugateGradient
{
    public static class ConjugateGradientSolver
    {
        public static double[] Solve(Func<double[], double[]> matrixMultiply, double[] b, double tolerance, int maxIterations)
        {
            if (matrixMultiply == null)
            {
                return new double[0];
            }

            if (b == null)
            {
                return new double[0];
            }

            int length = b.Length;
            double[] x = new double[length];
            double[] r = Subtract(b, matrixMultiply(x));
            double[] p = (double[])r.Clone();
            double rsold = Dot(r, r);
            for (int i = 0; i < maxIterations; i++)
            {
                double[] Ap = matrixMultiply(p);
                double alpha = rsold / Dot(p, Ap);
                x = Add(x, MultiplyByScalar(p, alpha));
                r = Subtract(r, MultiplyByScalar(Ap, alpha));
                double rsnew = Dot(r, r);
                if (System.Math.Sqrt(rsnew) < tolerance)
                {
                    break;
                }

                p = Add(r, MultiplyByScalar(p, rsnew / rsold));
                rsold = rsnew;
            }

            return x;
        }

        private static double Dot(double[] a, double[] b)
        {
            double sum = 0.0;
            for (int i = 0; i < a.Length; i++)
            {
                sum += a[i] * b[i];
            }

            return sum;
        }

        private static double[] Add(double[] a, double[] b)
        {
            int length = a.Length;
            double[] result = new double[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = a[i] + b[i];
            }

            return result;
        }

        private static double[] Subtract(double[] a, double[] b)
        {
            int length = a.Length;
            double[] result = new double[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = a[i] - b[i];
            }

            return result;
        }

        private static double[] MultiplyByScalar(double[] vector, double scalar)
        {
            int length = vector.Length;
            double[] result = new double[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = vector[i] * scalar;
            }

            return result;
        }
    }
}