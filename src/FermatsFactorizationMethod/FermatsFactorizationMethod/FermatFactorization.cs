using System;

namespace FermatsFactorizationMethod
{
    public static class FermatFactorization
    {
        public static (long, long) Factorize(long n)
        {
            if (n == 1)
            {
                return (1, 1);
            }

            long a = (long)Math.Ceiling(Math.Sqrt(n));
            long b2;
            long b;
            while (true)
            {
                b2 = a * a - n;
                b = (long)Math.Sqrt(b2);
                if (b * b == b2)
                {
                    break;
                }

                a++;
            }

            return (a - b, a + b);
        }
    }
}