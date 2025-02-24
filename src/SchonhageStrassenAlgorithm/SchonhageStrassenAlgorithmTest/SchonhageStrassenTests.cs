using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchonhageStrassenAlgorithm;

namespace SchonhageStrassenAlgorithmTest
{
    [TestClass]
    public class SchonhageStrassenTests
    {
        [TestMethod]
        public void TestMultiplySmallNumbers()
        {
            BigInteger result = SchonhageStrassen.Multiply(new BigInteger(2), new BigInteger(3));
            Assert.AreEqual(new BigInteger(6), result);
        }

        [TestMethod]
        public void TestMultiplyWithZero()
        {
            BigInteger result1 = SchonhageStrassen.Multiply(BigInteger.Zero, new BigInteger(12345));
            Assert.AreEqual(BigInteger.Zero, result1);
            BigInteger result2 = SchonhageStrassen.Multiply(new BigInteger(6789), BigInteger.Zero);
            Assert.AreEqual(BigInteger.Zero, result2);
        }

        [TestMethod]
        public void TestMultiplyNegativeNumbers()
        {
            BigInteger a = new BigInteger(-123);
            BigInteger b = new BigInteger(456);
            BigInteger result = SchonhageStrassen.Multiply(a, b);
            Assert.AreEqual(a * b, result);
            a = new BigInteger(-789);
            b = new BigInteger(-321);
            result = SchonhageStrassen.Multiply(a, b);
            Assert.AreEqual(a * b, result);
        }

        [TestMethod]
        public void TestMultiplyLargeNumbers()
        {
            BigInteger a = GenerateRandomBigInteger(100);
            BigInteger b = GenerateRandomBigInteger(100);
            BigInteger expected = a * b;
            BigInteger result = SchonhageStrassen.Multiply(a, b);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestFFTUtilRoundTrip()
        {
            int n = 8;
            Complex[] original = new Complex[n];
            for (int i = 0; i < n; i++)
            {
                original[i] = new Complex(i, 0);
            }

            Complex[] data = new Complex[n];
            for (int i = 0; i < n; i++)
            {
                data[i] = original[i];
            }

            FFTUtil.FFT(data, false);
            FFTUtil.FFT(data, true);
            double epsilon = 1e-6;
            for (int i = 0; i < n; i++)
            {
                Assert.IsTrue(Math.Abs(data[i].Real - original[i].Real) < epsilon, "Mismatch at index " + i);
                Assert.IsTrue(Math.Abs(data[i].Imaginary - original[i].Imaginary) < epsilon, "Mismatch at index " + i);
            }
        }

        private BigInteger GenerateRandomBigInteger(int digits)
        {
            if (digits <= 0)
            {
                return BigInteger.Zero;
            }

            Random random = new Random(42);
            string numberStr = random.Next(1, 10).ToString();
            for (int i = 1; i < digits; i++)
            {
                numberStr += random.Next(0, 10).ToString();
            }

            return BigInteger.Parse(numberStr);
        }
    }
}