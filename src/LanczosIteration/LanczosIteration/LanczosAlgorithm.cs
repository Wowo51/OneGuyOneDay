using System;

namespace LanczosIteration
{
    public class LanczosAlgorithm
    {
        public static LanczosResult ReduceToTridiagonal(double[, ] symmetricMatrix)
        {
            if (symmetricMatrix == null)
            {
                return new LanczosResult(new double[0], new double[0]);
            }

            int rows = symmetricMatrix.GetLength(0);
            int cols = symmetricMatrix.GetLength(1);
            if (rows != cols)
            {
                return new LanczosResult(new double[0], new double[0]);
            }

            int n = rows;
            double[] alphas = new double[n];
            double[] betas = new double[n - 1];
            double[] qPrevious = new double[n];
            double[] qCurrent = new double[n];
            // Initialize qCurrent: choose unit vector along the first coordinate.
            for (int i = 0; i < n; i++)
            {
                qCurrent[i] = (i == 0) ? 1.0 : 0.0;
            }

            double beta = 0.0;
            for (int j = 0; j < n; j++)
            {
                double[] w = MultiplyMatrixVector(symmetricMatrix, qCurrent);
                if (j > 0)
                {
                    for (int i = 0; i < n; i++)
                    {
                        w[i] = w[i] - beta * qPrevious[i];
                    }
                }

                double alpha = DotProduct(qCurrent, w);
                alphas[j] = alpha;
                for (int i = 0; i < n; i++)
                {
                    w[i] = w[i] - alpha * qCurrent[i];
                }

                beta = Norm(w);
                if (j < n - 1)
                {
                    betas[j] = beta;
                }

                if (beta == 0 || j == n - 1)
                {
                    break;
                }

                qPrevious = CopyVector(qCurrent);
                for (int i = 0; i < n; i++)
                {
                    qCurrent[i] = w[i] / beta;
                }
            }

            return new LanczosResult(alphas, betas);
        }

        private static double[] MultiplyMatrixVector(double[, ] matrix, double[] vector)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            double[] result = new double[rows];
            for (int i = 0; i < rows; i++)
            {
                double sum = 0.0;
                for (int j = 0; j < cols; j++)
                {
                    sum += matrix[i, j] * vector[j];
                }

                result[i] = sum;
            }

            return result;
        }

        private static double DotProduct(double[] vector1, double[] vector2)
        {
            int length = vector1.Length;
            double sum = 0.0;
            for (int i = 0; i < length; i++)
            {
                sum += vector1[i] * vector2[i];
            }

            return sum;
        }

        private static double Norm(double[] vector)
        {
            double sumSquares = 0.0;
            for (int i = 0; i < vector.Length; i++)
            {
                sumSquares += vector[i] * vector[i];
            }

            return Math.Sqrt(sumSquares);
        }

        private static double[] CopyVector(double[] vector)
        {
            int length = vector.Length;
            double[] copy = new double[length];
            for (int i = 0; i < length; i++)
            {
                copy[i] = vector[i];
            }

            return copy;
        }
    }

    public class LanczosResult
    {
        public double[] Alphas { get; }
        public double[] Betas { get; }

        public LanczosResult(double[] alphas, double[] betas)
        {
            this.Alphas = alphas ?? new double[0];
            this.Betas = betas ?? new double[0];
        }

        public double[, ] GetTridiagonalMatrix()
        {
            int n = this.Alphas.Length;
            double[, ] matrix = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                matrix[i, i] = this.Alphas[i];
                if (i < n - 1)
                {
                    matrix[i, i + 1] = this.Betas[i];
                    matrix[i + 1, i] = this.Betas[i];
                }
            }

            return matrix;
        }
    }
}