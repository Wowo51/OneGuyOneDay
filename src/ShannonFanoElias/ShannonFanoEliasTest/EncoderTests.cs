using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShannonFanoElias;

namespace ShannonFanoEliasTest
{
    [TestClass]
    public class EncoderTests
    {
        [TestMethod]
        public void TestEncodeWithNullDictionary()
        {
            Encoder encoder = new Encoder();
            Dictionary<string, double>? input = null;
            Dictionary<string, string> result = encoder.Encode(input);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestEncodeWithEmptyDictionary()
        {
            Encoder encoder = new Encoder();
            Dictionary<string, double> input = new Dictionary<string, double>();
            Dictionary<string, string> result = encoder.Encode(input);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestEncodeWithValidData()
        {
            Encoder encoder = new Encoder();
            Dictionary<string, double> input = new Dictionary<string, double>();
            input.Add("A", 0.5);
            input.Add("B", 0.25);
            input.Add("C", 0.25);
            Dictionary<string, string> result = encoder.Encode(input);
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            foreach (KeyValuePair<string, double> entry in input)
            {
                string code = result[entry.Key];
                int expectedLength = (int)Math.Ceiling(-Math.Log(entry.Value, 2)) + 1;
                Assert.AreEqual(expectedLength, code.Length);
            }
        }

        [TestMethod]
        public void TestGenerateBinaryFraction_KnownValue()
        {
            Encoder encoder = new Encoder();
            string binary = encoder.GenerateBinaryFraction(0.625, 4);
            Assert.AreEqual("1010", binary);
        }

        [TestMethod]
        public void TestEncodeLargeDataset()
        {
            Encoder encoder = new Encoder();
            Dictionary<string, double> input = new Dictionary<string, double>();
            for (int i = 0; i < 1000; i++)
            {
                string key = "Key" + i.ToString();
                double probability = 1.0 / (i + 2);
                input.Add(key, probability);
            }

            Dictionary<string, string> result = encoder.Encode(input);
            Assert.IsNotNull(result);
            Assert.AreEqual(1000, result.Count);
        }
    }
}