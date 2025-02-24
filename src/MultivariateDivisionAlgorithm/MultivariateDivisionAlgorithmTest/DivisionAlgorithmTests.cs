using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultivariateDivisionAlgorithm;

namespace MultivariateDivisionAlgorithmTest
{
    [TestClass]
    public class DivisionAlgorithmTests
    {
        private static bool AreMonomialsEqual(Monomial a, Monomial b)
        {
            if (a.Exponents.Count != b.Exponents.Count)
            {
                return false;
            }

            foreach (KeyValuePair<string, int> kv in a.Exponents)
            {
                int bValue = 0;
                if (!b.Exponents.TryGetValue(kv.Key, out bValue))
                {
                    return false;
                }

                if (kv.Value != bValue)
                {
                    return false;
                }
            }

            return true;
        }

        private static void AssertPolynomialsEqual(Polynomial expected, Polynomial actual)
        {
            expected.Simplify();
            actual.Simplify();
            Assert.AreEqual(expected.Terms.Count, actual.Terms.Count, "Mismatch in term count.");
            foreach (Term expectedTerm in expected.Terms)
            {
                bool found = false;
                foreach (Term actualTerm in actual.Terms)
                {
                    if (AreMonomialsEqual(expectedTerm.Monomial, actualTerm.Monomial) && Math.Abs(expectedTerm.Coefficient - actualTerm.Coefficient) < 1e-10)
                    {
                        found = true;
                        break;
                    }
                }

                Assert.IsTrue(found, "Expected term " + expectedTerm.ToString() + " not found.");
            }
        }

        [TestMethod]
        public void TestDivisionExact()
        {
            // f(x) = x^2 + 2x + 1, divisor g(x) = x + 1.
            // Expected: quotient = x + 1, remainder = 0.
            Dictionary<string, int> expoX2 = new Dictionary<string, int>
            {
                {
                    "x",
                    2
                }
            };
            Dictionary<string, int> expoX1 = new Dictionary<string, int>
            {
                {
                    "x",
                    1
                }
            };
            Dictionary<string, int> expoConst = new Dictionary<string, int>();
            Term term1 = new Term(1, new Monomial(expoX2));
            Term term2 = new Term(2, new Monomial(expoX1));
            Term term3 = new Term(1, new Monomial(expoConst));
            Polynomial f = new Polynomial(new List<Term> { term1, term2, term3 });
            Term gTerm1 = new Term(1, new Monomial(expoX1));
            Term gTerm2 = new Term(1, new Monomial(expoConst));
            Polynomial g = new Polynomial(new List<Term> { gTerm1, gTerm2 });
            List<Polynomial> divisors = new List<Polynomial>
            {
                g
            };
            List<string> variableOrder = new List<string>
            {
                "x"
            };
            DivisionResult result = DivisionAlgorithm.Divide(f, divisors, variableOrder);
            Polynomial expectedQuotient = new Polynomial(new List<Term> { new Term(1, new Monomial(expoX1)), new Term(1, new Monomial(expoConst)) });
            Polynomial expectedRemainder = Polynomial.Zero;
            AssertPolynomialsEqual(expectedQuotient, result.Quotients[0]);
            Assert.IsTrue(result.Remainder.IsZero(), "Expected zero remainder.");
        }

        [TestMethod]
        public void TestDivisionNonExact()
        {
            // f(x) = x^2 + 1, divisor g(x) = x + 1.
            // Expected: quotient = x - 1, remainder = 2.
            Dictionary<string, int> expoX2 = new Dictionary<string, int>
            {
                {
                    "x",
                    2
                }
            };
            Dictionary<string, int> expoX1 = new Dictionary<string, int>
            {
                {
                    "x",
                    1
                }
            };
            Dictionary<string, int> expoConst = new Dictionary<string, int>();
            Term term1 = new Term(1, new Monomial(expoX2));
            Term term2 = new Term(1, new Monomial(expoConst));
            Polynomial f = new Polynomial(new List<Term> { term1, term2 });
            Term gTerm1 = new Term(1, new Monomial(expoX1));
            Term gTerm2 = new Term(1, new Monomial(expoConst));
            Polynomial g = new Polynomial(new List<Term> { gTerm1, gTerm2 });
            List<Polynomial> divisors = new List<Polynomial>
            {
                g
            };
            List<string> variableOrder = new List<string>
            {
                "x"
            };
            DivisionResult result = DivisionAlgorithm.Divide(f, divisors, variableOrder);
            Polynomial expectedQuotient = new Polynomial(new List<Term> { new Term(1, new Monomial(expoX1)), new Term(-1, new Monomial(expoConst)) });
            Polynomial expectedRemainder = new Polynomial(new List<Term> { new Term(2, new Monomial(expoConst)) });
            AssertPolynomialsEqual(expectedQuotient, result.Quotients[0]);
            AssertPolynomialsEqual(expectedRemainder, result.Remainder);
        }

        [TestMethod]
        public void TestDivisionZeroDividend()
        {
            // f(x) = 0, divisor g(x) = x + 1.
            // Expected: quotient = 0, remainder = 0.
            Dictionary<string, int> expoX1 = new Dictionary<string, int>
            {
                {
                    "x",
                    1
                }
            };
            Dictionary<string, int> expoConst = new Dictionary<string, int>();
            Polynomial f = Polynomial.Zero;
            Term gTerm1 = new Term(1, new Monomial(expoX1));
            Term gTerm2 = new Term(1, new Monomial(expoConst));
            Polynomial g = new Polynomial(new List<Term> { gTerm1, gTerm2 });
            List<Polynomial> divisors = new List<Polynomial>
            {
                g
            };
            List<string> variableOrder = new List<string>
            {
                "x"
            };
            DivisionResult result = DivisionAlgorithm.Divide(f, divisors, variableOrder);
            Assert.IsTrue(result.Quotients[0].IsZero(), "Expected zero quotient.");
            Assert.IsTrue(result.Remainder.IsZero(), "Expected zero remainder.");
        }

        [TestMethod]
        public void TestDivisionWithZeroDivisor()
        {
            // f(x) = x^2 + x, divisors: [0, x + 1].
            // Expected: quotient[0] remains 0; quotient[1] = x, remainder = 0.
            Dictionary<string, int> expoX2 = new Dictionary<string, int>
            {
                {
                    "x",
                    2
                }
            };
            Dictionary<string, int> expoX1 = new Dictionary<string, int>
            {
                {
                    "x",
                    1
                }
            };
            Dictionary<string, int> expoConst = new Dictionary<string, int>();
            Term term1 = new Term(1, new Monomial(expoX2));
            Term term2 = new Term(1, new Monomial(expoX1));
            Polynomial f = new Polynomial(new List<Term> { term1, term2 });
            Polynomial zeroPoly = Polynomial.Zero;
            Term gTerm1 = new Term(1, new Monomial(expoX1));
            Term gTerm2 = new Term(1, new Monomial(expoConst));
            Polynomial g = new Polynomial(new List<Term> { gTerm1, gTerm2 });
            List<Polynomial> divisors = new List<Polynomial>
            {
                zeroPoly,
                g
            };
            List<string> variableOrder = new List<string>
            {
                "x"
            };
            DivisionResult result = DivisionAlgorithm.Divide(f, divisors, variableOrder);
            Assert.IsTrue(result.Quotients[0].IsZero(), "Expected zero quotient for zero divisor.");
            Polynomial expectedQuotient = new Polynomial(new List<Term> { new Term(1, new Monomial(expoX1)) });
            AssertPolynomialsEqual(expectedQuotient, result.Quotients[1]);
            Assert.IsTrue(result.Remainder.IsZero(), "Expected zero remainder.");
        }

        [TestMethod]
        public void TestDivisionLargeSynthetic()
        {
            // Synthetic test: generate a random polynomial in variables x and y.
            // Divisors: d1 = x + 1, d2 = y + 1.
            // Verify that no term in the remainder is divisible by the leading term of any nonzero divisor.
            Random random = new Random(42);
            List<Term> terms = new List<Term>();
            for (int i = 0; i < 20; i++)
            {
                double coeff = (random.NextDouble() * 20.0) - 10.0;
                int expX = random.Next(0, 4);
                int expY = random.Next(0, 4);
                Dictionary<string, int> exponents = new Dictionary<string, int>();
                if (expX > 0)
                {
                    exponents.Add("x", expX);
                }

                if (expY > 0)
                {
                    exponents.Add("y", expY);
                }

                terms.Add(new Term(coeff, new Monomial(exponents)));
            }

            Polynomial f = new Polynomial(terms);
            Dictionary<string, int> expoX1 = new Dictionary<string, int>
            {
                {
                    "x",
                    1
                }
            };
            Dictionary<string, int> expoY1 = new Dictionary<string, int>
            {
                {
                    "y",
                    1
                }
            };
            Dictionary<string, int> expoConst = new Dictionary<string, int>();
            // Divisor d1: x + 1.
            Polynomial d1 = new Polynomial(new List<Term> { new Term(1, new Monomial(expoX1)), new Term(1, new Monomial(expoConst)) });
            // Divisor d2: y + 1.
            Polynomial d2 = new Polynomial(new List<Term> { new Term(1, new Monomial(expoY1)), new Term(1, new Monomial(expoConst)) });
            List<Polynomial> divisors = new List<Polynomial>
            {
                d1,
                d2
            };
            List<string> variableOrder = new List<string>
            {
                "x",
                "y"
            };
            DivisionResult result = DivisionAlgorithm.Divide(f, divisors, variableOrder);
            // Verify that for each divisor, no term in the remainder is divisible by its leading term.
            foreach (Polynomial divisor in divisors)
            {
                if (!divisor.IsZero())
                {
                    Term? ltDivisor = divisor.GetLeadingTerm(variableOrder);
                    if (ltDivisor != null)
                    {
                        foreach (Term rTerm in result.Remainder.Terms)
                        {
                            bool divisible = ltDivisor.Monomial.Divides(rTerm.Monomial);
                            Assert.IsFalse(divisible, "Remainder term " + rTerm.ToString() + " is divisible by divisor's leading term.");
                        }
                    }
                }
            }
        }
    }
}