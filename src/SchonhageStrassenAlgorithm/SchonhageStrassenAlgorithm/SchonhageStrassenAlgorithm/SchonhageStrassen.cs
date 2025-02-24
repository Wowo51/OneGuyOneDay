using System;
using System.Numerics;
using System.Collections.Generic;

namespace SchonhageStrassenAlgorithm
{
    /// <summary>
    /// Implements multiplication of very large integers using the Schönhage–Strassen algorithm,
    /// an asymptotically fast algorithm based on the Fast Fourier Transform (FFT). This implementation
    /// applies a complex FFT approach and is intended for numbers that fit within the precision constraints
    /// of double-precision arithmetic.
    /// </summary>
    public static class SchonhageStrassen
    {
        private const int Base = 10000;
        public static BigInteger Multiply(BigInteger a, BigInteger b)
        {
            if (a == BigInteger.Zero || b == BigInteger.Zero)
            {
                return BigInteger.Zero;
            }

            int sign = a.Sign * b.Sign;
            BigInteger absa = BigInteger.Abs(a);
            BigInteger absb = BigInteger.Abs(b);
            int[] aDigits = GetDigits(absa);
            int[] bDigits = GetDigits(absb);
            int n = aDigits.Length;
            int m = bDigits.Length;
            int size = 1;
            while (size < n + m)
            {
                size <<= 1;
            }

            Complex[] fa = new Complex[size];
            Complex[] fb = new Complex[size];
            for (int i = 0; i < size; i++)
            {
                fa[i] = new Complex(i < n ? aDigits[i] : 0, 0);
                fb[i] = new Complex(i < m ? bDigits[i] : 0, 0);
            }

            FFTUtil.FFT(fa, false);
            FFTUtil.FFT(fb, false);
            for (int i = 0; i < size; i++)
            {
                fa[i] *= fb[i];
            }

            FFTUtil.FFT(fa, true);
            long[] resultDigits = new long[size];
            for (int i = 0; i < size; i++)
            {
                resultDigits[i] = (long)Math.Round(fa[i].Real);
            }

            for (int i = 0; i < size; i++)
            {
                if (resultDigits[i] >= Base)
                {
                    long carry = resultDigits[i] / Base;
                    resultDigits[i] %= Base;
                    if (i + 1 < size)
                    {
                        resultDigits[i + 1] += carry;
                    }
                }
            }

            BigInteger result = BigInteger.Zero;
            BigInteger multiplier = BigInteger.One;
            for (int i = 0; i < size; i++)
            {
                result += new BigInteger(resultDigits[i]) * multiplier;
                multiplier *= Base;
            }

            return sign < 0 ? -result : result;
        }

        private static int[] GetDigits(BigInteger number)
        {
            List<int> digits = new List<int>();
            BigInteger current = number;
            while (current > BigInteger.Zero)
            {
                BigInteger remainder;
                current = BigInteger.DivRem(current, Base, out remainder);
                digits.Add((int)remainder);
            }

            return digits.ToArray();
        }
    }
}