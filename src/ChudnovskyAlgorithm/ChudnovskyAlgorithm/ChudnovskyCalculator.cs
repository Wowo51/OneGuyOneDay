using System;
using System.Numerics;

namespace ChudnovskyAlgorithm
{
    public class ChudnovskyCalculator
    {
        // Calculates π to the specified number of digits using the Chudnovsky algorithm.
        public static string CalculatePi(int digits)
        {
            if (digits < 1)
            {
                return "";
            }

            int extra = 10; // extra precision for internal calculation
            int prec = digits + extra;
            BigInteger scale = BigInteger.Pow(10, prec);
            // Compute sqrt(10005) scaled up by 10^prec:
            BigInteger sqrt10005 = Sqrt(10005 * BigInteger.Pow(10, 2 * prec));
            BigInteger C = 426880 * sqrt10005;
            // Each term contributes roughly 14 digits. Calculate number of terms needed.
            int nTerms = (int)Math.Ceiling((double)digits / 14.181647462725477) + 1;
            BigInteger P, Q, T;
            BS(0, nTerms, out P, out Q, out T);
            BigInteger piInt = (C * Q) / T;
            string piStr = piInt.ToString();
            if (piStr.Length <= prec)
            {
                piStr = piStr.PadLeft(prec + 1, '0');
            }

            int pointPosition = piStr.Length - prec;
            string result = piStr.Substring(0, pointPosition) + "." + piStr.Substring(pointPosition);
            return result;
        }

        // Binary splitting implementation for faster series summation.
        private static void BS(int a, int b, out BigInteger P, out BigInteger Q, out BigInteger T)
        {
            if (b - a == 1)
            {
                BigInteger k = a;
                if (k == 0)
                {
                    P = BigInteger.One;
                    Q = BigInteger.One;
                    T = 13591409;
                }
                else
                {
                    P = (6 * k - 5) * (2 * k - 1) * (6 * k - 1);
                    Q = k * k * k * 262537412640768000;
                    T = P * (13591409 + 545140134 * k);
                    if ((k & 1) == 1)
                    {
                        T = -T;
                    }
                }
            }
            else
            {
                int m = (a + b) / 2;
                BigInteger P1, Q1, T1;
                BS(a, m, out P1, out Q1, out T1);
                BigInteger P2, Q2, T2;
                BS(m, b, out P2, out Q2, out T2);
                P = P1 * P2;
                Q = Q1 * Q2;
                T = T1 * Q2 + P1 * T2;
            }
        }

        // Computes the integer square root of n using Newton's method.
        private static BigInteger Sqrt(BigInteger n)
        {
            if (n < 0)
            {
                n = -n;
            }

            if (n == 0)
            {
                return 0;
            }

            BigInteger x = n;
            BigInteger y = (x + n / x) / 2;
            while (y < x)
            {
                x = y;
                y = (x + n / x) / 2;
            }

            return x;
        }
    }
}