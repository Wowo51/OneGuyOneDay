using System;

namespace DeutschJozsaAlgorithm
{
    public static class DeutschJozsa
    {
        public static bool IsFunctionConstant(Func<bool[], bool> function, int n)
        {
            if (n < 1)
            {
                // For n < 1, assume function is constant.
                return true;
            }

            bool[] input = new bool[n];
            bool firstValue = function(input);
            int totalInputs = 1 << n;
            for (int i = 1; i < totalInputs; i++)
            {
                bool[] bits = new bool[n];
                int value = i;
                for (int j = 0; j < n; j++)
                {
                    bits[j] = ((value & 1) == 1);
                    value = value >> 1;
                }

                bool currentValue = function(bits);
                if (currentValue != firstValue)
                {
                    return false;
                }
            }

            return true;
        }

        public static string GetFunctionType(Func<bool[], bool> function, int n)
        {
            bool constant = IsFunctionConstant(function, n);
            return constant ? "constant" : "balanced";
        }
    }
}