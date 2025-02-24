using System;

namespace HammingWeight
{
    public static class HammingWeightCalculator
    {
        public static int Compute(uint number)
        {
            int count = 0;
            while (number != 0u)
            {
                count++;
                number &= number - 1;
            }

            return count;
        }
    }
}