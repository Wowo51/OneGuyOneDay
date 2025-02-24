using System;

namespace QrAlgorithmEigenvalues
{
    public class QRAlgorithm
    {
        public class EigenResult
        {
            public double[] Eigenvalues { get; set; } = Array.Empty<double>();
            public double[, ]? Eigenvectors { get; set; }
        }

        public static EigenResult Compute(double[, ] matrix, bool computeEigenvectors = false)
        {
            if (matrix == null)
            {
                return new EigenResult
                {
                    Eigenvalues = new double[0],
                    Eigenvectors = null
                };
            }

            int n = matrix.GetLength(0);
            if (n != matrix.GetLength(1))
            {
                return new EigenResult
                {
                    Eigenvalues = new double[0],
                    Eigenvectors = null
                };
            }

            double tolerance = 1e-10;
            int maxIterations = 1000;
            double[, ] A = Copy(matrix);
            double[, ]? V = computeEigenvectors ? Identity(n) : null;
            for (int iter = 0; iter < maxIterations; iter++)
            {
                double[, ] Q;
                double[, ] R;
                QRDecomposition(A, out Q, out R);
                double[, ] newA = Multiply(R, Q);
                if (computeEigenvectors && V != null)
                {
                    V = Multiply(V, Q);
                }

                double off = 0.0;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (i != j)
                        {
                            off += Math.Abs(newA[i, j]);
                        }
                    }
                }

                A = newA;
                if (off < tolerance)
                {
                    break;
                }
            }

            double[] eigenvalues = new double[n];
            for (int i = 0; i < n; i++)
            {
                eigenvalues[i] = A[i, i];
            }

            return new EigenResult
            {
                Eigenvalues = eigenvalues,
                Eigenvectors = computeEigenvectors ? V : null
            };
        }

        private static void QRDecomposition(double[, ] A, out double[, ] Q, out double[, ] R)
        {
            int n = A.GetLength(0);
            Q = new double[n, n];
            R = new double[n, n];
            for (int j = 0; j < n; j++)
            {
                double[] v = new double[n];
                for (int i = 0; i < n; i++)
                {
                    v[i] = A[i, j];
                }

                for (int k = 0; k < j; k++)
                {
                    double sum = 0.0;
                    for (int i = 0; i < n; i++)
                    {
                        sum += Q[i, k] * A[i, j];
                    }

                    R[k, j] = sum;
                    for (int i = 0; i < n; i++)
                    {
                        v[i] -= sum * Q[i, k];
                    }
                }

                double norm = Norm(v);
                R[j, j] = norm;
                if (norm > 1e-12)
                {
                    for (int i = 0; i < n; i++)
                    {
                        Q[i, j] = v[i] / norm;
                    }
                }
                else
                {
                    for (int i = 0; i < n; i++)
                    {
                        Q[i, j] = 0.0;
                    }
                }
            }
        }

        private static double Norm(double[] vector)
        {
            double sum = 0.0;
            for (int i = 0; i < vector.Length; i++)
            {
                sum += vector[i] * vector[i];
            }

            return Math.Sqrt(sum);
        }

        private static double[, ] Multiply(double[, ] A, double[, ] B)
        {
            int m = A.GetLength(0);
            int n = A.GetLength(1);
            int p = B.GetLength(1);
            double[, ] C = new double[m, p];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < p; j++)
                {
                    double sum = 0.0;
                    for (int k = 0; k < n; k++)
                    {
                        sum += A[i, k] * B[k, j];
                    }

                    C[i, j] = sum;
                }
            }

            return C;
        }

        private static double[, ] Identity(int n)
        {
            double[, ] I = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                I[i, i] = 1.0;
            }

            return I;
        }

        private static double[, ] Copy(double[, ] M)
        {
            int rows = M.GetLength(0);
            int cols = M.GetLength(1);
            double[, ] copy = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    copy[i, j] = M[i, j];
                }
            }

            return copy;
        }
    }
}