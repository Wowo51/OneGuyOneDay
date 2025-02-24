using System;
using System.Collections.Generic;

namespace PackageMergeAlgorithm
{
    public static class PackageMergeOptimizer
    {
        // Optimize computes length‐limited (maxLength) Huffman code lengths
        // for an array of symbol frequencies using a package‐merge style adjustment.
        public static int[] Optimize(int[] frequencies, int maxLength)
        {
            if (frequencies == null)
            {
                return new int[0];
            }

            int countSymbols = frequencies.Length;
            if (countSymbols == 0)
            {
                return new int[0];
            }

            // Step 1: Compute unconstrained Huffman code lengths.
            int[] unconstrainedLengths = BuildUnconstrainedHuffmanCodeLengths(frequencies);
            int unconstrainedMax = 0;
            for (int i = 0; i < unconstrainedLengths.Length; i++)
            {
                if (unconstrainedLengths[i] > unconstrainedMax)
                {
                    unconstrainedMax = unconstrainedLengths[i];
                }
            }

            // If already within allowed maximum then return directly.
            if (unconstrainedMax <= maxLength)
            {
                return unconstrainedLengths;
            }

            // Step 2: Build a histogram of code lengths.
            int[] blCount = new int[unconstrainedMax + 1];
            for (int i = 0; i < unconstrainedLengths.Length; i++)
            {
                int len = unconstrainedLengths[i];
                if (len <= 0)
                {
                    len = 1;
                }

                blCount[len]++;
            }

            // Step 3: Adjust lengths longer than maxLength.
            // For each code length above maxLength, reduce its count by moving one unit
            // to the next shorter length. This simple redistribution is inspired by
            // package‐merge ideas to enforce a length limit.
            for (int l = unconstrainedMax; l > maxLength; l--)
            {
                while (blCount[l] > 0)
                {
                    blCount[l]--;
                    blCount[l - 1]++;
                }
            }

            // Step 4: Assign new code lengths to symbols.
            // In an optimal prefix code high frequency symbols get shorter codes.
            // We sort indices descending by frequency so that symbols with higher frequency
            // (which are more important) get assigned the shortest available code lengths.
            List<int> indices = new List<int>();
            for (int i = 0; i < countSymbols; i++)
            {
                indices.Add(i);
            }

            indices.Sort(delegate (int x, int y)
            {
                // Descending order: higher frequency first.
                return frequencies[y].CompareTo(frequencies[x]);
            });
            int[] newCodeLengths = new int[countSymbols];
            int pos = 0;
            for (int l = 1; l <= maxLength; l++)
            {
                int numWithLength = blCount[l];
                for (int j = 0; j < numWithLength; j++)
                {
                    if (pos < countSymbols)
                    {
                        newCodeLengths[indices[pos]] = l;
                        pos++;
                    }
                }
            }

            // For safety assign any symbols not yet set to maxLength.
            for (int i = 0; i < countSymbols; i++)
            {
                if (newCodeLengths[i] == 0)
                {
                    newCodeLengths[i] = maxLength;
                }
            }

            return newCodeLengths;
        }

        // BuildUnconstrainedHuffmanCodeLengths builds a simple Huffman tree
        // and returns an array of code lengths for the input frequencies.
        private static int[] BuildUnconstrainedHuffmanCodeLengths(int[] frequencies)
        {
            int n = frequencies.Length;
            List<Node> nodes = new List<Node>();
            for (int i = 0; i < n; i++)
            {
                Node leaf = new Node();
                leaf.Weight = frequencies[i];
                leaf.Index = i;
                leaf.Left = null;
                leaf.Right = null;
                nodes.Add(leaf);
            }

            // Special case: only one symbol.
            if (nodes.Count == 1)
            {
                int[] singleLength = new int[n];
                singleLength[nodes[0].Index] = 1;
                return singleLength;
            }

            // Iteratively combine two lightest nodes.
            while (nodes.Count > 1)
            {
                nodes.Sort(delegate (Node a, Node b)
                {
                    return a.Weight.CompareTo(b.Weight);
                });
                Node first = nodes[0];
                Node second = nodes[1];
                nodes.RemoveAt(0);
                nodes.RemoveAt(0);
                Node parent = new Node();
                parent.Weight = first.Weight + second.Weight;
                parent.Index = -1; // internal node marker
                parent.Left = first;
                parent.Right = second;
                nodes.Add(parent);
            }

            Node root = nodes[0];
            int[] codeLengths = new int[n];
            AssignCodeLengths(root, 0, codeLengths);
            return codeLengths;
        }

        // Recursively assign code lengths based on depth.
        private static void AssignCodeLengths(Node node, int depth, int[] codeLengths)
        {
            if (node.Left == null && node.Right == null)
            {
                int d = depth;
                if (d == 0)
                {
                    d = 1; // single-node tree
                }

                codeLengths[node.Index] = d;
            }
            else
            {
                if (node.Left != null)
                {
                    AssignCodeLengths(node.Left, depth + 1, codeLengths);
                }

                if (node.Right != null)
                {
                    AssignCodeLengths(node.Right, depth + 1, codeLengths);
                }
            }
        }

        // Basic tree node used for Huffman tree construction.
        private class Node
        {
            public int Weight;
            public int Index;
            public Node? Left;
            public Node? Right;
        }
    }
}