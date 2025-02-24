using System;
using System.Collections.Generic;

namespace SimpleLrParser
{
    public class Production
    {
        public string Left { get; }
        public List<string> Right { get; }

        public Production(string left, IEnumerable<string> right)
        {
            if (left == null)
            {
                left = "";
            }

            Left = left;
            Right = new List<string>(right ?? new List<string>());
        }

        public override bool Equals(object? obj)
        {
            if (!(obj is Production other))
            {
                return false;
            }

            if (Left != other.Left)
            {
                return false;
            }

            if (Right.Count != other.Right.Count)
            {
                return false;
            }

            for (int i = 0; i < Right.Count; i++)
            {
                if (Right[i] != other.Right[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int hash = Left.GetHashCode();
            foreach (string s in Right)
            {
                hash = (hash * 397) ^ s.GetHashCode();
            }

            return hash;
        }

        public override string ToString()
        {
            return Left + " -> " + string.Join(" ", Right);
        }
    }

    public class Grammar
    {
        public string StartSymbol { get; }
        public List<Production> Productions { get; }

        public Grammar(string startSymbol)
        {
            if (startSymbol == null)
            {
                startSymbol = "";
            }

            StartSymbol = startSymbol;
            Productions = new List<Production>();
        }

        public void AddProduction(string left, IEnumerable<string> right)
        {
            Productions.Add(new Production(left, right));
        }
    }
}