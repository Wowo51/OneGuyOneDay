using System;

namespace Rc4Cipher
{
    public static class RC4Cipher
    {
        public static byte[] Encrypt(byte[] data, byte[] key)
        {
            if (data == null)
            {
                return new byte[0];
            }

            if (key == null || key.Length == 0)
            {
                return new byte[0];
            }

            byte[] S = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                S[i] = (byte)i;
            }

            int j = 0;
            for (int i = 0; i < 256; i++)
            {
                j = (j + S[i] + key[i % key.Length]) & 0xff;
                byte temp = S[i];
                S[i] = S[j];
                S[j] = temp;
            }

            byte[] output = new byte[data.Length];
            int iIndex = 0;
            j = 0;
            for (int k = 0; k < data.Length; k++)
            {
                iIndex = (iIndex + 1) & 0xff;
                j = (j + S[iIndex]) & 0xff;
                byte temp = S[iIndex];
                S[iIndex] = S[j];
                S[j] = temp;
                int t = (S[iIndex] + S[j]) & 0xff;
                output[k] = (byte)(data[k] ^ S[t]);
            }

            return output;
        }

        public static byte[] Decrypt(byte[] data, byte[] key)
        {
            return Encrypt(data, key);
        }
    }
}