using System;
using System.Text;
using System.Collections.Generic;

namespace Lz77AndLz78
{
    public static class LZ78Compressor
    {
        public static string Compress(string input)
        {
            if (input == null)
            {
                return "";
            }

            List<string> dictionary = new List<string>();
            StringBuilder output = new StringBuilder();
            string current = "";
            for (int i = 0; i < input.Length; i++)
            {
                string candidate = current + input[i];
                if (dictionary.Contains(candidate))
                {
                    current = candidate;
                }
                else
                {
                    int prefixIndex = 0;
                    if (!string.IsNullOrEmpty(current))
                    {
                        prefixIndex = dictionary.IndexOf(current) + 1;
                    }

                    if (output.Length > 0)
                    {
                        output.Append(";");
                    }

                    output.Append(prefixIndex);
                    output.Append(",");
                    output.Append((int)input[i]);
                    dictionary.Add(candidate);
                    current = "";
                }
            }

            if (!string.IsNullOrEmpty(current))
            {
                int prefixIndex = dictionary.IndexOf(current) + 1;
                if (output.Length > 0)
                {
                    output.Append(";");
                }

                output.Append(prefixIndex);
                output.Append(",");
            }

            return output.ToString();
        }

        public static string Decompress(string compressed)
        {
            if (compressed == null || compressed.Length == 0)
            {
                return "";
            }

            List<string> dictionary = new List<string>();
            StringBuilder output = new StringBuilder();
            string[] tokens = compressed.Split(';');
            foreach (string token in tokens)
            {
                if (string.IsNullOrEmpty(token))
                {
                    continue;
                }

                string[] parts = token.Split(',');
                int prefixIndex = Convert.ToInt32(parts[0]);
                int nextCharCode = -1;
                if (parts.Length > 1 && !string.IsNullOrEmpty(parts[1]))
                {
                    nextCharCode = Convert.ToInt32(parts[1]);
                }

                string entry = "";
                if (prefixIndex > 0)
                {
                    entry = dictionary[prefixIndex - 1];
                }

                if (nextCharCode != -1)
                {
                    entry = entry + ((char)nextCharCode).ToString();
                }

                output.Append(entry);
                dictionary.Add(entry);
            }

            return output.ToString();
        }
    }
}