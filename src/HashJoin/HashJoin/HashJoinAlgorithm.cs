using System;
using System.Collections.Generic;

namespace HashJoin
{
    public static class HashJoinAlgorithm
    {
        public static IEnumerable<TResult> HashJoinMethod<TLeft, TRight, TKey, TResult>(IEnumerable<TLeft> left, IEnumerable<TRight> right, Func<TLeft, TKey> leftKeySelector, Func<TRight, TKey> rightKeySelector, Func<TLeft, TRight, TResult> resultSelector)
            where TKey : notnull
        {
            if (left == null || right == null || leftKeySelector == null || rightKeySelector == null || resultSelector == null)
            {
                yield break;
            }

            Dictionary<TKey, List<TLeft>> leftLookup = new Dictionary<TKey, List<TLeft>>();
            foreach (TLeft leftItem in left)
            {
                TKey key = leftKeySelector(leftItem)!;
                if (!leftLookup.TryGetValue(key, out List<TLeft> list))
                {
                    list = new List<TLeft>();
                    leftLookup.Add(key, list);
                }

                list.Add(leftItem);
            }

            foreach (TRight rightItem in right)
            {
                TKey key = rightKeySelector(rightItem)!;
                if (leftLookup.TryGetValue(key, out List<TLeft> leftItems))
                {
                    foreach (TLeft leftItem in leftItems)
                    {
                        yield return resultSelector(leftItem, rightItem);
                    }
                }
            }
        }
    }
}