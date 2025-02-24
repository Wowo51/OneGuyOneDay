using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FastFourierTransform;

namespace FastFourierTransformTest
{
    [TestClass]
    public class FFTTests
    {
        private const double Tolerance = 1e-9;
        [TestMethod]
        public void TestCompute_NullInput()
        {
            Complex[] result = FFT.Compute(null);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void TestCompute_EmptyInput()
        {
            Complex[] input = new Complex[0];
            Complex[] result = FFT.Compute(input);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void TestCompute_SingleElement()
        {
            Complex[] input = new Complex[]
            {
                new Complex(5, 0)
            };
            Complex[] result = FFT.Compute(input);
            Assert.AreEqual(1, result.Length);
            AssertComplexEquals(input[0], result[0]);
        }

        [TestMethod]
        public void TestCompute_NonPowerOfTwo()
        {
            Complex[] input = new Complex[]
            {
                new Complex(1, 0),
                new Complex(2, 0),
                new Complex(3, 0)
            };
            Complex[] result = FFT.Compute(input);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void TestCompute_TwoElements()
        {
            Complex a = new Complex(1, 0);
            Complex b = new Complex(2, 0);
            Complex[] input = new Complex[]
            {
                a,
                b
            };
            Complex[] expected = new Complex[]
            {
                a + b,
                a - b
            };
            Complex[] result = FFT.Compute(input);
            Assert.AreEqual(2, result.Length);
            AssertComplexEquals(expected[0], result[0]);
            AssertComplexEquals(expected[1], result[1]);
        }

        [TestMethod]
        public void TestCompute_Impulse()
        {
            Complex[] input = new Complex[]
            {
                new Complex(1, 0),
                new Complex(0, 0),
                new Complex(0, 0),
                new Complex(0, 0)
            };
            Complex[] result = FFT.Compute(input);
            Assert.AreEqual(4, result.Length);
            for (int i = 0; i < 4; i++)
            {
                AssertComplexEquals(new Complex(1, 0), result[i]);
            }
        }

        [TestMethod]
        public void TestCompute_Symmetry()
        {
            Complex[] input = new Complex[]
            {
                new Complex(1, 0),
                new Complex(2, 0),
                new Complex(3, 0),
                new Complex(4, 0)
            };
            Complex[] result = FFT.Compute(input);
            Assert.AreEqual(4, result.Length);
            AssertComplexEquals(Complex.Conjugate(result[1]), result[3]);
        }

        [TestMethod]
        public void TestCompute_LargeDataSet()
        {
            int size = 1024;
            Complex[] input = new Complex[size];
            for (int i = 0; i < size; i++)
            {
                double value = Math.Sin(2 * Math.PI * i / size);
                input[i] = new Complex(value, 0);
            }

            Complex[] result = FFT.Compute(input);
            Assert.AreEqual(size, result.Length);
            double expectedSum = 0;
            for (int i = 0; i < size; i++)
            {
                expectedSum += Math.Sin(2 * Math.PI * i / size);
            }

            AssertComplexEquals(new Complex(expectedSum, 0), result[0]);
        }

        private void AssertComplexEquals(Complex expected, Complex actual)
        {
            Assert.AreEqual(expected.Real, actual.Real, Tolerance, "Real parts differ.");
            Assert.AreEqual(expected.Imaginary, actual.Imaginary, Tolerance, "Imaginary parts differ.");
        }
    }
}