using System;
using System.Collections.Generic;

namespace HuffmanCoding
{
    public class HuffmanNode
    {
        public char? Symbol { get; set; }
        public int Frequency { get; set; }
        public HuffmanNode? Left { get; set; }
        public HuffmanNode? Right { get; set; }

        public bool IsLeaf()
        {
            return Left == null && Right == null;
        }
    }

    public class HuffmanTree
    {
        public HuffmanNode? Root { get; set; }

        public HuffmanTree(HuffmanNode? root)
        {
            this.Root = root;
        }

        public static HuffmanTree BuildTree(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new HuffmanTree(null);
            }

            Dictionary<char, int> frequency = new Dictionary<char, int>();
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (frequency.ContainsKey(c))
                {
                    frequency[c] = frequency[c] + 1;
                }
                else
                {
                    frequency[c] = 1;
                }
            }

            List<HuffmanNode> nodes = new List<HuffmanNode>();
            foreach (KeyValuePair<char, int> entry in frequency)
            {
                HuffmanNode node = new HuffmanNode();
                node.Symbol = entry.Key;
                node.Frequency = entry.Value;
                node.Left = null;
                node.Right = null;
                nodes.Add(node);
            }

            while (nodes.Count > 1)
            {
                nodes.Sort(delegate (HuffmanNode n1, HuffmanNode n2)
                {
                    return n1.Frequency.CompareTo(n2.Frequency);
                });
                HuffmanNode left = nodes[0];
                HuffmanNode right = nodes[1];
                HuffmanNode newNode = new HuffmanNode();
                newNode.Symbol = null;
                newNode.Frequency = left.Frequency + right.Frequency;
                newNode.Left = left;
                newNode.Right = right;
                nodes.RemoveRange(0, 2);
                nodes.Add(newNode);
            }

            return new HuffmanTree(nodes[0]);
        }

        public Dictionary<char, string> BuildCodes()
        {
            Dictionary<char, string> codes = new Dictionary<char, string>();
            BuildCodesRecursive(this.Root, "", codes);
            return codes;
        }

        private void BuildCodesRecursive(HuffmanNode? node, string prefix, Dictionary<char, string> codes)
        {
            if (node == null)
            {
                return;
            }

            if (node.IsLeaf() && node.Symbol.HasValue)
            {
                codes[node.Symbol.Value] = prefix.Length > 0 ? prefix : "0";
            }
            else
            {
                BuildCodesRecursive(node.Left, prefix + "0", codes);
                BuildCodesRecursive(node.Right, prefix + "1", codes);
            }
        }
    }
}