using System;
using System.Collections.Generic;
using System.Text;

namespace HuffmanCoding
{
    public static class HuffmanCoding
    {
        public static string Compress(string text, HuffmanTree tree)
        {
            if (string.IsNullOrEmpty(text) || tree.Root == null)
            {
                return "";
            }

            Dictionary<char, string> codes = tree.BuildCodes();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (codes.ContainsKey(c))
                {
                    builder.Append(codes[c]);
                }
            }

            return builder.ToString();
        }

        public static string Decompress(string bits, HuffmanTree tree)
        {
            if (string.IsNullOrEmpty(bits) || tree.Root == null)
            {
                return "";
            }

            StringBuilder builder = new StringBuilder();
            HuffmanNode current = tree.Root;
            for (int i = 0; i < bits.Length; i++)
            {
                char bit = bits[i];
                if (bit == '0')
                {
                    if (current.Left != null)
                    {
                        current = current.Left;
                    }
                }
                else if (bit == '1')
                {
                    if (current.Right != null)
                    {
                        current = current.Right;
                    }
                }

                if (current.IsLeaf())
                {
                    if (current.Symbol.HasValue)
                    {
                        builder.Append(current.Symbol.Value);
                    }

                    current = tree.Root;
                }
            }

            return builder.ToString();
        }
    }
}