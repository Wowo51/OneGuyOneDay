using System;

namespace RidderMethod
{
    public static class RidderMethodSolver
    {
        public static double Solve(Func<double, double> f, double a, double b, double tol, int maxIterations)
        {
            if (f == null)
            {
                return double.NaN;
            }

            double fa = f(a);
            double fb = f(b);
            if (fa * fb >= 0)
            {
                return double.NaN;
            }

            for (int iteration = 0; iteration < maxIterations; iteration++)
            {
                double m = (a + b) / 2.0;
                double fm = f(m);
                double sSquared = fm * fm - fa * fb;
                if (sSquared < 0)
                {
                    sSquared = 0;
                }

                double s = Math.Sqrt(sSquared);
                if (s == 0)
                {
                    return m;
                }

                double signCorr = Math.Sign(fa - fb);
                double d = (m - a) * fm / s;
                double x = m + signCorr * d;
                double fx = f(x);
                if (Math.Abs(fx) < tol)
                {
                    return x;
                }

                if (Math.Sign(fm) != Math.Sign(fx))
                {
                    a = m;
                    fa = fm;
                    b = x;
                    fb = fx;
                }
                else if (Math.Sign(fa) != Math.Sign(fx))
                {
                    b = x;
                    fb = fx;
                }
                else
                {
                    a = x;
                    fa = fx;
                }

                if (Math.Abs(b - a) < tol)
                {
                    return (a + b) / 2.0;
                }
            }

            return (a + b) / 2.0;
        }
    }
}