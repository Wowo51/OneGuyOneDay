using System;
using System.Collections.Generic;
using System.Linq;

namespace FaugereF4Algorithm
{
    public class Monomial : IComparable<Monomial>
    {
        public SortedDictionary<string, int> Exponents { get; private set; }

        public Monomial(Dictionary<string, int> exponents)
        {
            Exponents = new SortedDictionary<string, int>();
            foreach (KeyValuePair<string, int> entry in exponents)
            {
                if (entry.Value != 0)
                {
                    Exponents[entry.Key] = entry.Value;
                }
            }
        }

        public int GetTotalDegree()
        {
            int total = 0;
            foreach (int exp in Exponents.Values)
            {
                total += exp;
            }

            return total;
        }

        public Monomial Multiply(Monomial other)
        {
            Dictionary<string, int> newExponents = new Dictionary<string, int>(Exponents);
            foreach (KeyValuePair<string, int> entry in other.Exponents)
            {
                if (newExponents.ContainsKey(entry.Key))
                {
                    newExponents[entry.Key] = newExponents[entry.Key] + entry.Value;
                }
                else
                {
                    newExponents[entry.Key] = entry.Value;
                }
            }

            return new Monomial(newExponents);
        }

        public int CompareTo(Monomial? other)
        {
            if (other is null)
            {
                return 1;
            }

            int degreeComparison = this.GetTotalDegree().CompareTo(other.GetTotalDegree());
            if (degreeComparison != 0)
            {
                return degreeComparison;
            }

            IEnumerator<KeyValuePair<string, int>> enum1 = this.Exponents.GetEnumerator();
            IEnumerator<KeyValuePair<string, int>> enum2 = other.Exponents.GetEnumerator();
            while (enum1.MoveNext() && enum2.MoveNext())
            {
                int keyComparison = string.Compare(enum1.Current.Key, enum2.Current.Key, StringComparison.Ordinal);
                if (keyComparison != 0)
                {
                    return keyComparison;
                }

                int expComparison = enum1.Current.Value.CompareTo(enum2.Current.Value);
                if (expComparison != 0)
                {
                    return expComparison;
                }
            }

            return this.Exponents.Count.CompareTo(other.Exponents.Count);
        }

        public bool Divides(Monomial other)
        {
            foreach (KeyValuePair<string, int> entry in Exponents)
            {
                int otherExp = 0;
                if (other.Exponents.ContainsKey(entry.Key))
                {
                    otherExp = other.Exponents[entry.Key];
                }

                if (otherExp < entry.Value)
                {
                    return false;
                }
            }

            return true;
        }

        public Monomial? DivideBy(Monomial divisor)
        {
            Dictionary<string, int> result = new Dictionary<string, int>(Exponents);
            foreach (KeyValuePair<string, int> entry in divisor.Exponents)
            {
                if (!result.ContainsKey(entry.Key) || result[entry.Key] < entry.Value)
                {
                    return null;
                }

                result[entry.Key] = result[entry.Key] - entry.Value;
                if (result[entry.Key] == 0)
                {
                    result.Remove(entry.Key);
                }
            }

            return new Monomial(result);
        }

        public static Monomial LCM(Monomial m1, Monomial m2)
        {
            Dictionary<string, int> lcmExponents = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> entry in m1.Exponents)
            {
                lcmExponents[entry.Key] = entry.Value;
            }

            foreach (KeyValuePair<string, int> entry in m2.Exponents)
            {
                if (lcmExponents.ContainsKey(entry.Key))
                {
                    lcmExponents[entry.Key] = Math.Max(lcmExponents[entry.Key], entry.Value);
                }
                else
                {
                    lcmExponents[entry.Key] = entry.Value;
                }
            }

            return new Monomial(lcmExponents);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Monomial other)
            {
                return this.Exponents.SequenceEqual(other.Exponents);
            }

            return false;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            foreach (KeyValuePair<string, int> entry in Exponents)
            {
                hash = hash * 23 + entry.Key.GetHashCode();
                hash = hash * 23 + entry.Value.GetHashCode();
            }

            return hash;
        }

        public override string ToString()
        {
            if (Exponents.Count == 0)
            {
                return "1";
            }

            List<string> parts = new List<string>();
            foreach (KeyValuePair<string, int> entry in Exponents)
            {
                if (entry.Value == 1)
                {
                    parts.Add(entry.Key);
                }
                else
                {
                    parts.Add(entry.Key + "^" + entry.Value.ToString());
                }
            }

            return string.Join("*", parts);
        }
    }

    public class Term
    {
        public double Coefficient { get; set; }
        public Monomial Monomial { get; set; }

        public Term(double coefficient, Monomial monomial)
        {
            Coefficient = coefficient;
            Monomial = monomial;
        }

        public Term Multiply(Term other)
        {
            return new Term(this.Coefficient * other.Coefficient, this.Monomial.Multiply(other.Monomial));
        }

        public override string ToString()
        {
            string monoStr = Monomial.ToString();
            if (monoStr == "1")
            {
                return Coefficient.ToString();
            }

            if (Math.Abs(Coefficient - 1.0) < 1e-9)
            {
                return monoStr;
            }

            if (Math.Abs(Coefficient + 1.0) < 1e-9)
            {
                return "-" + monoStr;
            }

            return Coefficient.ToString() + "*" + monoStr;
        }
    }

    public class Polynomial
    {
        public List<Term> Terms { get; set; }

        public Polynomial()
        {
            Terms = new List<Term>();
        }

        public Polynomial(List<Term> terms)
        {
            Terms = new List<Term>(terms);
            Normalize();
        }

        public void Normalize()
        {
            Dictionary<Monomial, double> termDict = new Dictionary<Monomial, double>();
            foreach (Term term in Terms)
            {
                if (termDict.ContainsKey(term.Monomial))
                {
                    termDict[term.Monomial] += term.Coefficient;
                }
                else
                {
                    termDict[term.Monomial] = term.Coefficient;
                }
            }

            List<Term> newTerms = new List<Term>();
            foreach (KeyValuePair<Monomial, double> kvp in termDict)
            {
                if (Math.Abs(kvp.Value) > 1e-9)
                {
                    newTerms.Add(new Term(kvp.Value, kvp.Key));
                }
            }

            newTerms.Sort(delegate (Term a, Term b)
            {
                return -a.Monomial.CompareTo(b.Monomial);
            });
            Terms = newTerms;
        }

        public Polynomial Add(Polynomial other)
        {
            List<Term> newTerms = new List<Term>(this.Terms);
            newTerms.AddRange(other.Terms);
            return new Polynomial(newTerms);
        }

        public Polynomial Subtract(Polynomial other)
        {
            List<Term> newTerms = new List<Term>(this.Terms);
            foreach (Term term in other.Terms)
            {
                newTerms.Add(new Term(-term.Coefficient, term.Monomial));
            }

            return new Polynomial(newTerms);
        }

        public Polynomial Multiply(Polynomial other)
        {
            List<Term> newTerms = new List<Term>();
            foreach (Term term1 in this.Terms)
            {
                foreach (Term term2 in other.Terms)
                {
                    newTerms.Add(term1.Multiply(term2));
                }
            }

            return new Polynomial(newTerms);
        }

        public Polynomial MultiplyByTerm(Term term)
        {
            List<Term> newTerms = new List<Term>();
            foreach (Term t in this.Terms)
            {
                newTerms.Add(t.Multiply(term));
            }

            return new Polynomial(newTerms);
        }

        public Polynomial Clone()
        {
            List<Term> clonedTerms = new List<Term>();
            foreach (Term term in this.Terms)
            {
                clonedTerms.Add(new Term(term.Coefficient, term.Monomial));
            }

            return new Polynomial(clonedTerms);
        }

        public Term LeadingTerm()
        {
            if (Terms.Count > 0)
            {
                return Terms[0];
            }

            return new Term(0, new Monomial(new Dictionary<string, int>()));
        }

        public Polynomial Reduce(List<Polynomial> basis)
        {
            Polynomial remainder = this.Clone();
            bool reductionOccurred = true;
            while (reductionOccurred)
            {
                remainder.Normalize();
                if (remainder.Terms.Count == 0)
                {
                    break;
                }

                Term lead = remainder.LeadingTerm();
                bool reduced = false;
                foreach (Polynomial g in basis)
                {
                    Term lt = g.LeadingTerm();
                    if (lt.Monomial.Divides(lead.Monomial))
                    {
                        Monomial? quotient = lead.Monomial.DivideBy(lt.Monomial);
                        if (quotient == null)
                        {
                            continue;
                        }

                        double factor = lead.Coefficient / lt.Coefficient;
                        Term multiplier = new Term(factor, quotient);
                        Polynomial subtraction = g.MultiplyByTerm(multiplier);
                        remainder = remainder.Subtract(subtraction);
                        reduced = true;
                        break;
                    }
                }

                reductionOccurred = reduced;
            }

            remainder.Normalize();
            return remainder;
        }

        public bool IsZero()
        {
            Normalize();
            return Terms.Count == 0;
        }

        public override string ToString()
        {
            if (Terms.Count == 0)
            {
                return "0";
            }

            string result = "";
            foreach (Term term in Terms)
            {
                if (result != "")
                {
                    result += (term.Coefficient >= 0 ? " + " : " - ");
                }
                else if (term.Coefficient < 0)
                {
                    result += "-";
                }

                double absCoeff = Math.Abs(term.Coefficient);
                if (term.Monomial.ToString() == "1")
                {
                    result += absCoeff.ToString();
                }
                else
                {
                    if (Math.Abs(absCoeff - 1.0) > 1e-9)
                    {
                        result += absCoeff.ToString() + "*";
                    }

                    result += term.Monomial.ToString();
                }
            }

            return result;
        }
    }
}