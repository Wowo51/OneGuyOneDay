using System;
using System.Collections.Generic;

namespace LZWLSyllableVariant
{
    public class SyllableSequence : IEquatable<SyllableSequence>
    {
        public List<string> Tokens { get; }

        public SyllableSequence(List<string>? tokens)
        {
            if (tokens == null)
            {
                Tokens = new List<string>();
            }
            else
            {
                Tokens = new List<string>(tokens);
            }
        }

        public SyllableSequence Append(string token)
        {
            List<string> newTokens = new List<string>(this.Tokens);
            newTokens.Add(token);
            return new SyllableSequence(newTokens);
        }

        public override bool Equals(object? obj)
        {
            if (obj is SyllableSequence other)
            {
                return Equals(other);
            }

            return false;
        }

        public bool Equals(SyllableSequence? other)
        {
            if (other == null)
            {
                return false;
            }

            if (this.Tokens.Count != other.Tokens.Count)
            {
                return false;
            }

            for (int i = 0; i < this.Tokens.Count; i++)
            {
                if (!this.Tokens[i].Equals(other.Tokens[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            foreach (string token in Tokens)
            {
                hash = hash * 23 + (token != null ? token.GetHashCode() : 0);
            }

            return hash;
        }

        public override string ToString()
        {
            return string.Join(string.Empty, Tokens);
        }
    }
}