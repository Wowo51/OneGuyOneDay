using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaileyBorweinPlouffe;

namespace BaileyBorweinPlouffeTest
{
    [TestClass]
    public class BBPFormulaTests
    {
        [TestMethod]
        public void TestComputeHexDigit_Range()
        {
            for (int i = 1; i <= 10; i++)
            {
                int hexDigit = BBPFormula.ComputeHexDigit(i);
                Assert.IsTrue(hexDigit >= 0 && hexDigit <= 15, "Hex digit " + hexDigit + " is out of range for input " + i + ".");
            }
        }

        [TestMethod]
        public void TestComputeBinaryDigit_Range()
        {
            for (int i = 1; i <= 20; i++)
            {
                int binaryDigit = BBPFormula.ComputeBinaryDigit(i);
                Assert.IsTrue(binaryDigit == 0 || binaryDigit == 1, "Binary digit " + binaryDigit + " is not 0 or 1 for input " + i + ".");
            }
        }

        [TestMethod]
        public void TestSeries_ReturnsFraction()
        {
            for (int j = 1; j <= 6; j++)
            {
                for (int n = 0; n <= 5; n++)
                {
                    double result = BBPFormula.Series(j, n);
                    Assert.IsTrue(result >= 0.0 && result < 1.0, "Series returned " + result + " out of range for j=" + j + ", n=" + n + ".");
                }
            }
        }

        [TestMethod]
        public void TestModPow_ZeroExponentAndOneMod()
        {
            long result = BBPFormula.ModPow(16, 0, 1);
            Assert.AreEqual(1L, result, "ModPow(16, 0, 1) should return 1 as implemented.");
        }

        [TestMethod]
        public void TestModPow_KnownValues()
        {
            long result = BBPFormula.ModPow(16, 5, 7);
            Assert.AreEqual(4L, result, "ModPow(16, 5, 7) should return 4.");
        }

        [TestMethod]
        public void TestComputeBinaryDigit_SyntheticLargeInput()
        {
            Random random = new Random(42);
            for (int i = 0; i < 50; i++)
            {
                int n = random.Next(1, 100);
                int digit = BBPFormula.ComputeBinaryDigit(n);
                Assert.IsTrue(digit == 0 || digit == 1, "Binary digit " + digit + " is not 0 or 1 for random input " + n + ".");
            }
        }
    }
}