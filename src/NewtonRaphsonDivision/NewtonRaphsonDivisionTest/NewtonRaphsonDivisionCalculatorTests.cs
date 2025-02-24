using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewtonRaphsonDivision;

namespace NewtonRaphsonDivisionTest
{
    [TestClass]
    public class NewtonRaphsonDivisionCalculatorTests
    {
        private const double Tolerance = 1e-8;
        [TestMethod]
        public void TestDivide_PositiveNumbers()
        {
            double result = NewtonRaphsonDivisionCalculator.Divide(10.0, 2.0);
            Assert.AreEqual(5.0, result, Tolerance);
        }

        [TestMethod]
        public void TestDivide_NegativeNumbers()
        {
            double result = NewtonRaphsonDivisionCalculator.Divide(-10.0, 2.0);
            Assert.AreEqual(-5.0, result, Tolerance);
            result = NewtonRaphsonDivisionCalculator.Divide(10.0, -2.0);
            Assert.AreEqual(-5.0, result, Tolerance);
            result = NewtonRaphsonDivisionCalculator.Divide(-10.0, -2.0);
            Assert.AreEqual(5.0, result, Tolerance);
        }

        [TestMethod]
        public void TestDivide_ZeroNumerator()
        {
            double result = NewtonRaphsonDivisionCalculator.Divide(0.0, 5.0);
            Assert.AreEqual(0.0, result, Tolerance);
        }

        [TestMethod]
        public void TestDivide_BothZeros()
        {
            double result = NewtonRaphsonDivisionCalculator.Divide(0.0, 0.0);
            Assert.IsTrue(double.IsNaN(result));
        }

        [TestMethod]
        public void TestDivide_DivisorZero_PositiveNumerator()
        {
            double result = NewtonRaphsonDivisionCalculator.Divide(5.0, 0.0);
            Assert.IsTrue(double.IsPositiveInfinity(result));
        }

        [TestMethod]
        public void TestDivide_DivisorZero_NegativeNumerator()
        {
            double result = NewtonRaphsonDivisionCalculator.Divide(-5.0, 0.0);
            Assert.IsTrue(double.IsNegativeInfinity(result));
        }

        [TestMethod]
        public void TestDivide_NaNInput()
        {
            double result = NewtonRaphsonDivisionCalculator.Divide(double.NaN, 5.0);
            Assert.IsTrue(double.IsNaN(result));
            result = NewtonRaphsonDivisionCalculator.Divide(5.0, double.NaN);
            Assert.IsTrue(double.IsNaN(result));
        }

        [TestMethod]
        public void TestDivide_RandomValues()
        {
            System.Random random = new System.Random(12345);
            for (int i = 0; i < 100; i++)
            {
                double numerator = random.NextDouble() * 1000 - 500;
                double divisor = 0.0;
                while (divisor == 0.0)
                {
                    divisor = random.NextDouble() * 1000 - 500;
                }

                double expected = numerator / divisor;
                double actual = NewtonRaphsonDivisionCalculator.Divide(numerator, divisor);
                Assert.AreEqual(expected, actual, Math.Abs(expected) * 1e-6 + 1e-10);
            }
        }

        [TestMethod]
        public void TestDivide_CustomToleranceAndIterations()
        {
            double resultDefault = NewtonRaphsonDivisionCalculator.Divide(9.0, 3.0);
            double resultCustom = NewtonRaphsonDivisionCalculator.Divide(9.0, 3.0, 1e-15, 50);
            Assert.AreEqual(3.0, resultDefault, Tolerance);
            Assert.AreEqual(3.0, resultCustom, Tolerance);
        }

        [TestMethod]
        public void TestDivide_SmallDivisor()
        {
            double result = NewtonRaphsonDivisionCalculator.Divide(10.0, 0.5);
            Assert.AreEqual(20.0, result, Tolerance);
        }
    }
}