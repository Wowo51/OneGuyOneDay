using System;
using System.Collections.Generic;

namespace BlockLoopJoin
{
    public static class BlockNestedLoopJoin
    {
        public static IEnumerable<(TLeft, TRight)> Join<TLeft, TRight, TKey>(IEnumerable<TLeft> left, IEnumerable<TRight> right, Func<TLeft, TKey> leftKeySelector, Func<TRight, TKey> rightKeySelector, int blockSize)
            where TKey : IEquatable<TKey>
        {
            if (left == null)
            {
                left = new List<TLeft>();
            }

            if (right == null)
            {
                right = new List<TRight>();
            }

            if (leftKeySelector == null || rightKeySelector == null)
            {
                yield break;
            }

            List<TLeft> leftList = new List<TLeft>(left);
            int total = leftList.Count;
            if (blockSize < 1)
            {
                blockSize = 1;
            }

            for (int i = 0; i < total; i += blockSize)
            {
                int end = i + blockSize > total ? total : i + blockSize;
                for (int j = i; j < end; j++)
                {
                    TLeft leftItem = leftList[j];
                    TKey leftKey = leftKeySelector(leftItem);
                    foreach (TRight rightItem in right)
                    {
                        TKey rightKey = rightKeySelector(rightItem);
                        if (leftKey != null && leftKey.Equals(rightKey))
                        {
                            yield return (leftItem, rightItem);
                        }
                    }
                }
            }
        }
    }
}