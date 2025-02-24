using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LenstraEllipticFactorization;

namespace LenstraEllipticFactorizationTest
{
    [TestClass]
    public class MathHelpersTests
    {
        [TestMethod]
        public void TestModularInverseExists()
        {
            BigInteger inverse;
            BigInteger factor;
            bool result = MathHelpers.TryModularInverse(new BigInteger(3), new BigInteger(11), out inverse, out factor);
            Assert.IsTrue(result, "Modular inverse should exist for 3 mod 11.");
            Assert.AreEqual(new BigInteger(4), inverse, "Modular inverse of 3 mod 11 should be 4.");
        }

        [TestMethod]
        public void TestModularInverseDoesNotExist()
        {
            BigInteger inverse;
            BigInteger factor;
            bool result = MathHelpers.TryModularInverse(new BigInteger(2), new BigInteger(4), out inverse, out factor);
            Assert.IsFalse(result, "Modular inverse should not exist for 2 mod 4.");
            Assert.AreEqual(new BigInteger(2), factor, "GCD for 2 and 4 should be 2.");
        }
    }
}