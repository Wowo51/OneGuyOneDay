using System;
using System.Text;

namespace Lz77AndLz78
{
    public static class LZ77Compressor
    {
        public static string Compress(string input)
        {
            if (input == null)
            {
                return "";
            }

            StringBuilder output = new StringBuilder();
            int inputLength = input.Length;
            int index = 0;
            int windowSize = 20;
            while (index < inputLength)
            {
                int bestLength = 0;
                int bestOffset = 0;
                int start = index - windowSize;
                if (start < 0)
                {
                    start = 0;
                }

                for (int j = start; j < index; j++)
                {
                    int length = 0;
                    while (index + length < inputLength && j + length < index && input[j + length] == input[index + length])
                    {
                        length++;
                    }

                    if (length > bestLength)
                    {
                        bestLength = length;
                        bestOffset = index - j;
                    }
                }

                int nextIndex = index + bestLength;
                int nextCharCode = -1;
                if (nextIndex < inputLength)
                {
                    nextCharCode = (int)input[nextIndex];
                }

                if (output.Length > 0)
                {
                    output.Append(";");
                }

                output.Append(bestOffset);
                output.Append(",");
                output.Append(bestLength);
                output.Append(",");
                if (nextCharCode != -1)
                {
                    output.Append(nextCharCode);
                }

                index = nextIndex + 1;
            }

            return output.ToString();
        }

        public static string Decompress(string compressed)
        {
            if (compressed == null || compressed.Length == 0)
            {
                return "";
            }

            StringBuilder output = new StringBuilder();
            string[] tokens = compressed.Split(';');
            foreach (string token in tokens)
            {
                if (string.IsNullOrEmpty(token))
                {
                    continue;
                }

                string[] parts = token.Split(',');
                if (parts.Length < 3)
                {
                    continue;
                }

                int offset = Convert.ToInt32(parts[0]);
                int length = Convert.ToInt32(parts[1]);
                int nextCharCode = -1;
                if (!string.IsNullOrEmpty(parts[2]))
                {
                    nextCharCode = Convert.ToInt32(parts[2]);
                }

                if (offset > 0 && length > 0)
                {
                    int start = output.Length - offset;
                    if (start < 0)
                    {
                        start = 0;
                    }

                    string sub = output.ToString().Substring(start, length);
                    output.Append(sub);
                }

                if (nextCharCode != -1)
                {
                    output.Append((char)nextCharCode);
                }
            }

            return output.ToString();
        }
    }
}