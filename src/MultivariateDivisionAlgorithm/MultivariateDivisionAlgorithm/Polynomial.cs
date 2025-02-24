using System;
using System.Collections.Generic;
using System.Text;

namespace MultivariateDivisionAlgorithm
{
    public class Monomial
    {
        public Dictionary<string, int> Exponents { get; }

        public Monomial(Dictionary<string, int> exponents)
        {
            Exponents = new Dictionary<string, int>(exponents);
        }

        public Monomial Multiply(Monomial other)
        {
            Dictionary<string, int> newExp = new Dictionary<string, int>(this.Exponents);
            foreach (KeyValuePair<string, int> kv in other.Exponents)
            {
                if (newExp.ContainsKey(kv.Key))
                {
                    newExp[kv.Key] = newExp[kv.Key] + kv.Value;
                }
                else
                {
                    newExp[kv.Key] = kv.Value;
                }
            }

            return new Monomial(newExp);
        }

        public bool Divides(Monomial other)
        {
            foreach (KeyValuePair<string, int> kv in this.Exponents)
            {
                int otherExp = 0;
                if (!other.Exponents.TryGetValue(kv.Key, out otherExp))
                {
                    otherExp = 0;
                }

                if (otherExp < kv.Value)
                    return false;
            }

            return true;
        }

        public Monomial Divide(Monomial divisor)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> kv in this.Exponents)
            {
                int expDivisor = 0;
                if (!divisor.Exponents.TryGetValue(kv.Key, out expDivisor))
                {
                    expDivisor = 0;
                }

                int newExp = kv.Value - expDivisor;
                if (newExp != 0)
                    result[kv.Key] = newExp;
            }

            return new Monomial(result);
        }

        public static int Compare(Monomial a, Monomial b, List<string> order)
        {
            foreach (string var in order)
            {
                int expA = 0;
                int expB = 0;
                if (!a.Exponents.TryGetValue(var, out expA))
                    expA = 0;
                if (!b.Exponents.TryGetValue(var, out expB))
                    expB = 0;
                if (expA > expB)
                    return 1;
                if (expA < expB)
                    return -1;
            }

            return 0;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            List<string> keys = new List<string>(Exponents.Keys);
            keys.Sort();
            foreach (string key in keys)
            {
                sb.Append(key);
                int exp = Exponents[key];
                if (exp != 1)
                {
                    sb.Append("^").Append(exp);
                }
            }

            return sb.ToString();
        }
    }

    public class Term
    {
        public double Coefficient { get; }
        public Monomial Monomial { get; }

        public Term(double coefficient, Monomial monomial)
        {
            this.Coefficient = coefficient;
            this.Monomial = monomial;
        }

        public Term Multiply(Term other)
        {
            double newCoeff = this.Coefficient * other.Coefficient;
            Monomial newMono = this.Monomial.Multiply(other.Monomial);
            return new Term(newCoeff, newMono);
        }

        public override string ToString()
        {
            return Coefficient + "*" + Monomial.ToString();
        }
    }

    public class Polynomial
    {
        public List<Term> Terms { get; set; }

        public Polynomial(List<Term> terms)
        {
            Terms = new List<Term>(terms);
            Simplify();
        }

        public static Polynomial Zero
        {
            get
            {
                return new Polynomial(new List<Term>());
            }
        }

        public bool IsZero()
        {
            return Terms.Count == 0;
        }

        public void Simplify()
        {
            Dictionary<string, Tuple<Monomial, double>> map = new Dictionary<string, Tuple<Monomial, double>>();
            foreach (Term term in Terms)
            {
                string key = term.Monomial.ToString();
                if (map.ContainsKey(key))
                {
                    Tuple<Monomial, double> existing = map[key];
                    map[key] = new Tuple<Monomial, double>(existing.Item1, existing.Item2 + term.Coefficient);
                }
                else
                {
                    map[key] = new Tuple<Monomial, double>(term.Monomial, term.Coefficient);
                }
            }

            List<Term> newTerms = new List<Term>();
            foreach (KeyValuePair<string, Tuple<Monomial, double>> kv in map)
            {
                if (Math.Abs(kv.Value.Item2) > 1e-10)
                    newTerms.Add(new Term(kv.Value.Item2, kv.Value.Item1));
            }

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

        public Polynomial Multiply(Term term)
        {
            List<Term> newTerms = new List<Term>();
            foreach (Term t in this.Terms)
            {
                newTerms.Add(t.Multiply(term));
            }

            return new Polynomial(newTerms);
        }

        public Polynomial Multiply(Polynomial other)
        {
            List<Term> newTerms = new List<Term>();
            foreach (Term t in this.Terms)
            {
                foreach (Term ot in other.Terms)
                {
                    newTerms.Add(t.Multiply(ot));
                }
            }

            return new Polynomial(newTerms);
        }

        public Term? GetLeadingTerm(List<string> order)
        {
            if (IsZero())
                return null;
            Term leading = this.Terms[0];
            foreach (Term term in this.Terms)
            {
                int cmp = Monomial.Compare(term.Monomial, leading.Monomial, order);
                if (cmp > 0)
                {
                    leading = term;
                }
            }

            return leading;
        }

        public override string ToString()
        {
            if (IsZero())
                return "0";
            StringBuilder sb = new StringBuilder();
            foreach (Term term in Terms)
            {
                sb.Append(term.ToString()).Append(" + ");
            }

            if (sb.Length >= 3)
                sb.Length -= 3;
            return sb.ToString();
        }
    }
}