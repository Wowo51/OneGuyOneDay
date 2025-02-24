using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuchbergerAlgorithm
{
    public class Polynomial
    {
        public List<Term> Terms;
        public Polynomial(List<Term> terms)
        {
            this.Terms = new List<Term>();
            foreach (Term term in terms)
            {
                this.Terms.Add(term.Copy());
            }

            this.Simplify();
        }

        public void Simplify()
        {
            Dictionary<string, Term> dict = new Dictionary<string, Term>();
            foreach (Term term in this.Terms)
            {
                string key = GetMonomialKey(term);
                if (dict.ContainsKey(key))
                {
                    dict[key].Coefficient += term.Coefficient;
                }
                else
                {
                    dict[key] = term.Copy();
                }
            }

            List<Term> newTerms = new List<Term>();
            foreach (Term term in dict.Values)
            {
                if (Math.Abs(term.Coefficient) > 1e-10)
                {
                    newTerms.Add(term);
                }
            }

            this.Terms = newTerms;
            this.Terms.Sort();
        }

        private string GetMonomialKey(Term term)
        {
            List<string> parts = new List<string>();
            foreach (KeyValuePair<string, int> pair in term.Exponents)
            {
                parts.Add(pair.Key + "^" + pair.Value.ToString());
            }

            parts.Sort();
            return string.Join("*", parts);
        }

        public Polynomial Add(Polynomial other)
        {
            List<Term> newTerms = new List<Term>();
            newTerms.AddRange(this.Terms);
            newTerms.AddRange(other.Terms.Select(t => t.Copy()));
            return new Polynomial(newTerms);
        }

        public Polynomial Subtract(Polynomial other)
        {
            List<Term> negTerms = new List<Term>();
            foreach (Term t in other.Terms)
            {
                negTerms.Add(new Term(-t.Coefficient, t.Exponents));
            }

            return this.Add(new Polynomial(negTerms));
        }

        public Polynomial Multiply(Polynomial other)
        {
            List<Term> newTerms = new List<Term>();
            foreach (Term a in this.Terms)
            {
                foreach (Term b in other.Terms)
                {
                    newTerms.Add(Term.Multiply(a, b));
                }
            }

            return new Polynomial(newTerms);
        }

        public Polynomial MultiplyByTerm(Term t)
        {
            List<Term> newTerms = new List<Term>();
            foreach (Term term in this.Terms)
            {
                newTerms.Add(Term.Multiply(term, t));
            }

            return new Polynomial(newTerms);
        }

        public Term? LeadingTerm()
        {
            if (this.Terms.Count == 0)
            {
                return null;
            }

            this.Simplify();
            return this.Terms[0];
        }

        public bool IsZero()
        {
            this.Simplify();
            return this.Terms.Count == 0;
        }

        public Polynomial Normalize()
        {
            Term? lt = this.LeadingTerm();
            if (lt == null)
            {
                return new Polynomial(new List<Term>());
            }

            double factor = lt.Coefficient;
            List<Term> newTerms = new List<Term>();
            foreach (Term term in this.Terms)
            {
                newTerms.Add(new Term(term.Coefficient / factor, term.Exponents));
            }

            return new Polynomial(newTerms);
        }

        public Polynomial Copy()
        {
            List<Term> newTerms = new List<Term>();
            foreach (Term t in this.Terms)
            {
                newTerms.Add(t.Copy());
            }

            return new Polynomial(newTerms);
        }

        public static Polynomial SPolynomial(Polynomial f, Polynomial g)
        {
            Term? lt_f = f.LeadingTerm();
            Term? lt_g = g.LeadingTerm();
            if (lt_f == null || lt_g == null)
            {
                return new Polynomial(new List<Term>());
            }

            SortedDictionary<string, int> lcm = Term.LcmExponents(lt_f, lt_g);
            Term m1 = Term.ComputeMultiplier(lt_f, lcm);
            Term m2 = Term.ComputeMultiplier(lt_g, lcm);
            Polynomial term1 = f.MultiplyByTerm(m1);
            Polynomial term2 = g.MultiplyByTerm(m2);
            return term1.Subtract(term2);
        }

        public static Polynomial Reduce(Polynomial f, List<Polynomial> basis)
        {
            Polynomial remainder = new Polynomial(new List<Term>());
            Polynomial p = f.Copy();
            p.Simplify();
            while (!p.IsZero())
            {
                Term? lt_p = p.LeadingTerm();
                if (lt_p == null)
                {
                    break;
                }

                bool reduced = false;
                for (int i = 0; i < basis.Count; i++)
                {
                    Polynomial g = basis[i];
                    Term? lt_g = g.LeadingTerm();
                    if (lt_g != null && lt_g.Divides(lt_p))
                    {
                        double coeffRatio = lt_p.Coefficient / lt_g.Coefficient;
                        SortedDictionary<string, int> diffExp = new SortedDictionary<string, int>();
                        foreach (KeyValuePair<string, int> pair in lt_p.Exponents)
                        {
                            int exp_g = 0;
                            if (lt_g.Exponents.ContainsKey(pair.Key))
                            {
                                exp_g = lt_g.Exponents[pair.Key];
                            }

                            int diff = pair.Value - exp_g;
                            if (diff > 0)
                            {
                                diffExp[pair.Key] = diff;
                            }
                        }

                        Term multiplier = new Term(coeffRatio, diffExp);
                        Polynomial subtractPoly = g.MultiplyByTerm(multiplier);
                        p = p.Subtract(subtractPoly);
                        p.Simplify();
                        reduced = true;
                        break;
                    }
                }

                if (!reduced)
                {
                    List<Term> ltList = new List<Term>()
                    {
                        lt_p.Copy()
                    };
                    Polynomial ltPoly = new Polynomial(ltList);
                    remainder = remainder.Add(ltPoly);
                    p = p.Subtract(ltPoly);
                    p.Simplify();
                }
            }

            return remainder;
        }

        public override string ToString()
        {
            if (this.IsZero())
            {
                return "0";
            }

            StringBuilder sb = new StringBuilder();
            bool first = true;
            foreach (Term term in this.Terms)
            {
                if (!first && term.Coefficient > 0)
                {
                    sb.Append(" + ");
                }
                else if (!first && term.Coefficient < 0)
                {
                    sb.Append(" - ");
                }
                else if (first && term.Coefficient < 0)
                {
                    sb.Append("-");
                }

                double absCoeff = Math.Abs(term.Coefficient);
                if (absCoeff != 1 || term.Exponents.Count == 0)
                {
                    sb.Append(absCoeff.ToString());
                }

                foreach (KeyValuePair<string, int> pair in term.Exponents)
                {
                    sb.Append(pair.Key);
                    if (pair.Value != 1)
                    {
                        sb.Append("^" + pair.Value.ToString());
                    }
                }

                first = false;
            }

            return sb.ToString();
        }
    }
}