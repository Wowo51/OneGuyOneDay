using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DeutschJozsaAlgorithm;

namespace DeutschJozsaAlgorithmTest
{
    [TestClass]
    public class DeutschJozsaTests
    {
        [TestMethod]
        public void TestConstantFunctionFalse()
        {
            Func<bool[], bool> falseFunction = delegate (bool[] input)
            {
                return false;
            };
            int n = 3;
            string result = DeutschJozsa.GetFunctionType(falseFunction, n);
            Assert.AreEqual("constant", result);
        }

        [TestMethod]
        public void TestConstantFunctionTrue()
        {
            Func<bool[], bool> trueFunction = delegate (bool[] input)
            {
                return true;
            };
            int n = 4;
            string result = DeutschJozsa.GetFunctionType(trueFunction, n);
            Assert.AreEqual("constant", result);
        }

        [TestMethod]
        public void TestBalancedFunction_N1()
        {
            // For n == 1, a function that returns the only input bit is balanced.
            Func<bool[], bool> balancedFunction = delegate (bool[] input)
            {
                return input[0];
            };
            int n = 1;
            string result = DeutschJozsa.GetFunctionType(balancedFunction, n);
            Assert.AreEqual("balanced", result);
        }

        [TestMethod]
        public void TestBalancedFunction_N2()
        {
            // For n == 2, using the first bit yields two false and two true outputs.
            Func<bool[], bool> balancedFunction = delegate (bool[] input)
            {
                return input[0];
            };
            int n = 2;
            string result = DeutschJozsa.GetFunctionType(balancedFunction, n);
            Assert.AreEqual("balanced", result);
        }

        [TestMethod]
        public void TestN0_ReturnsConstant()
        {
            // For n < 1, the algorithm assumes the function is constant.
            Func<bool[], bool> anyFunction = delegate (bool[] input)
            {
                return true;
            };
            int n = 0;
            string result = DeutschJozsa.GetFunctionType(anyFunction, n);
            Assert.AreEqual("constant", result);
        }

        [TestMethod]
        public void TestSyntheticLargeN_Constant()
        {
            // Using a moderate n (e.g., 5) to simulate a larger data set with a constant function.
            int n = 5;
            Func<bool[], bool> constantFunction = delegate (bool[] input)
            {
                return false;
            };
            string result = DeutschJozsa.GetFunctionType(constantFunction, n);
            Assert.AreEqual("constant", result);
        }

        [TestMethod]
        public void TestSyntheticLargeN_Balanced()
        {
            // Using n = 5 with a simple balanced function (returns first bit)
            int n = 5;
            Func<bool[], bool> balancedFunction = delegate (bool[] input)
            {
                return input[0];
            };
            string result = DeutschJozsa.GetFunctionType(balancedFunction, n);
            Assert.AreEqual("balanced", result);
        }
    }
}