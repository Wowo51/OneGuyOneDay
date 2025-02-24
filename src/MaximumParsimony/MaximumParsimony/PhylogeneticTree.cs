using System;
using System.Collections.Generic;

namespace MaximumParsimony
{
    public class PhylogeneticTree
    {
        public string? Name { get; set; }
        public string? Sequence { get; set; }
        public PhylogeneticTree? Left { get; set; }
        public PhylogeneticTree? Right { get; set; }

        public PhylogeneticTree()
        {
        }

        public PhylogeneticTree(string name, string sequence)
        {
            Name = name;
            Sequence = sequence;
        }

        public bool IsLeaf()
        {
            return Left == null && Right == null;
        }
    }
}