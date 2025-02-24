using System;

namespace BisectionMethod
{
    public static class BisectionSolver
    {
        public static double FindRoot(Func<double, double> f, double a, double b, double tolerance, int maxIterations = 1000)
        {
            double fa = f(a);
            double fb = f(b);
            if ((fa * fb) >= 0)
            {
                return double.NaN;
            }

            double mid = 0.0;
            for (int i = 0; i < maxIterations; i++)
            {
                mid = (a + b) / 2.0;
                double fmid = f(mid);
                if (Math.Abs(fmid) < tolerance)
                {
                    return mid;
                }

                if ((fa * fmid) < 0)
                {
                    b = mid;
                    fb = fmid;
                }
                else
                {
                    a = mid;
                    fa = fmid;
                }
            }

            return mid;
        }
    }
}