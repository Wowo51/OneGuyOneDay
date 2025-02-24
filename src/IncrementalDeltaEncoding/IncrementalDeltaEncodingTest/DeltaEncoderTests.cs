using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IncrementalDeltaEncoding;

namespace IncrementalDeltaEncodingTest
{
    [TestClass]
    public class DeltaEncoderTests
    {
        [TestMethod]
        public void Encode_NullInput_ReturnsEmptyArray()
        {
            string? [] input = null;
            string[] result = DeltaEncoder.Encode(input);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Encode_EmptyInput_ReturnsEmptyArray()
        {
            string? [] input = new string? [0];
            string[] result = DeltaEncoder.Encode(input);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Encode_SingleElement_ReturnsSameElement()
        {
            string? [] input = new string? []
            {
                "Hello"
            };
            string[] result = DeltaEncoder.Encode(input);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("Hello", result[0]);
        }

        [TestMethod]
        public void Encode_MultipleElements_ReturnsCorrectDelta()
        {
            string? [] input = new string? []
            {
                "abc",
                "abd",
                "a",
                ""
            };
            string[] expected = new string[]
            {
                "abc",
                "2:d",
                "1:",
                "0:"
            };
            string[] result = DeltaEncoder.Encode(input);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Decode_NullInput_ReturnsEmptyArray()
        {
            string? [] input = null;
            string[] result = DeltaEncoder.Decode(input);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Decode_EmptyInput_ReturnsEmptyArray()
        {
            string? [] input = new string? [0];
            string[] result = DeltaEncoder.Decode(input);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Decode_InvalidFormat_ReturnsOriginal()
        {
            string? [] encoded = new string? []
            {
                "abc",
                "noColon"
            };
            string[] result = DeltaEncoder.Decode(encoded);
            CollectionAssert.AreEqual(new string[] { "abc", "noColon" }, result);
        }

        [TestMethod]
        public void EncodeDecode_RoundTrip_ReturnsOriginal()
        {
            string? [] original = new string? []
            {
                "test",
                "tester",
                "testing",
                "tested",
                "test"
            };
            string[] encoded = DeltaEncoder.Encode(original);
            string[] decoded = DeltaEncoder.Decode(encoded);
            CollectionAssert.AreEqual(new string[] { "test", "tester", "testing", "tested", "test" }, decoded);
        }

        [TestMethod]
        public void Encode_LongSyntheticData_RoundTrip()
        {
            int count = 1000;
            string? [] original = new string? [count];
            for (int i = 0; i < count; i++)
            {
                original[i] = new string ('A', i + 1);
            }

            string[] encoded = DeltaEncoder.Encode(original);
            string[] decoded = DeltaEncoder.Decode(encoded);
            CollectionAssert.AreEqual(original, decoded);
        }

        [TestMethod]
        public void Encode_WithNullElements_TreatsNullAsEmpty()
        {
            string? [] input = new string? []
            {
                null,
                "a",
                null,
                "ab",
                null
            };
            string[] expectedEncoded = new string[]
            {
                "",
                "0:a",
                "0:",
                "0:ab",
                "0:"
            };
            string[] encoded = DeltaEncoder.Encode(input);
            CollectionAssert.AreEqual(expectedEncoded, encoded);
            string[] decoded = DeltaEncoder.Decode(encoded);
            string[] expectedDecoded = new string[]
            {
                "",
                "a",
                "",
                "ab",
                ""
            };
            CollectionAssert.AreEqual(expectedDecoded, decoded);
        }

        [TestMethod]
        public void Decode_InvalidPrefixNumber_ReturnsOriginal()
        {
            string? [] encoded = new string? []
            {
                "base",
                "x:abc"
            };
            string[] result = DeltaEncoder.Decode(encoded);
            CollectionAssert.AreEqual(new string[] { "base", "x:abc" }, result);
        }
    }
}