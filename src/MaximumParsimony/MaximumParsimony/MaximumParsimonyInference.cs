using System;
using System.Collections.Generic;

namespace MaximumParsimony
{
    public class MaximumParsimonyInference
    {
        public PhylogeneticTree InferTree(Dictionary<string, string> taxonSequences)
        {
            List<PhylogeneticTree> leaves = new List<PhylogeneticTree>();
            foreach (KeyValuePair<string, string> pair in taxonSequences)
            {
                leaves.Add(new PhylogeneticTree(pair.Key, pair.Value));
            }

            if (leaves.Count == 0)
            {
                return new PhylogeneticTree();
            }

            if (leaves.Count == 1)
            {
                return leaves[0];
            }

            PhylogeneticTree currentTree = new PhylogeneticTree();
            currentTree.Left = TreeUtilities.CloneTree(leaves[0]);
            currentTree.Right = TreeUtilities.CloneTree(leaves[1]);
            MaximumParsimonyCalculator calculator = new MaximumParsimonyCalculator();
            for (int i = 2; i < leaves.Count; i++)
            {
                PhylogeneticTree newLeaf = leaves[i];
                List<PhylogeneticTree> candidateTrees = GetAllInsertionTrees(currentTree, newLeaf);
                int bestScore = Int32.MaxValue;
                PhylogeneticTree bestTree = currentTree;
                foreach (PhylogeneticTree candidate in candidateTrees)
                {
                    int score = calculator.ComputeParsimony(candidate);
                    if (score < bestScore)
                    {
                        bestScore = score;
                        bestTree = candidate;
                    }
                }

                currentTree = bestTree;
            }

            return currentTree;
        }

        private List<PhylogeneticTree> GetAllInsertionTrees(PhylogeneticTree tree, PhylogeneticTree newLeaf)
        {
            List<PhylogeneticTree> candidates = new List<PhylogeneticTree>();
            List<TreeUtilities.EdgeInsertionInfo> edges = new List<TreeUtilities.EdgeInsertionInfo>();
            List<bool> emptyPath = new List<bool>();
            TreeUtilities.GetEdges(tree, emptyPath, edges);
            foreach (TreeUtilities.EdgeInsertionInfo edgeInfo in edges)
            {
                PhylogeneticTree candidate = TreeUtilities.InsertLeafAtPath(tree, edgeInfo.Path, edgeInfo.IsLeftChild, newLeaf);
                candidates.Add(candidate);
            }

            return candidates;
        }
    }
}