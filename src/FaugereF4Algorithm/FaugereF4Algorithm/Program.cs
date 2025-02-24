using System;
using System.Collections.Generic;

namespace FaugereF4Algorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Define two example polynomials in variables x and y.
            // p1 = x^2 + y^2 - 1
            List<Term> terms1 = new List<Term>();
            terms1.Add(new Term(1.0, new Monomial(new Dictionary<string, int> { { "x", 2 } })));
            terms1.Add(new Term(1.0, new Monomial(new Dictionary<string, int> { { "y", 2 } })));
            terms1.Add(new Term(-1.0, new Monomial(new Dictionary<string, int>())));
            Polynomial p1 = new Polynomial(terms1);
            // p2 = x^2 - y
            List<Term> terms2 = new List<Term>();
            terms2.Add(new Term(1.0, new Monomial(new Dictionary<string, int> { { "x", 2 } })));
            terms2.Add(new Term(-1.0, new Monomial(new Dictionary<string, int> { { "y", 1 } })));
            Polynomial p2 = new Polynomial(terms2);
            List<Polynomial> basis = new List<Polynomial>
            {
                p1,
                p2
            };
            // Compute the Groebner basis using the Faugère F4 algorithm implementation.
            List<Polynomial> groebnerBasis = F4Algorithm.ComputeGroebnerBasis(basis);
            Console.WriteLine("Computed Groebner Basis using F4:");
            foreach (Polynomial poly in groebnerBasis)
            {
                Console.WriteLine(poly.ToString());
            }

            // Additionally, compute the Groebner basis using the F5 method (placeholder implementation).
            List<Polynomial> groebnerBasisF5 = F4Algorithm.ComputeGroebnerBasisF5(basis);
            Console.WriteLine("\nComputed Groebner Basis using F5 (placeholder):");
            foreach (Polynomial poly in groebnerBasisF5)
            {
                Console.WriteLine(poly.ToString());
            }
        }
    }
}