using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShannonFanoCoding;

namespace ShannonFanoCodingTest
{
    [TestClass]
    public class ShannonFanoEncoderTests
    {
        [TestMethod]
        public void TestBuildCodes_NullInput()
        {
            ShannonFanoEncoder encoder = new ShannonFanoEncoder();
            Dictionary<char, string> codes = encoder.BuildCodes(null);
            Assert.AreEqual(0, codes.Count);
        }

        [TestMethod]
        public void TestEncode_NullInput()
        {
            ShannonFanoEncoder encoder = new ShannonFanoEncoder();
            string encoded = encoder.Encode(null);
            Assert.AreEqual("", encoded);
        }

        [TestMethod]
        public void TestDecode_NullInput()
        {
            ShannonFanoEncoder encoder = new ShannonFanoEncoder();
            string decoded1 = encoder.Decode(null, new Dictionary<char, string>());
            string decoded2 = encoder.Decode("010", null);
            Assert.AreEqual("", decoded1);
            Assert.AreEqual("", decoded2);
        }

        [TestMethod]
        public void TestSingleSymbolInput()
        {
            ShannonFanoEncoder encoder = new ShannonFanoEncoder();
            string input = "aaaa";
            Dictionary<char, string> codes = encoder.BuildCodes(input);
            Assert.AreEqual(1, codes.Count);
            Assert.IsTrue(codes.ContainsKey('a'));
            Assert.AreEqual("0", codes['a']);
            string encoded = encoder.Encode(input);
            Assert.AreEqual("0000", encoded);
            string decoded = encoder.Decode(encoded, codes);
            Assert.AreEqual(input, decoded);
        }

        [TestMethod]
        public void TestMultipleSymbols()
        {
            ShannonFanoEncoder encoder = new ShannonFanoEncoder();
            string input = "abacabad";
            Dictionary<char, string> codes = encoder.BuildCodes(input);
            StringBuilder assembled = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                char ch = input[i];
                if (codes.ContainsKey(ch))
                {
                    assembled.Append(codes[ch]);
                }
            }

            string encoded = encoder.Encode(input);
            Assert.AreEqual(assembled.ToString(), encoded);
            string decoded = encoder.Decode(encoded, codes);
            Assert.AreEqual(input, decoded);
        }

        [TestMethod]
        public void TestLargeInput()
        {
            ShannonFanoEncoder encoder = new ShannonFanoEncoder();
            StringBuilder sb = new StringBuilder();
            Random random = new Random(42);
            int length = 1000;
            for (int i = 0; i < length; i++)
            {
                int randomValue = random.Next(97, 123);
                sb.Append((char)randomValue);
            }

            string input = sb.ToString();
            Dictionary<char, string> codes = encoder.BuildCodes(input);
            string encoded = encoder.Encode(input);
            string decoded = encoder.Decode(encoded, codes);
            Assert.AreEqual(input, decoded);
        }

        [TestMethod]
        public void TestEmptyInput()
        {
            ShannonFanoEncoder encoder = new ShannonFanoEncoder();
            Dictionary<char, string> codes = encoder.BuildCodes("");
            Assert.AreEqual(0, codes.Count);
            string encoded = encoder.Encode("");
            Assert.AreEqual("", encoded);
            string decoded = encoder.Decode("", codes);
            Assert.AreEqual("", decoded);
        }

        [TestMethod]
        public void TestPunctuationInput()
        {
            ShannonFanoEncoder encoder = new ShannonFanoEncoder();
            string input = "Hello, World!";
            Dictionary<char, string> codes = encoder.BuildCodes(input);
            string encoded = encoder.Encode(input);
            string decoded = encoder.Decode(encoded, codes);
            Assert.AreEqual(input, decoded);
        }

        [TestMethod]
        public void TestMultipleRandomInputs()
        {
            ShannonFanoEncoder encoder = new ShannonFanoEncoder();
            Random random = new Random(100);
            for (int testIndex = 0; testIndex < 10; testIndex++)
            {
                StringBuilder inputBuilder = new StringBuilder();
                int length = random.Next(1, 200);
                for (int i = 0; i < length; i++)
                {
                    int r = random.Next(32, 127);
                    inputBuilder.Append((char)r);
                }

                string input = inputBuilder.ToString();
                Dictionary<char, string> codes = encoder.BuildCodes(input);
                string encoded = encoder.Encode(input);
                string decoded = encoder.Decode(encoded, codes);
                Assert.AreEqual(input, decoded);
            }
        }
    }
}