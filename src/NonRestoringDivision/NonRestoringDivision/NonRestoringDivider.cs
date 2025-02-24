using System;

namespace NonRestoringDivision
{
    public struct DivisionResult
    {
        public int Quotient { get; set; }
        public int Remainder { get; set; }

        public DivisionResult(int quotient, int remainder)
        {
            Quotient = quotient;
            Remainder = remainder;
        }
    }

    public class NonRestoringDivider
    {
        public static DivisionResult Divide(int dividend, int divisor)
        {
            if (divisor == 0)
            {
                // Division by zero is not allowed; return dividend as remainder.
                return new DivisionResult(0, dividend);
            }

            bool quotientNegative = (dividend < 0) ^ (divisor < 0);
            bool remainderNegative = dividend < 0;
            int absDividend = dividend < 0 ? -dividend : dividend;
            int absDivisor = divisor < 0 ? -divisor : divisor;
            int n = absDividend == 0 ? 1 : (int)Math.Floor(Math.Log(absDividend, 2)) + 1;
            int mask = (1 << n) - 1;
            int A = 0;
            int Q = absDividend & mask;
            for (int i = 0; i < n; i++)
            {
                int msb = (Q >> (n - 1)) & 1;
                A = A * 2 + msb;
                Q = (Q * 2) & mask;
                if (A >= 0)
                {
                    A = A - absDivisor;
                }
                else
                {
                    A = A + absDivisor;
                }

                if (A >= 0)
                {
                    Q = Q | 1;
                }
            }

            if (A < 0)
            {
                A = A + absDivisor;
            }

            int quotient = quotientNegative ? -Q : Q;
            int remainder = remainderNegative ? -A : A;
            return new DivisionResult(quotient, remainder);
        }
    }
}