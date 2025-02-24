using System;
using System.Numerics;

namespace MullersMethod
{
    public static class MullersMethodSolver
    {
        public static Complex FindRoot(Func<Complex, Complex> f, Complex x0, Complex x1, Complex x2, double tolerance = 1e-6, int maxIterations = 100)
        {
            for (int iteration = 0; iteration < maxIterations; iteration++)
            {
                Complex f0 = f(x0);
                Complex f1 = f(x1);
                Complex f2 = f(x2);
                if (Complex.Abs(f2) < tolerance)
                {
                    return x2;
                }

                Complex h0 = x1 - x0;
                Complex h1 = x2 - x1;
                if (h0 == Complex.Zero || h1 == Complex.Zero)
                {
                    return x2;
                }

                Complex delta1 = (f1 - f0) / h0;
                Complex delta2 = (f2 - f1) / h1;
                Complex d = (delta2 - delta1) / (h0 + h1);
                Complex a = d;
                Complex b = a * h1 + delta2;
                Complex c = f2;
                Complex sqrtTerm = Complex.Sqrt(b * b - 4 * a * c);
                Complex denominator = Complex.Abs(b + sqrtTerm) > Complex.Abs(b - sqrtTerm) ? (b + sqrtTerm) : (b - sqrtTerm);
                if (denominator == Complex.Zero)
                {
                    return x2;
                }

                Complex dx = -2 * c / denominator;
                Complex x3 = x2 + dx;
                if (Complex.Abs(dx) < tolerance)
                {
                    return x3;
                }

                x0 = x1;
                x1 = x2;
                x2 = x3;
            }

            return x2;
        }
    }
}