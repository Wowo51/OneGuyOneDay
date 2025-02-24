using System;
using System.Numerics;

namespace FastCosineTransform
{
    public static class FFT
    {
        public static Complex[] Compute(Complex[] x)
        {
            if (x == null)
            {
                return new Complex[0];
            }

            int n = x.Length;
            if (IsPowerOfTwo(n))
            {
                return FFTRecursive(x);
            }
            else
            {
                return DFT(x);
            }
        }

        private static bool IsPowerOfTwo(int n)
        {
            return (n > 0) && ((n & (n - 1)) == 0);
        }

        private static int NextPowerOfTwo(int n)
        {
            int power = 1;
            while (power < n)
            {
                power *= 2;
            }

            return power;
        }

        private static Complex[] FFTRecursive(Complex[] x)
        {
            int n = x.Length;
            if (n == 1)
            {
                return new Complex[]
                {
                    x[0]
                };
            }

            int half = n / 2;
            Complex[] even = new Complex[half];
            Complex[] odd = new Complex[half];
            for (int i = 0; i < half; i++)
            {
                even[i] = x[2 * i];
                odd[i] = x[2 * i + 1];
            }

            Complex[] fftEven = FFTRecursive(even);
            Complex[] fftOdd = FFTRecursive(odd);
            Complex[] result = new Complex[n];
            for (int k = 0; k < half; k++)
            {
                double angle = -2.0 * Math.PI * k / n;
                Complex exp = new Complex(Math.Cos(angle), Math.Sin(angle));
                result[k] = fftEven[k] + exp * fftOdd[k];
                result[k + half] = fftEven[k] - exp * fftOdd[k];
            }

            return result;
        }

        private static Complex[] DFT(Complex[] x)
        {
            int n = x.Length;
            Complex[] result = new Complex[n];
            for (int k = 0; k < n; k++)
            {
                Complex sum = Complex.Zero;
                for (int j = 0; j < n; j++)
                {
                    double angle = -2.0 * Math.PI * k * j / n;
                    Complex exp = new Complex(Math.Cos(angle), Math.Sin(angle));
                    sum += x[j] * exp;
                }

                result[k] = sum;
            }

            return result;
        }
    }
}