using System.Collections.Generic;

namespace StoneMethodSIP
{
    public class SparseMatrix
    {
        public int RowCount { get; private set; }
        public int ColumnCount { get; private set; }
        public Dictionary<(int, int), double> Data { get; private set; }

        public SparseMatrix(int rowCount, int columnCount)
        {
            RowCount = rowCount;
            ColumnCount = columnCount;
            Data = new Dictionary<(int, int), double>();
        }

        public double Get(int i, int j)
        {
            double value;
            if (Data.TryGetValue((i, j), out value))
            {
                return value;
            }

            return 0;
        }

        public void Set(int i, int j, double value)
        {
            Data[(i, j)] = value;
        }
    }
}