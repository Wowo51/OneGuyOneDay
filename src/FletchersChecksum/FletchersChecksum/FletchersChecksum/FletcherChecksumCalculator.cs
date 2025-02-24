using System;

namespace FletchersChecksum
{
    public static class FletcherChecksumCalculator
    {
        public static ushort ComputeFletcher16(byte[] input)
        {
            if (input == null || input.Length == 0)
            {
                return 0;
            }

            int sum1 = 0;
            int sum2 = 0;
            for (int i = 0; i < input.Length; i++)
            {
                sum1 = (sum1 + input[i]) % 255;
                sum2 = (sum2 + sum1) % 255;
            }

            return (ushort)((sum2 << 8) | sum1);
        }
    }
}