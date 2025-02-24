using System;
using System.Collections.Generic;

namespace LPBoost
{
    public static class LinearProgramSolver
    {
        // LPBoost solver: computes the optimal margin as the median of the input data.
        // The median minimizes the absolute deviation loss function,
        // representing a simplified LP formulation for boosting.
        public static double Solve(List<double> data)
        {
            if (data == null || data.Count == 0)
            {
                return 0.0;
            }

            List<double> sortedData = new List<double>(data);
            sortedData.Sort();
            int count = sortedData.Count;
            if (count % 2 == 1)
            {
                return sortedData[count / 2];
            }
            else
            {
                return (sortedData[(count / 2) - 1] + sortedData[count / 2]) / 2.0;
            }
        }
    }
}