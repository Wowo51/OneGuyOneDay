using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using FaugereF4Algorithm;

namespace FaugereF4AlgorithmTest
{
    [TestClass]
    public class FaugereF4AlgorithmTests
    {
        [TestMethod]
        public void TestComputeGroebnerBasis_Basic()
        {
            List<Term> terms1 = new List<Term>();
            terms1.Add(new Term(1.0, new Monomial(new Dictionary<string, int> { { "x", 2 } })));
            terms1.Add(new Term(1.0, new Monomial(new Dictionary<string, int> { { "y", 2 } })));
            terms1.Add(new Term(-1.0, new Monomial(new Dictionary<string, int>())));
            Polynomial p1 = new Polynomial(terms1);
            List<Term> terms2 = new List<Term>();
            terms2.Add(new Term(1.0, new Monomial(new Dictionary<string, int> { { "x", 2 } })));
            terms2.Add(new Term(-1.0, new Monomial(new Dictionary<string, int> { { "y", 1 } })));
            Polynomial p2 = new Polynomial(terms2);
            List<Polynomial> generators = new List<Polynomial>();
            generators.Add(p1);
            generators.Add(p2);
            List<Polynomial> basis = F4Algorithm.ComputeGroebnerBasis(generators);
            Assert.IsNotNull(basis);
            Assert.IsTrue(basis.Count >= 2);
            foreach (Polynomial poly in basis)
            {
                Assert.IsFalse(poly.IsZero());
            }
        }

        [TestMethod]
        public void TestComputeGroebnerBasisF5_Basic()
        {
            List<Term> terms1 = new List<Term>();
            terms1.Add(new Term(1.0, new Monomial(new Dictionary<string, int> { { "x", 2 } })));
            terms1.Add(new Term(1.0, new Monomial(new Dictionary<string, int> { { "y", 2 } })));
            terms1.Add(new Term(-1.0, new Monomial(new Dictionary<string, int>())));
            Polynomial p1 = new Polynomial(terms1);
            List<Term> terms2 = new List<Term>();
            terms2.Add(new Term(1.0, new Monomial(new Dictionary<string, int> { { "x", 2 } })));
            terms2.Add(new Term(-1.0, new Monomial(new Dictionary<string, int> { { "y", 1 } })));
            Polynomial p2 = new Polynomial(terms2);
            List<Polynomial> generators = new List<Polynomial>();
            generators.Add(p1);
            generators.Add(p2);
            List<Polynomial> basisF5 = F4Algorithm.ComputeGroebnerBasisF5(generators);
            Assert.IsNotNull(basisF5);
            Assert.IsTrue(basisF5.Count >= 2);
        }

        [TestMethod]
        public void TestPolynomialArithmetic()
        {
            List<Term> termsA = new List<Term>();
            termsA.Add(new Term(2.0, new Monomial(new Dictionary<string, int> { { "x", 1 } })));
            Polynomial polyA = new Polynomial(termsA);
            List<Term> termsB = new List<Term>();
            termsB.Add(new Term(3.0, new Monomial(new Dictionary<string, int> { { "x", 1 } })));
            Polynomial polyB = new Polynomial(termsB);
            Polynomial sum = polyA.Add(polyB);
            Assert.AreEqual("5*x", sum.ToString());
            Polynomial diff = polyB.Subtract(polyA);
            Assert.AreEqual("x", diff.ToString());
            Polynomial product = polyA.Multiply(polyB);
            Assert.AreEqual("6*x^2", product.ToString());
        }

        [TestMethod]
        public void TestReduction()
        {
            List<Term> terms = new List<Term>();
            terms.Add(new Term(2.0, new Monomial(new Dictionary<string, int> { { "x", 1 } })));
            terms.Add(new Term(4.0, new Monomial(new Dictionary<string, int>())));
            Polynomial poly = new Polynomial(terms);
            List<Term> termsDivisor = new List<Term>();
            termsDivisor.Add(new Term(1.0, new Monomial(new Dictionary<string, int> { { "x", 1 } })));
            Polynomial divisor = new Polynomial(termsDivisor);
            List<Polynomial> basis = new List<Polynomial>();
            basis.Add(divisor);
            Polynomial reduced = poly.Reduce(basis);
            Assert.AreEqual("4", reduced.ToString());
        }

        [TestMethod]
        public void TestSyntheticDataGeneration()
        {
            System.Random rand = new System.Random(42);
            int numPolynomials = 10;
            int maxTerms = 5;
            List<Polynomial> polynomials = new List<Polynomial>();
            for (int i = 0; i < numPolynomials; i++)
            {
                List<Term> terms = new List<Term>();
                int numTerms = rand.Next(1, maxTerms + 1);
                for (int j = 0; j < numTerms; j++)
                {
                    double coeff = rand.NextDouble() * 10 - 5;
                    Dictionary<string, int> exp = new Dictionary<string, int>();
                    int expX = rand.Next(0, 3);
                    int expY = rand.Next(0, 3);
                    if (expX > 0)
                    {
                        exp["x"] = expX;
                    }

                    if (expY > 0)
                    {
                        exp["y"] = expY;
                    }

                    terms.Add(new Term(coeff, new Monomial(exp)));
                }

                Polynomial poly = new Polynomial(terms);
                polynomials.Add(poly);
            }

            List<Polynomial> basis = F4Algorithm.ComputeGroebnerBasis(polynomials);
            Assert.IsNotNull(basis);
        }
    }
}