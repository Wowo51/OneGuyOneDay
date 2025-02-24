using System;

namespace NewtonsMethod
{
    public static class NewtonSolver
    {
        public static double Solve(Func<double, double> f, Func<double, double> fPrime, double initialGuess, double tolerance, int maxIterations)
        {
            if (f == null || fPrime == null)
            {
                return double.NaN;
            }

            double x = initialGuess;
            for (int i = 0; i < maxIterations; i++)
            {
                double fValue = f(x);
                double fPrimeValue = fPrime(x);
                if (Math.Abs(fPrimeValue) < tolerance)
                {
                    return double.NaN;
                }

                double delta = fValue / fPrimeValue;
                x = x - delta;
                if (Math.Abs(delta) < tolerance)
                {
                    return x;
                }
            }

            return x;
        }
    }
}