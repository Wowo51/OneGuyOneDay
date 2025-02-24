using System;

namespace RadialBasisNetwork
{
    public static class RadialBasisFunction
    {
        public static double Activate(double[] input, double[] center, double beta)
        {
            if (input == null || center == null || input.Length != center.Length)
            {
                return 0.0;
            }

            double distanceSquared = 0.0;
            for (int i = 0; i < input.Length; i++)
            {
                double diff = input[i] - center[i];
                distanceSquared += diff * diff;
            }

            return Math.Exp(-beta * distanceSquared);
        }
    }
}