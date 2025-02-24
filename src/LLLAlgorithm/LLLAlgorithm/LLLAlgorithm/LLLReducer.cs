using System;

namespace LLLAlgorithm
{
    public static class LLLReducer
    {
        public static double[][] Reduce(double[][] basis, double delta)
        {
            if (basis == null)
            {
                return new double[0][];
            }

            int n = basis.Length;
            if (n == 0)
            {
                return basis;
            }

            int dim = basis[0].Length;
            double[][] B = new double[n][];
            for (int i = 0; i < n; i++)
            {
                B[i] = (double[])basis[i].Clone();
            }

            double[][] bStar = new double[n][];
            double[, ] mu = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                bStar[i] = (double[])B[i].Clone();
                for (int j = 0; j < i; j++)
                {
                    mu[i, j] = VectorMath.Dot(B[i], bStar[j]) / VectorMath.NormSquared(bStar[j]);
                    bStar[i] = VectorMath.Subtract(bStar[i], VectorMath.Multiply(bStar[j], mu[i, j]));
                }
            }

            int k = 1;
            while (k < n)
            {
                for (int j = k - 1; j >= 0; j--)
                {
                    double r = Math.Round(mu[k, j]);
                    if (Math.Abs(r) > 1e-10)
                    {
                        for (int i = 0; i < dim; i++)
                        {
                            B[k][i] = B[k][i] - r * B[j][i];
                        }

                        for (int i = 0; i < j; i++)
                        {
                            mu[k, i] = mu[k, i] - r * mu[j, i];
                        }

                        mu[k, j] = mu[k, j] - r;
                        bStar[k] = (double[])B[k].Clone();
                        for (int i = 0; i < k; i++)
                        {
                            mu[k, i] = VectorMath.Dot(B[k], bStar[i]) / VectorMath.NormSquared(bStar[i]);
                            bStar[k] = VectorMath.Subtract(bStar[k], VectorMath.Multiply(bStar[i], mu[k, i]));
                        }
                    }
                }

                if (VectorMath.NormSquared(bStar[k]) >= (delta - mu[k, k - 1] * mu[k, k - 1]) * VectorMath.NormSquared(bStar[k - 1]))
                {
                    k = k + 1;
                }
                else
                {
                    double[] temp = B[k];
                    B[k] = B[k - 1];
                    B[k - 1] = temp;
                    for (int i = k - 1; i <= k; i++)
                    {
                        bStar[i] = (double[])B[i].Clone();
                        for (int j = 0; j < i; j++)
                        {
                            mu[i, j] = VectorMath.Dot(B[i], bStar[j]) / VectorMath.NormSquared(bStar[j]);
                            bStar[i] = VectorMath.Subtract(bStar[i], VectorMath.Multiply(bStar[j], mu[i, j]));
                        }
                    }

                    k = Math.Max(k - 1, 1);
                }
            }

            return B;
        }

        public static double[][] Reduce(double[][] basis)
        {
            return Reduce(basis, 0.75);
        }
    }
}