using System;

namespace CannonsAlgorithm
{
    public static class CannonAlgorithm
    {
        public static Matrix Multiply(Matrix a, Matrix b, int gridSize)
        {
            if (a == null || b == null)
            {
                return new Matrix(0, 0);
            }

            if (a.Columns != b.Rows || a.Rows != a.Columns || b.Rows != b.Columns)
            {
                return new Matrix(0, 0);
            }

            if (a.Rows % gridSize != 0)
            {
                return new Matrix(0, 0);
            }

            ProcessorGrid grid = new ProcessorGrid(a, b, gridSize);
            Matrix result = grid.ExecuteCannonAlgorithm();
            return result;
        }
    }
}