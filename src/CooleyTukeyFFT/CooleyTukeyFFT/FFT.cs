using System;
using System.Numerics;

namespace CooleyTukeyFFT
{
    public static class FFT
    {
        public static Complex[] Compute(Complex[] input)
        {
            if (input == null)
            {
                return new Complex[0];
            }

            int length = input.Length;
            if (length == 0)
            {
                return new Complex[0];
            }

            // If the input length is not a power of two, pad with zeros.
            if ((length & (length - 1)) != 0)
            {
                int m = 1;
                while (m < length)
                {
                    m <<= 1;
                }

                Complex[] paddedInput = new Complex[m];
                for (int i = 0; i < length; i++)
                {
                    paddedInput[i] = input[i];
                }

                for (int i = length; i < m; i++)
                {
                    paddedInput[i] = Complex.Zero;
                }

                input = paddedInput;
                length = m;
            }

            return FFTRecursive(input);
        }

        private static Complex[] FFTRecursive(Complex[] input)
        {
            int n = input.Length;
            if (n == 1)
            {
                return new Complex[]
                {
                    input[0]
                };
            }

            int halfLength = n / 2;
            Complex[] even = new Complex[halfLength];
            Complex[] odd = new Complex[halfLength];
            for (int i = 0; i < halfLength; i++)
            {
                even[i] = input[2 * i];
                odd[i] = input[2 * i + 1];
            }

            Complex[] fftEven = FFTRecursive(even);
            Complex[] fftOdd = FFTRecursive(odd);
            Complex[] result = new Complex[n];
            for (int k = 0; k < halfLength; k++)
            {
                double angle = -2 * Math.PI * k / n;
                Complex twiddle = new Complex(Math.Cos(angle), Math.Sin(angle));
                result[k] = fftEven[k] + twiddle * fftOdd[k];
                result[k + halfLength] = fftEven[k] - twiddle * fftOdd[k];
            }

            return result;
        }
    }
}