using System;
using System.Numerics;

namespace FastFourierTransform
{
    public static class FFT
    {
        public static Complex[] Compute(Complex[] input)
        {
            if (input == null)
            {
                return new Complex[0];
            }

            int n = input.Length;
            if (n <= 1)
            {
                return input;
            }

            // FFT algorithm requires the input length to be a power of two.
            if ((n & (n - 1)) != 0)
            {
                return new Complex[0];
            }

            int halfLength = n / 2;
            Complex[] even = new Complex[halfLength];
            Complex[] odd = new Complex[halfLength];
            for (int i = 0; i < halfLength; i++)
            {
                even[i] = input[2 * i];
                odd[i] = input[2 * i + 1];
            }

            Complex[] fftEven = Compute(even);
            Complex[] fftOdd = Compute(odd);
            Complex[] result = new Complex[n];
            for (int k = 0; k < halfLength; k++)
            {
                double angle = -2 * Math.PI * k / n;
                Complex w = new Complex(Math.Cos(angle), Math.Sin(angle));
                Complex t = w * fftOdd[k];
                result[k] = fftEven[k] + t;
                result[k + halfLength] = fftEven[k] - t;
            }

            return result;
        }
    }
}