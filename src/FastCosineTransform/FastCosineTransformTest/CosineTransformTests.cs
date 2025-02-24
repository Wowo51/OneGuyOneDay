using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FastCosineTransform;

namespace FastCosineTransformTest
{
    [TestClass]
    public class CosineTransformTests
    {
        [TestMethod]
        public void Test_NullInput()
        {
            double[]? input = null;
            double[] result = CosineTransform.ComputeDCT(input);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Test_EmptyInput()
        {
            double[] input = new double[0];
            double[] result = CosineTransform.ComputeDCT(input);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Test_SingleElement()
        {
            double[] input = new double[]
            {
                1.0
            };
            double[] result = CosineTransform.ComputeDCT(input);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(2.0, result[0], 1e-6);
        }

        [TestMethod]
        public void Test_KnownDCT()
        {
            double[] input = new double[]
            {
                1.0,
                2.0
            };
            double[] result = CosineTransform.ComputeDCT(input);
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(6.0, result[0], 1e-6);
            double expected = -1.41421356;
            Assert.AreEqual(expected, result[1], 1e-6);
        }

        [TestMethod]
        public void Test_LargeInput()
        {
            int size = 1024;
            double[] input = new double[size];
            System.Random random = new System.Random(0);
            for (int i = 0; i < size; i++)
            {
                input[i] = random.NextDouble();
            }

            double[] result = CosineTransform.ComputeDCT(input);
            Assert.AreEqual(size, result.Length);
            for (int i = 0; i < size; i++)
            {
                Assert.IsFalse(double.IsNaN(result[i]));
            }
        }
    }
}