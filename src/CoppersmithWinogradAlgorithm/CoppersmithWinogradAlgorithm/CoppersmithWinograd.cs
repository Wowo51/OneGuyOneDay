namespace CoppersmithWinogradAlgorithm
{
    public static class CoppersmithWinograd
    {
        // Multiply implements a practical variant inspired by the Coppersmith–Winograd algorithm.
        // It first pads the input matrices to an even dimension if needed.
        // Then it precomputes row and column factors and performs the optimized multiplication.
        // Finally, if padding was applied, it removes the extra row and column.
        public static Matrix Multiply(Matrix? A, Matrix? B)
        {
            if (A == null || B == null || A.Size != B.Size)
            {
                return new Matrix(0);
            }

            int originalSize = A.Size;
            Matrix A_padded;
            Matrix B_padded;
            bool padded = false;
            int m = originalSize;
            if (originalSize % 2 != 0)
            {
                padded = true;
                m = originalSize + 1;
                A_padded = PadMatrix(A, m);
                B_padded = PadMatrix(B, m);
            }
            else
            {
                A_padded = A;
                B_padded = B;
            }

            Matrix resultPadded = new Matrix(m);
            double[] rowFactors = new double[m];
            double[] colFactors = new double[m];
            int half = m / 2;
            // Precompute row factors for A_padded
            for (int i = 0; i < m; i++)
            {
                double sum = 0.0;
                for (int k = 0; k < half; k++)
                {
                    int index1 = 2 * k;
                    int index2 = index1 + 1;
                    sum += A_padded.Data[i, index1] * A_padded.Data[i, index2];
                }

                rowFactors[i] = sum;
            }

            // Precompute column factors for B_padded
            for (int j = 0; j < m; j++)
            {
                double sum = 0.0;
                for (int k = 0; k < half; k++)
                {
                    int index1 = 2 * k;
                    int index2 = index1 + 1;
                    sum += B_padded.Data[index1, j] * B_padded.Data[index2, j];
                }

                colFactors[j] = sum;
            }

            // Main multiplication loop using Coppersmith–Winograd style optimizations
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    double prod = -rowFactors[i] - colFactors[j];
                    for (int k = 0; k < half; k++)
                    {
                        int index1 = 2 * k;
                        int index2 = index1 + 1;
                        prod += (A_padded.Data[i, index1] + B_padded.Data[index2, j]) * (A_padded.Data[i, index2] + B_padded.Data[index1, j]);
                    }

                    resultPadded.Data[i, j] = prod;
                }
            }

            if (padded)
            {
                return UnpadMatrix(resultPadded, originalSize);
            }
            else
            {
                return resultPadded;
            }
        }

        // Pads a square matrix to a new size by copying its existing values and leaving extra cells as zero.
        private static Matrix PadMatrix(Matrix matrix, int newSize)
        {
            Matrix paddedMatrix = new Matrix(newSize);
            for (int i = 0; i < matrix.Size; i++)
            {
                for (int j = 0; j < matrix.Size; j++)
                {
                    paddedMatrix.Data[i, j] = matrix.Data[i, j];
                }
            }

            return paddedMatrix;
        }

        // Removes the padding from a matrix, returning only the top-left originalSize x originalSize submatrix.
        private static Matrix UnpadMatrix(Matrix paddedMatrix, int originalSize)
        {
            Matrix result = new Matrix(originalSize);
            for (int i = 0; i < originalSize; i++)
            {
                for (int j = 0; j < originalSize; j++)
                {
                    result.Data[i, j] = paddedMatrix.Data[i, j];
                }
            }

            return result;
        }
    }
}