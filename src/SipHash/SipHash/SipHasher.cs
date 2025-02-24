using System;

namespace SipHash
{
    public static class SipHasher
    {
        public static ulong ComputeHash(byte[] data, ulong key0, ulong key1)
        {
            if (data == null)
            {
                return 0UL;
            }

            int length = data.Length;
            ulong v0 = key0 ^ 0x736f6d6570736575UL;
            ulong v1 = key1 ^ 0x646f72616e646f6dUL;
            ulong v2 = key0 ^ 0x6c7967656e657261UL;
            ulong v3 = key1 ^ 0x7465646279746573UL;
            int i = 0;
            while (i + 8 <= length)
            {
                ulong m = BitConverter.ToUInt64(data, i);
                v3 ^= m;
                SipRound(ref v0, ref v1, ref v2, ref v3);
                SipRound(ref v0, ref v1, ref v2, ref v3);
                v0 ^= m;
                i += 8;
            }

            ulong mFinal = ((ulong)length) << 56;
            int remainder = length - i;
            for (int j = 0; j < remainder; j++)
            {
                mFinal |= ((ulong)data[i + j]) << (8 * j);
            }

            v3 ^= mFinal;
            SipRound(ref v0, ref v1, ref v2, ref v3);
            SipRound(ref v0, ref v1, ref v2, ref v3);
            v0 ^= mFinal;
            v2 ^= 0xff;
            SipRound(ref v0, ref v1, ref v2, ref v3);
            SipRound(ref v0, ref v1, ref v2, ref v3);
            SipRound(ref v0, ref v1, ref v2, ref v3);
            SipRound(ref v0, ref v1, ref v2, ref v3);
            return v0 ^ v1 ^ v2 ^ v3;
        }

        public static ulong ComputeHash(byte[] data, byte[] key)
        {
            if (key == null || key.Length != 16)
            {
                return 0UL;
            }

            ulong key0 = BitConverter.ToUInt64(key, 0);
            ulong key1 = BitConverter.ToUInt64(key, 8);
            return ComputeHash(data, key0, key1);
        }

        private static void SipRound(ref ulong v0, ref ulong v1, ref ulong v2, ref ulong v3)
        {
            v0 += v1;
            v1 = RotateLeft(v1, 13);
            v1 ^= v0;
            v0 = RotateLeft(v0, 32);
            v2 += v3;
            v3 = RotateLeft(v3, 16);
            v3 ^= v2;
            v0 += v3;
            v3 = RotateLeft(v3, 21);
            v3 ^= v0;
            v2 += v1;
            v1 = RotateLeft(v1, 17);
            v1 ^= v2;
            v2 = RotateLeft(v2, 32);
        }

        private static ulong RotateLeft(ulong x, int b)
        {
            return (x << b) | (x >> (64 - b));
        }
    }
}