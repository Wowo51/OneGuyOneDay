using System;
using System.Numerics;

namespace SchonhageStrassenAlgorithm
{
    /// <summary>
    /// Provides methods for performing Fast Fourier Transform (FFT) on complex arrays.
    /// </summary>
    public static class FFTUtil
    {
        public static void FFT(Complex[] a, bool invert)
        {
            int n = a.Length;
            for (int i = 1, j = 0; i < n; i++)
            {
                int bit = n >> 1;
                while (j >= bit)
                {
                    j -= bit;
                    bit >>= 1;
                }

                j += bit;
                if (i < j)
                {
                    Complex temp = a[i];
                    a[i] = a[j];
                    a[j] = temp;
                }
            }

            for (int len = 2; len <= n; len <<= 1)
            {
                double angle = 2 * Math.PI / len * (invert ? -1 : 1);
                Complex wlen = new Complex(Math.Cos(angle), Math.Sin(angle));
                for (int i = 0; i < n; i += len)
                {
                    Complex w = Complex.One;
                    for (int j = 0; j < len / 2; j++)
                    {
                        Complex u = a[i + j];
                        Complex v = w * a[i + j + len / 2];
                        a[i + j] = u + v;
                        a[i + j + len / 2] = u - v;
                        w *= wlen;
                    }
                }
            }

            if (invert)
            {
                for (int i = 0; i < n; i++)
                {
                    a[i] /= n;
                }
            }
        }
    }
}