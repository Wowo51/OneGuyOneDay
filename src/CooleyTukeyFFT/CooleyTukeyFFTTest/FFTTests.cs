using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CooleyTukeyFFT;

namespace CooleyTukeyFFTTest
{
    [TestClass]
    public class FFTTests
    {
        private const double Tolerance = 1e-10;
        private static void AssertComplexAreEqual(Complex expected, Complex actual)
        {
            Assert.IsTrue(Math.Abs(expected.Real - actual.Real) < Tolerance && Math.Abs(expected.Imaginary - actual.Imaginary) < Tolerance, $"Expected complex {expected} but got {actual}");
        }

        [TestMethod]
        public void FFT_NullInput_ReturnsEmptyArray()
        {
            Complex[] result = FFT.Compute(null);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void FFT_EmptyInput_ReturnsEmptyArray()
        {
            Complex[] input = new Complex[0];
            Complex[] result = FFT.Compute(input);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void FFT_DeltaFunction_PowerOfTwoInput()
        {
            Complex[] input = new Complex[]
            {
                new Complex(1, 0),
                new Complex(0, 0),
                new Complex(0, 0),
                new Complex(0, 0)
            };
            Complex[] result = FFT.Compute(input);
            Complex[] expected = new Complex[]
            {
                new Complex(1, 0),
                new Complex(1, 0),
                new Complex(1, 0),
                new Complex(1, 0)
            };
            Assert.AreEqual(expected.Length, result.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                AssertComplexAreEqual(expected[i], result[i]);
            }
        }

        [TestMethod]
        public void FFT_NonPowerOfTwoInput_PaddingTest()
        {
            // Input length 3 will be padded to length 4.
            Complex[] input = new Complex[]
            {
                new Complex(1, 0),
                new Complex(2, 0),
                new Complex(3, 0)
            };
            Complex[] result = FFT.Compute(input);
            Assert.AreEqual(4, result.Length);
            // Expected FFT for padded input [1,2,3,0]:
            // k = 0: 1 + 2 + 3 + 0 = 6
            // k = 1: 1 + 2*exp(-i*pi/2) + 3*exp(-i*pi) + 0 = -2 - 2i
            // k = 2: 1 + 2*exp(-i*pi) + 3*exp(-i*2pi) + 0 = 2
            // k = 3: 1 + 2*exp(-i*3pi/2) + 3*exp(-i*3pi) + 0 = -2 + 2i
            Complex[] expected = new Complex[]
            {
                new Complex(6, 0),
                new Complex(-2, -2),
                new Complex(2, 0),
                new Complex(-2, 2)
            };
            for (int i = 0; i < expected.Length; i++)
            {
                AssertComplexAreEqual(expected[i], result[i]);
            }
        }

        [TestMethod]
        public void FFT_LargeInput_SyntheticDataTest()
        {
            // Generate an array of 1000 random Complex numbers.
            Complex[] input = new Complex[1000];
            Random rand = new Random(42);
            for (int i = 0; i < input.Length; i++)
            {
                double realPart = rand.NextDouble() * 100 - 50;
                double imaginaryPart = rand.NextDouble() * 100 - 50;
                input[i] = new Complex(realPart, imaginaryPart);
            }

            Complex[] result = FFT.Compute(input);
            // The padded length should be the next power of two: 1024.
            Assert.AreEqual(1024, result.Length);
        }

        [TestMethod]
        public void FFT_SingleElement_ReturnsSameElement()
        {
            Complex[] input = new Complex[]
            {
                new Complex(5, -3)
            };
            Complex[] result = FFT.Compute(input);
            Assert.AreEqual(1, result.Length);
            AssertComplexAreEqual(input[0], result[0]);
        }

        [TestMethod]
        public void FFT_PowerOfTwoInput_ReturnsCorrectResult()
        {
            // Testing FFT on a power-of-two input array [1, 2, 3, 4]
            Complex[] input = new Complex[]
            {
                new Complex(1, 0),
                new Complex(2, 0),
                new Complex(3, 0),
                new Complex(4, 0)
            };
            Complex[] result = FFT.Compute(input);
            Complex[] expected = new Complex[]
            {
                new Complex(10, 0), // k = 0: 1+2+3+4 = 10
                new Complex(-2, 2), // k = 1: -2+2i
                new Complex(-2, 0), // k = 2: -2
                new Complex(-2, -2) // k = 3: -2-2i
            };
            Assert.AreEqual(expected.Length, result.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                AssertComplexAreEqual(expected[i], result[i]);
            }
        }
    }
}