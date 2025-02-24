using System;

namespace RestoringDivisionAlgorithm
{
    public static class RestoringDivider
    {
        public static (uint Quotient, uint Remainder) Divide(uint dividend, uint divisor)
        {
            if (divisor == 0U)
            {
                return (0U, dividend);
            }

            int remainder = 0;
            uint quotient = 0U;
            for (int i = 31; i >= 0; i--)
            {
                remainder = remainder << 1;
                remainder = remainder | (int)((dividend >> i) & 1U);
                remainder = remainder - (int)divisor;
                if (remainder < 0)
                {
                    remainder = remainder + (int)divisor;
                }
                else
                {
                    quotient |= (uint)(1 << i);
                }
            }

            return (quotient, (uint)remainder);
        }
    }
}