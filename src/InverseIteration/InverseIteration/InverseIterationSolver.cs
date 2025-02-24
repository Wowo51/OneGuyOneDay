using System;

namespace InverseIteration
{
    public static class InverseIterationSolver
    {
        public static double[] ComputeEigenvector(double[, ] A, double eigenvalue, double[] initialVector, int maxIterations, double tolerance)
        {
            if (A == null || initialVector == null)
            {
                return new double[0];
            }

            int rows = A.GetLength(0);
            int cols = A.GetLength(1);
            if (rows != cols || rows != initialVector.Length)
            {
                return new double[0];
            }

            double[] v = MatrixHelper.Normalize(initialVector);
            for (int i = 0; i < maxIterations; i++)
            {
                double[, ] B = MatrixHelper.CopyMatrix(A);
                MatrixHelper.SubtractFromDiagonal(B, eigenvalue);
                double[] x = MatrixHelper.Solve(B, v);
                if (x.Length == 0)
                {
                    return v;
                }

                double[] next = MatrixHelper.Normalize(x);
                double diff = MatrixHelper.Distance(next, v);
                if (diff < tolerance)
                {
                    return next;
                }

                v = next;
            }

            return v;
        }
    }
}