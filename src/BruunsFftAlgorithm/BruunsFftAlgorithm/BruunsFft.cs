using System;
using System.Numerics;
using System.Collections.Generic;

namespace BruunsFftAlgorithm
{
    public static class BruunsFft
    {
        public static Complex[] Compute(double[] coefficients)
        {
            if (coefficients == null)
            {
                return new Complex[0];
            }

            int n = coefficients.Length;
            int m = 1;
            while (m < n)
            {
                m = m << 1;
            }

            double[] padded = new double[m];
            for (int i = 0; i < n; i++)
            {
                padded[i] = coefficients[i];
            }

            Complex[] result = new Complex[m];
            // Frequency 0: evaluate polynomial at x = 1.
            result[0] = EvaluatePolynomial(padded, Complex.One);
            // Frequency m/2 (if m is even): evaluate polynomial at x = -1.
            if ((m % 2) == 0)
            {
                result[m / 2] = EvaluatePolynomial(padded, new Complex(-1, 0));
            }

            // For frequencies 1 <= j < m/2, use Bruun's algorithm via polynomial division.
            for (int j = 1; j < m / 2; j++)
            {
                double theta = 2.0 * Math.PI * j / m;
                Tuple<double, double> remainder = PolynomialDivisionRemainder(padded, theta);
                double A = remainder.Item1;
                double B = remainder.Item2;
                Complex expTheta = new Complex(Math.Cos(theta), Math.Sin(theta));
                result[j] = new Complex(A, 0) * expTheta + new Complex(B, 0);
                Complex expNegTheta = new Complex(Math.Cos(theta), -Math.Sin(theta));
                result[m - j] = new Complex(A, 0) * expNegTheta + new Complex(B, 0);
            }

            return result;
        }

        // Evaluates the polynomial P(x) = a0 + a1*x + ... + a[n-1]*x^(n-1)
        private static Complex EvaluatePolynomial(double[] coefficients, Complex x)
        {
            Complex result = Complex.Zero;
            Complex multiplier = Complex.One;
            for (int i = 0; i < coefficients.Length; i++)
            {
                result += new Complex(coefficients[i], 0) * multiplier;
                multiplier *= x;
            }

            return result;
        }

        // Divides polynomial P(x) by Q(x) = x^2 - 2*cos(theta)*x + 1 and returns the remainder (A, B)
        // P(x) is given in ascending order; it is reversed to descending order for long division.
        private static Tuple<double, double> PolynomialDivisionRemainder(double[] coefficients, double theta)
        {
            int length = coefficients.Length;
            List<double> poly = new List<double>();
            for (int i = length - 1; i >= 0; i--)
            {
                poly.Add(coefficients[i]);
            }

            double q0 = 1.0;
            double q1 = -2.0 * Math.Cos(theta);
            double q2 = 1.0;
            while (poly.Count > 2)
            {
                double factor = poly[0];
                poly[0] = poly[0] - factor * q0;
                poly[1] = poly[1] - factor * q1;
                poly[2] = poly[2] - factor * q2;
                poly.RemoveAt(0);
            }

            double A = poly[0];
            double B = poly[1];
            return Tuple.Create(A, B);
        }
    }
}