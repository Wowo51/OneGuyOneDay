using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZeroAttributeRule;

namespace ZeroAttributeRuleTest
{
    [TestClass]
    public class ZeroAttributeCheckerTests
    {
        [TestMethod]
        public void Test_NullInput_ReturnsFalse()
        {
            bool result = ZeroAttributeChecker.HasAttributes(null);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_EmptyString_ReturnsFalse()
        {
            bool result = ZeroAttributeChecker.HasAttributes(string.Empty);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_NoAttributes_ReturnsFalse()
        {
            string input = "public class Sample { }";
            bool result = ZeroAttributeChecker.HasAttributes(input);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_WithAttributeSyntax_ReturnsTrue()
        {
            string input = "[Serializable] public class Sample { }";
            bool result = ZeroAttributeChecker.HasAttributes(input);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_WithMismatchedBrackets_ReturnsFalse()
        {
            string input = "This [ is strange but no ending";
            bool result = ZeroAttributeChecker.HasAttributes(input);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_WithClosingBracketOnly_ReturnsFalse()
        {
            string input = "No starting bracket but a ] closing one appears";
            bool result = ZeroAttributeChecker.HasAttributes(input);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_WithBothBracketsButNotAnAttribute_ReturnsTrue()
        {
            string input = "This is [just] a random text";
            bool result = ZeroAttributeChecker.HasAttributes(input);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_LargeInputWithAttributes_ReturnsTrue()
        {
            StringBuilder builder = new StringBuilder();
            int iterations = 10000;
            for (int i = 0; i < iterations; i++)
            {
                builder.Append("Some text. ");
            }

            builder.Insert(50, "[Test]");
            string input = builder.ToString();
            bool result = ZeroAttributeChecker.HasAttributes(input);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_LargeInputNoAttributes_ReturnsFalse()
        {
            StringBuilder builder = new StringBuilder();
            int iterations = 10000;
            for (int i = 0; i < iterations; i++)
            {
                builder.Append("No attributes here. ");
            }

            string input = builder.ToString();
            bool result = ZeroAttributeChecker.HasAttributes(input);
            Assert.IsFalse(result);
        }
    }
}