using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VerhoeffAlgorithm;

namespace VerhoeffAlgorithmTest
{
    [TestClass]
    public class VerhoeffTests
    {
        [TestMethod]
        public void TestValidate_Valid()
        {
            bool result = Verhoeff.Validate("2363");
            Assert.IsTrue(result, "2363 should be valid.");
        }

        [TestMethod]
        public void TestValidate_Invalid()
        {
            bool resultNull = Verhoeff.Validate(null);
            Assert.IsFalse(resultNull, "null should be invalid.");
            bool resultNonDigit = Verhoeff.Validate("23a3");
            Assert.IsFalse(resultNonDigit, "Non-digit string should be invalid.");
            bool resultWrong = Verhoeff.Validate("2364");
            Assert.IsFalse(resultWrong, "2364 should be invalid.");
        }

        [TestMethod]
        public void TestComputeCheckDigit()
        {
            int digit = Verhoeff.ComputeCheckDigit("236");
            Assert.AreEqual(3, digit, "Check digit for '236' should be 3.");
            int digitNonDigit = Verhoeff.ComputeCheckDigit("23a");
            Assert.AreEqual(-1, digitNonDigit, "Non-digit input should yield -1.");
            int digitNull = Verhoeff.ComputeCheckDigit(null);
            Assert.AreEqual(-1, digitNull, "null input should yield -1.");
        }

        [TestMethod]
        public void TestAppendCheckDigit()
        {
            string appended = Verhoeff.AppendCheckDigit("236");
            Assert.AreEqual("2363", appended, "Appending check digit to '236' should yield '2363'.");
            string appendedNull = Verhoeff.AppendCheckDigit(null);
            Assert.AreEqual(null, appendedNull, "Appending check digit on null should return null.");
        }

        [TestMethod]
        public void TestValidate_EmptyString()
        {
            bool result = Verhoeff.Validate("");
            Assert.IsTrue(result, "Empty string should be valid as no invalid digit is found.");
            string appended = Verhoeff.AppendCheckDigit("");
            Assert.AreEqual("0", appended, "Appending check digit to empty string should yield '0'.");
        }

        [TestMethod]
        public void TestAppendAndValidate_RandomNumbers()
        {
            System.Random random = new System.Random(42);
            for (int i = 0; i < 100; i++)
            {
                string number = "";
                int length = random.Next(1, 11);
                for (int j = 0; j < length; j++)
                {
                    int digit = random.Next(0, 10);
                    number = number + digit.ToString();
                }

                string fullNumber = Verhoeff.AppendCheckDigit(number);
                bool isValid = Verhoeff.Validate(fullNumber);
                Assert.IsTrue(isValid, "The generated number with appended check digit should be valid.");
            }
        }

        [TestMethod]
        public void TestAppendAndValidate_LargeRandomNumbers()
        {
            System.Random random = new System.Random(12345);
            for (int i = 0; i < 10; i++)
            {
                string number = "";
                int length = random.Next(50, 151);
                for (int j = 0; j < length; j++)
                {
                    int digit = random.Next(0, 10);
                    number = number + digit.ToString();
                }

                string fullNumber = Verhoeff.AppendCheckDigit(number);
                bool isValid = Verhoeff.Validate(fullNumber);
                Assert.IsTrue(isValid, "Large generated number with appended check digit should be valid.");
            }
        }

        [TestMethod]
        public void TestValidate_WhitespaceInput()
        {
            bool result = Verhoeff.Validate(" 12345 ");
            Assert.IsFalse(result, "String with surrounding whitespace should be invalid.");
        }

        [TestMethod]
        public void TestAppendAndValidate_Deterministic()
        {
            string number = "1234567890";
            string fullNumber = Verhoeff.AppendCheckDigit(number);
            bool isValid = Verhoeff.Validate(fullNumber);
            Assert.IsTrue(isValid, "Deterministic test: the full number should be valid.");
        }
    }
}