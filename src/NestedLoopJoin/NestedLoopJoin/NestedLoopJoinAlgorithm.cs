using System;
using System.Collections.Generic;

namespace NestedLoopJoin
{
    public static class NestedLoopJoinAlgorithm
    {
        public static IEnumerable<TResult> Join<TLeft, TRight, TResult>(IEnumerable<TLeft> left, IEnumerable<TRight> right, Func<TLeft, TRight, bool> predicate, Func<TLeft, TRight, TResult> resultSelector)
        {
            if (left == null || right == null || predicate == null || resultSelector == null)
            {
                yield break;
            }

            foreach (TLeft leftItem in left)
            {
                foreach (TRight rightItem in right)
                {
                    if (predicate(leftItem, rightItem))
                    {
                        yield return resultSelector(leftItem, rightItem);
                    }
                }
            }
        }
    }
}