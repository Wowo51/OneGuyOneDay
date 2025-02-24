using System;

namespace TernarySearch
{
    public static class TernarySearcher
    {
        public static double FindMaximum(Func<double, double> function, double left, double right, double tolerance = 1e-7)
        {
            if (function == null)
            {
                return left;
            }

            while (right - left > tolerance)
            {
                double m1 = left + (right - left) / 3.0;
                double m2 = right - (right - left) / 3.0;
                double f1 = function(m1);
                double f2 = function(m2);
                if (f1 < f2)
                {
                    left = m1;
                }
                else
                {
                    right = m2;
                }
            }

            return (left + right) / 2.0;
        }

        public static double FindMinimum(Func<double, double> function, double left, double right, double tolerance = 1e-7)
        {
            if (function == null)
            {
                return left;
            }

            while (right - left > tolerance)
            {
                double m1 = left + (right - left) / 3.0;
                double m2 = right - (right - left) / 3.0;
                double f1 = function(m1);
                double f2 = function(m2);
                if (f1 > f2)
                {
                    left = m1;
                }
                else
                {
                    right = m2;
                }
            }

            return (left + right) / 2.0;
        }
    }
}