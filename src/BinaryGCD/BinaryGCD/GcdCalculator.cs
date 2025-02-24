using System;

namespace BinaryGCD
{
    public static class GcdCalculator
    {
        public static int BinaryGcd(int u, int v)
        {
            u = (u < 0) ? -u : u;
            v = (v < 0) ? -v : v;
            if (u == 0)
            {
                return v;
            }

            if (v == 0)
            {
                return u;
            }

            int shift = 0;
            while (((u | v) & 1) == 0)
            {
                u >>= 1;
                v >>= 1;
                shift++;
            }

            while ((u & 1) == 0)
            {
                u >>= 1;
            }

            do
            {
                while ((v & 1) == 0)
                {
                    v >>= 1;
                }

                if (u > v)
                {
                    int temp = u;
                    u = v;
                    v = temp;
                }

                v = v - u;
            }
            while (v != 0);
            return u << shift;
        }
    }
}