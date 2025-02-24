using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChudnovskyAlgorithm;

namespace ChudnovskyAlgorithmTest
{
    [TestClass]
    public class ChudnovskyCalculatorTests
    {
        [TestMethod]
        public void CalculatePi_WithZeroDigits_ReturnsEmpty()
        {
            string result = ChudnovskyCalculator.CalculatePi(0);
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void CalculatePi_WithNegativeDigits_ReturnsEmpty()
        {
            string result = ChudnovskyCalculator.CalculatePi(-10);
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void CalculatePi_FormatTest_SmallPrecision()
        {
            int digits = 1;
            string result = ChudnovskyCalculator.CalculatePi(digits);
            string[] parts = result.Split('.');
            Assert.AreEqual(2, parts.Length);
            Assert.AreEqual("3", parts[0]);
            Assert.AreEqual(digits + 10, parts[1].Length);
        }

        [TestMethod]
        public void CalculatePi_FormatTest_MediumPrecision()
        {
            int digits = 50;
            string result = ChudnovskyCalculator.CalculatePi(digits);
            string[] parts = result.Split('.');
            Assert.AreEqual(2, parts.Length);
            Assert.AreEqual("3", parts[0]);
            Assert.AreEqual(digits + 10, parts[1].Length);
        }

        [TestMethod]
        public void CalculatePi_ConsistencyTest()
        {
            int digits = 30;
            string first = ChudnovskyCalculator.CalculatePi(digits);
            string second = ChudnovskyCalculator.CalculatePi(digits);
            Assert.AreEqual(first, second);
        }

        [TestMethod]
        public void CalculatePi_LargePrecision_Test()
        {
            int digits = 200;
            string result = ChudnovskyCalculator.CalculatePi(digits);
            string[] parts = result.Split('.');
            Assert.AreEqual(2, parts.Length);
            Assert.AreEqual("3", parts[0]);
            Assert.AreEqual(digits + 10, parts[1].Length);
        }
    }
}