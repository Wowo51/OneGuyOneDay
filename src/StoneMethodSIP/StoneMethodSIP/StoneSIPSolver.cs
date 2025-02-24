using System;

namespace StoneMethodSIP
{
    public class StoneSIPSolver
    {
        public Vector Solve(SparseMatrix A, Vector b, int maxIterations, double tolerance)
        {
            int n = A.RowCount;
            double[, ] D = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    D[i, j] = A.Get(i, j);
                }
            }

            double[, ] L = new double[n, n];
            double[, ] U = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    L[i, j] = 0;
                    U[i, j] = 0;
                }
            }

            for (int i = 0; i < n; i++)
            {
                L[i, i] = 1;
                for (int j = i; j < n; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < i; k++)
                    {
                        sum += L[i, k] * U[k, j];
                    }

                    U[i, j] = D[i, j] - sum;
                }

                for (int j = i + 1; j < n; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < i; k++)
                    {
                        sum += L[j, k] * U[k, i];
                    }

                    if (U[i, i] != 0)
                    {
                        L[j, i] = (D[j, i] - sum) / U[i, i];
                    }
                    else
                    {
                        L[j, i] = 0.0;
                    }
                }
            }

            Vector x = new Vector(n);
            for (int iter = 0; iter < maxIterations; iter++)
            {
                Vector r = new Vector(n);
                for (int i = 0; i < n; i++)
                {
                    double Ax_i = 0;
                    for (int j = 0; j < n; j++)
                    {
                        Ax_i += D[i, j] * x.Values[j];
                    }

                    r.Values[i] = b.Values[i] - Ax_i;
                }

                if (r.Norm() < tolerance)
                {
                    break;
                }

                Vector y = new Vector(n);
                for (int i = 0; i < n; i++)
                {
                    double sum = 0;
                    for (int j = 0; j < i; j++)
                    {
                        sum += L[i, j] * y.Values[j];
                    }

                    y.Values[i] = r.Values[i] - sum;
                }

                Vector d = new Vector(n);
                for (int i = n - 1; i >= 0; i--)
                {
                    double sum = 0;
                    for (int j = i + 1; j < n; j++)
                    {
                        sum += U[i, j] * d.Values[j];
                    }

                    if (U[i, i] != 0)
                    {
                        d.Values[i] = (y.Values[i] - sum) / U[i, i];
                    }
                    else
                    {
                        d.Values[i] = 0;
                    }
                }

                for (int i = 0; i < n; i++)
                {
                    x.Values[i] += d.Values[i];
                }
            }

            return x;
        }
    }
}