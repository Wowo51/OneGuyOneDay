using System;

namespace UpgmaAlgorithm
{
    public class TreeNode
    {
        public string? Label { get; set; }
        public double Height { get; set; }
        public TreeNode? Left { get; set; }
        public TreeNode? Right { get; set; }
        public int ClusterSize { get; set; }

        public TreeNode(string? label)
        {
            this.Label = label;
            this.Height = 0.0;
            this.Left = null;
            this.Right = null;
            this.ClusterSize = 1;
        }
    }
}