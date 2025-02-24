using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BinarySplitting;

namespace BinarySplittingTest
{
    [TestClass]
    public class BinarySplitterTests
    {
        [TestMethod]
        public void TestEmptyRange()
        {
            double result = BinarySplitter.SumSeries(10, 10, x => x);
            Assert.AreEqual(0.0, result, "Sum for empty range should be 0.0");
        }

        [TestMethod]
        public void TestSingleElement()
        {
            double result = BinarySplitter.SumSeries(5, 6, x => x * 2.0);
            Assert.AreEqual(10.0, result, "Sum for single element should return term value");
        }

        [TestMethod]
        public void TestMultipleElements()
        {
            int left = 1;
            int right = 11;
            double expected = 0.0;
            for (int i = left; i < right; i++)
            {
                expected += i;
            }

            double result = BinarySplitter.SumSeries(left, right, x => x);
            Assert.AreEqual(expected, result, "Sum for multiple elements is incorrect");
        }

        [TestMethod]
        public void TestConstantValue()
        {
            int left = 7;
            int right = 17;
            double constant = 3.5;
            double expected = (right - left) * constant;
            double result = BinarySplitter.SumSeries(left, right, x => constant);
            Assert.AreEqual(expected, result, 1e-5, "Sum for constant function is incorrect");
        }

        [TestMethod]
        public void TestLargeRangeSyntheticData()
        {
            int left = 0;
            int right = 10000;
            double expected = (right - 1.0) * right / 2.0;
            double result = BinarySplitter.SumSeries(left, right, x => (double)x);
            Assert.AreEqual(expected, result, 1e-5, "Sum for large range synthetic data is incorrect");
        }
    }
}