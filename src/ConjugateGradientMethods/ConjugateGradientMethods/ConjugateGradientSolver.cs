using System;

namespace ConjugateGradientMethods
{
    public class ConjugateGradientSolver
    {
        public static double[] Solve(double[, ] A, double[] b, double[] x, double tolerance, int maxIterations)
        {
            if (A == null || b == null || x == null)
            {
                return new double[0];
            }

            int n = b.Length;
            if (A.GetLength(0) != n || A.GetLength(1) != n || x.Length != n)
            {
                return new double[0];
            }

            double[] r = VectorSubtract(b, MatrixVectorMultiply(A, x));
            double[] p = (double[])r.Clone();
            double rsold = DotProduct(r, r);
            for (int i = 0; i < maxIterations; i++)
            {
                double[] Ap = MatrixVectorMultiply(A, p);
                double pAp = DotProduct(p, Ap);
                if (pAp == 0.0)
                {
                    break;
                }

                double alpha = rsold / pAp;
                x = VectorAdd(x, ScaleVector(p, alpha));
                r = VectorSubtract(r, ScaleVector(Ap, alpha));
                double rsnew = DotProduct(r, r);
                if (Math.Sqrt(rsnew) < tolerance)
                {
                    break;
                }

                double beta = rsnew / rsold;
                p = VectorAdd(r, ScaleVector(p, beta));
                rsold = rsnew;
            }

            return x;
        }

        private static double[] MatrixVectorMultiply(double[, ] matrix, double[] vector)
        {
            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);
            double[] result = new double[m];
            for (int i = 0; i < m; i++)
            {
                double sum = 0;
                for (int j = 0; j < n; j++)
                {
                    sum += matrix[i, j] * vector[j];
                }

                result[i] = sum;
            }

            return result;
        }

        private static double DotProduct(double[] a, double[] b)
        {
            int length = a.Length;
            double sum = 0;
            for (int i = 0; i < length; i++)
            {
                sum += a[i] * b[i];
            }

            return sum;
        }

        private static double[] VectorAdd(double[] a, double[] b)
        {
            int length = a.Length;
            double[] result = new double[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = a[i] + b[i];
            }

            return result;
        }

        private static double[] VectorSubtract(double[] a, double[] b)
        {
            int length = a.Length;
            double[] result = new double[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = a[i] - b[i];
            }

            return result;
        }

        private static double[] ScaleVector(double[] vector, double scalar)
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