using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoldschmidtDivision;

namespace GoldschmidtDivisionTest
{
    [TestClass]
    public class GoldschmidtDividerTests
    {
        [TestMethod]
        public void Divide_RegularNumbers_ReturnsExpectedValue()
        {
            double dividend = 10.0;
            double divisor = 2.0;
            double expected = 5.0;
            double result = GoldschmidtDivider.Divide(dividend, divisor);
            Assert.AreEqual(expected, result, 1e-9, $"{dividend}/{divisor} should be {expected} but was {result}");
        }

        [TestMethod]
        public void Divide_DivideByZero_ReturnsNaN()
        {
            double dividend = 10.0;
            double divisor = 0.0;
            double result = GoldschmidtDivider.Divide(dividend, divisor);
            Assert.IsTrue(double.IsNaN(result), "Division by zero should return NaN.");
        }

        [TestMethod]
        public void Divide_ZeroDividend_ReturnsZero()
        {
            double dividend = 0.0;
            double divisor = 5.0;
            double expected = 0.0;
            double result = GoldschmidtDivider.Divide(dividend, divisor);
            Assert.AreEqual(expected, result, 1e-9, $"0 divided by {divisor} should be {expected}, but got {result}.");
        }

        [TestMethod]
        public void Divide_NegativeDividend_ReturnsNegativeValue()
        {
            double dividend = -10.0;
            double divisor = 2.0;
            double expected = -5.0;
            double result = GoldschmidtDivider.Divide(dividend, divisor);
            Assert.AreEqual(expected, result, 1e-9, $"Dividing {dividend} by {divisor} should yield {expected} but got {result}.");
        }

        [TestMethod]
        public void Divide_BothNegative_ReturnsPositiveValue()
        {
            double dividend = -10.0;
            double divisor = -2.0;
            double expected = 5.0;
            double result = GoldschmidtDivider.Divide(dividend, divisor);
            Assert.AreEqual(expected, result, 1e-9, $"Dividing {dividend} by {divisor} should yield {expected} but got {result}.");
        }

        [TestMethod]
        public void Divide_FractionalNumbers_ReturnsExpectedValue()
        {
            double dividend = 7.0;
            double divisor = 3.0;
            double expected = 7.0 / 3.0;
            double result = GoldschmidtDivider.Divide(dividend, divisor);
            Assert.AreEqual(expected, result, 1e-9, $"Dividing {dividend} by {divisor} should be approximately {expected} but was {result}.");
        }

        [TestMethod]
        public void Divide_RandomData_ReturnsAccurateResults()
        {
            System.Random random = new System.Random(42);
            for (int i = 0; i < 100; i++)
            {
                double dividend = random.NextDouble() * 1000.0 - 500.0;
                double divisor;
                do
                {
                    divisor = random.NextDouble() * 1000.0 - 500.0;
                }
                while (System.Math.Abs(divisor) < 1e-6);
                double expected = dividend / divisor;
                double result = GoldschmidtDivider.Divide(dividend, divisor);
                double tolerance = System.Math.Max(1e-8, System.Math.Abs(expected) * 1e-8);
                Assert.AreEqual(expected, result, tolerance, $"Random division test failed: {dividend} / {divisor} expected {expected} but got {result}");
            }
        }
    }
}