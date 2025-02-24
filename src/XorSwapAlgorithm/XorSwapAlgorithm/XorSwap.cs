using System;

namespace XorSwapAlgorithm
{
    public static class XorSwap
    {
        public static void Swap(ref int a, ref int b)
        {
            if (a == b)
            {
                return;
            }

            a ^= b;
            b ^= a;
            a ^= b;
        }
    }
}