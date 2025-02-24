using System;

namespace EuclideanAlgorithm
{
    public static class GcdCalculator
    {
        public static int Compute(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }
    }
}