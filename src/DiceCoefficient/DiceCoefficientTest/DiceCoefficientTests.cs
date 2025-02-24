using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DiceCoefficient;

namespace DiceCoefficientTest
{
    [TestClass]
    public class DiceCoefficientTests
    {
        [TestMethod]
        public void Test_BothNull()
        {
            double result = DiceCoefficientCalculator.Calculate(null, null);
            Assert.AreEqual(1.0, result, 1e-6);
        }

        [TestMethod]
        public void Test_OneNull_FirstNull()
        {
            double result = DiceCoefficientCalculator.Calculate(null, "a");
            Assert.AreEqual(0.0, result, 1e-6);
        }

        [TestMethod]
        public void Test_OneNull_SecondNull()
        {
            double result = DiceCoefficientCalculator.Calculate("a", null);
            Assert.AreEqual(0.0, result, 1e-6);
        }

        [TestMethod]
        public void Test_SingleCharacterEqual()
        {
            double result = DiceCoefficientCalculator.Calculate("a", "a");
            Assert.AreEqual(1.0, result, 1e-6);
        }

        [TestMethod]
        public void Test_SingleCharacterNotEqual()
        {
            double result = DiceCoefficientCalculator.Calculate("a", "b");
            Assert.AreEqual(0.0, result, 1e-6);
        }

        [TestMethod]
        public void Test_KnownStrings_NightNacht()
        {
            double result = DiceCoefficientCalculator.Calculate("night", "nacht");
            Assert.AreEqual(0.25, result, 1e-6);
        }

        [TestMethod]
        public void Test_KnownStrings_ContextContact()
        {
            double result = DiceCoefficientCalculator.Calculate("context", "contact");
            Assert.AreEqual(0.5, result, 1e-6);
        }

        [TestMethod]
        public void Test_LargeIdenticalStrings()
        {
            string s = new string ('a', 1000);
            double result = DiceCoefficientCalculator.Calculate(s, s);
            Assert.AreEqual(1.0, result, 1e-6);
        }

        [TestMethod]
        public void Test_LargeDifferentStrings()
        {
            string s1 = new string ('a', 1000);
            string s2 = new string ('b', 1000);
            double result = DiceCoefficientCalculator.Calculate(s1, s2);
            Assert.AreEqual(0.0, result, 1e-6);
        }

        [TestMethod]
        public void Test_LargeRandomStrings()
        {
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            Random random = new Random(0);
            for (int i = 0; i < 1000; i++)
            {
                char ch1 = (char)('a' + random.Next(0, 26));
                char ch2 = (char)('a' + random.Next(0, 26));
                sb1.Append(ch1);
                sb2.Append(ch2);
            }

            string s1 = sb1.ToString();
            string s2 = sb2.ToString();
            double result = DiceCoefficientCalculator.Calculate(s1, s2);
            Assert.IsTrue(result >= 0.0 && result <= 1.0);
        }

        [TestMethod]
        public void Test_EmptyStrings()
        {
            double result = DiceCoefficientCalculator.Calculate("", "");
            Assert.AreEqual(1.0, result, 1e-6);
        }

        [TestMethod]
        public void Test_VeryLargeRandomStrings()
        {
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            Random random = new Random(0);
            for (int i = 0; i < 10000; i++)
            {
                char ch1 = (char)('a' + random.Next(0, 26));
                char ch2 = (char)('a' + random.Next(0, 26));
                sb1.Append(ch1);
                sb2.Append(ch2);
            }

            string s1 = sb1.ToString();
            string s2 = sb2.ToString();
            double result = DiceCoefficientCalculator.Calculate(s1, s2);
            Assert.IsTrue(result >= 0.0 && result <= 1.0);
        }
    }
}