using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HuffmanCoding;
using static HuffmanCoding.HuffmanCoding;

namespace HuffmanCodingTest
{
    [TestClass]
    public class HuffmanCodingTests
    {
        [TestMethod]
        public void TestCompressDecompressSimple()
        {
            string text = "hello world";
            HuffmanTree tree = HuffmanTree.BuildTree(text);
            string compressed = Compress(text, tree);
            string decompressed = Decompress(compressed, tree);
            Assert.AreEqual(text, decompressed);
        }

        [TestMethod]
        public void TestCompressDecompressEmpty()
        {
            string text = "";
            HuffmanTree tree = HuffmanTree.BuildTree(text);
            string compressed = Compress(text, tree);
            string decompressed = Decompress(compressed, tree);
            Assert.AreEqual(text, decompressed);
        }

        [TestMethod]
        public void TestCompressDecompressSingleCharacter()
        {
            string text = "aaaaaa";
            HuffmanTree tree = HuffmanTree.BuildTree(text);
            string compressed = Compress(text, tree);
            string decompressed = Decompress(compressed, tree);
            Assert.AreEqual(text, decompressed);
        }

        [TestMethod]
        public void TestCompressDecompressLargeRandomData()
        {
            string text = GenerateRandomString(10000);
            HuffmanTree tree = HuffmanTree.BuildTree(text);
            string compressed = Compress(text, tree);
            string decompressed = Decompress(compressed, tree);
            Assert.AreEqual(text, decompressed);
        }

        private static string GenerateRandomString(int length)
        {
            StringBuilder sb = new StringBuilder();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ";
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                sb.Append(chars[index]);
            }

            return sb.ToString();
        }
    }
}