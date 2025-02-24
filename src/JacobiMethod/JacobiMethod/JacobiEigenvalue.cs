using System;

namespace JacobiMethod
{
    public static class JacobiEigenvalue
    {
        public static double[] ComputeEigenvalues(double[, ] A, double tolerance, int maxIterations)
        {
            if (A == null)
            {
                return new double[0];
            }

            int n = A.GetLength(0);
            if (n != A.GetLength(1))
            {
                return new double[0];
            }

            double[, ] matrix = CloneMatrix(A);
            for (int iteration = 0; iteration < maxIterations; iteration++)
            {
                int p = 0;
                int q = 1;
                double maxOffDiag = 0.0;
                for (int i = 0; i < n; i++)
                {
                    for (int j = i + 1; j < n; j++)
                    {
                        double absVal = Math.Abs(matrix[i, j]);
                        if (absVal > maxOffDiag)
                        {
                            maxOffDiag = absVal;
                            p = i;
                            q = j;
                        }
                    }
                }

                if (maxOffDiag < tolerance)
                {
                    break;
                }

                double a_pp = matrix[p, p];
                double a_qq = matrix[q, q];
                double a_pq = matrix[p, q];
                double theta = 0.5 * Math.Atan2(2 * a_pq, a_pp - a_qq);
                double cos = Math.Cos(theta);
                double sin = Math.Sin(theta);
                double new_a_pp = cos * cos * a_pp - 2 * sin * cos * a_pq + sin * sin * a_qq;
                double new_a_qq = sin * sin * a_pp + 2 * sin * cos * a_pq + cos * cos * a_qq;
                matrix[p, p] = new_a_pp;
                matrix[q, q] = new_a_qq;
                matrix[p, q] = 0;
                matrix[q, p] = 0;
                for (int i = 0; i < n; i++)
                {
                    if (i != p && i != q)
                    {
                        double a_ip = matrix[i, p];
                        double a_iq = matrix[i, q];
                        matrix[i, p] = cos * a_ip - sin * a_iq;
                        matrix[p, i] = matrix[i, p];
                        matrix[i, q] = sin * a_ip + cos * a_iq;
                        matrix[q, i] = matrix[i, q];
                    }
                }
            }

            double[] eigenvalues = new double[n];
            for (int i = 0; i < n; i++)
            {
                eigenvalues[i] = matrix[i, i];
            }

            return eigenvalues;
        }

        private static double[, ] CloneMatrix(double[, ] A)
        {
            int rows = A.GetLength(0);
            int cols = A.GetLength(1);
            double[, ] result = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = A[i, j];
                }
            }

            return result;
        }
    }
}