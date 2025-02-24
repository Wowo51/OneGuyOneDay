using System;

namespace CannonsAlgorithm
{
    public class Matrix
    {
        public int Rows { get; }
        public int Columns { get; }
        public double[, ] Data { get; }

        public Matrix(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Data = new double[rows, columns];
        }

        public Matrix GetSubMatrix(int startRow, int startCol, int numRows, int numCols)
        {
            Matrix sub = new Matrix(numRows, numCols);
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    sub.Data[i, j] = Data[startRow + i, startCol + j];
                }
            }

            return sub;
        }

        public void SetSubMatrix(int startRow, int startCol, Matrix sub)
        {
            for (int i = 0; i < sub.Rows; i++)
            {
                for (int j = 0; j < sub.Columns; j++)
                {
                    Data[startRow + i, startCol + j] = sub.Data[i, j];
                }
            }
        }
    }
}