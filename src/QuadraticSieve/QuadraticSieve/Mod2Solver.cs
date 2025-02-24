using System;
using System.Collections.Generic;

namespace QuadraticSieve
{
    public static class Mod2Solver
    {
        public static List<int>? Solve(List<List<int>> matrix)
        {
            int n = matrix.Count;
            if (n == 0)
            {
                return null;
            }

            int m = matrix[0].Count;
            int max = 1 << n;
            for (int subset = 1; subset < max; subset++)
            {
                List<int> sumVector = new List<int>(new int[m]);
                for (int j = 0; j < m; j++)
                {
                    sumVector[j] = 0;
                }

                for (int i = 0; i < n; i++)
                {
                    if (((subset >> i) & 1) == 1)
                    {
                        for (int j = 0; j < m; j++)
                        {
                            sumVector[j] = (sumVector[j] + matrix[i][j]) % 2;
                        }
                    }
                }

                bool allZero = true;
                for (int j = 0; j < m; j++)
                {
                    if (sumVector[j] != 0)
                    {
                        allZero = false;
                        break;
                    }
                }

                if (allZero)
                {
                    List<int> dependency = new List<int>();
                    for (int i = 0; i < n; i++)
                    {
                        dependency.Add(((subset >> i) & 1) == 1 ? 1 : 0);
                    }

                    return dependency;
                }
            }

            return null;
        }
    }
}