using System;
using System.Collections.Generic;
using System.Text;

namespace BuchbergerAlgorithm
{
    public class Term : IComparable<Term>
    {
        public double Coefficient;
        public SortedDictionary<string, int> Exponents;
        public Term(double coefficient, SortedDictionary<string, int> exponents)
        {
            this.Coefficient = coefficient;
            this.Exponents = new SortedDictionary<string, int>(exponents);
        }

        public Term Copy()
        {
            SortedDictionary<string, int> newExponents = new SortedDictionary<string, int>();
            foreach (KeyValuePair<string, int> pair in this.Exponents)
            {
                newExponents.Add(pair.Key, pair.Value);
            }

            return new Term(this.Coefficient, newExponents);
        }

        public static Term Multiply(Term a, Term b)
        {
            SortedDictionary<string, int> newExp = new SortedDictionary<string, int>();
            foreach (KeyValuePair<string, int> pair in a.Exponents)
            {
                newExp[pair.Key] = pair.Value;
            }

            foreach (KeyValuePair<string, int> pair in b.Exponents)
            {
                if (newExp.ContainsKey(pair.Key))
                {
                    newExp[pair.Key] = newExp[pair.Key] + pair.Value;
                }
                else
                {
                    newExp[pair.Key] = pair.Value;
                }
            }

            return new Term(a.Coefficient * b.Coefficient, newExp);
        }

        public bool Divides(Term other)
        {
            foreach (KeyValuePair<string, int> pair in this.Exponents)
            {
                int expOther = 0;
                if (other.Exponents.ContainsKey(pair.Key))
                {
                    expOther = other.Exponents[pair.Key];
                }

                if (expOther < pair.Value)
                {
                    return false;
                }
            }

            return true;
        }

        public static SortedDictionary<string, int> LcmExponents(Term a, Term b)
        {
            SortedDictionary<string, int> lcm = new SortedDictionary<string, int>();
            foreach (KeyValuePair<string, int> pair in a.Exponents)
            {
                lcm[pair.Key] = pair.Value;
            }

            foreach (KeyValuePair<string, int> pair in b.Exponents)
            {
                if (lcm.ContainsKey(pair.Key))
                {
                    if (pair.Value > lcm[pair.Key])
                    {
                        lcm[pair.Key] = pair.Value;
                    }
                }
                else
                {
                    lcm[pair.Key] = pair.Value;
                }
            }

            return lcm;
        }

        public static Term ComputeMultiplier(Term term, SortedDictionary<string, int> lcm)
        {
            SortedDictionary<string, int> multExp = new SortedDictionary<string, int>();
            foreach (KeyValuePair<string, int> pair in lcm)
            {
                int expTerm = 0;
                if (term.Exponents.ContainsKey(pair.Key))
                {
                    expTerm = term.Exponents[pair.Key];
                }

                int diff = pair.Value - expTerm;
                if (diff > 0)
                {
                    multExp[pair.Key] = diff;
                }
            }

            return new Term(1.0, multExp);
        }

        public int TotalDegree()
        {
            int sum = 0;
            foreach (KeyValuePair<string, int> pair in this.Exponents)
            {
                sum += pair.Value;
            }

            return sum;
        }

        public int CompareTo(Term? other)
        {
            if (other == null)
            {
                return 1;
            }

            int degreeDiff = other.TotalDegree() - this.TotalDegree();
            if (degreeDiff != 0)
            {
                return degreeDiff;
            }

            IEnumerator<KeyValuePair<string, int>> thisEnum = this.Exponents.GetEnumerator();
            IEnumerator<KeyValuePair<string, int>> otherEnum = other.Exponents.GetEnumerator();
            bool thisHas = thisEnum.MoveNext();
            bool otherHas = otherEnum.MoveNext();
            while (thisHas && otherHas)
            {
                int nameCompare = string.Compare(thisEnum.Current.Key, otherEnum.Current.Key);
                if (nameCompare != 0)
                {
                    return nameCompare;
                }

                int expDiff = otherEnum.Current.Value - thisEnum.Current.Value;
                if (expDiff != 0)
                {
                    return expDiff;
                }

                thisHas = thisEnum.MoveNext();
                otherHas = otherEnum.MoveNext();
            }

            if (thisHas)
            {
                return 1;
            }

            if (otherHas)
            {
                return -1;
            }

            return 0;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Coefficient.ToString());
            foreach (KeyValuePair<string, int> pair in Exponents)
            {
                sb.Append(" * " + pair.Key);
                if (pair.Value != 1)
                {
                    sb.Append("^" + pair.Value.ToString());
                }
            }

            return sb.ToString();
        }
    }
}