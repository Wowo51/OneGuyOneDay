using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LZWLSyllableVariant;

namespace LZWLSyllableVariantTest
{
    [TestClass]
    public class LZWLSyllableVariantTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestCompress_NullInput_ReturnsEmptyList()
        {
            CompressionResult result = SyllableLzw.Compress(null);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.CompressedData.Count);
        }

        [TestMethod]
        public void TestCompress_EmptyString_ReturnsEmptyList()
        {
            CompressionResult result = SyllableLzw.Compress("");
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.CompressedData.Count);
        }

        [TestMethod]
        public void TestDecompress_NullInput_ReturnsEmptyString()
        {
            string result = SyllableLzw.Decompress(null);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void TestRoundTrip_SimpleText()
        {
            string input = "hello world";
            CompressionResult result = SyllableLzw.Compress(input);
            string output = SyllableLzw.Decompress(result);
            Assert.AreEqual(input, output);
        }

        [TestMethod]
        public void TestRoundTrip_TextWithMultipleSpaces()
        {
            string input = "this   is    a    test";
            CompressionResult result = SyllableLzw.Compress(input);
            string output = SyllableLzw.Decompress(result);
            Assert.AreEqual(input, output);
        }

        [TestMethod]
        public void TestRoundTrip_SingleWord()
        {
            string input = "test";
            CompressionResult result = SyllableLzw.Compress(input);
            string output = SyllableLzw.Decompress(result);
            Assert.AreEqual(input, output);
        }

        [TestMethod]
        public void TestRoundTrip_LargeRandomString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random(42);
            for (int i = 0; i < 1000; i++)
            {
                int wordLength = random.Next(3, 11);
                for (int j = 0; j < wordLength; j++)
                {
                    char letter = (char)random.Next('a', 'z' + 1);
                    stringBuilder.Append(letter);
                }

                if (i < 999)
                {
                    stringBuilder.Append(" ");
                }
            }

            string input = stringBuilder.ToString();
            CompressionResult result = SyllableLzw.Compress(input);
            string output = SyllableLzw.Decompress(result);
            Assert.AreEqual(input, output);
        }

        [TestMethod]
        public void TestParallel_CompressionAndDecompression()
        {
            int iterations = 100;
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < iterations; i++)
            {
                int iteration = i;
                tasks.Add(Task.Run(() =>
                {
                    string input = $"parallel test {iteration}";
                    CompressionResult localResult = SyllableLzw.Compress(input);
                    string output = SyllableLzw.Decompress(localResult);
                    Assert.AreEqual(input, output, $"Mismatch in iteration {iteration}");
                    TestContext.WriteLine($"Iteration {iteration} passed.");
                }));
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}