using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CantorZassenhausAlgorithm;

namespace CantorZassenhausAlgorithmTest
{
    [TestClass]
    public class PolynomialTests
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

        [TestMethod]
        public void TestAddition()
        {
            // a = 1 + 2x + 3x^2, b = 6 + 5x; modulus = 7.
            Polynomial a = new Polynomial(new List<int> { 1, 2, 3 }, 7);
            Polynomial b = new Polynomial(new List<int> { 6, 5 }, 7);
            // Expected: [0, 0, 3] because (1+6) mod 7 = 0 and (2+5) mod 7 = 0.
            Polynomial expected = new Polynomial(new List<int> { 0, 0, 3 }, 7);
            Polynomial result = Polynomial.Add(a, b);
            Assert.IsTrue(PolynomialsEqual(expected, result), "Addition result incorrect.");
        }

        [TestMethod]
        public void TestSubtraction()
        {
            // a = 3 + 4x + 5x^2, b = 1 + 2x; modulus = 7.
            Polynomial a = new Polynomial(new List<int> { 3, 4, 5 }, 7);
            Polynomial b = new Polynomial(new List<int> { 1, 2 }, 7);
            // Expected: [2, 2, 5] because (3-1)=2, (4-2)=2.
            Polynomial expected = new Polynomial(new List<int> { 2, 2, 5 }, 7);
            Polynomial result = Polynomial.Subtract(a, b);
            Assert.IsTrue(PolynomialsEqual(expected, result), "Subtraction result incorrect.");
        }

        [TestMethod]
        public void TestMultiplication()
        {
            // Multiply (1 + x) and (1 + 2x) mod 7.
            Polynomial a = new Polynomial(new List<int> { 1, 1 }, 7);
            Polynomial b = new Polynomial(new List<int> { 1, 2 }, 7);
            // Expected: (1 + 3x + 2x^2) represented as [1, 3, 2].
            Polynomial expected = new Polynomial(new List<int> { 1, 3, 2 }, 7);
            Polynomial result = Polynomial.Multiply(a, b);
            Assert.IsTrue(PolynomialsEqual(expected, result), "Multiplication result incorrect.");
        }

        [TestMethod]
        public void TestDivision()
        {
            // Divide (x^2 - 4x + 3) by (x - 1) over mod 7.
            // (x^2 - 4x + 3) is represented as [3, 3, 1] and (x - 1) as [6, 1] since -1 mod 7 = 6.
            // Expected quotient: (x - 3) which is [4, 1] because -3 mod 7 = 4, and remainder 0.
            Polynomial dividend = new Polynomial(new List<int> { 3, 3, 1 }, 7);
            Polynomial divisor = new Polynomial(new List<int> { 6, 1 }, 7);
            Tuple<Polynomial, Polynomial> division = Polynomial.Divide(dividend, divisor);
            Polynomial quotient = division.Item1;
            Polynomial remainder = division.Item2;
            Polynomial expectedQuotient = new Polynomial(new List<int> { 4, 1 }, 7);
            Polynomial expectedRemainder = new Polynomial(new List<int> { 0 }, 7);
            Assert.IsTrue(PolynomialsEqual(expectedQuotient, quotient), "Division quotient incorrect.");
            Assert.IsTrue(PolynomialsEqual(expectedRemainder, remainder), "Division remainder should be zero.");
        }

        [TestMethod]
        public void TestGCD()
        {
            // Test GCD of f = (x - 1)(x - 2) and g = (x - 1) over mod 7.
            // (x - 1) is [6, 1] and (x - 2) is [5, 1] because -2 mod 7 = 5.
            Polynomial f1 = new Polynomial(new List<int> { 6, 1 }, 7);
            Polynomial f2 = new Polynomial(new List<int> { 5, 1 }, 7);
            Polynomial f = Polynomial.Multiply(f1, f2);
            Polynomial g = f1;
            Polynomial gcd = Polynomial.GCD(f, g);
            Assert.IsTrue(PolynomialsEqual(f1, gcd), "GCD computation incorrect.");
        }

        [TestMethod]
        public void TestPow()
        {
            // Compute (1 + x)^3 mod 7.
            Polynomial a = new Polynomial(new List<int> { 1, 1 }, 7);
            Polynomial power = a.Pow(3);
            // (1+x)^3 = 1 + 3x + 3x^2 + x^3.
            Polynomial expected = new Polynomial(new List<int> { 1, 3, 3, 1 }, 7);
            Assert.IsTrue(PolynomialsEqual(expected, power), "Power computation incorrect.");
        }

        [TestMethod]
        public void TestModExp()
        {
            // Compute (1+x)^4 mod (x^2+1) over mod 7.
            // Note: In the ring modulo (x^2+1), substitute x^2 = -1.
            // (1+x)^4 expands to 1 + 4x + 6x^2 + 4x^3 + x^4.
            // Rewriting using x^2 = -1 and x^3 = -x, x^4 = 1, we get 1+4x-6-4x+1 = -4 mod 7 which is 3.
            Polynomial a = new Polynomial(new List<int> { 1, 1 }, 7);
            Polynomial modulusPoly = new Polynomial(new List<int> { 1, 0, 1 }, 7);
            Polynomial result = a.ModExp(4, modulusPoly);
            Polynomial expected = new Polynomial(new List<int> { 3 }, 7);
            Assert.IsTrue(PolynomialsEqual(expected, result), "ModExp computation incorrect.");
        }
    }
}