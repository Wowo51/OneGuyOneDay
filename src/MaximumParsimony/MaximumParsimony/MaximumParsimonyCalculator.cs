using System;
using System.Collections.Generic;
using System.Linq;

namespace MaximumParsimony
{
    public class MaximumParsimonyCalculator
    {
        public int ComputeParsimony(PhylogeneticTree tree)
        {
            string sampleSequence = GetSampleSequence(tree);
            int length = sampleSequence.Length;
            int totalCost = 0;
            for (int i = 0; i < length; i++)
            {
                Tuple<HashSet<char>, int> result = Fitch(tree, i);
                totalCost += result.Item2;
            }

            return totalCost;
        }

        private string GetSampleSequence(PhylogeneticTree tree)
        {
            if (tree.IsLeaf() && !string.IsNullOrEmpty(tree.Sequence))
            {
                return tree.Sequence;
            }

            if (tree.Left != null)
            {
                return GetSampleSequence(tree.Left);
            }

            if (tree.Right != null)
            {
                return GetSampleSequence(tree.Right);
            }

            return "";
        }

        private Tuple<HashSet<char>, int> Fitch(PhylogeneticTree node, int position)
        {
            if (node.IsLeaf())
            {
                char c = (!string.IsNullOrEmpty(node.Sequence) && node.Sequence.Length > position) ? node.Sequence[position] : ' ';
                HashSet<char> set = new HashSet<char>();
                set.Add(c);
                return Tuple.Create(set, 0);
            }

            Tuple<HashSet<char>, int> leftResult = Fitch(node.Left!, position);
            Tuple<HashSet<char>, int> rightResult = Fitch(node.Right!, position);
            HashSet<char> intersection = new HashSet<char>(leftResult.Item1);
            intersection.IntersectWith(rightResult.Item1);
            int cost = leftResult.Item2 + rightResult.Item2;
            HashSet<char> currentSet;
            if (intersection.Count > 0)
            {
                currentSet = intersection;
            }
            else
            {
                currentSet = new HashSet<char>(leftResult.Item1);
                currentSet.UnionWith(rightResult.Item1);
                cost = cost + 1;
            }

            return Tuple.Create(currentSet, cost);
        }
    }
}