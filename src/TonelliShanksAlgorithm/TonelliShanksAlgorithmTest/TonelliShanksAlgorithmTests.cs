using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TonelliShanksAlgorithm;

namespace TonelliShanksAlgorithmTest
{
    [TestClass]
    public class TonelliShanksAlgorithmTests
    {
        [TestMethod]
        public void SquareRootModuloPrime_ZeroInput_ReturnsZero()
        {
            long? result = TonelliShanks.SquareRootModuloPrime(0, 7);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Value);
        }

        [TestMethod]
        public void SquareRootModuloPrime_NonResidue_ReturnsNull()
        {
            long? result = TonelliShanks.SquareRootModuloPrime(3, 7);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void SquareRootModuloPrime_PositiveResidue_ReturnsValidRoot()
        {
            long? result = TonelliShanks.SquareRootModuloPrime(2, 7);
            Assert.IsNotNull(result);
            long computed = result.Value;
            long remainder = (computed * computed) % 7;
            Assert.AreEqual(2 % 7, remainder);
        }

        [TestMethod]
        public void SquareRootModuloPrime_NegativeInput_ReturnsNullForNonResidue()
        {
            // For negative input, a = -2 normalizes to 5 mod 7 which is not a quadratic residue.
            long? result = TonelliShanks.SquareRootModuloPrime(-2, 7);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void SquareRootModuloPrime_NonPMod4Branch_ReturnsValidRoot()
        {
            // Use prime 13 (13 % 4 == 1) to force the non p % 4 == 3 branch.
            // 10 is a quadratic residue modulo 13 because 6*6 = 36 ≡ 10 (mod 13).
            long? result = TonelliShanks.SquareRootModuloPrime(10, 13);
            Assert.IsNotNull(result);
            long computed = result.Value;
            long remainder = (computed * computed) % 13;
            Assert.AreEqual(10 % 13, remainder);
        }

        [TestMethod]
        public void SquareRootModuloPrime_RandomQuadraticResidues()
        {
            // Use a small prime to generate synthetic data set testing all residues.
            long prime = 17;
            for (long a = 0; a < prime; a++)
            {
                long? result = TonelliShanks.SquareRootModuloPrime(a, prime);
                if (a == 0)
                {
                    Assert.IsNotNull(result);
                    Assert.AreEqual(0, result.Value);
                }
                else if (IsQuadraticResidue(a, prime))
                {
                    Assert.IsNotNull(result);
                    long computed = result.Value;
                    long remainder = (computed * computed) % prime;
                    Assert.AreEqual(a % prime, remainder);
                }
                else
                {
                    Assert.IsNull(result);
                }
            }
        }

        private static long PowMod(long baseValue, long exponent, long mod)
        {
            long result = 1;
            baseValue = baseValue % mod;
            while (exponent > 0)
            {
                if ((exponent & 1) == 1)
                {
                    result = (result * baseValue) % mod;
                }

                exponent = exponent / 2;
                baseValue = (baseValue * baseValue) % mod;
            }

            return result;
        }

        private static bool IsQuadraticResidue(long a, long p)
        {
            if (a % p == 0)
            {
                return true;
            }

            long expResult = PowMod(a, (p - 1) / 2, p);
            return expResult == 1;
        }
    }
}