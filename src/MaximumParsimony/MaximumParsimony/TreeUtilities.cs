using System;
using System.Collections.Generic;

namespace MaximumParsimony
{
    public static class TreeUtilities
    {
        public static PhylogeneticTree CloneTree(PhylogeneticTree tree)
        {
            PhylogeneticTree clone = new PhylogeneticTree();
            clone.Name = tree.Name;
            clone.Sequence = tree.Sequence;
            if (tree.Left != null)
            {
                clone.Left = CloneTree(tree.Left);
            }

            if (tree.Right != null)
            {
                clone.Right = CloneTree(tree.Right);
            }

            return clone;
        }

        public class EdgeInsertionInfo
        {
            public List<bool> Path { get; set; }
            public bool IsLeftChild { get; set; }

            public EdgeInsertionInfo(List<bool> path, bool isLeftChild)
            {
                Path = path;
                IsLeftChild = isLeftChild;
            }
        }

        public static void GetEdges(PhylogeneticTree node, List<bool> currentPath, List<EdgeInsertionInfo> edges)
        {
            if (node.Left != null)
            {
                edges.Add(new EdgeInsertionInfo(new List<bool>(currentPath), true));
                List<bool> leftPath = new List<bool>(currentPath);
                leftPath.Add(true);
                GetEdges(node.Left, leftPath, edges);
            }

            if (node.Right != null)
            {
                edges.Add(new EdgeInsertionInfo(new List<bool>(currentPath), false));
                List<bool> rightPath = new List<bool>(currentPath);
                rightPath.Add(false);
                GetEdges(node.Right, rightPath, edges);
            }
        }

        public static PhylogeneticTree InsertLeafAtPath(PhylogeneticTree tree, List<bool> path, bool isLeftChild, PhylogeneticTree newLeaf)
        {
            PhylogeneticTree clone = CloneTree(tree);
            PhylogeneticTree current = clone;
            foreach (bool direction in path)
            {
                if (direction)
                {
                    current = current.Left!;
                }
                else
                {
                    current = current.Right!;
                }
            }

            if (isLeftChild)
            {
                PhylogeneticTree? oldChild = current.Left;
                PhylogeneticTree newInternal = new PhylogeneticTree();
                newInternal.Left = oldChild;
                newInternal.Right = CloneTree(newLeaf);
                current.Left = newInternal;
            }
            else
            {
                PhylogeneticTree? oldChild = current.Right;
                PhylogeneticTree newInternal = new PhylogeneticTree();
                newInternal.Left = oldChild;
                newInternal.Right = CloneTree(newLeaf);
                current.Right = newInternal;
            }

            return clone;
        }
    }
}