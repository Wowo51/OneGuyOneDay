using System;

namespace SecantMethod
{
    public static class SecantMethodSolver
    {
        public static double Solve(Func<double, double> function, double x0, double x1, double tolerance, int maxIterations)
        {
            if (function == null)
            {
                return double.NaN;
            }

            double xPrev = x0;
            double xCurr = x1;
            for (int iteration = 0; iteration < maxIterations; iteration++)
            {
                double fPrev = function(xPrev);
                double fCurr = function(xCurr);
                double denominator = fCurr - fPrev;
                if (Math.Abs(denominator) < tolerance)
                {
                    return xCurr;
                }

                double xNew = xCurr - fCurr * (xCurr - xPrev) / denominator;
                if (Math.Abs(xNew - xCurr) < tolerance)
                {
                    return xNew;
                }

                xPrev = xCurr;
                xCurr = xNew;
            }

            return xCurr;
        }
    }
}