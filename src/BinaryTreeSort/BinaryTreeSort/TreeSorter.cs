using System;
using System.Collections.Generic;

namespace BinaryTreeSort
{
    public static class TreeSorter
    {
        public static List<T> Sort<T>(List<T> unsortedList)
            where T : IComparable<T>
        {
            BinaryTree<T> binaryTree = new BinaryTree<T>();
            foreach (T item in unsortedList)
            {
                binaryTree.Insert(item);
            }

            return binaryTree.GetSortedValues();
        }
    }
}