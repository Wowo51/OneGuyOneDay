using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdaptiveHuffmanCoding;
using System;
using System.Text;

namespace AdaptiveHuffmanCodingTest
{
    [TestClass]
    public class AdaptiveHuffmanCodingTests
    {
        private string GenerateRandomString(Random random, int length)
        {
            StringBuilder builder = new StringBuilder();
            string charset = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(charset.Length);
                builder.Append(charset[index]);
            }

            return builder.ToString();
        }

        [TestMethod]
        public void TestEmptyString()
        {
            AdaptiveHuffmanEncoder encoder = new AdaptiveHuffmanEncoder();
            AdaptiveHuffmanDecoder decoder = new AdaptiveHuffmanDecoder();
            string input = "";
            string encoded = encoder.Encode(input);
            string decoded = decoder.Decode(encoded);
            Assert.AreEqual(input, decoded);
        }

        [TestMethod]
        public void TestSingleCharacter()
        {
            AdaptiveHuffmanEncoder encoder = new AdaptiveHuffmanEncoder();
            AdaptiveHuffmanDecoder decoder = new AdaptiveHuffmanDecoder();
            string input = "A";
            string encoded = encoder.Encode(input);
            string decoded = decoder.Decode(encoded);
            Assert.AreEqual(input, decoded);
        }

        [TestMethod]
        public void TestRepeatedCharacters()
        {
            AdaptiveHuffmanEncoder encoder = new AdaptiveHuffmanEncoder();
            AdaptiveHuffmanDecoder decoder = new AdaptiveHuffmanDecoder();
            string input = "AAAAAA";
            string encoded = encoder.Encode(input);
            string decoded = decoder.Decode(encoded);
            Assert.AreEqual(input, decoded);
        }

        [TestMethod]
        public void TestSimpleString()
        {
            AdaptiveHuffmanEncoder encoder = new AdaptiveHuffmanEncoder();
            AdaptiveHuffmanDecoder decoder = new AdaptiveHuffmanDecoder();
            string input = "HELLO WORLD!";
            string encoded = encoder.Encode(input);
            string decoded = decoder.Decode(encoded);
            Assert.AreEqual(input, decoded);
        }

        [TestMethod]
        public void TestRandomStrings()
        {
            AdaptiveHuffmanEncoder encoder = new AdaptiveHuffmanEncoder();
            AdaptiveHuffmanDecoder decoder = new AdaptiveHuffmanDecoder();
            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                int length = random.Next(10, 101);
                string input = GenerateRandomString(random, length);
                string encoded = encoder.Encode(input);
                string decoded = decoder.Decode(encoded);
                Assert.AreEqual(input, decoded);
            }
        }

        [TestMethod]
        public void TestLargeRandomString()
        {
            AdaptiveHuffmanEncoder encoder = new AdaptiveHuffmanEncoder();
            AdaptiveHuffmanDecoder decoder = new AdaptiveHuffmanDecoder();
            Random random = new Random();
            int length = 1000;
            string input = GenerateRandomString(random, length);
            string encoded = encoder.Encode(input);
            string decoded = decoder.Decode(encoded);
            Assert.AreEqual(input, decoded);
        }
    }
}