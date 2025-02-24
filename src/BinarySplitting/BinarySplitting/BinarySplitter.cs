using System;

namespace BinarySplitting
{
    public static class BinarySplitter
    {
        // Recursively computes the sum of a series defined by a term function.
        // Sums terms from index 'left' (inclusive) to 'right' (exclusive)
        public static double SumSeries(int left, int right, Func<int, double> term)
        {
            if (right <= left)
            {
                return 0.0;
            }

            if (right - left == 1)
            {
                return term(left);
            }

            int mid = left + (right - left) / 2;
            double leftSum = SumSeries(left, mid, term);
            double rightSum = SumSeries(mid, right, term);
            return leftSum + rightSum;
        }
    }
}