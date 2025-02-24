using System;

namespace ArnoldiIteration
{
    public class ArnoldiSolver
    {
        // Computes an Arnoldi factorization of the matrix A given an initial vector b and m iterations.
        // Returns an ArnoldiResult containing the orthonormal basis (V) and the upper Hessenberg matrix (H).
        public ArnoldiResult Compute(double[, ] A, double[] b, int m)
        {
            int n = A.GetLength(0);
            if (A.GetLength(1) != n)
            {
                return new ArnoldiResult(null, null);
            }

            double normB = MathUtil.Norm(b);
            if (normB < 1e-12)
            {
                return new ArnoldiResult(null, null);
            }

            // V: n x (m+1) matrix; H: (m+1) x m matrix.
            double[, ] V = new double[n, m + 1];
            double[] v0 = new double[n];
            for (int i = 0; i < n; i++)
            {
                v0[i] = b[i] / normB;
                V[i, 0] = v0[i];
            }

            double[, ] H = new double[m + 1, m];
            int k = 0;
            for (k = 0; k < m; k++)
            {
                double[] v_k = new double[n];
                for (int i = 0; i < n; i++)
                {
                    v_k[i] = V[i, k];
                }

                double[] w = MultiplyMatrixVector(A, v_k);
                for (int j = 0; j <= k; j++)
                {
                    double[] v_j = new double[n];
                    for (int i = 0; i < n; i++)
                    {
                        v_j[i] = V[i, j];
                    }

                    double h_jk = MathUtil.Dot(v_j, w);
                    H[j, k] = h_jk;
                    for (int i = 0; i < n; i++)
                    {
                        w[i] -= h_jk * v_j[i];
                    }
                }

                double normW = MathUtil.Norm(w);
                H[k + 1, k] = normW;
                if (normW > 1e-12)
                {
                    for (int i = 0; i < n; i++)
                    {
                        V[i, k + 1] = w[i] / normW;
                    }
                }
                else
                {
                    break;
                }
            }

            return new ArnoldiResult(V, H);
        }

        private double[] MultiplyMatrixVector(double[, ] matrix, double[] vector)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            double[] result = new double[rows];
            for (int i = 0; i < rows; i++)
            {
                double sum = 0;
                for (int j = 0; j < cols; j++)
                {
                    sum += matrix[i, j] * vector[j];
                }

                result[i] = sum;
            }

            return result;
        }
    }
}