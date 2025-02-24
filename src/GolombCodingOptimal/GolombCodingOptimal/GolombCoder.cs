namespace GolombCodingOptimal
{
    public static class GolombCoder
    {
        public static string EncodeInteger(int number, int m)
        {
            if (m <= 0)
            {
                return "";
            }

            if (m == 1)
            {
                return new string ('1', number) + "0";
            }

            int quotient = number / m;
            int remainder = number % m;
            string unary = new string ('1', quotient) + "0";
            int b = (int)System.Math.Ceiling(System.Math.Log(m, 2));
            int cutoff = (1 << b) - m;
            string remainderBits = "";
            if (b - 1 > 0)
            {
                if (remainder < cutoff)
                {
                    remainderBits = System.Convert.ToString(remainder, 2).PadLeft(b - 1, '0');
                }
                else
                {
                    remainderBits = System.Convert.ToString(remainder + cutoff, 2).PadLeft(b, '0');
                }
            }
            else if (m == 2)
            {
                // For m==2, b equals 1 so we encode remainder in 1 bit.
                remainderBits = System.Convert.ToString(remainder, 2).PadLeft(1, '0');
            }

            return unary + remainderBits;
        }

        public static int DecodeInteger(string code, int m, out int readBits)
        {
            readBits = 0;
            if (code == null)
            {
                return -1;
            }

            int index = 0;
            if (m == 1)
            {
                int count = 0;
                while (index < code.Length && code[index] == '1')
                {
                    count++;
                    index++;
                }

                if (index < code.Length && code[index] == '0')
                {
                    index++;
                }
                else
                {
                    readBits = index;
                    return -1;
                }

                readBits = index;
                return count;
            }

            int quotient = 0;
            while (index < code.Length && code[index] == '1')
            {
                quotient++;
                index++;
            }

            if (index < code.Length && code[index] == '0')
            {
                index++;
            }
            else
            {
                readBits = index;
                return -1;
            }

            int b = (int)System.Math.Ceiling(System.Math.Log(m, 2));
            int cutoff = (1 << b) - m;
            int remainder = 0;
            if (b - 1 > 0)
            {
                if (index + b - 1 > code.Length)
                {
                    readBits = index;
                    return -1;
                }

                string sub = code.Substring(index, b - 1);
                remainder = System.Convert.ToInt32(sub, 2);
                index += b - 1;
            }

            if (remainder >= cutoff)
            {
                if (index >= code.Length)
                {
                    readBits = index;
                    return -1;
                }

                char extraBit = code[index];
                index++;
                remainder = ((remainder << 1) | (extraBit == '1' ? 1 : 0)) - cutoff;
            }

            readBits = index;
            return quotient * m + remainder;
        }
    }
}