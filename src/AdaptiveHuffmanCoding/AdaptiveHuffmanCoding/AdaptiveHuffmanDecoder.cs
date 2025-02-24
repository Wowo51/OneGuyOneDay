using System;
using System.Text;

namespace AdaptiveHuffmanCoding
{
    public class AdaptiveHuffmanDecoder
    {
        private AdaptiveHuffmanTree _tree;
        public AdaptiveHuffmanDecoder()
        {
            this._tree = new AdaptiveHuffmanTree();
        }

        public string Decode(string bits)
        {
            StringBuilder output = new StringBuilder();
            int index = 0;
            while (index < bits.Length)
            {
                AdaptiveHuffmanNode current = this._tree.GetRoot();
                while (!current.IsLeaf() && index < bits.Length)
                {
                    char bit = bits[index];
                    index++;
                    if (bit == '0')
                    {
                        current = current.Left!;
                    }
                    else
                    {
                        current = current.Right!;
                    }
                }

                if (current.IsNYT)
                {
                    if (index + 8 > bits.Length)
                    {
                        break;
                    }

                    string charBits = bits.Substring(index, 8);
                    index += 8;
                    int charValue = Convert.ToInt32(charBits, 2);
                    char symbol = (char)charValue;
                    output.Append(symbol);
                    this._tree.Update(symbol);
                }
                else
                {
                    char symbol = current.Symbol;
                    output.Append(symbol);
                    this._tree.Update(symbol);
                }
            }

            return output.ToString();
        }
    }
}