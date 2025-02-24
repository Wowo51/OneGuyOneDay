using System;
using System.Collections.Generic;

namespace SortMergeJoin
{
    public static class SortMergeJoiner
    {
        public static List<Tuple<T, U>> Join<T, U, TKey>(IEnumerable<T> left, IEnumerable<U> right, Func<T, TKey> leftKeySelector, Func<U, TKey> rightKeySelector, IComparer<TKey> comparer)
        {
            if (left == null || right == null || leftKeySelector == null || rightKeySelector == null || comparer == null)
            {
                return new List<Tuple<T, U>>();
            }

            List<Tuple<T, U>> results = new List<Tuple<T, U>>();
            IEnumerator<T> leftEnumerator = left.GetEnumerator();
            IEnumerator<U> rightEnumerator = right.GetEnumerator();
            bool leftHas = leftEnumerator.MoveNext();
            bool rightHas = rightEnumerator.MoveNext();
            while (leftHas && rightHas)
            {
                TKey leftKey = leftKeySelector(leftEnumerator.Current);
                TKey rightKey = rightKeySelector(rightEnumerator.Current);
                int compareResult = comparer.Compare(leftKey, rightKey);
                if (compareResult < 0)
                {
                    leftHas = leftEnumerator.MoveNext();
                }
                else if (compareResult > 0)
                {
                    rightHas = rightEnumerator.MoveNext();
                }
                else
                {
                    List<T> leftMatches = new List<T>();
                    List<U> rightMatches = new List<U>();
                    TKey currentKey = leftKey;
                    do
                    {
                        leftMatches.Add(leftEnumerator.Current);
                        leftHas = leftEnumerator.MoveNext();
                        if (leftHas)
                        {
                            TKey nextLeftKey = leftKeySelector(leftEnumerator.Current);
                            if (comparer.Compare(nextLeftKey, currentKey) != 0)
                            {
                                break;
                            }
                        }
                    }
                    while (leftHas);
                    do
                    {
                        rightMatches.Add(rightEnumerator.Current);
                        rightHas = rightEnumerator.MoveNext();
                        if (rightHas)
                        {
                            TKey nextRightKey = rightKeySelector(rightEnumerator.Current);
                            if (comparer.Compare(nextRightKey, currentKey) != 0)
                            {
                                break;
                            }
                        }
                    }
                    while (rightHas);
                    for (int i = 0; i < leftMatches.Count; i++)
                    {
                        for (int j = 0; j < rightMatches.Count; j++)
                        {
                            results.Add(new Tuple<T, U>(leftMatches[i], rightMatches[j]));
                        }
                    }
                }
            }

            return results;
        }
    }
}