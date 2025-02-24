using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;
using BruunsFftAlgorithm;

namespace BruunsFftAlgorithmTest
{
    [TestClass]
    public class BruunsFftTests
    {
        [TestMethod]
        public void Test_NullInput()
        {
            Complex[] result = BruunsFft.Compute(null);
            Assert.AreEqual(0, result.Length, "Null input should return an empty array.");
        }

        [TestMethod]
        public void Test_EmptyInput()
        {
            double[] coefficients = new double[0];
            Complex[] result = BruunsFft.Compute(coefficients);
            // For an empty input, the algorithm pads with a zero yielding a single zero value.
            Assert.AreEqual(1, result.Length, "Empty input should return an array with one element (0).");
            AssertComplexAreEqual(Complex.Zero, result[0], 1e-8, "Empty input result should be zero.");
        }

        [TestMethod]
        public void Test_SingleCoefficient()
        {
            double[] coefficients = new double[]
            {
                5.0
            };
            Complex[] result = BruunsFft.Compute(coefficients);
            Assert.AreEqual(1, result.Length, "Single coefficient input should return an array with one element.");
            AssertComplexAreEqual(new Complex(5.0, 0.0), result[0], 1e-8, "Result should equal the single coefficient.");
        }

        [TestMethod]
        public void Test_KnownCoefficients()
        {
            // Test with coefficients that allow manual verification.
            double[] coefficients = new double[]
            {
                1.0,
                2.0,
                3.0,
                4.0
            };
            // Expected behavior:
            // m becomes 4.
            // result[0]: EvaluatePolynomial at 1 -> 1+2+3+4 = 10.
            // result[2]: EvaluatePolynomial at -1 -> 1-2+3-4 = -2.
            // For j=1 with theta = pi/2, PolynomialDivisionRemainder yields remainder (-2, -2)
            //   result[1] = (-2)*exp(i*pi/2) + (-2) = -2 - 2i.
            //   result[3] = (-2)*exp(-i*pi/2) + (-2) = -2 + 2i.
            Complex[] result = BruunsFft.Compute(coefficients);
            Assert.AreEqual(4, result.Length, "Result length should be 4.");
            Complex expected0 = new Complex(10.0, 0.0);
            Complex expected2 = new Complex(-2.0, 0.0);
            Complex expected1 = new Complex(-2.0, -2.0);
            Complex expected3 = new Complex(-2.0, 2.0);
            AssertComplexAreEqual(expected0, result[0], 1e-8, "FFT result at index 0 not as expected.");
            AssertComplexAreEqual(expected1, result[1], 1e-8, "FFT result at index 1 not as expected.");
            AssertComplexAreEqual(expected2, result[2], 1e-8, "FFT result at index 2 not as expected.");
            AssertComplexAreEqual(expected3, result[3], 1e-8, "FFT result at index 3 not as expected.");
        }

        [TestMethod]
        public void Test_NonPowerOfTwoLength()
        {
            // For n=3, the padded length m becomes 4.
            double[] coefficients = new double[]
            {
                1.0,
                2.0,
                3.0
            };
            // Expected:
            // Sum (at x=1): 1+2+3+0 = 6.
            // Evaluation at -1: 1-2+3-0 = 2.
            // For j = 1, theta = pi/2, PolynomialDivisionRemainder on padded [1,2,3,0]
            //   yields remainder (2, -2), so:
            //   result[1] = 2*exp(i*pi/2) - 2 = -2 + 2i.
            //   result[3] = 2*exp(-i*pi/2) - 2 = -2 - 2i.
            Complex[] result = BruunsFft.Compute(coefficients);
            Assert.AreEqual(4, result.Length, "Result length should be padded to 4.");
            Complex expected0 = new Complex(6.0, 0.0);
            Complex expected2 = new Complex(2.0, 0.0);
            Complex expected1 = new Complex(-2.0, 2.0);
            Complex expected3 = new Complex(-2.0, -2.0);
            AssertComplexAreEqual(expected0, result[0], 1e-8, "Index 0 incorrect.");
            AssertComplexAreEqual(expected1, result[1], 1e-8, "Index 1 incorrect.");
            AssertComplexAreEqual(expected2, result[2], 1e-8, "Index 2 incorrect.");
            AssertComplexAreEqual(expected3, result[3], 1e-8, "Index 3 incorrect.");
        }

        [TestMethod]
        public void Test_LargeRandomInput()
        {
            // Generate a large array with synthetic random data.
            Random random = new Random(42);
            int length = 1024;
            double[] coefficients = new double[length];
            double expectedSum = 0.0;
            for (int i = 0; i < length; i++)
            {
                coefficients[i] = random.NextDouble() * 100.0 - 50.0; // values in [-50, 50]
                expectedSum += coefficients[i];
            }

            Complex[] result = BruunsFft.Compute(coefficients);
            Assert.AreEqual(length, result.Length, "For power-of-two length input, output length should match.");
            // The zeroth element is the evaluation at 1 so it should equal the sum of the coefficients.
            AssertComplexAreEqual(new Complex(expectedSum, 0.0), result[0], 1e-8, "FFT sum at index 0 incorrect.");
        }

        [TestMethod]
        public void Test_AllZeros()
        {
            double[] coefficients = new double[]
            {
                0.0,
                0.0,
                0.0,
                0.0
            };
            Complex[] result = BruunsFft.Compute(coefficients);
            Assert.AreEqual(4, result.Length, "All zeros input should return length 4.");
            for (int i = 0; i < result.Length; i++)
            {
                AssertComplexAreEqual(Complex.Zero, result[i], 1e-8, "Expected zero at index " + i + " but got " + result[i] + ".");
            }
        }

        [TestMethod]
        public void Test_TwoCoefficients()
        {
            double[] coefficients = new double[]
            {
                1.0,
                2.0
            };
            Complex[] result = BruunsFft.Compute(coefficients);
            Assert.AreEqual(2, result.Length, "Two coefficients input should return length 2.");
            AssertComplexAreEqual(new Complex(3.0, 0.0), result[0], 1e-8, "Index 0 incorrect.");
            AssertComplexAreEqual(new Complex(-1.0, 0.0), result[1], 1e-8, "Index 1 incorrect.");
        }

        private static void AssertComplexAreEqual(Complex expected, Complex actual, double delta, string message)
        {
            bool isEqual = Math.Abs(expected.Real - actual.Real) < delta && Math.Abs(expected.Imaginary - actual.Imaginary) < delta;
            Assert.IsTrue(isEqual, message + " Expected: " + expected.ToString() + ", Actual: " + actual.ToString());
        }
    }
}