using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LempelZivWelch;

namespace LempelZivWelchTest
{
    [TestClass]
    public class LzwTests
    {
        [TestMethod]
        public void Compress_NullInput_ReturnsEmpty()
        {
            List<int> result = Lzw.Compress(null);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Compress_EmptyString_ReturnsEmpty()
        {
            List<int> result = Lzw.Compress("");
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Decompress_NullInput_ReturnsEmptyString()
        {
            string result = Lzw.Decompress(null);
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void Decompress_EmptyList_ReturnsEmptyString()
        {
            string result = Lzw.Decompress(new List<int>());
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void RoundTrip_Test_SimpleString()
        {
            string original = "TOBEORNOTTOBEORTOBEORNOT";
            List<int> compressed = Lzw.Compress(original);
            string decompressed = Lzw.Decompress(compressed);
            Assert.AreEqual(original, decompressed);
        }

        [TestMethod]
        public void RoundTrip_Test_RandomString()
        {
            StringBuilder randomBuilder = new StringBuilder();
            Random random = new Random(0);
            int len = 1000;
            for (int i = 0; i < len; i++)
            {
                int ascii = random.Next(32, 127);
                randomBuilder.Append((char)ascii);
            }

            string original = randomBuilder.ToString();
            List<int> compressed = Lzw.Compress(original);
            string decompressed = Lzw.Decompress(compressed);
            Assert.AreEqual(original, decompressed);
        }

        [TestMethod]
        public void Compress_SingleCharacter_ReturnsAsciiValue()
        {
            string input = "A";
            List<int> result = Lzw.Compress(input);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual((int)'A', result[0]);
        }

        [TestMethod]
        public void Decompress_SingleValue_ReturnsSingleCharacter()
        {
            List<int> compressed = new List<int>
            {
                65
            };
            string result = Lzw.Decompress(compressed);
            Assert.AreEqual("A", result);
        }

        [TestMethod]
        public void RoundTrip_Test_LargeRandomString()
        {
            StringBuilder randomBuilder = new StringBuilder();
            Random random = new Random(1);
            int len = 10000;
            for (int i = 0; i < len; i++)
            {
                int ascii = random.Next(32, 127);
                randomBuilder.Append((char)ascii);
            }

            string original = randomBuilder.ToString();
            List<int> compressed = Lzw.Compress(original);
            string decompressed = Lzw.Decompress(compressed);
            Assert.AreEqual(original, decompressed);
        }
    }
}