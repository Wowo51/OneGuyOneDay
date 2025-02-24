using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LuhnModN;

namespace LuhnModNTest
{
    [TestClass]
    public class LuhnTests
    {
        [TestMethod]
        public void TestComputeCheckCharacter_NullInput()
        {
            char check = LuhnModNAlgorithm.ComputeCheckCharacter(null);
            Assert.AreEqual('0', check, "Check character for null input should be '0'.");
        }

        [TestMethod]
        public void TestComputeCheckCharacter_KnownValue()
        {
            char check = LuhnModNAlgorithm.ComputeCheckCharacter("ABC");
            Assert.AreEqual('H', check, "Check character for 'ABC' should be 'H'.");
        }

        [TestMethod]
        public void TestAppendCheckCharacter_AppendsCorrectly()
        {
            string result = LuhnModNAlgorithm.AppendCheckCharacter("ABC");
            Assert.AreEqual("ABCH", result, "Appended check character for 'ABC' should be 'ABCH'.");
        }

        [TestMethod]
        public void TestValidate_Success()
        {
            string valid = LuhnModNAlgorithm.AppendCheckCharacter("ABC");
            Assert.IsTrue(LuhnModNAlgorithm.Validate(valid), "Validation should succeed for a correctly appended check.");
        }

        [TestMethod]
        public void TestValidate_Failure()
        {
            bool isValid = LuhnModNAlgorithm.Validate("ABCZ");
            Assert.IsFalse(isValid, "Validation should fail for an incorrect check digit.");
        }

        [TestMethod]
        public void TestCustomAllowed()
        {
            string allowed = "012345";
            char check = LuhnModNAlgorithm.ComputeCheckCharacter("12", allowed);
            Assert.AreEqual('1', check, "For allowed '012345', check for '12' should be '1'.");
        }

        [TestMethod]
        public void TestValidateWithCustomAllowed()
        {
            string allowed = "012345";
            string appended = LuhnModNAlgorithm.AppendCheckCharacter("12", allowed);
            Assert.IsTrue(LuhnModNAlgorithm.Validate(appended, allowed), "Validation with custom allowed string failed.");
            Assert.IsFalse(LuhnModNAlgorithm.Validate("122", allowed), "Validation should fail for wrong check digit with custom allowed string.");
        }

        [TestMethod]
        public void TestRandomSyntheticData()
        {
            System.Random random = new System.Random(0);
            string allowed = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < 100; i++)
            {
                int length = random.Next(1, 20);
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < length; j++)
                {
                    int index = random.Next(allowed.Length);
                    sb.Append(allowed[index]);
                }

                string body = sb.ToString();
                string full = LuhnModNAlgorithm.AppendCheckCharacter(body, allowed);
                Assert.IsTrue(LuhnModNAlgorithm.Validate(full, allowed), "Random synthetic test validation failed.");
            }
        }

        [TestMethod]
        public void TestComputeCheckCharacter_EmptyInput()
        {
            char check = LuhnModNAlgorithm.ComputeCheckCharacter("");
            Assert.AreEqual('0', check, "Check character for empty input should be '0'.");
        }

        [TestMethod]
        public void TestValidate_EmptyInput()
        {
            bool isValid = LuhnModNAlgorithm.Validate("");
            Assert.IsFalse(isValid, "Validation should fail for empty input.");
        }

        [TestMethod]
        public void TestComputeCheckCharacter_InvalidCharacter()
        {
            char check = LuhnModNAlgorithm.ComputeCheckCharacter("A?C");
            Assert.AreEqual('S', check, "Check character for 'A?C' with invalid character should be 'S'.");
        }
    }
}