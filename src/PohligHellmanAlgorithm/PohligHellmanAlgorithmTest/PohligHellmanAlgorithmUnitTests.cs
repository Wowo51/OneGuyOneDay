using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;
using PohligHellmanAlgorithm;

namespace PohligHellmanAlgorithmTest
{
    [TestClass]
    public class PohligHellmanAlgorithmUnitTests
    {
        [TestMethod]
        public void TestDiscreteLog_ValidSmallCase1()
        {
            BigInteger g = new BigInteger(5);
            BigInteger h = new BigInteger(10);
            BigInteger p = new BigInteger(23);
            BigInteger result = PohligHellman.ComputeDiscreteLog(g, h, p);
            Assert.AreEqual(new BigInteger(3), result, "Discrete log for (5,10,23) should be 3.");
        }

        [TestMethod]
        public void TestDiscreteLog_ValidSmallCase2()
        {
            BigInteger g = new BigInteger(5);
            BigInteger h = new BigInteger(1);
            BigInteger p = new BigInteger(23);
            BigInteger result = PohligHellman.ComputeDiscreteLog(g, h, p);
            Assert.AreEqual(new BigInteger(0), result, "Discrete log for (5,1,23) should be 0.");
        }

        [TestMethod]
        public void TestDiscreteLog_NoSolution()
        {
            BigInteger g = new BigInteger(2);
            BigInteger h = new BigInteger(5);
            BigInteger p = new BigInteger(23);
            BigInteger result = PohligHellman.ComputeDiscreteLog(g, h, p);
            Assert.AreEqual(new BigInteger(-1), result, "Discrete log for (2,5,23) should return -1 for no solution.");
        }

        [TestMethod]
        public void TestDiscreteLog_ValidCase3()
        {
            BigInteger g = new BigInteger(6);
            BigInteger p = new BigInteger(41);
            BigInteger expectedX = new BigInteger(5);
            BigInteger h = BigInteger.ModPow(g, 5, p);
            BigInteger result = PohligHellman.ComputeDiscreteLog(g, h, p);
            Assert.AreEqual(expectedX, result, "Discrete log for (6, h computed as 6^5 mod 41) should be 5.");
        }

        [TestMethod]
        public void TestDiscreteLog_MultipleRandomCases()
        {
            BigInteger g = new BigInteger(5);
            BigInteger p = new BigInteger(97);
            for (int i = 0; i < 10; i++)
            {
                BigInteger h = BigInteger.ModPow(g, i, p);
                BigInteger result = PohligHellman.ComputeDiscreteLog(g, h, p);
                Assert.AreEqual(new BigInteger(i), result, "Discrete log for exponent " + i + " failed.");
            }
        }
    }
}