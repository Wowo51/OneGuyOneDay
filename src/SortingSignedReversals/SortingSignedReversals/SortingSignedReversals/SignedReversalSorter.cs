using System;
using System.Collections.Generic;

namespace SortingSignedReversals
{
    public static class SignedReversalSorter
    {
        public static List<string> GreedySort(List<int> permutation)
        {
            List<string> steps = new List<string>();
            List<int> current = new List<int>(permutation);
            for (int index = 0; index < current.Count; index++)
            {
                if (current[index] != index + 1)
                {
                    int position = index;
                    while (position < current.Count && Math.Abs(current[position]) != index + 1)
                    {
                        position++;
                    }

                    if (position < current.Count)
                    {
                        ReverseAndFlip(current, index, position);
                        steps.Add(PrintPermutation(current));
                    }
                }

                if (current[index] == -(index + 1))
                {
                    current[index] = index + 1;
                    steps.Add(PrintPermutation(current));
                }
            }

            return steps;
        }

        private static void ReverseAndFlip(List<int> permutation, int start, int end)
        {
            while (start < end)
            {
                int temp = permutation[start];
                permutation[start] = -permutation[end];
                permutation[end] = -temp;
                start++;
                end--;
            }

            if (start == end)
            {
                permutation[start] = -permutation[start];
            }
        }

        private static string PrintPermutation(List<int> permutation)
        {
            return string.Join(" ", permutation);
        }
    }
}