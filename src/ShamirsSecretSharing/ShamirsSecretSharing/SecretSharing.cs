using System;
using System.Collections.Generic;

namespace ShamirsSecretSharing
{
    public static class SecretSharing
    {
        public const long Prime = 2147483647L;
        public static List<Share> SplitSecret(int secret, int threshold, int numShares)
        {
            List<Share> shares = new List<Share>();
            if (threshold > numShares || threshold < 2)
            {
                return shares;
            }

            List<long> coefficients = new List<long>();
            coefficients.Add(secret);
            Random random = new Random();
            for (int i = 1; i < threshold; i++)
            {
                long randomCoefficient = random.Next(0, (int)Prime);
                coefficients.Add(randomCoefficient);
            }

            for (int i = 1; i <= numShares; i++)
            {
                long y = EvaluatePolynomial(coefficients, i);
                shares.Add(new Share(i, (int)y));
            }

            return shares;
        }

        private static long EvaluatePolynomial(List<long> coefficients, long x)
        {
            long result = 0;
            for (int i = coefficients.Count - 1; i >= 0; i--)
            {
                result = (result * x + coefficients[i]) % Prime;
            }

            return result;
        }

        public static int CombineShares(List<Share> shares)
        {
            if (shares.Count == 0)
            {
                return 0;
            }

            long secret = 0;
            int count = shares.Count;
            for (int i = 0; i < count; i++)
            {
                long numerator = 1;
                long denominator = 1;
                for (int j = 0; j < count; j++)
                {
                    if (i != j)
                    {
                        numerator = (numerator * ((Prime - shares[j].X) % Prime)) % Prime;
                        long diff = (shares[i].X - shares[j].X + Prime) % Prime;
                        denominator = (denominator * diff) % Prime;
                    }
                }

                long lagrangeCoefficient = (numerator * ModularInverse(denominator, Prime)) % Prime;
                secret = (secret + (lagrangeCoefficient * shares[i].Y) % Prime) % Prime;
            }

            return (int)secret;
        }

        private static long ModularInverse(long a, long mod)
        {
            long m0 = mod;
            long x0 = 0;
            long x1 = 1;
            if (mod == 1)
            {
                return 0;
            }

            while (a > 1)
            {
                long q = a / mod;
                long t = mod;
                mod = a % mod;
                a = t;
                t = x0;
                x0 = x1 - q * x0;
                x1 = t;
            }

            if (x1 < 0)
            {
                x1 += m0;
            }

            return x1;
        }
    }
}