using System;

namespace EspressoHeuristicMinimizer
{
    public class BooleanCube : IEquatable<BooleanCube>
    {
        public string Term { get; }

        public BooleanCube(string term)
        {
            Term = term ?? "";
        }

        public bool CanMerge(BooleanCube? other)
        {
            if (other == null || Term.Length != other.Term.Length)
            {
                return false;
            }

            int diffCount = 0;
            for (int i = 0; i < Term.Length; i++)
            {
                char a = Term[i];
                char b = other.Term[i];
                if (a != b)
                {
                    if ((a == '0' && b == '1') || (a == '1' && b == '0'))
                    {
                        diffCount++;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return diffCount == 1;
        }

        public BooleanCube? Merge(BooleanCube? other)
        {
            if (!CanMerge(other))
            {
                return null;
            }

            char[] newTerm = new char[Term.Length];
            for (int i = 0; i < Term.Length; i++)
            {
                char a = Term[i];
                char b = other!.Term[i];
                newTerm[i] = (a == b) ? a : '-';
            }

            return new BooleanCube(new string (newTerm));
        }

        public bool Covers(BooleanCube? other)
        {
            if (other == null || Term.Length != other.Term.Length)
            {
                return false;
            }

            for (int i = 0; i < Term.Length; i++)
            {
                char a = Term[i];
                char b = other.Term[i];
                if (a != '-' && a != b)
                {
                    return false;
                }
            }

            return true;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as BooleanCube);
        }

        public bool Equals(BooleanCube? other)
        {
            if (other == null)
            {
                return false;
            }

            return Term == other.Term;
        }

        public override int GetHashCode()
        {
            return Term.GetHashCode();
        }

        public override string ToString()
        {
            return Term;
        }
    }
}