using System;

namespace StrassenAlgorithm
{
    public class Matrix
    {
        public int Size { get; private set; }
        public double[, ] Data { get; private set; }

        public Matrix(int size)
        {
            if (size <= 0)
            {
                Size = 0;
                Data = new double[0, 0];
            }
            else
            {
                Size = size;
                Data = new double[size, size];
            }
        }

        public Matrix(double[, ] data)
        {
            if (data == null)
            {
                Size = 0;
                Data = new double[0, 0];
            }
            else
            {
                int rows = data.GetLength(0);
                int cols = data.GetLength(1);
                if (rows != cols)
                {
                    Size = 0;
                    Data = new double[0, 0];
                }
                else
                {
                    Size = rows;
                    Data = new double[Size, Size];
                    for (int i = 0; i < Size; i++)
                    {
                        for (int j = 0; j < Size; j++)
                        {
                            Data[i, j] = data[i, j];
                        }
                    }
                }
            }
        }

        public static Matrix? Add(Matrix a, Matrix b)
        {
            if (a == null || b == null || a.Size != b.Size)
            {
                return null;
            }

            int size = a.Size;
            Matrix result = new Matrix(size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result.Data[i, j] = a.Data[i, j] + b.Data[i, j];
                }
            }

            return result;
        }

        public static Matrix? Subtract(Matrix a, Matrix b)
        {
            if (a == null || b == null || a.Size != b.Size)
            {
                return null;
            }

            int size = a.Size;
            Matrix result = new Matrix(size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result.Data[i, j] = a.Data[i, j] - b.Data[i, j];
                }
            }

            return result;
        }

        public Matrix GetSubMatrix(int row, int col, int subSize)
        {
            Matrix sub = new Matrix(subSize);
            for (int i = 0; i < subSize; i++)
            {
                for (int j = 0; j < subSize; j++)
                {
                    sub.Data[i, j] = Data[row + i, col + j];
                }
            }

            return sub;
        }

        public void SetSubMatrix(int row, int col, Matrix subMatrix)
        {
            if (subMatrix == null)
            {
                return;
            }

            int subSize = subMatrix.Size;
            for (int i = 0; i < subSize; i++)
            {
                for (int j = 0; j < subSize; j++)
                {
                    Data[row + i, col + j] = subMatrix.Data[i, j];
                }
            }
        }
    }
}