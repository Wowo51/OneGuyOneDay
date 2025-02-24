using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EllipticCurveCryptography;

namespace EllipticCurveCryptographyTest
{
    [TestClass]
    public class ECCryptographyTests
    {
        private static void AssertECPointEqual(ECPoint expected, ECPoint actual)
        {
            if (expected == null)
            {
                Assert.IsNull(actual);
            }
            else if (expected.IsInfinity)
            {
                Assert.IsTrue(actual.IsInfinity, $"Expected Infinity but got ({actual.X}, {actual.Y}).");
            }
            else
            {
                Assert.IsFalse(actual.IsInfinity, "Expected a finite point but got Infinity.");
                Assert.AreEqual(expected.X, actual.X, "X coordinates are not equal.");
                Assert.AreEqual(expected.Y, actual.Y, "Y coordinates are not equal.");
            }
        }

        [TestMethod]
        public void TestAdd_NullInputs_ReturnsInfinity()
        {
            EllipticCurve curve = new EllipticCurve(2, 2, 17);
            ECPoint p = new ECPoint(5, 1);
            ECPoint result1 = ECCryptography.Add(curve, null, p);
            ECPoint result2 = ECCryptography.Add(curve, p, null);
            ECPoint result3 = ECCryptography.Add(curve, null, null);
            AssertECPointEqual(ECPoint.Infinity, result1);
            AssertECPointEqual(ECPoint.Infinity, result2);
            AssertECPointEqual(ECPoint.Infinity, result3);
        }

        [TestMethod]
        public void TestAdd_DistinctPoints()
        {
            EllipticCurve curve = new EllipticCurve(2, 2, 17);
            ECPoint p = new ECPoint(5, 1);
            ECPoint q = new ECPoint(6, 3);
            // Expected: (10,6) from the addition formula.
            ECPoint expected = new ECPoint(10, 6);
            ECPoint result = ECCryptography.Add(curve, p, q);
            AssertECPointEqual(expected, result);
            // Verify addition with infinity.
            ECPoint resultWithInfinity1 = ECCryptography.Add(curve, ECPoint.Infinity, p);
            AssertECPointEqual(p, resultWithInfinity1);
            ECPoint resultWithInfinity2 = ECCryptography.Add(curve, p, ECPoint.Infinity);
            AssertECPointEqual(p, resultWithInfinity2);
        }

        [TestMethod]
        public void TestDouble()
        {
            EllipticCurve curve = new EllipticCurve(2, 2, 17);
            ECPoint p = new ECPoint(5, 1);
            // Doubling (5,1) should yield (6,3) as computed.
            ECPoint expected = new ECPoint(6, 3);
            ECPoint result = ECCryptography.Double(curve, p);
            AssertECPointEqual(expected, result);
        }

        [TestMethod]
        public void TestDouble_YZero_ReturnsInfinity()
        {
            EllipticCurve curve = new EllipticCurve(2, 2, 17);
            ECPoint p = new ECPoint(3, 0);
            ECPoint result = ECCryptography.Double(curve, p);
            AssertECPointEqual(ECPoint.Infinity, result);
        }

        [TestMethod]
        public void TestMultiply()
        {
            EllipticCurve curve = new EllipticCurve(2, 2, 17);
            ECPoint p = new ECPoint(5, 1);
            // k = 0 should return Infinity.
            ECPoint result0 = ECCryptography.Multiply(curve, new BigInteger(0), p);
            AssertECPointEqual(ECPoint.Infinity, result0);
            // k = 1 should return the same point.
            ECPoint result1 = ECCryptography.Multiply(curve, new BigInteger(1), p);
            AssertECPointEqual(p, result1);
            // k = 3: (5,1) + 2*(5,1) should yield (10,6).
            ECPoint result3 = ECCryptography.Multiply(curve, new BigInteger(3), p);
            ECPoint expected = new ECPoint(10, 6);
            AssertECPointEqual(expected, result3);
        }

        [TestMethod]
        public void TestMultiply_RandomData()
        {
            EllipticCurve curve = new EllipticCurve(2, 2, 17);
            ECPoint p = new ECPoint(5, 1);
            Random random = new Random(42);
            for (int i = 0; i < 10; i++)
            {
                int kInt = random.Next(0, 100);
                BigInteger k = new BigInteger(kInt);
                ECPoint expected = ECPoint.Infinity;
                for (int j = 0; j < kInt; j++)
                {
                    expected = ECCryptography.Add(curve, expected, p);
                }

                ECPoint result = ECCryptography.Multiply(curve, k, p);
                AssertECPointEqual(expected, result);
            }
        }

        [TestMethod]
        public void TestIsOnCurve()
        {
            EllipticCurve curve = new EllipticCurve(2, 2, 17);
            ECPoint p = new ECPoint(5, 1);
            ECPoint q = new ECPoint(6, 3);
            ECPoint r = new ECPoint(10, 6);
            ECPoint notOnCurve = new ECPoint(0, 0);
            Assert.IsTrue(curve.IsOnCurve(p));
            Assert.IsTrue(curve.IsOnCurve(q));
            Assert.IsTrue(curve.IsOnCurve(r));
            Assert.IsFalse(curve.IsOnCurve(notOnCurve));
            Assert.IsTrue(curve.IsOnCurve(ECPoint.Infinity));
        }
    }
}