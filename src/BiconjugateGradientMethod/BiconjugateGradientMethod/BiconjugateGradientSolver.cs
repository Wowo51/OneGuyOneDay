using System;

namespace BiconjugateGradientMethod
{
    public class BiconjugateGradientSolver
    {
        public static double[] Solve(double[, ] A, double[] b, double[]? initialGuess, double tol, int maxIter)
        {
            int n = b.Length;
            double[] x;
            if (initialGuess == null)
            {
                x = new double[n];
            }
            else
            {
                x = new double[n];
                for (int i = 0; i < n; i++)
                {
                    x[i] = initialGuess[i];
                }
            }

            double[] Ax = Multiply(A, x);
            double[] r = Subtract(b, Ax);
            double[] rTilde = new double[r.Length];
            for (int i = 0; i < r.Length; i++)
            {
                rTilde[i] = r[i];
            }

            double[] p = new double[r.Length];
            double[] pTilde = new double[r.Length];
            for (int i = 0; i < r.Length; i++)
            {
                p[i] = r[i];
                pTilde[i] = rTilde[i];
            }

            double rhoPrev = DotProduct(rTilde, r);
            int iter = 0;
            while (iter < maxIter && Norm(r) > tol)
            {
                double[] Ap = Multiply(A, p);
                double denominator = DotProduct(pTilde, Ap);
                if (denominator == 0)
                {
                    break;
                }

                double alpha = rhoPrev / denominator;
                x = Add(x, Scale(p, alpha));
                double[] rNew = Subtract(r, Scale(Ap, alpha));
                double[] At_pTilde = MultiplyTranspose(A, pTilde);
                double[] rTildeNew = Subtract(rTilde, Scale(At_pTilde, alpha));
                double rhoNew = DotProduct(rTildeNew, rNew);
                if (rhoPrev == 0)
                {
                    break;
                }

                double beta = rhoNew / rhoPrev;
                p = Add(rNew, Scale(p, beta));
                pTilde = Add(rTildeNew, Scale(pTilde, beta));
                r = rNew;
                rTilde = rTildeNew;
                rhoPrev = rhoNew;
                iter++;
            }

            return x;
        }

        private static double[] Multiply(double[, ] A, double[] x)
        {
            int rows = A.GetLength(0);
            int cols = A.GetLength(1);
            double[] result = new double[rows];
            for (int i = 0; i < rows; i++)
            {
                result[i] = 0;
                for (int j = 0; j < cols; j++)
                {
                    result[i] += A[i, j] * x[j];
                }
            }

            return result;
        }

        private static double[] MultiplyTranspose(double[, ] A, double[] x)
        {
            int rows = A.GetLength(0);
            int cols = A.GetLength(1);
            double[] result = new double[cols];
            for (int j = 0; j < cols; j++)
            {
                result[j] = 0;
                for (int i = 0; i < rows; i++)
                {
                    result[j] += A[i, j] * x[i];
                }
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

        private static double[] Scale(double[] a, double scalar)
        {
            int length = a.Length;
            double[] result = new double[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = a[i] * scalar;
            }

            return result;
        }

        private static double Norm(double[] a)
        {
            return Math.Sqrt(DotProduct(a, a));
        }
    }
}