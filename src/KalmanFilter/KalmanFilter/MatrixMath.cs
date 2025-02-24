using System;

namespace KalmanFilter
{
    public static class MatrixMath
    {
        public static double[, ] IdentityMatrix(int size)
        {
            double[, ] id = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    id[i, j] = (i == j) ? 1.0 : 0.0;
                }
            }

            return id;
        }

        public static double[, ] MultiplyMatrices(double[, ] A, double[, ] B)
        {
            int aRows = A.GetLength(0), aCols = A.GetLength(1);
            int bCols = B.GetLength(1);
            double[, ] result = new double[aRows, bCols];
            for (int i = 0; i < aRows; i++)
            {
                for (int j = 0; j < bCols; j++)
                {
                    double sum = 0.0;
                    for (int k = 0; k < aCols; k++)
                    {
                        sum += A[i, k] * B[k, j];
                    }

                    result[i, j] = sum;
                }
            }

            return result;
        }

        public static double[] MultiplyMatrixVector(double[, ] m, double[] v)
        {
            int rows = m.GetLength(0), cols = m.GetLength(1);
            double[] result = new double[rows];
            for (int i = 0; i < rows; i++)
            {
                double sum = 0.0;
                for (int j = 0; j < cols; j++)
                {
                    sum += m[i, j] * v[j];
                }

                result[i] = sum;
            }

            return result;
        }

        public static double[, ] AddMatrices(double[, ] A, double[, ] B)
        {
            int rows = A.GetLength(0), cols = A.GetLength(1);
            double[, ] result = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = A[i, j] + B[i, j];
                }
            }

            return result;
        }

        public static double[, ] SubtractMatrices(double[, ] A, double[, ] B)
        {
            int rows = A.GetLength(0), cols = A.GetLength(1);
            double[, ] result = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = A[i, j] - B[i, j];
                }
            }

            return result;
        }

        public static double[] AddVectors(double[] a, double[] b)
        {
            int length = a.Length;
            double[] result = new double[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = a[i] + b[i];
            }

            return result;
        }

        public static double[] SubtractVectors(double[] a, double[] b)
        {
            int length = a.Length;
            double[] result = new double[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = a[i] - b[i];
            }

            return result;
        }

        public static double[, ] Transpose(double[, ] matrix)
        {
            int rows = matrix.GetLength(0), cols = matrix.GetLength(1);
            double[, ] result = new double[cols, rows];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[j, i] = matrix[i, j];
                }
            }

            return result;
        }

        public static double[, ]? InvertMatrix(double[, ] matrix)
        {
            int n = matrix.GetLength(0);
            if (n != matrix.GetLength(1))
            {
                return null;
            }

            double[, ] aug = new double[n, 2 * n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    aug[i, j] = matrix[i, j];
                }

                for (int j = n; j < 2 * n; j++)
                {
                    aug[i, j] = (i == j - n) ? 1.0 : 0.0;
                }
            }

            for (int i = 0; i < n; i++)
            {
                double pivot = aug[i, i];
                if (Math.Abs(pivot) < 1e-10)
                {
                    bool swapped = false;
                    for (int r = i + 1; r < n; r++)
                    {
                        if (Math.Abs(aug[r, i]) > 1e-10)
                        {
                            for (int j = 0; j < 2 * n; j++)
                            {
                                double temp = aug[i, j];
                                aug[i, j] = aug[r, j];
                                aug[r, j] = temp;
                            }

                            pivot = aug[i, i];
                            swapped = true;
                            break;
                        }
                    }

                    if (!swapped)
                    {
                        return null;
                    }
                }

                for (int j = 0; j < 2 * n; j++)
                {
                    aug[i, j] /= pivot;
                }

                for (int r = 0; r < n; r++)
                {
                    if (r != i)
                    {
                        double factor = aug[r, i];
                        for (int j = 0; j < 2 * n; j++)
                        {
                            aug[r, j] -= factor * aug[i, j];
                        }
                    }
                }
            }

            double[, ] inv = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    inv[i, j] = aug[i, j + n];
                }
            }

            return inv;
        }
    }
}