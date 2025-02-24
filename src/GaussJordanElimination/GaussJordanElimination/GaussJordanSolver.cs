using System;

namespace GaussJordanElimination
{
    public class GaussJordanSolver
    {
        public static double[, ] ReduceToRREF(double[, ] matrix)
        {
            if (matrix == null)
            {
                return new double[0, 0];
            }

            int rowCount = matrix.GetLength(0);
            int colCount = matrix.GetLength(1);
            double[, ] result = new double[rowCount, colCount];
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    result[i, j] = matrix[i, j];
                }
            }

            double epsilon = 1e-10;
            int lead = 0;
            for (int r = 0; r < rowCount; r++)
            {
                if (lead >= colCount)
                {
                    break;
                }

                int i2 = r;
                while (Math.Abs(result[i2, lead]) < epsilon)
                {
                    i2++;
                    if (i2 == rowCount)
                    {
                        i2 = r;
                        lead++;
                        if (lead >= colCount)
                        {
                            return result;
                        }
                    }
                }

                SwapRows(result, r, i2);
                double lv = result[r, lead];
                for (int j = 0; j < colCount; j++)
                {
                    result[r, j] /= lv;
                }

                for (int i3 = 0; i3 < rowCount; i3++)
                {
                    if (i3 != r)
                    {
                        double lv2 = result[i3, lead];
                        for (int j = 0; j < colCount; j++)
                        {
                            result[i3, j] -= lv2 * result[r, j];
                        }
                    }
                }

                lead++;
            }

            return result;
        }

        private static void SwapRows(double[, ] matrix, int row1, int row2)
        {
            if (row1 == row2)
            {
                return;
            }

            int colCount = matrix.GetLength(1);
            for (int j = 0; j < colCount; j++)
            {
                double temp = matrix[row1, j];
                matrix[row1, j] = matrix[row2, j];
                matrix[row2, j] = temp;
            }
        }
    }
}