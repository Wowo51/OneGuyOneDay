using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LenstraEllipticFactorization;

namespace LenstraEllipticFactorizationTest
{
    [TestClass]
    public class EllipticCurveTests
    {
        [TestMethod]
        public void TestAddWithInfinity()
        {
            BigInteger modulus = new BigInteger(97);
            BigInteger a = new BigInteger(2);
            BigInteger b = new BigInteger(3);
            EllipticCurve curve = new EllipticCurve(modulus, a, b);
            EllipticCurvePoint P = new EllipticCurvePoint(new BigInteger(3), new BigInteger(6));
            BigInteger factor;
            EllipticCurvePoint sum1 = curve.Add(P, EllipticCurvePoint.Infinity, out factor);
            Assert.AreEqual(P.X, sum1.X, "Adding point and infinity should return the original point (X coordinate).");
            Assert.AreEqual(P.Y, sum1.Y, "Adding point and infinity should return the original point (Y coordinate).");
            EllipticCurvePoint sum2 = curve.Add(EllipticCurvePoint.Infinity, P, out factor);
            Assert.AreEqual(P.X, sum2.X, "Adding infinity and point should return the original point (X coordinate).");
            Assert.AreEqual(P.Y, sum2.Y, "Adding infinity and point should return the original point (Y coordinate).");
        }

        [TestMethod]
        public void TestDouble()
        {
            BigInteger modulus = new BigInteger(97);
            BigInteger a = new BigInteger(2);
            BigInteger b = new BigInteger(3);
            EllipticCurve curve = new EllipticCurve(modulus, a, b);
            EllipticCurvePoint P = new EllipticCurvePoint(new BigInteger(3), new BigInteger(6));
            BigInteger factor;
            EllipticCurvePoint doubled = curve.Double(P, out factor);
            // Expected result: (80, 10)
            Assert.AreEqual(new BigInteger(80), doubled.X, "Doubling point did not yield expected X coordinate.");
            Assert.AreEqual(new BigInteger(10), doubled.Y, "Doubling point did not yield expected Y coordinate.");
        }

        [TestMethod]
        public void TestMultiplyConsistency()
        {
            BigInteger modulus = new BigInteger(97);
            BigInteger a = new BigInteger(2);
            BigInteger b = new BigInteger(3);
            EllipticCurve curve = new EllipticCurve(modulus, a, b);
            EllipticCurvePoint P = new EllipticCurvePoint(new BigInteger(3), new BigInteger(6));
            BigInteger factor;
            EllipticCurvePoint r1 = curve.Multiply(P, new BigInteger(3), out factor);
            BigInteger dummy;
            EllipticCurvePoint r2 = curve.Add(P, curve.Double(P, out dummy), out factor);
            Assert.AreEqual(r1.X, r2.X, "Multiplication by 3 is inconsistent with addition and doubling (X coordinate).");
            Assert.AreEqual(r1.Y, r2.Y, "Multiplication by 3 is inconsistent with addition and doubling (Y coordinate).");
        }
    }
}