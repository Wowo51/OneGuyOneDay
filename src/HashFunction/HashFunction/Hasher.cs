using System;

namespace HashFunction
{
    public static class Hasher
    {
        public static int ComputeHash(string input)
        {
            if (input == null)
            {
                return 0;
            }

            int hash = 17;
            foreach (char character in input)
            {
                hash = hash * 31 + character;
            }

            return hash;
        }

        public static int ComputeHash(byte[] data)
        {
            if (data == null)
            {
                return 0;
            }

            int hash = 17;
            foreach (byte b in data)
            {
                hash = hash * 31 + b;
            }

            return hash;
        }
    }
}