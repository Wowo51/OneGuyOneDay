using System;

namespace FreivaldsAlgorithm
{
    public static class Freivalds
    {
        public static bool VerifyMultiplication(int[, ] A, int[, ] B, int[, ] C, int iterations)
        {
            int mA = A.GetLength(0);
            int nA = A.GetLength(1);
            int nB = B.GetLength(0);
            int pB = B.GetLength(1);
            int mC = C.GetLength(0);
            int pC = C.GetLength(1);
            if (nA != nB || mA != mC || pB != pC)
            {
                return false;
            }

            Random random = new Random();
            for (int iter = 0; iter < iterations; iter++)
            {
                int[] r = new int[pB];
                for (int i = 0; i < pB; i++)
                {
                    r[i] = random.Next(0, 2);
                }

                int[] Br = MultiplyMatrixVector(B, r);
                int[] ABr = MultiplyMatrixVector(A, Br);
                int[] Cr = MultiplyMatrixVector(C, r);
                for (int i = 0; i < ABr.Length; i++)
                {
                    if (ABr[i] != Cr[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static int[] MultiplyMatrixVector(int[, ] matrix, int[] vector)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int[] result = new int[rows];
            for (int i = 0; i < rows; i++)
            {
                int sum = 0;
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