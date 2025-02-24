using System;
using System.Numerics;

namespace DiscreteFourierTransform
{
    public static class DFT
    {
        public static Complex[] Compute(double[] signal)
        {
            if (signal == null)
            {
                return Array.Empty<Complex>();
            }

            int N = signal.Length;
            Complex[] result = new Complex[N];
            double factor = -2 * Math.PI / N;
            for (int k = 0; k < N; k++)
            {
                Complex sum = Complex.Zero;
                for (int n = 0; n < N; n++)
                {
                    double angle = factor * k * n;
                    Complex c = new Complex(Math.Cos(angle), Math.Sin(angle));
                    sum += signal[n] * c;
                }

                result[k] = sum;
            }

            return result;
        }
    }
}