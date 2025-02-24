using System;
using System.Collections.Generic;

namespace C45Algorithm
{
    public class DecisionTreeNode
    {
        public bool IsLeaf { get; set; }
        public string? ClassLabel { get; set; }
        public int SplittingFeatureIndex { get; set; }
        public string? FeatureName { get; set; }
        public bool IsContinuous { get; set; }
        public double? Threshold { get; set; }
        public Dictionary<string, DecisionTreeNode> Children { get; set; }
        public DecisionTreeNode? Left { get; set; }
        public DecisionTreeNode? Right { get; set; }

        public DecisionTreeNode()
        {
            Children = new Dictionary<string, DecisionTreeNode>();
        }
    }
}