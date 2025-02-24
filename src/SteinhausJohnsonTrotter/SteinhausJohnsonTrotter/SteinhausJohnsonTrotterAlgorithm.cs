using System;
using System.Collections.Generic;

namespace SteinhausJohnsonTrotter
{
    public enum Direction
    {
        Left,
        Right
    }

    public static class SteinhausJohnsonTrotterAlgorithm
    {
        public static IEnumerable<int[]> Generate(int n)
        {
            List<Element> elements = new List<Element>();
            for (int i = 1; i <= n; i++)
            {
                elements.Add(new Element(i, Direction.Left));
            }

            yield return GetPermutation(elements);
            while (true)
            {
                int mobileIndex = -1;
                int mobileValue = -1;
                for (int i = 0; i < elements.Count; i++)
                {
                    if (IsMobile(elements, i) && elements[i].Value > mobileValue)
                    {
                        mobileValue = elements[i].Value;
                        mobileIndex = i;
                    }
                }

                if (mobileIndex == -1)
                {
                    break;
                }

                int swapIndex = mobileIndex + (elements[mobileIndex].Dir == Direction.Left ? -1 : 1);
                Element temp = elements[mobileIndex];
                elements[mobileIndex] = elements[swapIndex];
                elements[swapIndex] = temp;
                mobileIndex = swapIndex;
                for (int i = 0; i < elements.Count; i++)
                {
                    if (elements[i].Value > mobileValue)
                    {
                        elements[i].Dir = (elements[i].Dir == Direction.Left) ? Direction.Right : Direction.Left;
                    }
                }

                yield return GetPermutation(elements);
            }
        }

        private static bool IsMobile(List<Element> elements, int index)
        {
            Element element = elements[index];
            if (element.Dir == Direction.Left)
            {
                if (index == 0)
                {
                    return false;
                }

                if (elements[index - 1].Value < element.Value)
                {
                    return true;
                }
            }
            else
            {
                if (index == elements.Count - 1)
                {
                    return false;
                }

                if (elements[index + 1].Value < element.Value)
                {
                    return true;
                }
            }

            return false;
        }

        private static int[] GetPermutation(List<Element> elements)
        {
            int[] permutation = new int[elements.Count];
            for (int i = 0; i < elements.Count; i++)
            {
                permutation[i] = elements[i].Value;
            }

            return permutation;
        }

        private class Element
        {
            public int Value;
            public Direction Dir;
            public Element(int value, Direction dir)
            {
                Value = value;
                Dir = dir;
            }
        }
    }
}