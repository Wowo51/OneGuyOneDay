using System;
using System.Text;

namespace AdaptiveHuffmanCoding
{
    public class AdaptiveHuffmanEncoder
    {
        private AdaptiveHuffmanTree _tree;
        public AdaptiveHuffmanEncoder()
        {
            this._tree = new AdaptiveHuffmanTree();
        }

        public string Encode(string input)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                char symbol = input[i];
                string code = this._tree.GetCodeForSymbol(symbol);
                if (!this._tree.ContainsSymbol(symbol))
                {
                    int charValue = (int)symbol;
                    string fixedCode = Convert.ToString(charValue, 2).PadLeft(8, '0');
                    result.Append(code);
                    result.Append(fixedCode);
                }
                else
                {
                    result.Append(code);
                }

                this._tree.Update(symbol);
            }

            return result.ToString();
        }
    }
}