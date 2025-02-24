using System;
using System.Collections.Generic;

namespace BinaryTreeSort
{
    public class BinaryTree<T>
        where T : IComparable<T>
    {
        public TreeNode<T>? Root { get; set; }

        public BinaryTree()
        {
            Root = null;
        }

        public void Insert(T value)
        {
            if (Root == null)
            {
                Root = new TreeNode<T>(value);
            }
            else
            {
                Root.Insert(value);
            }
        }

        public List<T> GetSortedValues()
        {
            List<T> sortedValues = new List<T>();
            if (Root != null)
            {
                Root.InOrderTraversal(sortedValues);
            }

            return sortedValues;
        }
    }
}