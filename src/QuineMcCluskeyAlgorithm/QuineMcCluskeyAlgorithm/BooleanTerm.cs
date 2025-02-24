using System;
using System.Collections.Generic;

namespace QuineMcCluskeyAlgorithm
{
    public class BooleanTerm
    {
        public string Term { get; }
        public List<int> Minterms { get; }
        public bool IsCombined { get; private set; }

        public BooleanTerm(string term, List<int> minterms)
        {
            this.Term = term;
            this.Minterms = new List<int>(minterms);
            this.IsCombined = false;
        }

        public int CountOnes()
        {
            int count = 0;
            for (int i = 0; i < this.Term.Length; i++)
            {
                if (this.Term[i] == '1')
                {
                    count++;
                }
            }

            return count;
        }

        public BooleanTerm? Combine(BooleanTerm other)
        {
            if (this.Term.Length != other.Term.Length)
            {
                return null;
            }

            int differences = 0;
            char[] newTerm = new char[this.Term.Length];
            for (int i = 0; i < this.Term.Length; i++)
            {
                char a = this.Term[i];
                char b = other.Term[i];
                if (a == b)
                {
                    newTerm[i] = a;
                }
                else
                {
                    if (a == '-' || b == '-')
                    {
                        return null;
                    }

                    differences++;
                    newTerm[i] = '-';
                }
            }

            if (differences == 1)
            {
                List<int> combinedMinterms = new List<int>();
                for (int i = 0; i < this.Minterms.Count; i++)
                {
                    combinedMinterms.Add(this.Minterms[i]);
                }

                for (int i = 0; i < other.Minterms.Count; i++)
                {
                    if (!combinedMinterms.Contains(other.Minterms[i]))
                    {
                        combinedMinterms.Add(other.Minterms[i]);
                    }
                }

                return new BooleanTerm(new string (newTerm), combinedMinterms);
            }

            return null;
        }

        public void MarkCombined()
        {
            this.IsCombined = true;
        }
    }
}