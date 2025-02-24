using System;
using System.Collections.Generic;

namespace BinaryTreeSort
{
    public class TreeNode<T>
        where T : IComparable<T>
    {
        public T Value { get; set; }
        public TreeNode<T>? Left { get; set; }
        public TreeNode<T>? Right { get; set; }

        public TreeNode(T value)
        {
            Value = value;
            Left = null;
            Right = null;
        }

        public void Insert(T newValue)
        {
            if (newValue.CompareTo(Value) < 0)
            {
                if (Left == null)
                {
                    Left = new TreeNode<T>(newValue);
                }
                else
                {
                    Left.Insert(newValue);
                }
            }
            else
            {
                if (Right == null)
                {
                    Right = new TreeNode<T>(newValue);
                }
                else
                {
                    Right.Insert(newValue);
                }
            }
        }

        public void InOrderTraversal(List<T> sortedValues)
        {
            if (Left != null)
            {
                Left.InOrderTraversal(sortedValues);
            }

            sortedValues.Add(Value);
            if (Right != null)
            {
                Right.InOrderTraversal(sortedValues);
            }
        }
    }
}