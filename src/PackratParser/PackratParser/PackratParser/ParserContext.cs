using System;
using System.Collections.Generic;

namespace PackratParser
{
    public class ParserContext
    {
        public string Input { get; }
        public Dictionary<MemoKey, Result> Memo { get; }

        public ParserContext(string input)
        {
            Input = input;
            Memo = new Dictionary<MemoKey, Result>(new MemoKeyComparer());
        }

        public Result Memoize(Expression expression, int position, Func<Result> compute)
        {
            MemoKey key = new MemoKey(expression, position);
            if (Memo.ContainsKey(key))
            {
                return Memo[key];
            }

            Result result = compute();
            Memo[key] = result;
            return result;
        }

        public class MemoKey
        {
            public Expression Expression { get; }
            public int Position { get; }

            public MemoKey(Expression expression, int position)
            {
                Expression = expression;
                Position = position;
            }
        }

        private class MemoKeyComparer : IEqualityComparer<MemoKey>
        {
            public bool Equals(MemoKey? x, MemoKey? y)
            {
                if (x == null || y == null)
                {
                    return false;
                }

                return ReferenceEquals(x.Expression, y.Expression) && x.Position == y.Position;
            }

            public int GetHashCode(MemoKey obj)
            {
                if (obj == null)
                {
                    return 0;
                }

                int hash = 17;
                hash = hash * 23 + System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(obj.Expression);
                hash = hash * 23 + obj.Position.GetHashCode();
                return hash;
            }
        }
    }
}