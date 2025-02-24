namespace BensonAlgorithm.Algorithms
{
    public class SimplexSolver
    {
        public static double[]? SolveMinimization(double[] c, double[, ] A, double[] b)
        {
            int m = A.GetLength(0);
            int n = A.GetLength(1);
            int totalVars = n + m;
            double[, ] tableau = new double[m + 1, totalVars + 1];
            int row, col;
            // Set up constraint rows
            for (row = 0; row < m; row++)
            {
                tableau[row, 0] = b[row];
                for (col = 0; col < n; col++)
                {
                    tableau[row, col + 1] = A[row, col];
                }

                for (col = 0; col < m; col++)
                {
                    tableau[row, n + 1 + col] = (row == col) ? 1.0 : 0.0;
                }
            }

            // Objective row: convert minimization to maximization of -c
            tableau[m, 0] = 0.0;
            for (col = 0; col < n; col++)
            {
                tableau[m, col + 1] = -c[col];
            }

            for (col = n; col < totalVars; col++)
            {
                tableau[m, col + 1] = 0.0;
            }

            int[] basic = new int[m];
            for (row = 0; row < m; row++)
            {
                basic[row] = n + row;
            }

            while (true)
            {
                int entering = -1;
                double maxCoeff = 1e-8;
                for (col = 1; col <= totalVars; col++)
                {
                    if (tableau[m, col] > maxCoeff)
                    {
                        maxCoeff = tableau[m, col];
                        entering = col;
                    }
                }

                if (entering == -1)
                {
                    break;
                }

                int leaving = -1;
                double minRatio = double.MaxValue;
                for (row = 0; row < m; row++)
                {
                    if (tableau[row, entering] > 1e-8)
                    {
                        double ratio = tableau[row, 0] / tableau[row, entering];
                        if (ratio < minRatio)
                        {
                            minRatio = ratio;
                            leaving = row;
                        }
                    }
                }

                if (leaving == -1)
                {
                    return null;
                }

                Pivot(tableau, m, totalVars, leaving, entering);
                basic[leaving] = entering - 1;
            }

            double[] solution = new double[n];
            for (row = 0; row < m; row++)
            {
                if (basic[row] < n)
                {
                    solution[basic[row]] = tableau[row, 0];
                }
            }

            for (int i = 0; i < n; i++)
            {
                if (solution[i] < 0)
                {
                    solution[i] = 0;
                }
            }

            return solution;
        }

        private static void Pivot(double[, ] tableau, int numConstraints, int totalVars, int pivotRow, int pivotCol)
        {
            int i, j;
            double pivotElement = tableau[pivotRow, pivotCol];
            for (j = 0; j <= totalVars; j++)
            {
                tableau[pivotRow, j] /= pivotElement;
            }

            for (i = 0; i <= numConstraints; i++)
            {
                if (i != pivotRow)
                {
                    double factor = tableau[i, pivotCol];
                    for (j = 0; j <= totalVars; j++)
                    {
                        tableau[i, j] -= factor * tableau[pivotRow, j];
                    }
                }
            }
        }
    }
}