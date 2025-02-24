using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DoubleDabble;

namespace DoubleDabbleTest
{
    [TestClass]
    public class DoubleDabbleConverterTests
    {
        [TestMethod]
        public void TestConvertToBCD_WithZero()
        {
            uint input = 0;
            string expected = "0";
            string actual = DoubleDabbleConverter.ConvertToBCDString(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestConvertToBCD_WithSmallNumbers()
        {
            uint[] inputs = new uint[]
            {
                1,
                2,
                3,
                12,
                45,
                99,
                100
            };
            for (int i = 0; i < inputs.Length; i++)
            {
                uint input = inputs[i];
                string expected = input.ToString();
                string actual = DoubleDabbleConverter.ConvertToBCDString(input);
                Assert.AreEqual(expected, actual, "Failed for input: " + input.ToString());
            }
        }

        [TestMethod]
        public void TestConvertToBCD_WithMaxValue()
        {
            uint input = 4294967295U;
            string expected = "4294967295";
            string actual = DoubleDabbleConverter.ConvertToBCDString(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestConvertToBCD_RandomNumbers()
        {
            Random random = new Random(42);
            for (int i = 0; i < 1000; i++)
            {
                byte[] bytes = new byte[4];
                random.NextBytes(bytes);
                uint input = BitConverter.ToUInt32(bytes, 0);
                string expected = input.ToString();
                string actual = DoubleDabbleConverter.ConvertToBCDString(input);
                Assert.AreEqual(expected, actual, "Failed for input: " + input.ToString());
            }
        }
    }
}