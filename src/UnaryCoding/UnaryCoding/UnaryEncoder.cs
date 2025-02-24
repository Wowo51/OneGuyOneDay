using System;

namespace UnaryCoding
{
    public static class UnaryEncoder
    {
        public static string Encode(int n)
        {
            if (n < 0)
            {
                return "";
            }

            return new string ('1', n) + "0";
        }

        public static int Decode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return 0;
            }

            int index = code.IndexOf('0');
            if (index == -1 || index != code.Length - 1)
            {
                return 0;
            }

            return index;
        }
    }
}