using System;

namespace SchreierSimsAlgorithm
{
    public sealed class Permutation
    {
        private readonly int[] mapping;
        public int[] Mapping
        {
            get
            {
                return (int[])this.mapping.Clone();
            }
        }

        public int Length
        {
            get
            {
                return this.mapping.Length;
            }
        }

        public Permutation(int[]? mapping)
        {
            if (mapping == null)
            {
                mapping = new int[0];
            }

            this.mapping = new int[mapping.Length];
            for (int i = 0; i < mapping.Length; i++)
            {
                this.mapping[i] = mapping[i];
            }
        }

        public static Permutation Identity(int size)
        {
            int[] id = new int[size];
            for (int i = 0; i < size; i++)
            {
                id[i] = i;
            }

            return new Permutation(id);
        }

        public int Apply(int index)
        {
            if (index < 0 || index >= this.mapping.Length)
            {
                return index;
            }

            return this.mapping[index];
        }

        public Permutation Compose(Permutation other)
        {
            if (other == null || other.mapping.Length != this.mapping.Length)
            {
                return this;
            }

            int length = this.mapping.Length;
            int[] result = new int[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = this.Apply(other.Apply(i));
            }

            return new Permutation(result);
        }

        public Permutation Inverse()
        {
            int length = this.mapping.Length;
            int[] inv = new int[length];
            for (int i = 0; i < length; i++)
            {
                inv[this.mapping[i]] = i;
            }

            return new Permutation(inv);
        }

        public bool IsIdentity()
        {
            for (int i = 0; i < this.mapping.Length; i++)
            {
                if (this.mapping[i] != i)
                {
                    return false;
                }
            }

            return true;
        }

        public override bool Equals(object? obj)
        {
            Permutation? other = obj as Permutation;
            if (other == null)
            {
                return false;
            }

            if (this.mapping.Length != other.mapping.Length)
            {
                return false;
            }

            for (int i = 0; i < this.mapping.Length; i++)
            {
                if (this.mapping[i] != other.mapping[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            for (int i = 0; i < this.mapping.Length; i++)
            {
                hash = hash * 31 + this.mapping[i];
            }

            return hash;
        }

        public override string ToString()
        {
            string result = "(";
            for (int i = 0; i < this.mapping.Length; i++)
            {
                result += this.mapping[i].ToString();
                if (i < this.mapping.Length - 1)
                {
                    result += " ";
                }
            }

            result += ")";
            return result;
        }
    }
}