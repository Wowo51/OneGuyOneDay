using System;

namespace LongitudinalRedundancyCheck
{
    public static class LrcCalculator
    {
        public static byte ComputeLRC(byte[]? data)
        {
            if (data == null)
            {
                return 0;
            }

            int sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                sum = (sum + data[i]) & 0xFF;
            }

            return (byte)((256 - sum) & 0xFF);
        }
    }
}