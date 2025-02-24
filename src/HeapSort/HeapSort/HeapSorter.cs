using System;
using System.Collections.Generic;

namespace HeapSort
{
    public static class HeapSorter
    {
        public static void Sort<T>(List<T> list)
            where T : IComparable<T>
        {
            if (list == null)
            {
                return;
            }

            int count = list.Count;
            for (int i = count / 2 - 1; i >= 0; i--)
            {
                SiftDown(list, i, count);
            }

            for (int end = count - 1; end > 0; end--)
            {
                Swap(list, 0, end);
                SiftDown(list, 0, end);
            }
        }

        private static void SiftDown<T>(List<T> list, int start, int count)
            where T : IComparable<T>
        {
            int root = start;
            while (true)
            {
                int leftChild = 2 * root + 1;
                if (leftChild >= count)
                {
                    break;
                }

                int rightChild = leftChild + 1;
                int swapIndex = root;
                if (list[swapIndex].CompareTo(list[leftChild]) < 0)
                {
                    swapIndex = leftChild;
                }

                if (rightChild < count && list[swapIndex].CompareTo(list[rightChild]) < 0)
                {
                    swapIndex = rightChild;
                }

                if (swapIndex == root)
                {
                    return;
                }

                Swap(list, root, swapIndex);
                root = swapIndex;
            }
        }

        private static void Swap<T>(List<T> list, int indexA, int indexB)
        {
            T temp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = temp;
        }
    }
}