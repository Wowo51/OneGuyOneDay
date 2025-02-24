using System;

namespace SuccessiveOverRelaxation
{
    public class SORSolver
    {
        public double[] Solve(double[, ] A, double[] b, double relaxation, double tolerance, int maxIterations)
        {
            int n = b.Length;
            double[] x = new double[n];
            double[] xOld = new double[n];
            for (int iteration = 0; iteration < maxIterations; iteration++)
            {
                System.Array.Copy(x, xOld, n);
                for (int i = 0; i < n; i++)
                {
                    double sigma = 0.0;
                    for (int j = 0; j < n; j++)
                    {
                        if (j != i)
                        {
                            if (j < i)
                            {
                                sigma += A[i, j] * x[j];
                            }
                            else
                            {
                                sigma += A[i, j] * xOld[j];
                            }
                        }
                    }

                    if (System.Math.Abs(A[i, i]) < 1e-12)
                    {
                        x[i] = xOld[i];
                    }
                    else
                    {
                        x[i] = (1 - relaxation) * xOld[i] + (relaxation / A[i, i]) * (b[i] - sigma);
                    }
                }

                double norm = 0.0;
                for (int i = 0; i < n; i++)
                {
                    double diff = x[i] - xOld[i];
                    norm += diff * diff;
                }

                norm = System.Math.Sqrt(norm);
                if (norm < tolerance)
                {
                    break;
                }
            }

            return x;
        }
    }
}