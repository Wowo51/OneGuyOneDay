using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RunLengthEncoding;

namespace RunLengthEncodingTest
{
    [TestClass]
    public class RunLengthEncoderTests
    {
        [TestMethod]
        public void Encode_NullInput_ReturnsEmpty()
        {
            string expected = "";
            string actual = RunLengthEncoder.Encode(null);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Encode_EmptyInput_ReturnsEmpty()
        {
            string expected = "";
            string actual = RunLengthEncoder.Encode("");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Encode_SimpleTest()
        {
            string input = "AAA";
            string expected = "3A";
            string actual = RunLengthEncoder.Encode(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Encode_MultiSequenceTest()
        {
            string input = "AAAABBBCCDAA";
            string expected = "4A3B2C1D2A";
            string actual = RunLengthEncoder.Encode(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Encode_LongSequenceTest()
        {
            string input = new string ('A', 15);
            string expected = "15A";
            string actual = RunLengthEncoder.Encode(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Decode_NullInput_ReturnsEmpty()
        {
            string expected = "";
            string actual = RunLengthEncoder.Decode(null);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Decode_EmptyInput_ReturnsEmpty()
        {
            string expected = "";
            string actual = RunLengthEncoder.Decode("");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Decode_SimpleTest()
        {
            string input = "3A";
            string expected = "AAA";
            string actual = RunLengthEncoder.Decode(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Decode_MultiSequenceTest()
        {
            string input = "4A3B2C1D2A";
            string expected = "AAAABBBCCDAA";
            string actual = RunLengthEncoder.Decode(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Decode_LongSequenceTest()
        {
            string input = "15A";
            string expected = new string ('A', 15);
            string actual = RunLengthEncoder.Decode(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EncodeDecode_RoundTripTest()
        {
            string original = "ABBCCCDDDDEEEE";
            string encoded = RunLengthEncoder.Encode(original);
            string decoded = RunLengthEncoder.Decode(encoded);
            Assert.AreEqual(original, decoded);
        }

        [TestMethod]
        public void EncodeDecode_RandomTest()
        {
            System.Random random = new System.Random();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < 1000; i++)
            {
                char ch = (char)random.Next(65, 91);
                int repeatCount = random.Next(1, 10);
                for (int j = 0; j < repeatCount; j++)
                {
                    builder.Append(ch);
                }
            }

            string original = builder.ToString();
            string encoded = RunLengthEncoder.Encode(original);
            string decoded = RunLengthEncoder.Decode(encoded);
            Assert.AreEqual(original, decoded);
        }

        [TestMethod]
        public void Encode_SingleCharacterTest()
        {
            string input = "B";
            string expected = "1B";
            string actual = RunLengthEncoder.Encode(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Decode_ZeroCountTest()
        {
            string input = "0A";
            string expected = "";
            string actual = RunLengthEncoder.Decode(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Decode_MalformedInput_ReturnsEmpty()
        {
            string input = "3";
            string expected = "";
            string actual = RunLengthEncoder.Decode(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EncodeDecode_LargeRandomTest()
        {
            System.Random random = new System.Random();
            StringBuilder builder = new StringBuilder();
            int countLarge = 10000;
            for (int i = 0; i < countLarge; i++)
            {
                char ch = (char)random.Next(65, 91);
                int repeatCount = random.Next(1, 50);
                for (int j = 0; j < repeatCount; j++)
                {
                    builder.Append(ch);
                }
            }

            string original = builder.ToString();
            string encoded = RunLengthEncoder.Encode(original);
            string decoded = RunLengthEncoder.Decode(encoded);
            Assert.AreEqual(original, decoded);
        }
    }
}