using System;
using System.Collections.Generic;

namespace SymbolicCholeskyDecomposition
{
    public class SparseMatrix
    {
        public int Size { get; }
        public Dictionary<int, Dictionary<int, double>> Data { get; private set; }

        public SparseMatrix(int size)
        {
            this.Size = size;
            this.Data = new Dictionary<int, Dictionary<int, double>>();
        }

        public void AddValue(int row, int col, double value)
        {
            if (row < 0 || row >= this.Size || col < 0 || col >= this.Size)
            {
                return;
            }

            if (!this.Data.ContainsKey(row))
            {
                this.Data[row] = new Dictionary<int, double>();
            }

            this.Data[row][col] = value;
        }
    }
}