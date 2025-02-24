using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuchbergerAlgorithm;

namespace BuchbergerAlgorithmTest
{
    [TestClass]
    public class GroebnerBasisTests
    {
        [TestMethod]
        public void TestPolynomialAddition()
        {
            SortedDictionary<string, int> expX2 = new SortedDictionary<string, int>();
            expX2.Add("x", 2);
            Term term1 = new Term(1.0, expX2);
            Term term2 = new Term(2.0, expX2);
            List<Term> terms1 = new List<Term>()
            {
                term1
            };
            List<Term> terms2 = new List<Term>()
            {
                term2
            };
            Polynomial p1 = new Polynomial(terms1);
            Polynomial p2 = new Polynomial(terms2);
            Polynomial sum = p1.Add(p2);
            Assert.AreEqual(1, sum.Terms.Count, "Addition result should have one term.");
            Assert.IsTrue(Math.Abs(sum.Terms[0].Coefficient - 3.0) < 1e-10, "Coefficient mismatch.");
            Assert.IsTrue(sum.Terms[0].Exponents.ContainsKey("x") && sum.Terms[0].Exponents["x"] == 2, "Exponent mismatch.");
        }

        [TestMethod]
        public void TestPolynomialSubtraction()
        {
            SortedDictionary<string, int> expX3 = new SortedDictionary<string, int>();
            expX3.Add("x", 3);
            Polynomial p1 = new Polynomial(new List<Term>() { new Term(5.0, expX3) });
            Polynomial p2 = new Polynomial(new List<Term>() { new Term(2.0, expX3) });
            Polynomial diff = p1.Subtract(p2);
            Assert.AreEqual(1, diff.Terms.Count, "Subtraction result should have one term.");
            Assert.IsTrue(Math.Abs(diff.Terms[0].Coefficient - 3.0) < 1e-10, "Subtraction coefficient mismatch.");
        }

        [TestMethod]
        public void TestPolynomialMultiplication()
        {
            SortedDictionary<string, int> expX = new SortedDictionary<string, int>();
            expX.Add("x", 1);
            Polynomial p1 = new Polynomial(new List<Term>() { new Term(1.0, expX) });
            Polynomial p2 = new Polynomial(new List<Term>() { new Term(1.0, expX) });
            Polynomial product = p1.Multiply(p2);
            Assert.AreEqual(1, product.Terms.Count, "Multiplication result should have one term.");
            Assert.IsTrue(product.Terms[0].Exponents.ContainsKey("x") && product.Terms[0].Exponents["x"] == 2, "Multiplication exponent mismatch.");
            Assert.IsTrue(Math.Abs(product.Terms[0].Coefficient - 1.0) < 1e-10, "Multiplication coefficient mismatch.");
        }

        [TestMethod]
        public void TestPolynomialNormalize()
        {
            SortedDictionary<string, int> expX2 = new SortedDictionary<string, int>();
            expX2.Add("x", 2);
            SortedDictionary<string, int> expY2 = new SortedDictionary<string, int>();
            expY2.Add("y", 2);
            Polynomial p = new Polynomial(new List<Term>() { new Term(2.0, expX2), new Term(2.0, expY2) });
            Polynomial normalized = p.Normalize();
            Assert.AreEqual(2, normalized.Terms.Count, "Normalization: number of terms mismatch.");
            foreach (Term term in normalized.Terms)
            {
                Assert.IsTrue(Math.Abs(term.Coefficient - 1.0) < 1e-10, "Normalization: coefficient is not 1.");
            }
        }

        [TestMethod]
        public void TestSPolynomial()
        {
            SortedDictionary<string, int> expX = new SortedDictionary<string, int>();
            expX.Add("x", 1);
            SortedDictionary<string, int> expX2 = new SortedDictionary<string, int>();
            expX2.Add("x", 2);
            Polynomial f = new Polynomial(new List<Term>() { new Term(1.0, expX) });
            Polynomial g = new Polynomial(new List<Term>() { new Term(1.0, expX2) });
            Polynomial sPoly = Polynomial.SPolynomial(f, g);
            Assert.IsTrue(sPoly.IsZero(), "S-Polynomial should be zero.");
        }

        [TestMethod]
        public void TestPolynomialReduce()
        {
            SortedDictionary<string, int> expX2 = new SortedDictionary<string, int>();
            expX2.Add("x", 2);
            Polynomial f = new Polynomial(new List<Term>() { new Term(1.0, expX2) });
            Polynomial g = new Polynomial(new List<Term>() { new Term(1.0, expX2) });
            List<Polynomial> basis = new List<Polynomial>()
            {
                g
            };
            Polynomial remainder = Polynomial.Reduce(f, basis);
            Assert.IsTrue(remainder.IsZero(), "Reduction failed to produce zero remainder.");
        }

        [TestMethod]
        public void TestComputeGroebnerBasisSynthetic()
        {
            Random random = new Random(42);
            List<Polynomial> basis = new List<Polynomial>();
            for (int i = 0; i < 10; i++)
            {
                SortedDictionary<string, int> exp = new SortedDictionary<string, int>();
                int power = random.Next(0, 4);
                if (power > 0)
                {
                    exp.Add("x", power);
                }

                double coeff = random.NextDouble() * 10;
                Polynomial poly = new Polynomial(new List<Term>() { new Term(coeff, exp) });
                double constant = random.NextDouble() * 5;
                Polynomial poly2 = new Polynomial(new List<Term>() { new Term(constant, new SortedDictionary<string, int>()) });
                Polynomial combined = poly.Add(poly2);
                basis.Add(combined);
            }

            List<Polynomial> gb = GroebnerBasisCalculator.ComputeGroebnerBasis(basis);
            foreach (Polynomial poly in gb)
            {
                Polynomial remainder = Polynomial.Reduce(poly, gb);
                Assert.IsTrue(remainder.IsZero(), "Groebner basis polynomial did not reduce to zero.");
            }
        }

        [TestMethod]
        public void TestZeroPolynomial()
        {
            Polynomial zero = new Polynomial(new List<Term>());
            Assert.IsTrue(zero.IsZero(), "Zero polynomial is not detected.");
            Polynomial another = new Polynomial(new List<Term>());
            Polynomial sum = zero.Add(another);
            Assert.IsTrue(sum.IsZero(), "Zero polynomial addition failed.");
        }
    }
}