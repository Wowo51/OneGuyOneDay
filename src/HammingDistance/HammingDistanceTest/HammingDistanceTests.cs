using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HammingDistance;

namespace HammingDistanceTest
{
    [TestClass]
    public class HammingDistanceTests
    {
        [TestMethod]
        public void Test_IdenticalStrings_ReturnsZero()
        {
            int expected = 0;
            int actual = HammingDistanceCalculator.ComputeHammingDistance("101010", "101010");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_DifferentCharacters_ReturnsCorrectDistance()
        {
            int expected = 2;
            int actual = HammingDistanceCalculator.ComputeHammingDistance("101010", "100110");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_BothNullStrings_ReturnsZero()
        {
            int expected = 0;
            int actual = HammingDistanceCalculator.ComputeHammingDistance(null, null);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_OneNull_ReturnsMinusOne()
        {
            int expected = -1;
            int actual1 = HammingDistanceCalculator.ComputeHammingDistance(null, "test");
            int actual2 = HammingDistanceCalculator.ComputeHammingDistance("test", null);
            Assert.AreEqual(expected, actual1);
            Assert.AreEqual(expected, actual2);
        }

        [TestMethod]
        public void Test_DifferentLengths_ReturnsMinusOne()
        {
            int expected = -1;
            int actual = HammingDistanceCalculator.ComputeHammingDistance("abc", "abcd");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_LargeDataSet_CorrectlyComputesDistance()
        {
            int length = 1000;
            StringBuilder stringBuilder1 = new StringBuilder(length);
            StringBuilder stringBuilder2 = new StringBuilder(length);
            int countDifferences = 0;
            for (int i = 0; i < length; i++)
            {
                stringBuilder1.Append("a");
                if (i % 17 == 0)
                {
                    stringBuilder2.Append("b");
                    countDifferences++;
                }
                else
                {
                    stringBuilder2.Append("a");
                }
            }

            string string1 = stringBuilder1.ToString();
            string string2 = stringBuilder2.ToString();
            int actual = HammingDistanceCalculator.ComputeHammingDistance(string1, string2);
            Assert.AreEqual(countDifferences, actual);
        }
    }
}