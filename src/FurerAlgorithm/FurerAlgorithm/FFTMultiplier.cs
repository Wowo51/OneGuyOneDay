using System;
using System.Numerics;
using System.Collections.Generic;

namespace FurerAlgorithm
{
    /// <summary>
    /// Implements multiplication of large integers via FFT-based convolution.
    /// The algorithm converts BigIntegers into arrays of digits in base 10000,
    /// performs FFT on the coefficient arrays, multiplies them pointwise,
    /// and applies the inverse FFT to obtain the convolution.
    /// </summary>
    public static class FFTMultiplier
    {
        private const int BaseValue = 10000;
        public static BigInteger Multiply(BigInteger a, BigInteger b)
        {
            int signA = a.Sign;
            int signB = b.Sign;
            int signResult = signA * signB;
            BigInteger absA = BigInteger.Abs(a);
            BigInteger absB = BigInteger.Abs(b);
            int[] aDigits = ToDigits(absA, BaseValue);
            int[] bDigits = ToDigits(absB, BaseValue);
            int[] resultDigits = MultiplyDigits(aDigits, bDigits, BaseValue);
            BigInteger result = FromDigits(resultDigits, BaseValue);
            if (signResult < 0)
            {
                result = -result;
            }

            return result;
        }

        private static int[] ToDigits(BigInteger number, int baseValue)
        {
            if (number.IsZero)
            {
                return new int[]
                {
                    0
                };
            }

            List<int> digits = new List<int>();
            while (number > 0)
            {
                BigInteger remainder;
                number = BigInteger.DivRem(number, baseValue, out remainder);
                digits.Add((int)remainder);
            }

            return digits.ToArray();
        }

        private static BigInteger FromDigits(int[] digits, int baseValue)
        {
            BigInteger result = BigInteger.Zero;
            BigInteger power = BigInteger.One;
            for (int i = 0; i < digits.Length; i++)
            {
                result += digits[i] * power;
                power *= baseValue;
            }

            return result;
        }

        private static int[] MultiplyDigits(int[] a, int[] b, int baseValue)
        {
            int n = 1;
            while (n < a.Length + b.Length)
            {
                n <<= 1;
            }

            Complex[] fa = new Complex[n];
            Complex[] fb = new Complex[n];
            for (int i = 0; i < n; i++)
            {
                fa[i] = new Complex(i < a.Length ? a[i] : 0, 0);
                fb[i] = new Complex(i < b.Length ? b[i] : 0, 0);
            }

            FFT(fa, false);
            FFT(fb, false);
            for (int i = 0; i < n; i++)
            {
                fa[i] *= fb[i];
            }

            FFT(fa, true);
            int[] result = new int[n];
            long carry = 0;
            for (int i = 0; i < n; i++)
            {
                long value = (long)Math.Round(fa[i].Real) + carry;
                result[i] = (int)(value % baseValue);
                carry = value / baseValue;
            }

            List<int> resultList = new List<int>(result);
            while (carry > 0)
            {
                resultList.Add((int)(carry % baseValue));
                carry /= baseValue;
            }

            int lastNonZero = resultList.Count - 1;
            while (lastNonZero > 0 && resultList[lastNonZero] == 0)
            {
                lastNonZero--;
            }

            int[] trimmed = new int[lastNonZero + 1];
            for (int i = 0; i <= lastNonZero; i++)
            {
                trimmed[i] = resultList[i];
            }

            return trimmed;
        }

        private static void FFT(Complex[] a, bool invert)
        {
            int n = a.Length;
            if (n <= 1)
            {
                return;
            }

            Complex[] even = new Complex[n / 2];
            Complex[] odd = new Complex[n / 2];
            for (int i = 0; i < n / 2; i++)
            {
                even[i] = a[2 * i];
                odd[i] = a[2 * i + 1];
            }

            FFT(even, invert);
            FFT(odd, invert);
            for (int k = 0; k < n / 2; k++)
            {
                double angle = 2 * Math.PI * k / n * (invert ? -1 : 1);
                Complex w = new Complex(Math.Cos(angle), Math.Sin(angle));
                a[k] = even[k] + w * odd[k];
                a[k + n / 2] = even[k] - w * odd[k];
                if (invert)
                {
                    a[k] /= 2;
                    a[k + n / 2] /= 2;
                }
            }
        }
    }
}