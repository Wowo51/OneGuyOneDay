using System;
using System.Collections.Generic;
using System.Text;

namespace AdaptiveHuffmanCoding
{
    public class AdaptiveHuffmanNode
    {
        public int Weight;
        public char Symbol;
        public bool IsNYT;
        public AdaptiveHuffmanNode? Left;
        public AdaptiveHuffmanNode? Right;
        public AdaptiveHuffmanNode? Parent;
        public int Order;
        public bool IsLeaf()
        {
            return (this.Left == null && this.Right == null);
        }
    }

    public class AdaptiveHuffmanTree
    {
        private AdaptiveHuffmanNode Root;
        private AdaptiveHuffmanNode NYT;
        private Dictionary<char, AdaptiveHuffmanNode> Nodes;
        private int NextOrder;
        public AdaptiveHuffmanTree()
        {
            this.NextOrder = 512;
            this.NYT = new AdaptiveHuffmanNode();
            this.NYT.IsNYT = true;
            this.NYT.Weight = 0;
            this.NYT.Order = this.NextOrder;
            this.NextOrder--;
            this.Root = this.NYT;
            this.Nodes = new Dictionary<char, AdaptiveHuffmanNode>();
        }

        public bool ContainsSymbol(char symbol)
        {
            return this.Nodes.ContainsKey(symbol);
        }

        public string GetCodeForSymbol(char symbol)
        {
            AdaptiveHuffmanNode node;
            if (this.Nodes.TryGetValue(symbol, out node))
            {
                return this.GetCodeForNode(node);
            }
            else
            {
                return this.GetCodeForNode(this.NYT);
            }
        }

        public string GetCodeForNode(AdaptiveHuffmanNode node)
        {
            List<char> code = new List<char>();
            AdaptiveHuffmanNode current = node;
            while (current.Parent != null)
            {
                if (current.Parent.Left == current)
                {
                    code.Insert(0, '0');
                }
                else
                {
                    code.Insert(0, '1');
                }

                current = current.Parent;
            }

            return new string (code.ToArray());
        }

        public void Update(char symbol)
        {
            AdaptiveHuffmanNode node;
            if (this.Nodes.TryGetValue(symbol, out node))
            {
                this.UpdateTree(node);
            }
            else
            {
                this.Insert(symbol);
            }
        }

        private void Insert(char symbol)
        {
            AdaptiveHuffmanNode oldNYT = this.NYT;
            oldNYT.IsNYT = false;
            oldNYT.Left = new AdaptiveHuffmanNode();
            oldNYT.Left.IsNYT = true;
            oldNYT.Left.Weight = 0;
            oldNYT.Left.Parent = oldNYT;
            oldNYT.Left.Order = this.NextOrder;
            this.NextOrder--;
            oldNYT.Right = new AdaptiveHuffmanNode();
            oldNYT.Right.IsNYT = false;
            oldNYT.Right.Weight = 1;
            oldNYT.Right.Symbol = symbol;
            oldNYT.Right.Parent = oldNYT;
            oldNYT.Right.Order = this.NextOrder;
            this.NextOrder--;
            this.Nodes[symbol] = oldNYT.Right;
            this.NYT = oldNYT.Left;
            oldNYT.Weight = oldNYT.Weight + 1;
            this.UpdateTree(oldNYT.Parent);
        }

        private void UpdateTree(AdaptiveHuffmanNode? node)
        {
            while (node != null)
            {
                AdaptiveHuffmanNode? swapNode = this.FindSwapCandidate(node);
                if (swapNode != null && swapNode != node.Parent)
                {
                    this.SwapNodes(node, swapNode);
                }

                node.Weight = node.Weight + 1;
                node = node.Parent;
            }
        }

        private AdaptiveHuffmanNode? FindSwapCandidate(AdaptiveHuffmanNode node)
        {
            AdaptiveHuffmanNode? candidate = null;
            List<AdaptiveHuffmanNode> allNodes = new List<AdaptiveHuffmanNode>();
            this.CollectNodes(this.Root, allNodes);
            foreach (AdaptiveHuffmanNode n in allNodes)
            {
                if ((n.Weight == node.Weight) && (n.Order > node.Order) && (n != node))
                {
                    if (candidate == null || n.Order > candidate.Order)
                    {
                        candidate = n;
                    }
                }
            }

            return candidate;
        }

        private void CollectNodes(AdaptiveHuffmanNode node, List<AdaptiveHuffmanNode> list)
        {
            if (node == null)
            {
                return;
            }

            list.Add(node);
            if (node.Left != null)
            {
                this.CollectNodes(node.Left, list);
            }

            if (node.Right != null)
            {
                this.CollectNodes(node.Right, list);
            }
        }

        private void SwapNodes(AdaptiveHuffmanNode node1, AdaptiveHuffmanNode node2)
        {
            AdaptiveHuffmanNode? parent1 = node1.Parent;
            AdaptiveHuffmanNode? parent2 = node2.Parent;
            if (parent1 != null)
            {
                if (parent1.Left == node1)
                {
                    parent1.Left = node2;
                }
                else
                {
                    parent1.Right = node2;
                }
            }
            else
            {
                this.Root = node2;
            }

            if (parent2 != null)
            {
                if (parent2.Left == node2)
                {
                    parent2.Left = node1;
                }
                else
                {
                    parent2.Right = node1;
                }
            }
            else
            {
                this.Root = node1;
            }

            AdaptiveHuffmanNode? tempParent = node1.Parent;
            node1.Parent = node2.Parent;
            node2.Parent = tempParent;
            int tempOrder = node1.Order;
            node1.Order = node2.Order;
            node2.Order = tempOrder;
        }

        public AdaptiveHuffmanNode GetRoot()
        {
            return this.Root;
        }
    }
}