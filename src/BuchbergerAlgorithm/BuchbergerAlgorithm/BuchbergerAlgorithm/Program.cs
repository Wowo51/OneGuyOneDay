using System;
using System.Collections.Generic;

namespace BuchbergerAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<Term> termsF1 = new List<Term>();
            SortedDictionary<string, int> expX2 = new SortedDictionary<string, int>();
            expX2.Add("x", 2);
            termsF1.Add(new Term(1.0, expX2));
            SortedDictionary<string, int> expY2 = new SortedDictionary<string, int>();
            expY2.Add("y", 2);
            termsF1.Add(new Term(1.0, expY2));
            SortedDictionary<string, int> expConst = new SortedDictionary<string, int>();
            termsF1.Add(new Term(-1.0, expConst));
            Polynomial f1 = new Polynomial(termsF1);
            List<Term> termsF2 = new List<Term>();
            SortedDictionary<string, int> expX = new SortedDictionary<string, int>();
            expX.Add("x", 1);
            termsF2.Add(new Term(1.0, expX));
            SortedDictionary<string, int> expY = new SortedDictionary<string, int>();
            expY.Add("y", 1);
            termsF2.Add(new Term(-1.0, expY));
            Polynomial f2 = new Polynomial(termsF2);
            List<Polynomial> basis = new List<Polynomial>
            {
                f1,
                f2
            };
            List<Polynomial> groebnerBasis = GroebnerBasisCalculator.ComputeGroebnerBasis(basis);
            Console.WriteLine("Groebner Basis:");
            foreach (Polynomial poly in groebnerBasis)
            {
                Console.WriteLine(poly.ToString());
            }
        }
    }
}