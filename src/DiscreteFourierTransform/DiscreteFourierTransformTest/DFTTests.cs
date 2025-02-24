using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DiscreteFourierTransform;

namespace DiscreteFourierTransformTest
{
    [TestClass]
    public class DFTTests
    {
        private const double Tolerance = 1e-9;
        private static void AssertComplexAreEqual(System.Numerics.Complex expected, System.Numerics.Complex actual, double tolerance)
        {
            Assert.IsTrue(Math.Abs(expected.Real - actual.Real) < tolerance && Math.Abs(expected.Imaginary - actual.Imaginary) < tolerance, $"Expected {expected} but got {actual}");
        }

        private static void AssertComplexArrayAreEqual(System.Numerics.Complex[] expected, System.Numerics.Complex[] actual, double tolerance)
        {
            Assert.AreEqual(expected.Length, actual.Length, "Array lengths differ.");
            for (int i = 0; i < expected.Length; i++)
            {
                AssertComplexAreEqual(expected[i], actual[i], tolerance);
            }
        }

        [TestMethod]
        public void Compute_NullSignal_ReturnsEmptyArray()
        {
            System.Numerics.Complex[] result = DFT.Compute(null);
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(0, result.Length, "Result array should be empty for null input.");
        }

        [TestMethod]
        public void Compute_EmptySignal_ReturnsEmptyArray()
        {
            double[] signal = new double[0];
            System.Numerics.Complex[] result = DFT.Compute(signal);
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(0, result.Length, "Result array should be empty for empty input.");
        }

        [TestMethod]
        public void Compute_SingleValue_ReturnsSameValue()
        {
            double[] signal = new double[]
            {
                5.0
            };
            System.Numerics.Complex[] result = DFT.Compute(signal);
            Assert.AreEqual(1, result.Length, "Result should have one element.");
            AssertComplexAreEqual(new System.Numerics.Complex(5.0, 0.0), result[0], Tolerance);
        }

        [TestMethod]
        public void Compute_TwoElementSignal_ReturnsExpected()
        {
            double[] signal = new double[]
            {
                1.0,
                2.0
            };
            // For N=2: 
            // k=0: 1 + 2 = 3
            // k=1: 1 + 2 * exp(-pi*i) = 1 - 2 = -1
            System.Numerics.Complex[] expected = new System.Numerics.Complex[]
            {
                new System.Numerics.Complex(3.0, 0.0),
                new System.Numerics.Complex(-1.0, 0.0)
            };
            System.Numerics.Complex[] result = DFT.Compute(signal);
            AssertComplexArrayAreEqual(expected, result, Tolerance);
        }

        [TestMethod]
        public void Compute_FourElementSignal_ReturnsExpected()
        {
            double[] signal = new double[]
            {
                1.0,
                2.0,
                3.0,
                4.0
            };
            // Expected DFT computed manually:
            // k=0: 1 + 2 + 3 + 4 = 10
            // k=1: 1 - 2i - 3 + 4i = -2 + 2i
            // k=2: 1 - 2 + 3 - 4 = -2
            // k=3: 1 + 2i - 3 - 4i = -2 - 2i
            System.Numerics.Complex[] expected = new System.Numerics.Complex[]
            {
                new System.Numerics.Complex(10.0, 0.0),
                new System.Numerics.Complex(-2.0, 2.0),
                new System.Numerics.Complex(-2.0, 0.0),
                new System.Numerics.Complex(-2.0, -2.0)
            };
            System.Numerics.Complex[] result = DFT.Compute(signal);
            AssertComplexArrayAreEqual(expected, result, Tolerance);
        }

        [TestMethod]
        public void Compute_LargeRandomSignal_HasCorrectLength()
        {
            int length = 1024;
            double[] signal = new double[length];
            Random random = new Random(42);
            for (int i = 0; i < length; i++)
            {
                signal[i] = random.NextDouble();
            }

            System.Numerics.Complex[] result = DFT.Compute(signal);
            Assert.AreEqual(length, result.Length, "Result length should match signal length.");
        }

        [TestMethod]
        public void Compute_SineWaveSignal_ReturnsExpectedPeaks()
        {
            int N = 128;
            double[] signal = new double[N];
            double frequency = 1.0;
            for (int n = 0; n < N; n++)
            {
                signal[n] = Math.Sin(2 * Math.PI * frequency * n / N);
            }

            System.Numerics.Complex[] result = DFT.Compute(signal);
            System.Numerics.Complex expectedPeak1 = new System.Numerics.Complex(0.0, -N / 2.0);
            System.Numerics.Complex expectedPeak2 = new System.Numerics.Complex(0.0, N / 2.0);
            AssertComplexAreEqual(expectedPeak1, result[1], Tolerance);
            AssertComplexAreEqual(expectedPeak2, result[N - 1], Tolerance);
            for (int k = 0; k < N; k++)
            {
                if (k != 1 && k != N - 1)
                {
                    Assert.IsTrue(Math.Abs(result[k].Real) < Tolerance && Math.Abs(result[k].Imaginary) < Tolerance, "Coefficient at index " + k + " is not near zero.");
                }
            }
        }
    }
}