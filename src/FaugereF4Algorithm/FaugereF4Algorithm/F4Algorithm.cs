using System;
using System.Collections.Generic;

namespace FaugereF4Algorithm
{
    public static class F4Algorithm
    {
        public static List<Polynomial> ComputeGroebnerBasis(List<Polynomial> generators)
        {
            if (generators == null)
            {
                return new List<Polynomial>();
            }

            List<Polynomial> basis = new List<Polynomial>();
            foreach (Polynomial poly in generators)
            {
                // Clone() already normalizes the polynomial.
                Polynomial normalized = poly.Clone();
                basis.Add(normalized);
            }

            List<Tuple<Polynomial, Polynomial>> pairs = new List<Tuple<Polynomial, Polynomial>>();
            int basisCount = basis.Count;
            for (int i = 0; i < basisCount; i++)
            {
                for (int j = i + 1; j < basisCount; j++)
                {
                    pairs.Add(new Tuple<Polynomial, Polynomial>(basis[i], basis[j]));
                }
            }

            while (pairs.Count > 0)
            {
                List<Polynomial> sPolys = new List<Polynomial>();
                foreach (Tuple<Polynomial, Polynomial> pair in pairs)
                {
                    Polynomial sPoly = SPolynomial(pair.Item1, pair.Item2);
                    sPolys.Add(sPoly);
                }

                pairs.Clear();
                List<Polynomial> reducedSPolys = F4ReduceBatch(sPolys, basis);
                foreach (Polynomial poly in reducedSPolys)
                {
                    if (!poly.IsZero())
                    {
                        basis.Add(poly);
                        int currentCount = basis.Count;
                        for (int i = 0; i < currentCount - 1; i++)
                        {
                            pairs.Add(new Tuple<Polynomial, Polynomial>(basis[i], poly));
                        }
                    }
                }
            }

            return basis;
        }

        public static Polynomial SPolynomial(Polynomial f, Polynomial g)
        {
            Term ltF = f.LeadingTerm();
            Term ltG = g.LeadingTerm();
            Monomial lcm = Monomial.LCM(ltF.Monomial, ltG.Monomial);
            Monomial? m1 = lcm.DivideBy(ltF.Monomial);
            if (m1 == null)
            {
                m1 = new Monomial(new Dictionary<string, int>());
            }

            Monomial? m2 = lcm.DivideBy(ltG.Monomial);
            if (m2 == null)
            {
                m2 = new Monomial(new Dictionary<string, int>());
            }

            Polynomial poly1 = f.MultiplyByTerm(new Term(1.0, m1));
            Polynomial poly2 = g.MultiplyByTerm(new Term(1.0, m2));
            Polynomial sPoly = poly1.Subtract(poly2);
            return sPoly;
        }

        public static List<Polynomial> F4ReduceBatch(List<Polynomial> sPolys, List<Polynomial> basis)
        {
            List<Polynomial> reducedList = new List<Polynomial>();
            foreach (Polynomial poly in sPolys)
            {
                Polynomial reduced = poly.Reduce(basis);
                if (!reduced.IsZero())
                {
                    reducedList.Add(reduced);
                }
            }

            return reducedList;
        }

        public static List<Polynomial> ComputeGroebnerBasisF5(List<Polynomial> generators)
        {
            // F5 algorithm is not independently implemented;
            // this method uses the F4 algorithm for Groebner basis computation.
            return ComputeGroebnerBasis(generators);
        }
    }
}