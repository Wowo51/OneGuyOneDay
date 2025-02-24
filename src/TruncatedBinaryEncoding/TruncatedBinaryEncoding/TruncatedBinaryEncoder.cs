using System;
using System.Text;

namespace TruncatedBinaryEncoding
{
    public static class TruncatedBinaryEncoder
    {
        public static string Encode(int value, int n)
        {
            if (n <= 0)
            {
                return "";
            }

            if (n == 1)
            {
                return "";
            }

            if (value < 0 || value >= n)
            {
                return "";
            }

            int L = (int)Math.Floor(Math.Log(n, 2));
            int threshold = (1 << (L + 1)) - n;
            if (value < threshold)
            {
                return ToBinaryString(value, L);
            }
            else
            {
                return ToBinaryString(value + threshold, L + 1);
            }
        }

        public static int Decode(string bits, int n, out int bitsUsed)
        {
            bitsUsed = 0;
            if (bits == null)
            {
                return 0;
            }

            if (n <= 0)
            {
                return 0;
            }

            if (n == 1)
            {
                bitsUsed = 0;
                return 0;
            }

            int L = (int)Math.Floor(Math.Log(n, 2));
            int threshold = (1 << (L + 1)) - n;
            if (bits.Length < L)
            {
                return 0;
            }

            int v = ParseBinaryString(bits, 0, L);
            if (v < threshold)
            {
                bitsUsed = L;
                return v;
            }
            else
            {
                if (bits.Length < L + 1)
                {
                    return 0;
                }

                int fullValue = (v << 1) | ((bits[L] == '1') ? 1 : 0);
                bitsUsed = L + 1;
                return fullValue - threshold;
            }
        }

        private static string ToBinaryString(int value, int length)
        {
            StringBuilder builder = new StringBuilder(length);
            for (int i = length - 1; i >= 0; i--)
            {
                if ((value & (1 << i)) != 0)
                {
                    builder.Append("1");
                }
                else
                {
                    builder.Append("0");
                }
            }

            return builder.ToString();
        }

        private static int ParseBinaryString(string bits, int offset, int length)
        {
            int value = 0;
            for (int i = 0; i < length; i++)
            {
                value = value << 1;
                char c = bits[offset + i];
                if (c == '1')
                {
                    value |= 1;
                }
            }

            return value;
        }
    }
}