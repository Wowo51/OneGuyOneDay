using System;
using System.Text;

namespace RunLengthEncoding
{
    public static class RunLengthEncoder
    {
        public static string Encode(string input)
        {
            if (input == null)
            {
                return string.Empty;
            }

            if (input.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder encoded = new StringBuilder();
            char currentChar = input[0];
            int count = 1;
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] == currentChar)
                {
                    count++;
                }
                else
                {
                    encoded.Append(count.ToString());
                    encoded.Append(currentChar);
                    currentChar = input[i];
                    count = 1;
                }
            }

            encoded.Append(count.ToString());
            encoded.Append(currentChar);
            return encoded.ToString();
        }

        public static string Decode(string input)
        {
            if (input == null)
            {
                return string.Empty;
            }

            if (input.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder decoded = new StringBuilder();
            StringBuilder countBuilder = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                char ch = input[i];
                if (char.IsDigit(ch))
                {
                    countBuilder.Append(ch);
                }
                else
                {
                    int count = 1;
                    if (countBuilder.Length > 0)
                    {
                        count = Convert.ToInt32(countBuilder.ToString());
                    }

                    for (int j = 0; j < count; j++)
                    {
                        decoded.Append(ch);
                    }

                    countBuilder.Clear();
                }
            }

            return decoded.ToString();
        }
    }
}