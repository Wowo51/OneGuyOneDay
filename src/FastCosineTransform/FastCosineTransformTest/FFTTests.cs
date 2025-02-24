using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FastCosineTransform;

namespace FastCosineTransformTest
{
    [TestClass]
    public class FFTTests
    {
        [TestMethod]
        public void Test_NullInput()
        {
            Complex[]? input = null;
            Complex[] result = FFT.Compute(input);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Test_PowerOfTwoInput()
        {
            int size = 4;
            Complex[] input = new Complex[size];
            input[0] = new Complex(1.0, 0.0);
            input[1] = Complex.Zero;
            input[2] = Complex.Zero;
            input[3] = Complex.Zero;
            Complex[] result = FFT.Compute(input);
            Assert.AreEqual(size, result.Length);
            for (int i = 0; i < size; i++)
            {
                Assert.AreEqual(1.0, result[i].Real, 1e-6);
                Assert.AreEqual(0.0, result[i].Imaginary, 1e-6);
            }
        }

        [TestMethod]
        public void Test_NonPowerOfTwoInput()
        {
            int size = 3;
            Complex[] input = new Complex[size];
            input[0] = new Complex(1.0, 0.0);
            input[1] = new Complex(2.0, 0.0);
            input[2] = new Complex(3.0, 0.0);
            Complex[] result = FFT.Compute(input);
            Assert.AreEqual(size, result.Length);
            Assert.AreEqual(6.0, result[0].Real, 1e-6);
            Assert.AreEqual(0.0, result[0].Imaginary, 1e-6);
        }

        [TestMethod]
        public void Test_LargeInput()
        {
            int size = 1024;
            Complex[] input = new Complex[size];
            System.Random random = new System.Random(0);
            for (int i = 0; i < size; i++)
            {
                double value = random.NextDouble();
                input[i] = new Complex(value, 0.0);
            }

            Complex[] result = FFT.Compute(input);
            Assert.AreEqual(size, result.Length);
            for (int i = 0; i < size; i++)
            {
                Assert.IsFalse(double.IsNaN(result[i].Real));
                Assert.IsFalse(double.IsNaN(result[i].Imaginary));
            }
        }
    }
}