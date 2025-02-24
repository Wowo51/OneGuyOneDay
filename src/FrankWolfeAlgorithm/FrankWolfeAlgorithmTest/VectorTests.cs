using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrankWolfeAlgorithm;

namespace FrankWolfeAlgorithmTest
{
    [TestClass]
    public class VectorTests
    {
        [TestMethod]
        public void TestConstructor_NullInput()
        {
            Vector vector = new Vector(null);
            Assert.AreEqual(0, vector.Dimension);
        }

        [TestMethod]
        public void TestOperatorAdd()
        {
            double[] a =
            {
                1.0,
                2.0,
                3.0
            };
            double[] b =
            {
                4.0,
                5.0,
                6.0
            };
            Vector vectorA = new Vector(a);
            Vector vectorB = new Vector(b);
            Vector result = vectorA + vectorB;
            Assert.AreEqual(3, result.Dimension);
            Assert.AreEqual(5.0, result.Values[0], 1e-6);
            Assert.AreEqual(7.0, result.Values[1], 1e-6);
            Assert.AreEqual(9.0, result.Values[2], 1e-6);
        }

        [TestMethod]
        public void TestOperatorSubtract()
        {
            double[] a =
            {
                4.0,
                5.0,
                6.0
            };
            double[] b =
            {
                1.0,
                2.0,
                3.0
            };
            Vector vectorA = new Vector(a);
            Vector vectorB = new Vector(b);
            Vector result = vectorA - vectorB;
            Assert.AreEqual(3, result.Dimension);
            Assert.AreEqual(3.0, result.Values[0], 1e-6);
            Assert.AreEqual(3.0, result.Values[1], 1e-6);
            Assert.AreEqual(3.0, result.Values[2], 1e-6);
        }

        [TestMethod]
        public void TestOperatorMultiply()
        {
            double[] values =
            {
                1.0,
                -2.0,
                3.0
            };
            Vector vector = new Vector(values);
            Vector result1 = 2.0 * vector;
            Vector result2 = vector * 2.0;
            Assert.AreEqual(3, result1.Dimension);
            Assert.AreEqual(2.0, result1.Values[0], 1e-6);
            Assert.AreEqual(-4.0, result1.Values[1], 1e-6);
            Assert.AreEqual(6.0, result1.Values[2], 1e-6);
            Assert.AreEqual(3, result2.Dimension);
            Assert.AreEqual(2.0, result2.Values[0], 1e-6);
            Assert.AreEqual(-4.0, result2.Values[1], 1e-6);
            Assert.AreEqual(6.0, result2.Values[2], 1e-6);
        }

        [TestMethod]
        public void TestDotProduct()
        {
            double[] a =
            {
                1.0,
                3.0,
                -5.0
            };
            double[] b =
            {
                4.0,
                -2.0,
                -1.0
            };
            Vector vectorA = new Vector(a);
            Vector vectorB = new Vector(b);
            double dot = vectorA.Dot(vectorB);
            Assert.AreEqual(1.0 * 4.0 + 3.0 * (-2.0) + (-5.0) * (-1.0), dot, 1e-6);
        }

        [TestMethod]
        public void TestNorm()
        {
            double[] a =
            {
                3.0,
                4.0
            };
            Vector vector = new Vector(a);
            double norm = vector.Norm();
            Assert.AreEqual(5.0, norm, 1e-6);
        }
    }
}