using System;

namespace EliasDeltaCoding
{
    public static class EliasCoding
    {
        public static string GammaEncode(int n)
        {
            if (n < 1)
            {
                return "";
            }

            string binary = Convert.ToString(n, 2);
            string prefix = new string ('0', binary.Length - 1);
            return prefix + binary;
        }

        public static int GammaDecode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return -1;
            }

            int index = 0;
            int zeroCount = 0;
            while (index < code.Length && code[index] == '0')
            {
                zeroCount++;
                index++;
            }

            if (index + zeroCount + 1 > code.Length)
            {
                return -1;
            }

            string binary = code.Substring(index, zeroCount + 1);
            int number = Convert.ToInt32(binary, 2);
            index += zeroCount + 1;
            if (index != code.Length)
            {
                return -1;
            }

            return number;
        }

        public static string DeltaEncode(int n)
        {
            if (n < 1)
            {
                return "";
            }

            string binary = Convert.ToString(n, 2);
            int length = binary.Length;
            string gamma = GammaEncode(length);
            return gamma + binary.Substring(1);
        }

        public static int DeltaDecode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return -1;
            }

            int index = 0;
            int zeroCount = 0;
            while (index < code.Length && code[index] == '0')
            {
                zeroCount++;
                index++;
            }

            if (index + zeroCount + 1 > code.Length)
            {
                return -1;
            }

            string gammaPart = code.Substring(index, zeroCount + 1);
            int lengthValue = Convert.ToInt32(gammaPart, 2);
            index += zeroCount + 1;
            if (index + (lengthValue - 1) > code.Length)
            {
                return -1;
            }

            string remainder = code.Substring(index, lengthValue - 1);
            string fullBinary = "1" + remainder;
            int number = Convert.ToInt32(fullBinary, 2);
            index += lengthValue - 1;
            if (index != code.Length)
            {
                return -1;
            }

            return number;
        }

        public static string OmegaEncode(int n)
        {
            if (n < 1)
            {
                return "";
            }

            string code = "0";
            while (n > 1)
            {
                string binary = Convert.ToString(n, 2);
                code = binary + code;
                n = binary.Length - 1;
            }

            return code;
        }

        public static int OmegaDecode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return -1;
            }

            int index = 0;
            int number = 1;
            while (index < code.Length)
            {
                if (code[index] == '0')
                {
                    index++;
                    break;
                }
                else
                {
                    index++;
                    if (index + number > code.Length)
                    {
                        return -1;
                    }

                    string bits = code.Substring(index, number);
                    index += number;
                    string combined = "1" + bits;
                    number = Convert.ToInt32(combined, 2);
                }
            }

            if (index != code.Length)
            {
                return -1;
            }

            return number;
        }
    }
}