using System;
using System.Collections.Generic;

namespace BuchbergerAlgorithm
{
    public static class GroebnerBasisCalculator
    {
        public static List<Polynomial> ComputeGroebnerBasis(List<Polynomial> basis)
        {
            List<Polynomial> G = new List<Polynomial>(basis);
            bool added = true;
            while (added)
            {
                added = false;
                int n = G.Count;
                for (int i = 0; i < n; i++)
                {
                    for (int j = i + 1; j < n; j++)
                    {
                        Polynomial sPoly = Polynomial.SPolynomial(G[i], G[j]);
                        Polynomial r = Polynomial.Reduce(sPoly, G);
                        if (!r.IsZero())
                        {
                            Polynomial normalized = r.Normalize();
                            G.Add(normalized);
                            added = true;
                            n = G.Count;
                        }
                    }
                }
            }

            return G;
        }
    }
}