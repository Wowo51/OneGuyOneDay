using System;

namespace FalsePositionMethod
{
    public static class FalsePositionMethodSolver
    {
        public static double FindRoot(Func<double, double> f, double a, double b, double tolerance, int maxIterations)
        {
            if (f == null)
            {
                return double.NaN;
            }

            double fa = f(a);
            double fb = f(b);
            if (fa * fb > 0)
            {
                return double.NaN;
            }

            double c = a;
            double fc = fa;
            for (int i = 0; i < maxIterations; i++)
            {
                double denominator = fb - fa;
                if (Math.Abs(denominator) < tolerance)
                {
                    break;
                }

                c = (a * fb - b * fa) / denominator;
                fc = f(c);
                if (Math.Abs(fc) < tolerance)
                {
                    return c;
                }

                if (fa * fc < 0)
                {
                    b = c;
                    fb = fc;
                    fa = fa / 2.0;
                }
                else if (fc * fb < 0)
                {
                    a = c;
                    fa = fc;
                    fb = fb / 2.0;
                }
                else
                {
                    break;
                }
            }

            return c;
        }
    }
}