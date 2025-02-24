using System;
using System.Collections.Generic;
using System.Text;

namespace MultivariateDivisionAlgorithm
{
    public class DivisionResult
    {
        public List<Polynomial> Quotients { get; set; }
        public Polynomial Remainder { get; set; }

        public DivisionResult(List<Polynomial> quotients, Polynomial remainder)
        {
            Quotients = quotients;
            Remainder = remainder;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Quotients.Count; i++)
            {
                sb.Append("Q").Append(i).Append(" = ").Append(Quotients[i].ToString()).Append("\n");
            }

            sb.Append("R = ").Append(Remainder.ToString());
            return sb.ToString();
        }
    }

    public class DivisionAlgorithm
    {
        public static DivisionResult Divide(Polynomial f, List<Polynomial> divisors, List<string>? variableOrder = null)
        {
            if (variableOrder == null)
            {
                SortedSet<string> varSet = new SortedSet<string>();
                foreach (Term t in f.Terms)
                {
                    foreach (string v in t.Monomial.Exponents.Keys)
                    {
                        varSet.Add(v);
                    }
                }

                foreach (Polynomial poly in divisors)
                {
                    foreach (Term t in poly.Terms)
                    {
                        foreach (string v in t.Monomial.Exponents.Keys)
                        {
                            varSet.Add(v);
                        }
                    }
                }

                variableOrder = new List<string>(varSet);
            }

            List<Polynomial> quotients = new List<Polynomial>();
            for (int i = 0; i < divisors.Count; i++)
            {
                quotients.Add(Polynomial.Zero);
            }

            Polynomial remainder = Polynomial.Zero;
            Polynomial dividend = f;
            while (!dividend.IsZero())
            {
                Term lt = dividend.GetLeadingTerm(variableOrder)!;
                bool divisionOccurred = false;
                for (int i = 0; i < divisors.Count; i++)
                {
                    Polynomial divisor = divisors[i];
                    if (divisor.IsZero())
                    {
                        continue;
                    }

                    Term ltDivisor = divisor.GetLeadingTerm(variableOrder)!;
                    if (ltDivisor.Monomial.Divides(lt.Monomial))
                    {
                        double coeffFactor = lt.Coefficient / ltDivisor.Coefficient;
                        Monomial newMono = lt.Monomial.Divide(ltDivisor.Monomial);
                        Term termQuotient = new Term(coeffFactor, newMono);
                        quotients[i] = quotients[i].Add(new Polynomial(new List<Term> { termQuotient }));
                        Polynomial subtraction = divisor.Multiply(termQuotient);
                        dividend = dividend.Subtract(subtraction);
                        divisionOccurred = true;
                        break;
                    }
                }

                if (!divisionOccurred)
                {
                    Polynomial ltPoly = new Polynomial(new List<Term> { lt });
                    remainder = remainder.Add(ltPoly);
                    dividend = dividend.Subtract(ltPoly);
                }
            }

            return new DivisionResult(quotients, remainder);
        }
    }
}