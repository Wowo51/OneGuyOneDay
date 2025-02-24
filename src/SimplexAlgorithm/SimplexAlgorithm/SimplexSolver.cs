using System;

namespace SimplexAlgorithm
{
    public static class SimplexSolver
    {
        public static double[] Solve(double[, ] A, double[] b, double[] c)
        {
            if (A == null || b == null || c == null)
            {
                return new double[0];
            }

            int numConstraints = A.GetLength(0);
            int numVariables = A.GetLength(1);
            int tableauColumns = numVariables + numConstraints + 1;
            int tableauRows = numConstraints + 1;
            double[, ] tableau = new double[tableauRows, tableauColumns];
            // Build initial tableau with slack variables.
            for (int i = 0; i < numConstraints; i++)
            {
                for (int j = 0; j < numVariables; j++)
                {
                    tableau[i, j] = A[i, j];
                }

                tableau[i, numVariables + i] = 1.0;
                tableau[i, tableauColumns - 1] = b[i];
            }

            // Set up the objective row.
            for (int j = 0; j < numVariables; j++)
            {
                tableau[tableauRows - 1, j] = -c[j];
            }

            // Perform the simplex iterations.
            while (true)
            {
                int pivotCol = -1;
                for (int j = 0; j < tableauColumns - 1; j++)
                {
                    if (tableau[tableauRows - 1, j] < 0)
                    {
                        pivotCol = j;
                        break;
                    }
                }

                if (pivotCol == -1)
                {
                    break; // Optimal solution found.
                }

                double minRatio = double.PositiveInfinity;
                int pivotRow = -1;
                for (int i = 0; i < numConstraints; i++)
                {
                    if (tableau[i, pivotCol] > 0)
                    {
                        double ratio = tableau[i, tableauColumns - 1] / tableau[i, pivotCol];
                        if (ratio < minRatio)
                        {
                            minRatio = ratio;
                            pivotRow = i;
                        }
                    }
                }

                if (pivotRow == -1)
                {
                    // Unbounded solution.
                    return new double[0];
                }

                Pivot(tableau, pivotRow, pivotCol, tableauRows, tableauColumns);
            }

            // Extract the solution.
            double[] solution = new double[numVariables];
            for (int j = 0; j < numVariables; j++)
            {
                int pivotRow = -1;
                int countOnes = 0;
                for (int i = 0; i < numConstraints; i++)
                {
                    if (Math.Abs(tableau[i, j] - 1.0) < 1e-8)
                    {
                        countOnes++;
                        pivotRow = i;
                    }
                    else if (Math.Abs(tableau[i, j]) > 1e-8)
                    {
                        countOnes = 0;
                        break;
                    }
                }

                if (countOnes == 1)
                {
                    solution[j] = tableau[pivotRow, tableauColumns - 1];
                }
                else
                {
                    solution[j] = 0.0;
                }
            }

            return solution;
        }

        private static void Pivot(double[, ] tableau, int pivotRow, int pivotCol, int tableauRows, int tableauColumns)
        {
            double pivotValue = tableau[pivotRow, pivotCol];
            for (int j = 0; j < tableauColumns; j++)
            {
                tableau[pivotRow, j] /= pivotValue;
            }

            for (int i = 0; i < tableauRows; i++)
            {
                if (i != pivotRow)
                {
                    double factor = tableau[i, pivotCol];
                    for (int j = 0; j < tableauColumns; j++)
                    {
                        tableau[i, j] -= factor * tableau[pivotRow, j];
                    }
                }
            }
        }
    }
}