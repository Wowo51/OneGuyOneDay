namespace RayleighQuotientIteration
{
    public static class RayleighQuotientIterationAlgorithm
    {
        public static Eigenpair Compute(double[, ] matrix, double[] initialVector, int maxIterations, double tolerance)
        {
            int n = matrix.GetLength(0);
            double[] v = VectorHelper.Normalize(initialVector);
            double lambda = 0.0;
            for (int it = 0; it < maxIterations; it++)
            {
                lambda = 0.0;
                for (int i = 0; i < n; i++)
                {
                    double sum = 0.0;
                    for (int j = 0; j < n; j++)
                    {
                        sum += matrix[i, j] * v[j];
                    }

                    lambda += v[i] * sum;
                }

                double[, ] M = new double[n, n];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        M[i, j] = matrix[i, j];
                    }

                    M[i, i] -= lambda;
                }

                double[] w = LinearSystemSolver.Solve(M, v);
                double[] vNew = VectorHelper.Normalize(w);
                double diffNorm = 0.0;
                for (int i = 0; i < n; i++)
                {
                    double diff = vNew[i] - v[i];
                    diffNorm += diff * diff;
                }

                diffNorm = System.Math.Sqrt(diffNorm);
                v = vNew;
                if (diffNorm < tolerance)
                {
                    break;
                }
            }

            return new Eigenpair(lambda, v);
        }
    }
}