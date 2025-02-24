using System;

namespace Adler32
{
    public static class Adler32
    {
        private const uint ModAdler = 65521;
        public static uint Compute(byte[] data)
        {
            if (data == null)
            {
                return 1;
            }

            uint a = 1;
            uint b = 0;
            for (int i = 0; i < data.Length; i++)
            {
                a = (a + data[i]) % ModAdler;
                b = (b + a) % ModAdler;
            }

            return (b << 16) | a;
        }
    }
}