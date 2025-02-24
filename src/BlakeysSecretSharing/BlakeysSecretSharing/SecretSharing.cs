using System;
using System.Collections.Generic;

namespace BlakeysSecretSharing
{
    public static class SecretSharing
    {
        // Generates a list of hyperplane shares such that each share's equation
        // a[0]*x0 + a[1]*x1 + ... + a[d-1]*x(d-1) = b holds for the secret point.
        public static List<Hyperplane> GenerateShares(double[] secret, int shareCount)
        {
            if (secret == null)
            {
                return new List<Hyperplane>();
            }

            int dimension = secret.Length;
            List<Hyperplane> shares = new List<Hyperplane>();
            System.Random random = new System.Random();
            for (int i = 0; i < shareCount; i++)
            {
                double[] coefficients = new double[dimension];
                for (int j = 0; j < dimension; j++)
                {
                    // Generate a random coefficient in the range [1, 11)
                    coefficients[j] = random.NextDouble() * 10.0 + 1.0;
                }

                double constant = DotProduct(coefficients, secret);
                shares.Add(new Hyperplane(coefficients, constant));
            }

            return shares;
        }

        // Reconstructs the secret point from a list of hyperplane shares.
        // If the provided shares are fewer than the required dimension,
        // an array filled with double.NaN is returned.
        public static double[] ReconstructSecret(List<Hyperplane> shares)
        {
            if (shares == null || shares.Count == 0)
            {
                return new double[0];
            }

            int dimension = shares[0].Coefficients.Length;
            if (shares.Count < dimension)
            {
                double[] failure = new double[dimension];
                for (int i = 0; i < dimension; i++)
                {
                    failure[i] = double.NaN;
                }

                return failure;
            }

            // Use the first 'dimension' shares to form a linear system.
            double[, ] matrix = new double[dimension, dimension];
            double[] vector = new double[dimension];
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    matrix[i, j] = shares[i].Coefficients[j];
                }

                vector[i] = shares[i].Constant;
            }

            double[] secret = SolveLinearSystem(matrix, vector);
            return secret;
        }

        // Computes the dot product of two arrays.
        private static double DotProduct(double[] array1, double[] array2)
        {
            double sum = 0.0;
            for (int i = 0; i < array1.Length; i++)
            {
                sum += array1[i] * array2[i];
            }

            return sum;
        }

        // Solves a linear system using Gaussian elimination.
        // Returns an array of solutions or an array filled with double.NaN if a unique solution cannot be determined.
        private static double[] SolveLinearSystem(double[, ] matrix, double[] vector)
        {
            int n = vector.Length;
            double[, ] mat = new double[n, n];
            double[] vec = new double[n];
            for (int i = 0; i < n; i++)
            {
                vec[i] = vector[i];
                for (int j = 0; j < n; j++)
                {
                    mat[i, j] = matrix[i, j];
                }
            }

            for (int pivot = 0; pivot < n; pivot++)
            {
                int maxRow = pivot;
                double maxVal = Math.Abs(mat[pivot, pivot]);
                for (int i = pivot + 1; i < n; i++)
                {
                    double absVal = Math.Abs(mat[i, pivot]);
                    if (absVal > maxVal)
                    {
                        maxVal = absVal;
                        maxRow = i;
                    }
                }

                if (maxVal < 1e-10)
                {
                    double[] failure = new double[n];
                    for (int i = 0; i < n; i++)
                    {
                        failure[i] = double.NaN;
                    }

                    return failure;
                }

                if (maxRow != pivot)
                {
                    for (int j = 0; j < n; j++)
                    {
                        double temp = mat[pivot, j];
                        mat[pivot, j] = mat[maxRow, j];
                        mat[maxRow, j] = temp;
                    }

                    double tempVal = vec[pivot];
                    vec[pivot] = vec[maxRow];
                    vec[maxRow] = tempVal;
                }

                for (int i = pivot + 1; i < n; i++)
                {
                    double factor = mat[i, pivot] / mat[pivot, pivot];
                    for (int j = pivot; j < n; j++)
                    {
                        mat[i, j] -= factor * mat[pivot, j];
                    }

                    vec[i] -= factor * vec[pivot];
                }
            }

            double[] solution = new double[n];
            for (int i = n - 1; i >= 0; i--)
            {
                double sum = vec[i];
                for (int j = i + 1; j < n; j++)
                {
                    sum -= mat[i, j] * solution[j];
                }

                if (Math.Abs(mat[i, i]) < 1e-10)
                {
                    solution[i] = double.NaN;
                }
                else
                {
                    solution[i] = sum / mat[i, i];
                }
            }

            return solution;
        }
    }
}