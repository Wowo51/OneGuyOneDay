using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LuhnAlgorithm;

namespace LuhnAlgorithmTest
{
    [TestClass]
    public class LuhnValidatorTests
    {
        [TestMethod]
        public void IsValid_NullInput_ReturnsFalse()
        {
            bool result = LuhnValidator.IsValid(null);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_EmptyString_ReturnsFalse()
        {
            bool result = LuhnValidator.IsValid("");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_WhitespaceOnly_ReturnsFalse()
        {
            bool result = LuhnValidator.IsValid("   ");
            Assert.IsFalse(result);
        }

        [DataTestMethod]
        [DataRow("79927398713")]
        [DataRow("4539148803436467")]
        [DataRow("4242424242424242")]
        [DataRow(" 79927398713")]
        [DataRow("79927398713 ")]
        [DataRow("79 92 73 98 713")]
        public void IsValid_ValidNumbers_ReturnsTrue(string input)
        {
            bool result = LuhnValidator.IsValid(input);
            Assert.IsTrue(result, input + " should be valid.");
        }

        [DataTestMethod]
        [DataRow("79927398714")]
        [DataRow("123456789")]
        [DataRow("abcdefg")]
        [DataRow("1234a567")]
        public void IsValid_InvalidNumbers_ReturnsFalse(string input)
        {
            bool result = LuhnValidator.IsValid(input);
            Assert.IsFalse(result, input + " should be invalid.");
        }

        [TestMethod]
        public void Synthetic_ValidNumbersAreRecognized()
        {
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                string synthetic = GenerateValidLuhnNumber(16, random);
                Assert.IsTrue(LuhnValidator.IsValid(synthetic), "Synthetic valid number " + synthetic + " was not recognized.");
            }
        }

        [TestMethod]
        public void Synthetic_InvalidNumbersAreRecognized()
        {
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                string valid = GenerateValidLuhnNumber(16, random);
                char lastChar = valid[valid.Length - 1];
                int digit = lastChar - '0';
                int modifiedDigit = (digit + 1) % 10;
                string invalid = valid.Substring(0, valid.Length - 1) + modifiedDigit.ToString();
                if (LuhnValidator.IsValid(invalid))
                {
                    char firstChar = valid[0];
                    int firstDigit = firstChar - '0';
                    int newDigit = (firstDigit + 1) % 10;
                    invalid = newDigit.ToString() + valid.Substring(1);
                }

                Assert.IsFalse(LuhnValidator.IsValid(invalid), "Synthetic invalid number " + invalid + " was not recognized as invalid.");
            }
        }

        private string GenerateValidLuhnNumber(int length, Random random)
        {
            if (length < 2)
            {
                length = 2;
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length - 1; i++)
            {
                int digit = random.Next(0, 10);
                sb.Append(digit.ToString());
            }

            string partial = sb.ToString();
            for (int candidate = 0; candidate < 10; candidate++)
            {
                string full = partial + candidate.ToString();
                if (LuhnValidator.IsValid(full))
                {
                    return full;
                }
            }

            return partial + "0";
        }
    }
}