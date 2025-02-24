using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CantorZassenhausAlgorithm;

namespace CantorZassenhausAlgorithmTest
{
    [TestClass]
    public class CantorZassenhausTests
    {
        private bool PolynomialsEqual(Polynomial a, Polynomial b)
        {
            if (a.Modulus != b.Modulus)
            {
                return false;
            }

            if (a.Coefficients.Count != b.Coefficients.Count)
            {
                return false;
            }

            for (int i = 0; i < a.Coefficients.Count; i++)
            {
                if (a.Coefficients[i] != b.Coefficients[i])
                {
                    return false;
                }
            }

            return true;
        }

        private Polynomial MultiplyList(List<Polynomial> factors)
        {
            Polynomial product = new Polynomial(new List<int> { 1 }, factors[0].Modulus);
            foreach (Polynomial factor in factors)
            {
                product = Polynomial.Multiply(product, factor);
            }

            return product;
        }

        [TestMethod]
        public void TestConstantPolynomialFactorization()
        {
            Polynomial f = new Polynomial(new List<int> { 4 }, 7);
            List<Polynomial> factors = CantorZassenhaus.Factor(f);
            Assert.AreEqual(1, factors.Count, "Constant polynomial should result in one factor.");
            Assert.IsTrue(PolynomialsEqual(f, factors[0]), "Factorization of constant polynomial should return the constant itself.");
        }

        [TestMethod]
        public void TestLinearPolynomialFactorization()
        {
            // x - 1 mod 7 is represented as [6, 1] because -1 mod 7 = 6.
            Polynomial f = new Polynomial(new List<int> { 6, 1 }, 7);
            List<Polynomial> factors = CantorZassenhaus.Factor(f);
            Assert.AreEqual(1, factors.Count, "Linear polynomial should result in one factor.");
            Assert.IsTrue(PolynomialsEqual(f, factors[0]), "Factorization of linear polynomial should return the polynomial itself.");
        }

        [TestMethod]
        public void TestQuadraticFactorization()
        {
            // (x - 1)(x - 3) mod 7 yields x^2 - 4x + 3.
            // Represented as [3, 3, 1] since -4 mod 7 equals 3.
            Polynomial f = new Polynomial(new List<int> { 3, 3, 1 }, 7);
            List<Polynomial> factors = CantorZassenhaus.Factor(f);
            Polynomial product = MultiplyList(factors);
            Assert.IsTrue(PolynomialsEqual(f, product), "Product of factors should equal the original polynomial.");
        }

        [TestMethod]
        public void TestSyntheticFactorization()
        {
            // Construct a polynomial as product of three linear factors over modulus 11:
            // (x - 1), (x - 3), (x - 5).
            // x - 1 is represented as [10, 1] because -1 mod 11 = 10,
            // x - 3 as [8, 1] because -3 mod 11 = 8,
            // x - 5 as [6, 1] because -5 mod 11 = 6.
            Polynomial f1 = new Polynomial(new List<int> { 10, 1 }, 11);
            Polynomial f2 = new Polynomial(new List<int> { 8, 1 }, 11);
            Polynomial f3 = new Polynomial(new List<int> { 6, 1 }, 11);
            Polynomial f = Polynomial.Multiply(Polynomial.Multiply(f1, f2), f3);
            List<Polynomial> factors = CantorZassenhaus.Factor(f);
            Polynomial product = MultiplyList(factors);
            Assert.IsTrue(PolynomialsEqual(f, product), "Product of factors should equal the original synthetic polynomial.");
        }
    }
}