using System;

namespace MuLawAlgorithm
{
    public static class MuLawCompander
    {
        public const int Mu = 255;
        public static double Encode(double value)
        {
            double clampedValue = value;
            if (clampedValue > 1.0)
            {
                clampedValue = 1.0;
            }
            else if (clampedValue < -1.0)
            {
                clampedValue = -1.0;
            }

            double sign = Math.Sign(clampedValue);
            double absValue = Math.Abs(clampedValue);
            double encoded = sign * Math.Log(1 + Mu * absValue) / Math.Log(1 + Mu);
            return encoded;
        }

        public static double Decode(double encodedValue)
        {
            double sign = Math.Sign(encodedValue);
            double absEncoded = Math.Abs(encodedValue);
            double decoded = sign * (Math.Pow(1 + Mu, absEncoded) - 1) / Mu;
            return decoded;
        }
    }
}